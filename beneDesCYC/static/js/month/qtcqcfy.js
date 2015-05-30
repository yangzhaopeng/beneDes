Ext.onReady(function() {

    // 对话框显示时，当前状态是否是“添加”角色，或者是“编辑”角色
    var isAdd = true;
    // 当前grid选中的行
    var gridSel;

    //--------------------------------------------------------------------------事件集中处理-------------------------------
    var events = {

        // 获取当前grid选中的行
        getGridSel: function() {
            var sel = gridPanel.getSelectionModel().getSelection();

            if (sel.length < 1) {
                Ext.Msg.alert('提示', '请先选中一口井！');
                return false;
            }

            gridSel = sel[0]; 

            return true;
        },

        // tree的节点点击事件
        treeNodeClick: function(view, record, item, index, e) {
            var params = record.raw;
            gridStore.load({ params: params });
        },

        // “添加”按钮点击事件
        addClick: function() {
            isAdd = true;
            windowDialog.setTitle('添加单井直接费用');
            formPanel.getForm().reset();
            formPanel.getForm().setValues({
                NY: month
            });
            windowDialog.show();
        },
        //下载模板
        download: function() {
            window.location.href = "../../static/dataTemplateExcel/gas/6采气厂费用.xls";
        },
        // “删除”按钮点击事件
        deleteClick: function() {
            if (!events.getGridSel()) return;

            Ext.Msg.confirm('确认', '您确定要删除该单井直接费用？', function(result) {
                if (result == 'yes') {
                    dataJs.get(
                        '../../api/month/zjfybj.aspx',

                        {
                            _type: 'delete',
                            DJ_ID: gridSel.data['DJ_ID'],
                            NY: gridSel.data['NY']
                        },

                        function(data, msg, response) {
                            Ext.Msg.alert('提示', msg, function() {
                                gridStore.reload();//location.href = location.href;
                            });
                        }
                    );
                }
            });
        },

        upload: function() {
            var form = sPanel.getForm();
            var file = Ext.getCmp("UPLOADFILE").getValue();
            if (file == "") {
//                Ext.Msg.alert('提示', '请选择文件！');
                return false;
            }
            if (form.isValid()) {

                form.submit({
                    url: '../../api/system/uploadfile3.aspx?cyctype=cqc_id&FT_TYPE=CQC',
                    waitMsg: '正在上传...',
                     success: function(msg,response) {
                          var resJson = response.response.responseText;
                           var msg = Ext.decode(resJson?resJson:'{}').msg;
                          Ext.Msg.alert('提示',msg, function() {
                          gridStore.reload();
                        });
                    },
                    failure: function(msg,response) {
                           var resJson = response.response.responseText;
                           var msg = Ext.decode(resJson?resJson:'{}').msg;
                           Ext.Msg.show({
                              title: '提示信息',
                              msg: msg,
                              minWidth: 400,//可以改
                              maxWidth: 600,//可以改
                              buttons: Ext.Msg.YES,
                              fn: gridStore.reload(),
                              icon: Ext.MessageBox.INFO
                            });
                        }
                })
            }
        },

        // “编辑”按钮点击事件
        editClick: function() {
            if (!events.getGridSel()) return;

            isAdd = false;
            windowDialog.setTitle('编辑单井直接费用');

            formPanel.getForm().setValues(gridSel.data);

            windowDialog.show();
        },

        // 弹出对话框的“确定”按钮点击事件
        addOrEdit: function() {
            var form = formPanel.getForm();

            if (form.isValid()) {
                var values = form.getValues();
                values['_type'] = 'add';

                if (!isAdd) {
                    values['DJ_ID'] = gridSel.data['DJ_ID'];
                    values['_type'] = 'edit';
                    values['FEE_CLASS'] = gridSel.data['FEE_CLASS'];
                }

                dataJs.get(
                   '../../api/month/qtcqcfy.aspx',

                   values,

                   function(data, msg, response) {
                       windowDialog.hide();
                       Ext.Msg.alert('提示', msg, function() {
                           gridStore.reload();//location.href = location.href;
                       });
                   }
               );
            }
        }
    };

    //---------------------------------------------------------创建、编辑对话框--------------------------------------------
    // 井别下拉数据
    /*    var jbStore = Ext.create('Ext.data.Store', {
    proxy: {
    type: 'ajax',
    url: '../../api/system/jb_info.aspx',
            
    extraParams: {
    _type: 'getAllList'
    },
            
    reader: {
    type: 'json',
    root: 'data'
    }
    },
        
    fields:['JBH', 'JBNAME'],
    autoLoad: true
    });
   
    // 作业区下拉数据
    var departmentStore = Ext.create('Ext.data.Store', {
    proxy: {
    type: 'ajax',
    url: '../../api/system/department.aspx',
            
    extraParams: {
    _type: 'getZYQList'
    },
            
    reader: {
    type: 'json',
    root: 'data'
    }
    },
        
    fields:['DEP_ID', 'DEP_NAME'],
    autoLoad: true
    });
    
    // 油气类型下拉数据
    var yqlxStore = Ext.create('Ext.data.Store', {
    proxy: {
    type: 'ajax',
    url: '../../api/system/yqlx_info.aspx',
            
    extraParams: {
    _type: 'getAllList'
    },
            
    reader: {
    type: 'json',
    root: 'data'
    }
    },
        
    fields:['YQLXDM', 'YQLXMC'],
    autoLoad: true
    });
    
    // 油藏类型下拉数据
    var yclxStore = Ext.create('Ext.data.Store', {
    proxy: {
    type: 'ajax',
    url: '../../api/system/yclx.aspx',
            
    extraParams: {
    _type: 'getAllList'
    },
            
    reader: {
    type: 'json',
    root: 'data'
    }
    },
        
    fields:['YCLX'],
    autoLoad: true
    });
    
    // 销售油品下拉数据
    var xsypStore = Ext.create('Ext.data.Store', {
    proxy: {
    type: 'ajax',
    url: '../../api/system/xsyp_info.aspx',
            
    extraParams: {
    _type: 'getAllList'
    },
            
    reader: {
    type: 'json',
    root: 'data'
    }
    },
        
    fields:['XSYPDM', 'XSYPMC'],
    autoLoad: true
    });
    
    // 单井电表下拉数据
    var djdbStore = Ext.create('Ext.data.Store', {
    fields: ['DJDBDM', 'DJDBMC'],
    data : [
    {'DJDBDM':'0', 'DJDBMC':'单井无电表'},
    {'DJDBDM':'1', 'DJDBMC':'单井有电表'}
    ]
    });
    */
    var formPanel = Ext.create('Ext.form.Panel', {
        frame: true,
        bodyPadding: 5,
        defaultType: 'textfield',
        layout: 'column',

        fieldDefaults: {
            msgTarget: 'side',
            labelAlign: 'right',
            labelWidth: 75,
            width: 250,
            margin: '7 0 0 0'
        },

        items: [
           {
               fieldLabel: '年月',
               name: 'NY',
               readOnlyCls: 'readOnly',
               allowBlank: false,
               readOnly: true
           },
        //           {
        //               fieldLabel: '井号',
        //               name: 'JH',
        //               allowBlank: false
        //           },

        //           {
        //               fieldLabel: '作业区',
        //               name: 'ZYQ',
        //               allowBlank: true
        //           },
           {
           fieldLabel: '费用名称',
           name: 'FEE_NAME',
           allowBlank: true
       },
           {
               fieldLabel: '费用',
               name: 'FEE',
               allowBlank: true
           }

        //           {
        //               fieldLabel: '直接燃料费',
        //               name: 'ZJRLF',
        //               allowBlank: true
        //           },
        //           {
        //               fieldLabel: '直接动力费',
        //               name: 'ZJDLF',
        //               allowBlank: false
        //           },
        //           {
        //               fieldLabel: '其它动力费',
        //               name: 'QTDLF',
        //               allowBlank: false
        //           },
        //           {
        //               fieldLabel: '驱油物注入费',
        //               name: 'QYWZRF',
        //               allowBlank: true
        //           },
        //           {
        //               fieldLabel: '其中:人员费',
        //              
        //               name: 'QYWZRF_RYF',
        //               allowBlank: true
        //           },
        //           {
        //               fieldLabel: '措施作业费',
        //               
        //               name: 'CSZYF',
        //               allowBlank: true
        //           },
        //           {
        //               fieldLabel: '维护性作业劳务费',
        //              
        //               name: 'WHXZYLWF',
        //               allowBlank: true
        //           },
        //           {
        //               fieldLabel: '水井作业费',
        //               name: 'SJZYF',
        //               allowBlank: false
        //           },
        //           {
        //               fieldLabel: '测井试井费',
        //               name: 'CJCSF',
        //               allowBlank: false
        //           },
        //           {
        //               fieldLabel: '油藏监测费',
        //               name: 'YCJCF',
        //               allowBlank: false
        //           },
        //           {
        //               fieldLabel: '维护及修理费',
        //               name: 'WHXLF',
        //               allowBlank: false
        //           },
        //           {
        //               fieldLabel: '油气处理费',
        //               name: 'YQCLF',
        //               allowBlank: false
        //           },
        //           {
        //               fieldLabel: '其中:人员费',
        //               name: 'YQCLF_RYF',
        //               allowBlank: false
        //           },
        //           {
        //               fieldLabel: '轻烃回收费',
        //               name: 'QTHSF',
        //               allowBlank: false
        //           },
        //           {
        //               fieldLabel: '运输费(扣除拉油费)',
        //               name: 'YSF',
        //               allowBlank: false
        //           },
        //           {
        //               fieldLabel: '拉油费',
        //               name: 'LYF',
        //               allowBlank: false
        //           },
        //           {
        //               fieldLabel: '其他直接费',
        //               name: 'QTZJF',
        //               allowBlank: false
        //           },
        //           {
        //               fieldLabel: '厂矿管理费',
        //               name: 'CKGLF',
        //               allowBlank: false
        //           },
        //           {
        //               fieldLabel: '直接人员费',
        //               name: 'ZJRYF',
        //               allowBlank: false
        //           },
        //            {
        //               fieldLabel: '减：自用产品',
        //               name: 'ZYYQCP',
        //               allowBlank: false
        //           },
        //            {
        //               fieldLabel: '折旧折耗',
        //               name: 'ZJZH',
        //               allowBlank: false
        //           }

      ],

        buttons: [
            {
                text: '确定',
                iconCls: 'icon-ok',
                handler: events.addOrEdit
            },
            {
                text: '取消',
                iconCls: 'icon-close',
                handler: function() {
                    windowDialog.hide();
                }
            }
        ]
    });

    var windowDialog = Ext.create('Ext.window.Window', {
        title: 'Hello world!',
        height: 100,
        width: 800,
        layout: 'fit',
        modal: true,
        closeAction: 'hide',
        constrain: true,
        resizable: false,
        border: false,
        items: [formPanel]
    });

    // -----------------------------------------------------------------中间grid表格-----------------------------------------
    var sPanel = Ext.create('Ext.form.Panel', {
        frame: false,
        layout: 'table',
        border: false,
        bodyPadding: 5,
        height: 28,
        bodyStyle: 'background-color:#dae7f6',

        defaultType: 'textfield',
        fieldDefaults: {
            msgTarget: 'side',
            labelAlign: 'right',
            labelWidth: 30,
            width: 200,
            margin: '0 0 15 0'
        },
        items: [{ xtype: 'fileuploadfield', buttonText: '导&nbsp入', name: 'UPLOADFILE',buttonOnly:true, id: 'UPLOADFILE', 
        margin: '-2 10 18 0', height: 20 ,   listeners:{
                        change:events.upload
                }}   
        ]
    });


    var gridStore = Ext.create('Ext.data.Store', {
        proxy: {
            type: 'ajax',
            url: '../../api/month/qtcqcfy.aspx',

            extraParams: {
                _type: 'getWellList'
            },

            reader: {
                type: 'json',
                root: 'data'

            }
        },

        fields: ['NY', 'DEP_NAME', 'DJ_ID', 'JH', 'ZYQ', 'ZYQ_NAME', 'FEE_NAME', 'FEE', 'FEE_CLASS', 'CYC_ID'],
        autoLoad: true
    });

    var gridPanel = Ext.create('Ext.grid.Panel', {
        store: gridStore,
        region: "center",
        enableColumnHide: false,    //不可隐藏列
        sortableColumns: false,     //不可排序
        columnLines: true,          //表格列竖线
        stripeRows: true,           //隔行换色

        selModel: Ext.create('Ext.selection.CheckboxModel', {   // grid中添加checkbox列
            mode: 'single',                                     // 单选
            showHeaderCheckbox: false                           // 隐藏列顶部的checkbox
        }),

        columns: [
            { xtype: 'rownumberer', width: 40, text: '序号' },
            { text: '年月', dataIndex: 'NY' },
            { text: '当前编辑', dataIndex: 'DEP_NAME' },
        //            {text: '作业区', dataIndex: 'ZYQ_NAME'},
            {text: '费用名称', dataIndex: 'FEE_NAME' },
            { text: '费用（元）', dataIndex: 'FEE' },
            { text: '费用代码', hidden: true, dataIndex: 'FEE_CLASS' }

        ],

        tbar: [
        //            '-',
        //            {xtype: 'button' ,hidden: true, text: '添加', iconCls: 'icon-add',hidden:true, handler: events.addClick},
        //            '-',
        //            {xtype: 'button',hidden:true, text: '删除', iconCls: 'icon-delete',hidden:true, handler: events.deleteClick},
            '-',
            { xtype: 'button', text: '编辑', iconCls: 'icon-edit', handler: events.editClick },
            '-',
            sPanel,
            '-',
            '->',
            { xtype: 'button', text: '下载模板', iconCls: 'icon-save', handler: events.download }

        ]
    });

    // -----------------------------------------------------------右侧井组 tree-------------------------------------------------
    var treeStore = Ext.create('Ext.data.TreeStore');

    var treePanel = Ext.create('Ext.tree.Panel', {
        title: '可通过点击树节点进行井的筛选',
        store: treeStore,
        rootVisible: true,
        height: '100%',
        region: "east",
        width:'20%',  
        split:true,
        collapsible: true,
        margin: '0 0 0 0',
        autoScroll: true,
        border: false,
        lines: false,
        singleExpand: false,
        useArrows: true,
        listeners: {
            'itemclick': events.treeNodeClick
        }
    });

    // 一次load所有tree数据，并渲染页面
    dataJs.get(
        '../../api/month/qtcqcfy.aspx',

        {
            _type: 'getZRZTree'
        },

        function(data, msg, response) {
            Ext.create('Ext.container.Viewport', {
                layout: 'border',
                items: [gridPanel, treePanel],
                renderTo: Ext.getBody()
            });

            treeStore.setRootNode(data);

            treeStore.getRootNode().expandChildren();

            window.top.sss = gridStore;
        }
    );

});