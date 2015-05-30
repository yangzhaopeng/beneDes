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
            windowDialog.setTitle('添加单井开发数据');
            formPanel.getForm().reset();
              formPanel.getForm().findField('NY').setReadOnly(true);
            formPanel.getForm().setValues({
                NY: month
            });
            windowDialog.show();
        },

        // “删除”按钮点击事件
        deleteClick: function() {
            if (!events.getGridSel()) return;

            Ext.Msg.confirm('确认', '您确定要删除该单井开发数据？', function(result) {
                if (result == 'yes') {
                    dataJs.get(
                        '../../api/month/qjkfsj.aspx',

                        {
                            _type: 'delete',
                            DJ_ID: gridSel.data['DJ_ID'],
                            NY: gridSel.data['NY']
                        },

                        function(data, msg, response) {
                            Ext.Msg.alert('提示', msg, function() {
                                gridStore.reload();
                            });
                        }
                    );
                }
            });
        },

        upload: function() {
            var form = sPanel.getForm();
//            var file = Ext.getCmp("UPLOADFILE").getValue();
//            if (file == "") {
//                Ext.Msg.alert('提示', '请选择文件！');
//                return false;
//            }
            if (form.isValid()) {

                form.submit({
                    url: '../../api/system/excelUploadQJKFSJ.aspx',
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
        //下载模板
        download: function() {
            window.location.href = "../../static/dataTemplateExcel/gas/8单井开发数据.xls";
        },
        // “编辑”按钮点击事件
        editClick: function() {
            if (!events.getGridSel()) return;

            isAdd = false;
            windowDialog.setTitle('编辑单井开发数据');
            formPanel.getForm().findField('NY').setReadOnly(true);
            formPanel.getForm().setValues(gridSel.data);

            windowDialog.show();
        },

        // 井搜索
        search: function() {
            var searchWord = Ext.DomQuery.selectNode('input[name=searchWord]').value;
            gridStore.load({ params: { searchWord: searchWord} });
        },

        // 井搜索框的键盘enter事件
        searchWordKeyPress: function(ele, e) {
            if (e.getKey() == 13) {
                events.search();
            }
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
                   '../../api/month/qjkfsj.aspx',

                   values,

                   function(data, msg, response) {
                       windowDialog.hide();
                       Ext.Msg.alert('提示', msg, function() {
                           gridStore.reload();
                       });
                   }
               );
            }
        }
    };

    //---------------------------------------------------------创建、编辑对话框--------------------------------------------
    // 井别下拉数据
    /*   var jbStore = Ext.create('Ext.data.Store', {
    proxy: {
    type: 'ajax',
    url: '/api/system/jb_info.aspx',
            
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
    */
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

    //    // 地质措施下拉数据
    //    var dzcsStore = Ext.create('Ext.data.Store', {
    //        proxy: {
    //            type: 'ajax',
    //            url: '/api/system/dzcs_info.aspx',

    //            extraParams: {
    //                _type: 'getAllList'
    //            },

    //            reader: {
    //                type: 'json',
    //                root: 'data'
    //            }
    //        },

    //        fields: ['DZCSDM', 'DZCSMC'],
    //        autoLoad: true
    //    });

    //    // 工艺措施下拉数据
    //    var gycsStore = Ext.create('Ext.data.Store', {
    //        proxy: {
    //            type: 'ajax',
    //            url: '/api/system/gycs_info.aspx',

    //            extraParams: {
    //                _type: 'getAllList'
    //            },

    //            reader: {
    //                type: 'json',
    //                root: 'data'
    //            }
    //        },

    //        fields: ['GYCSDM', 'GYCSMC'],
    //        autoLoad: true
    //    });

    // 销售油品下拉数据
    /*   var xsypStore = Ext.create('Ext.data.Store', {
    proxy: {
    type: 'ajax',
    url: '/api/system/xsyp_info.aspx',
            
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
        bodyPadding: 'auto',
        defaultType: 'textfield',
        layout: 'column',
        autoScroll:true,

        fieldDefaults: {
            msgTarget: 'side',
            labelAlign: 'center',
              labelWidth: 135,
//            width: 180,
            margin: '7 0 0 0'
        },

        items: [
        {     xtype: 'monthfield',
               fieldLabel: '年月',
               name: 'NY',
               readOnlyCls: 'readOnly',
               allowBlank: false },
        { fieldLabel: '井号', name: 'JH', allowBlank: false },
        { xtype: 'combo', fieldLabel: '作业区', name: 'ZYQ', store: departmentStore, displayField: 'DEP_NAME', valueField: 'DEP_ID', queryMode: 'local', allowBlank: false },
        { fieldLabel: 'ID', name: 'DJ_ID', hidden: true },
        { fieldLabel: '区块', name: 'QK', allowBlank: true },
        { fieldLabel: '层系', name: 'CX', allowBlank: true },
        { fieldLabel: '有效厚度（米）', name: 'CYHD', allowBlank: true },
        { fieldLabel: '井口压力（兆帕）', name: 'JKYL' },
        { fieldLabel: '生产时间（天）', name: 'SCSJ', allowBlank: true },
        { fieldLabel: '井口产气量（万方）', name: 'JKCQL' },
        { fieldLabel: '月核实产气量（万方）', name: 'HSCQL' },
        { fieldLabel: '自用气量（万方）', name: 'ZYQLL' },
        { fieldLabel: '放空气量（万方）', name: 'FKQL' },
        { fieldLabel: '凝析油量（吨）', name: 'JKCYL' },
        { fieldLabel: '产水量（方）', name: 'CYL' },
        { fieldLabel: '累计工业产气量（万方）', name: 'LJCQL' },
        { fieldLabel: '累计自用气量（万方）', name: 'LJZYQL' },
        { fieldLabel: '累计放空气量（万方）', name: 'LJFKQL' },
        { fieldLabel: '累计凝析油量（吨）', name: 'LJCYL' },
        { fieldLabel: '累计产水量（吨）', name: 'LJCSL' },
        { fieldLabel: '备注', name: 'BZ' }
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
          height: 460,
        width: 600,
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
        pageSize: 50, //一页容纳的数据量
        proxy: {
            type: 'ajax',
            url: '../../api/month/qjkfsj.aspx',

            extraParams: {
                _type: 'getWellList'
            },

            reader: {
                type: 'json',
                root: 'data',
                totalProperty: 'totalProperty'  //返回的数据总数
            }
        },

        fields: ['NY', 'JH', 'DEP_NAME', 'ZYQ', 'DJ_ID', 'QK', 'CX'
        , 'CYHD', 'JKYL', 'SCSJ', 'JKCQL', 'HSCQL', 'ZYQL', 'FKQL', 'JKCYL', 'CYL'
        , 'LJCQL', 'LJZYQL', 'LJFKQL', 'LJCYL', 'LJCSL', 'BZ'
        ]
        //        autoLoad: true
    });
    gridStore.load({ params: { start: 0, limit: 50} });
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
        { text: '井号', dataIndex: 'JH' },
        { text: '作业区', dataIndex: 'DEP_NAME' },
        { text: 'ID', dataIndex: 'DJ_ID', hidden: true },
        { text: '区块', dataIndex: 'QK' },
        { text: '层系', dataIndex: 'CX' },
        { text: '有效厚度（米）', dataIndex: 'CYHD' },
        { text: '井口压力（兆帕）', dataIndex: 'JKYL' },
        { text: '生产时间（天）', dataIndex: 'SCSJ' },
        { text: '井口产气量（万方）', dataIndex: 'JKCQL' },
        { text: '月核实产气量（万方）', dataIndex: 'HSCQL' },
        { text: '自用气量（万方）', dataIndex: 'ZYQLL' },
        { text: '放空气量（万方）', dataIndex: 'FKQL' },
        { text: '凝析油量（吨）', dataIndex: 'JKCYL' },
        { text: '产水量（方）', dataIndex: 'CYL' },
        { text: '累计工业产气量（万方）', dataIndex: 'LJCQL' },
        { text: '累计自用气量（万方）', dataIndex: 'LJZYQL' },
        { text: '累计放空气量（万方）', dataIndex: 'LJFKQL' },
        { text: '累计凝析油量（吨）', dataIndex: 'LJCYL' },
        { text: '累计产水量（吨）', dataIndex: 'LJCSL' },
        { text: '备注', dataIndex: 'BZ' }
        ],

        tbar: [
            '-',
            { xtype: 'button', text: '添加', iconCls: 'icon-add', handler: events.addClick },
            '-',
            { xtype: 'button', text: '删除', iconCls: 'icon-delete', handler: events.deleteClick },
            '-',
            { xtype: 'button', text: '编辑', iconCls: 'icon-edit', handler: events.editClick },
            '-',
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
        '../../api/month/qjkfsj.aspx',

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