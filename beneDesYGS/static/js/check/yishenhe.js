Ext.onReady(function() {

    // 对话框显示时，当前状态是否是“添加”，或者是“编辑”
    var isAdd = true;
    // 当前grid选中的行
    var gridSel;

    //--------------------------------------------------------------------------事件集中处理-------------------------------
    var events = {

        // 获取当前grid选中的行
        getGridSel: function() {
            var sel = gridPanel.getSelectionModel().getSelection();

            if (sel.length < 1) {
                Ext.Msg.alert('提示', '请先选中一行记录！');
                return false;
            }

            gridSel = sel[0];

            return true;
        },

        // “解锁”按钮点击事件
        jiesuoClick: function() {
            if (!events.getGridSel()) return;

            Ext.Msg.confirm('确认', '是否要解锁该条信息？', function(result) {
                if (result == 'yes') {
                    dataJs.get(
                        '../../api/check/yishenhe.aspx',

                        {
                            _type: 'jiesuoClick',
                            NY: gridSel.data['NY'],
                            DEP_NAME: gridSel.data['DEP_NAME']

                        },
                        function(data, msg, response) {
                            Ext.Msg.alert('提示', msg, function() {
                                location.href = location.href;
                            });
                        }
                    );
                }
            });
        }
    };

    // -----------------------------------------------------------------中间grid表格-----------------------------------------

    var nyStore = Ext.create('Ext.data.Store', {
        proxy: {
            type: 'ajax',
            url: '../../api/system/shny.aspx',

            extraParams: {
                _type: 'getSHNYList'
            },

            reader: {
                type: 'json',
                root: 'data'
            }

        },

        fields: ['NYNAME', 'NYID'],
        autoLoad: true
    });

//       var gridStore = Ext.create('Ext.data.Store', {

//            proxy: {
//                type: 'ajax',
//                url: '/api/check/yishenhe.aspx',

//                extraParams: {
//                    _type: 'getAllList'

//                },

//                reader: {
//                    type: 'json',
//                    root: 'data'
//                }
//            },

//    //        //fields: ['NY', 'DEP_ID', 'YQLXDM', 'SC', 'DEP_NAME', 'YQLXMC'],
//            fields: ['NY', 'DEP_NAME', 'SHENHE', 'HAS_SUO'],
//            autoLoad: true
//        });

    var gridPanel = Ext.create('Ext.grid.Panel', {
        // store: gridStore,
        region: "center",
        border: false,
        enableColumnHide: false,    //不可隐藏列
        sortableColumns: false,     //不可排序
        columnLines: true,          //表格列竖线
        stripeRows: true,           //隔行换色
        forcefit: true,

        selModel: Ext.create('Ext.selection.CheckboxModel', {   // grid中添加checkbox列
            mode: 'single',                                     // 单选
            showHeaderCheckbox: false                           // 隐藏列顶部的checkbox
        }),

        columns: [
            { text: '年月', dataIndex: 'NY' },
            { text: '采油厂', dataIndex: 'DEP_NAME', width: 200, fixed: true },
            { text: '是否审核', dataIndex: 'SHENHE' },
            { text: '是否解锁', dataIndex: 'HAS_SUO' },
            { text: '', dataIndex: '' }
        ],

        tbar: [
            '-',
            { xtype: 'combo', fieldLabel: '选择年月', labelWidth: 80, width: 200, labelAlign: 'right', name: 'XZNY', id: 'XZNY', store: nyStore, displayField: 'NYNAME', valueField: 'NYID', queryMode: 'local', emptyText: '请选择年月', margin: '0 0 0 20', allowBlank: true,
                listeners: {
                    select: function() {
                        var xzny = Ext.getCmp("XZNY").getValue();
                        var gridStore = Ext.create('Ext.data.Store', {

                            proxy: {
                                type: 'ajax',
                                url: '../../api/check/yishenhe.aspx',

                                extraParams: {
                                    _type: 'getAllList',
                                    XZNY: xzny
                                },

                                reader: {
                                    type: 'json',
                                    root: 'data'
                                }
                            },
                            fields: ['NY', 'DEP_NAME', 'SHENHE', 'HAS_SUO'],
                            autoLoad: true
                        });

                        gridStore.load();
                        gridPanel.reconfigure(gridStore);
                        
                    }
                }
            },
            '-',
            { xtype: 'button', text: '解锁', iconCls: 'icon-edit', handler: events.jiesuoClick },
            '-'
        ]
    });



    Ext.create('Ext.container.Viewport', {
        layout: 'border',
        items: [gridPanel],
        renderTo: Ext.getBody()
    });



});