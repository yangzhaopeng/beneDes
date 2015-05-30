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

        // “审核”按钮点击事件
        shenheClick: function() {
            if (!events.getGridSel()) return;

            Ext.Msg.confirm('确认', '是否通过审核？', function(result) {
                if (result == 'yes') {
                    dataJs.get(
                        '../../api/check/shenhe.aspx',

                        {
                            _type: 'shenheClick',
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
    var gridStore = Ext.create('Ext.data.Store', {
        proxy: {
            type: 'ajax',
            url: '../../api/check/shenhe.aspx',

            extraParams: {
                _type: 'getAllList'
            },

            reader: {
                type: 'json',
                root: 'data'
            }
        },

        //fields: ['NY', 'DEP_ID', 'YQLXDM', 'SC', 'DEP_NAME', 'YQLXMC'],
        fields: ['NY', 'DEP_NAME', 'SBTIME', 'SHENHE', 'HAS_SUO', 'SHENHEREN', 'REMARK'],
        autoLoad: true
    });

    var gridPanel = Ext.create('Ext.grid.Panel', {
        store: gridStore,
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
            { text: '上报时间', dataIndex: 'SBTIME' },
            { text: '是否审核', dataIndex: 'SHENHE' },
            { text: '是否解锁', dataIndex: 'HAS_SUO' },
            { text: '审核人', dataIndex: 'SHENHEREN' },
            { text: '查看', dataIndex: 'REMARK' }
        ],

        tbar: [

            '-',
            { xtype: 'button', text: '通过审核', iconCls: 'icon-shenhe', handler: events.shenheClick },
            '-'

        ]
    });


    Ext.create('Ext.container.Viewport', {
        layout: 'border',
        items: [gridPanel],
        renderTo: Ext.getBody()
    });
});