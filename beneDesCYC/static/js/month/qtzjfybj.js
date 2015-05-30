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

        // “删除”按钮点击事件
        deleteClick: function() {
            if (!events.getGridSel()) return;

            Ext.Msg.confirm('确认', '您确定要删除该单井直接费用？', function(result) {
                if (result == 'yes') {
                    dataJs.get(
                        '../../api/month/qtzjfybj.aspx',

                        {
                            _type: 'delete',
                            DJ_ID: gridSel.data['DJ_ID'],
                            NY: gridSel.data['NY']
                        },

                        function(data, msg, response) {
                            Ext.Msg.alert('提示', msg, function() {
//                                location.href = location.href;
                            gridStore.reload();
                            });
                        }
                    );
                }
            });
        },
         //下载模板
        download: function() {
            window.location.href = "../../static/dataTemplateExcel/gas/1直接费用编辑.xls";
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
                    url: '../../api/system/uploadfile2.aspx',
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

        // 井搜索
        /*     search: function() {
        var searchWord = Ext.DomQuery.selectNode('input[name=searchWord]').value;
        gridStore.load({params: {searchWord: searchWord}});
        },
        
        // 井搜索框的键盘enter事件
        searchWordKeyPress: function(ele, e) {
        if (e.getKey() == 13) {
        events.search();
        }
        },
        */
        // 弹出对话框的“确定”按钮点击事件
        addOrEdit: function() {
            var form = formPanel.getForm();

            if (form.isValid()) {
                var values = form.getValues();
                values['_type'] = 'add';

                if (!isAdd) {
                    values['DJ_ID'] = gridSel.data['DJ_ID'];
                    values['_type'] = 'edit';
                }

                dataJs.get(
                   '../../api/month/qtzjfybj.aspx',

                   values,

                   function(data, msg, response) {
                       windowDialog.hide();
                       gridStore.reload();
//                       Ext.Msg.alert('提示', msg, function() {
//                           location.href = location.href;
//                       });
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
           {
               fieldLabel: '井号',
               name: 'JH',
               allowBlank: false
           },

           {
               fieldLabel: '作业区',
               name: 'ZYQ',
               allowBlank: true
           },
           {
               fieldLabel: '直接材料费（元）',
               name: 'ZJCLF',
               allowBlank: true
           },
           {
               fieldLabel: '其他材料费（元）',
               name: 'QTCLF',
               allowBlank: true
           },

           {
               fieldLabel: '直接燃料费（元）',
               name: 'ZJRLF',
               allowBlank: true
           },
           {
               fieldLabel: '直接动力费（元）',
               name: 'ZJDLF',
               allowBlank: false
           },
           {
               fieldLabel: '其它动力费（元）',
               name: 'QTDLF',
               allowBlank: false
           },
           {
               fieldLabel: '驱油物注入费（元）',
               name: 'QYWZRF',
               allowBlank: true,
               hidden: true
           },
           {
               fieldLabel: '措施性作业劳务费（元）',
               name: 'CSXZYLWF',
               allowBlank: true
           },
           {
               fieldLabel: '维护性作业劳务费（元）',
               name: 'WHXZYLWF',
               allowBlank: false
           },
           {
               fieldLabel: '测井测试费（元）',
               name: 'CJCSF',
               allowBlank: false
           },
           {
               fieldLabel: '维护及修理费（元）',
               name: 'QTWHJXLF',
               allowBlank: true
           },
           {
               fieldLabel: '油气处理费（元）',
               name: 'YQCLF',
               allowBlank: true
           },

           {
               fieldLabel: '轻烃回收费（元）',
               name: 'QTHSF',
               allowBlank: true,
               hidden: true
           },
           {
               fieldLabel: '运输费（元）',
               name: 'YSF',
               allowBlank: false
           },
           {
               fieldLabel: '拉油费（元）',
               name: 'LYF',
               allowBlank: false,
               hidden: true
           },
           {
               fieldLabel: '其它直接费（元）',
               name: 'QTZJF',
               allowBlank: true
           },
           {
               fieldLabel: '厂矿管理费（元）',
               name: 'CKGLF',
               allowBlank: true
           },

           {
               fieldLabel: '直接人员费（元）',
               name: 'ZJRYF',
               allowBlank: true
           },
           {
               fieldLabel: '自用油气产品（元）',
               name: 'ZYYQCP',
               allowBlank: false
           },
           {
               fieldLabel: '折旧折耗（元）',
               name: 'ZJZH',
               allowBlank: false
           },

           {
               fieldLabel: '电度费（元）',
               name: 'DDF',
               allowBlank: true
           },
           {
               fieldLabel: '机加费（元）',
               name: 'JJF',
               allowBlank: false
           },
           {
               fieldLabel: '其他测试费（元）',
               name: 'QTCSF',
               allowBlank: false
           },
           {
               fieldLabel: '其它特车费（元）',
               name: 'QTTCF',
               allowBlank: true
           },
           {
               fieldLabel: '其它运输费（元）',
               name: 'QTYSF',
               allowBlank: true
           },

           {
               fieldLabel: '射孔费（元）',
               name: 'SKF',
               allowBlank: true
           },
           {
               fieldLabel: '特车费（元）',
               name: 'TCF',
               allowBlank: false
           },
           {
               fieldLabel: '洗井费（元）',
               name: 'XJF',
               allowBlank: false
           },

           {
               fieldLabel: '修理费（元）',
               name: 'XLF',
               allowBlank: true
           },
           {
               fieldLabel: '一般材料费（元）',
               name: 'YBCLF',
               allowBlank: false
           }
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
        height: 480,
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
                }}]
    });


    var gridStore = Ext.create('Ext.data.Store', {
        pageSize: 50, //一页容纳的数据量
        proxy: {
            type: 'ajax',
            url: '../../api/month/qtzjfybj.aspx',

            extraParams: {
                _type: 'getWellList'
            },

            reader: {
                type: 'json',
                root: 'data',
                totalProperty: 'totalProperty'  //返回的数据总数
            }
        },

        fields: ['NY', 'DJ_ID', 'JH', 'ZYQ', 'ZYQ_NAME',
                'ZJCLF', 'QTCLF', 'ZJRLF', 'ZJDLF', 'QTDLF', 'QYWZRF', 'QYWZRF_RYF',
                'CSXZYLWF', 'WHXZYLWF', 'SJZYF', 'CJCSF', 'CJSJF_RYF', 'YCJCF', 'QTWHJXLF', 'WHXLF_RYF',
                'YQCLF', 'YQCLF_RYF', 'QTHSF', 'YSF', 'YSF_RYF', 'LYF', 'QTZJF', 'CKGLF', 'ZJRYF',
                'ZYYQCP', 'ZJZH', 'CYC_ID', 'DDF', 'JJF', 'QTCSF', 'QTTCF', 'QTYSF', 'SKF', 'TCF',
                'XJF', 'XLF', 'YBCLF'
        ]

        //        autoLoad: true
    });
    gridStore.load({ params: { start: 0, limit: 50} });
    var gridPanel = Ext.create('Ext.grid.Panel', {
        store: gridStore,
        region: "center",
        width:'80%',  
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
            { text: '井号', dataIndex: 'JH' },
            { text: '作业区', dataIndex: 'ZYQ_NAME' },
            { text: '一般材料费（元）', dataIndex: 'YBCLF' },
            { text: '电度费（元）', dataIndex: 'DDF' },
            { text: '机加费（元）', dataIndex: 'JJF' },
            { text: '折旧折耗（元）', dataIndex: 'ZJZH' },
            { text: '运输费（元）', dataIndex: 'YSF' },
            { text: '直接材料费（元）', dataIndex: 'ZJCLF' },
            { text: '其他材料费（元）', dataIndex: 'QTCLF' },
            { text: '直接燃料费（元）', dataIndex: 'ZJRLF' },
            { text: '直接动力费（元）', dataIndex: 'ZJDLF' },
            { text: '其它动力费（元）', dataIndex: 'QTDLF' },
            { text: '测井测试费（元）', dataIndex: 'CJCSF' },
            { text: '措施性作业劳务费（元）', dataIndex: 'CSXZYLWF' },
            { text: '油气处理费（元）', dataIndex: 'YQCLF' },
            { text: '轻烃回收费（元）', dataIndex: 'QTHSF', hidden: true },
            { text: '维护及修理费（元）', dataIndex: 'QTWHJXLF' },
            { text: '维护性作业劳务费（元）', dataIndex: 'WHXZYLWF' },
            { text: '厂矿管理费（元）', dataIndex: 'CKGLF' },
            { text: '直接人员费（元）', dataIndex: 'ZJRYF' },
            { text: '自用油气产品（元）', dataIndex: 'ZYYQCP' },
            { text: '射孔费（元）', dataIndex: 'SKF' },
            { text: '特车费（元）', dataIndex: 'TCF' },
            { text: '洗井费（元）', dataIndex: 'XJF' },
            { text: '修理费（元）', dataIndex: 'XLF' },
            { text: '驱油物注入费（元）', dataIndex: 'QYWZRF', hidden: true },
            { text: '驱油物注入费-人员费（元）', dataIndex: 'QYWZRF_RYF', hidden: true },
            { TEXT: '', DATAINDEX: 'SJZYF', hidden: true },
            { text: '测井测试费-人员费', dataIndex: 'CJSJF_RYF', hidden: true },
            { text: '', dataIndex: 'YCJCF', hidden: true },
            { text: '维护及修理费-人员费（元）', dataIndex: 'WHXLF_RYF', hidden: true },
            { text: '油气处理费-人员费（元）', dataIndex: 'YQCLF_RYF', hidden: true },
            { text: '运输费-人员费（元）', dataIndex: 'YSF_RYF', hidden: true },
            { text: '拉油费', dataIndex: 'LYF', hidden: true },
            { text: '其它直接费（元）', dataIndex: 'QTZJF' },
            { text: '其他测试费（元）', dataIndex: 'QTCSF' },
            { text: '其它特车费（元）', dataIndex: 'QTTCF' },
            { text: '其它运输费（元）', dataIndex: 'QTYSF' }
        ],

        tbar: [
            '-',
            { xtype: 'button', text: '添加', iconCls: 'icon-add', hidden: true, handler: events.addClick },
            '-',
            { xtype: 'button', text: '删除', iconCls: 'icon-delete', hidden: true, handler: events.deleteClick },
            '-',
            { xtype: 'button', text: '编辑', iconCls: 'icon-edit', handler: events.editClick },
            '-',
            sPanel,
            '-',
            '->',
               { xtype: 'button', text: '下载模板', iconCls: 'icon-save', handler: events.download }
        ],
        bbar: new Ext.PagingToolbar({
            store: gridStore,
            displayInfo: true,
            displayMsg: '第 {0} 到 {1} 条数据 共 {2} 条',
            emptyMsg: '没有数据',
            beforePageText: '第',
            afterPageText: '页/共{0}页'
        })
    });

    // -----------------------------------------------------------右侧井组 tree-------------------------------------------------
    var treeStore = Ext.create('Ext.data.TreeStore');

    var treePanel = Ext.create('Ext.tree.Panel', {
        title: '可通过点击树节点进行井的筛选',
        store: treeStore,
        rootVisible: true,
        height: '100%',
//      width: 240,
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
        '../../api/month/qtzjfybj.aspx',

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