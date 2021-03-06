﻿Ext.onReady(function() {

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
            var month = Ext.Date.format(Ext.getCmp('month').getValue(), 'Ym');
            params['month']=month;
            gridStore.load({ params: params});
        },

        // 井搜索
        search: function(params) {
            var searchWord = Ext.DomQuery.selectNode('input[name=searchWord]').value;
            var month = Ext.Date.format(Ext.getCmp('month').getValue(), 'Ym');
            gridStore.load({ params:{searchWord: searchWord,month:month}});
        },

        // 井搜索框的键盘enter事件
        searchWordKeyPress: function(ele, e) {
            if (e.getKey() == 13) {
                events.search();
            }
        },
        
        // “添加”按钮点击事件
        addClick: function() {
            isAdd = true;
            var curDate = new Date();//格式化;
            var time=Ext.Date.format(curDate, 'Y.m.d'); 
            windowDialog.setTitle('添加单井基础信息');
            formPanel.getForm().reset();
            formPanel.getForm().setValues({
                NY: month,
                TCRQ:time             
            });
            windowDialog.show();
        },

        // “删除”按钮点击事件
        deleteClick: function() {
            if (!events.getGridSel()) return;

            Ext.Msg.confirm('确认', '请确定是否删除该数据？', function(result) {
                if (result == 'yes') {
                    dataJs.get(
                        '../../api/month/djsj.aspx',

                        {
                            _type: 'delete',
                            DJ_ID: gridSel.data['DJ_ID'],
                            NY: gridSel.data['NY']
                        },

                        function(data, msg, response) {
//                            Ext.Msg.alert('提示', msg, function() {
                                 gridStore.reload();
                                //gridStore.reload();//location.href = location.href;
//                            });
                        }
                    );
                }
            });
        },

        //上传文件按钮
        upload: function() {
            var form = sPanel.getForm();
            var file = Ext.getCmp("UPLOADFILE").getValue();
            if (file == "") {
//                Ext.Msg.alert('提示', '请选择文件！');
                return false;
            }
            if (form.isValid()) {

//                  dataJs.get(
//                   '../../api/system/uploadfile.aspx',
//                  form,
//                   function(data, msg, response) {
//                       windowDialog.hide();
//                       Ext.Msg.alert('提示', msg, function() {
//                            gridStore.reload();
//                       });
//                   },
//                   function(){},
//                   '正在上传...'
//               );
                form.submit({
                    url: '../../api/system/uploadfile.aspx',
                    waitMsg: '正在上传...',
                     success: function(msg,response) {
                          var resJson = response.response.responseText;
                           var msg = Ext.decode(resJson?resJson:'{}').msg;
                           Ext.Msg.alert('提示', msg.replace(/\n/g, "<br />"), function() {
                          gridStore.reload();
                        });
                    },
                    failure: function(msg,response) {
                           var resJson = response.response.responseText;
                           var msg = Ext.decode(resJson?resJson:'{}').msg;
                           Ext.Msg.show({
                              title: '提示信息',
                              msg: msg.replace(/\n/g, "<br />"),
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
        //下载模板
        download: function() {
            window.location.href = "../../static/dataTemplateExcel/oil/6单井基础信息.xls";
        },
        // “编辑”按钮点击事件
        editClick: function() {
            if (!events.getGridSel()) return;

            isAdd = false;
            windowDialog.setTitle('编辑单井基础信息');

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
                }

                dataJs.get(
                   '../../api/month/djsj.aspx',

                   values,

                   function(data, msg, response) {
                       windowDialog.hide();
//                       Ext.Msg.alert('提示', msg, function() {
                            gridStore.reload();
//                       });
                   }
               );
            }
        }
    };

    //---------------------------------------------------------创建、编辑对话框--------------------------------------------
    // 井别下拉数据
    var jbStore = Ext.create('Ext.data.Store', {
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

        fields: ['JBH', 'JBNAME'],
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

        fields: ['DEP_ID', 'DEP_NAME'],
        autoLoad: true
    });

    // 开发方式下拉数据
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

        fields: ['YQLXDM', 'YQLXMC'],
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

        fields: ['YCLX'],
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

        fields: ['XSYPDM', 'XSYPMC'],
        autoLoad: true
    });

    // 单井电表下拉数据
    var djdbStore = Ext.create('Ext.data.Store', {
        fields: ['DJDBDM', 'DJDBMC'],
        data: [
            { 'DJDBDM': '0', 'DJDBMC': '无' },
            { 'DJDBDM': '1', 'DJDBMC': '有' }
        ]
    });

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
                xtype: 'monthfield',
               fieldLabel: '年月',
               name: 'NY',
               readOnlyCls: 'readOnly',
               allowBlank: false
           },
           {
               fieldLabel: '井号',
               name: 'JH',
               allowBlank: false
           },
           {
               xtype: 'combo',
               fieldLabel: '井别',
               name: 'JB',
               store: jbStore,
               displayField: 'JBNAME',
               valueField: 'JBH',
               queryMode: 'local',
               allowBlank: true
           },
           {
               fieldLabel: '投产日期',
               name: 'TCRQ',
               allowBlank: true
           },
           {
               fieldLabel: '所属油田',
               name: 'SSYT',
               allowBlank: true
           },
           {
               fieldLabel: '评价单元',
               name: 'PJDY',
               allowBlank: true
           },
           {
               xtype: 'combo',
               fieldLabel: '油藏类型',
               name: 'YCLX',
               store: yclxStore,
               displayField: 'YCLX',
               valueField: 'YCLX',
               queryMode: 'local',
               allowBlank: false
           },
           {
               xtype: 'combo',
               fieldLabel: '作业区',
               name: 'ZYQ',
               store: departmentStore,
               displayField: 'DEP_NAME',
               valueField: 'DEP_ID',
               queryMode: 'local',
               allowBlank: false
           },
           {
               fieldLabel: '区块',
               name: 'QK',
               allowBlank: true
           },
           {
               fieldLabel: '中心站',
               name: 'ZXZ',
               allowBlank: false
           },
           {
               fieldLabel: '自然站',
               name: 'ZRZ',
               allowBlank: false
           },
           {
               fieldLabel: '抽油机型号',
               name: 'CYJXH',
               allowBlank: true
           },
           {
               fieldLabel: '抽油机名牌功率(千瓦)',
               xtype: 'numberfield',
               name: 'CYJMPGL',
               allowBlank: true
           },
           {
               fieldLabel: '电机功率(千瓦)',
               xtype: 'numberfield',
               name: 'DJGL',
               allowBlank: true
           },
           {
               fieldLabel: '负载率（%）',
               xtype: 'numberfield',
               name: 'FZL',
               allowBlank: true
           },
           {
               xtype: 'combo',
               fieldLabel: '单井电度',
               name: 'DJDB',
               store: djdbStore,
               displayField: 'DJDBMC',
               valueField: 'DJDBDM',
               queryMode: 'local',
               allowBlank: true
           },
           {
               xtype: 'combo',
               fieldLabel: '开采方式',
               name: 'YQLX',
               store: yqlxStore,
               displayField: 'YQLXMC',
               valueField: 'YQLXDM',
               queryMode: 'local',
               allowBlank: false
           },
           {
               xtype: 'combo',
               fieldLabel: '销售油品',
               name: 'XSYP',
               store: xsypStore,
               displayField: 'XSYPMC',
               valueField: 'XSYPDM',
               queryMode: 'local',
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
//        height: 354,
        width: 534,
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
//       , { xtype: 'button', text: '导入', iconCls: 'icon-send', handler: events.upload, margin: '-2 5 18 0' }
        ]
    });
   
    var gridStore = Ext.create('Ext.data.Store', {
        pageSize: 50, //一页容纳的数据量
        proxy: {
            type: 'ajax',
            url: '../../api/month/djsj.aspx',

            extraParams: {
                _type: 'getWellList',
                month:month            
            },

            reader: {
                type: 'json',
                root: 'data',
                totalProperty: 'totalProperty'  //返回的数据总数

            }
        },

        fields: ['NY', 'DJ_ID', 'JH', 'ZYQ', 'ZYQ_NAME',
                'QK', 'ZXZ', 'ZRZ', 'CYJXH', 'CYJMPGL',
                'DJGL', 'FZL', 'YQLX', 'YQLXMC', 'XSYP',
                'XSYPMC', 'TCRQ', 'JB', 'JBNAME', 'DJDB',
                'SSYT', 'PJDY', 'YCLX', 'CYC_ID'
        ]

    });

    gridStore.load({ params: { start: 0, limit: 50} }); //load()加载store的方法， start 记录开始的位置， limit在数据库中获取的数据数量

    var gridPanel = Ext.create('Ext.grid.Panel', {
        store: gridStore,
        region: "center",
        enableColumnHide: false,    //不可隐藏列
        sortableColumns: false,     //不可排序
        columnLines: true,          //表格列竖线
        stripeRows: true,           //隔行换色
        split:true,
        selModel: Ext.create('Ext.selection.CheckboxModel', {   // grid中添加checkbox列
            mode: 'single',                                     // 单选
            showHeaderCheckbox: false                           // 隐藏列顶部的checkbox
        }),

        columns: [
            { xtype: 'rownumberer', width: 40, text: '序号' },
            { text: '年月', dataIndex: 'NY' },
            { text: '井号', dataIndex: 'JH' },
            { text: '井别', dataIndex: 'JBNAME' },
            { text: '投产日期', dataIndex: 'TCRQ' },
            { text: '所属油田', dataIndex: 'SSYT' },
            { text: '评价单元', dataIndex: 'PJDY' },
            { text: '油藏类型', width: 130, dataIndex: 'YCLX' },
            { text: '作业区', dataIndex: 'ZYQ_NAME' },
            { text: '区块', dataIndex: 'QK' },
            { text: '中心站', dataIndex: 'ZXZ' },
            { text: '自然站', dataIndex: 'ZRZ' },
            { text: '抽油机型号', dataIndex: 'CYJXH' },
            { text: '抽油机名牌功率(千瓦)', width: 130, dataIndex: 'CYJMPGL' },
            { text: '电机功率(千瓦)', dataIndex: 'DJGL' },
            { text: '负载率(%)', dataIndex: 'FZL' },
            { text: '单井电度', dataIndex: 'DJDB', renderer: function(value) {
                if (value == '0') return '单井无电表';
                else if (value == '1') return '单井有电表';
            }
            },
            { text: '开采方式', dataIndex: 'YQLXMC' },
            { text: '销售油品', dataIndex: 'XSYPMC' }
        ],

        tbar: [
            '-',
            { xtype: 'button', text: '添加', iconCls: 'icon-add', handler: events.addClick },
            '-',
            { xtype: 'button', text: '删除', iconCls: 'icon-delete', handler: events.deleteClick },
            '-',
            { xtype: 'button', text: '编辑', iconCls: 'icon-edit', handler: events.editClick },
            '-',
             {
                  xtype: 'monthfield', id: 'month', editable: false, width: 75, labelWidth: 0, labelAlign: 'right',
                  format: 'Ym', value: month
              },
            { xtype: 'textfield', name: 'searchWord', emptyText: '输入井号', enableKeyEvents: true, listeners: {
                'keypress': events.searchWordKeyPress
            }
            },
            { xtype: 'button', text: '检索', iconCls: 'icon-search', handler: events.search },
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
         width:'20%',  
        split:true,
        collapsible: true,
        margin: '0 0 0 0',
        autoScroll: true,
        border: false,
        lines: false,
        singleExpand: false,
        useArrows: true,
        region: 'east',
        listeners: {
            'itemclick': events.treeNodeClick
        }
    });

    // 一次load所有tree数据，并渲染页面
    dataJs.get(
        '../../api/month/djsj.aspx',

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

            //treeStore.getRootNode().expandChildren();

            window.top.sss = gridStore;
        }
    );

});