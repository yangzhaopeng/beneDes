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



        // “编辑计算”按钮点击事件
        editClick: function() {
            if (!events.getGridSel()) return;

            isAdd = false;
            windowDialog.setTitle('编辑计算信息');

            formPanel.getForm().setValues({

                Num: Num,
                TB1: TB1,
                TB2: TB2,
                TB3: TB3,
                TB4: TB4,
                TB5: TB5,
                TB6: TB6,
                TB7: TB7,
                TB8: TB8,
                TB9: TB9,
                TB10: TB10,
                TB11: TB11,
                TB12: TB12,
                TB13: TB13,
                TB14: TB14,
                TB15: TB15,
                TB16: TB16,
                TB17: TB17,
                TB18: TB18,
                TB19: TB19

            });

            // 只读
            //            formPanel.getForm().findField('NY').setReadOnly(true);

            windowDialog.show();
        },

        // 弹出对话框的“保存”按钮点击事件

        save: function() {
            var form = formPanel.getForm();
            if (form.isValid()) {
                var values = form.getValues();
                values['_type'] = 'saveSJ';

            }
            Ext.Msg.confirm('确认', '您确定要保存？', function(result) {
                if (result == 'yes') {
                    dataJs.get(
                        '/api/month/tbsyjr.aspx',
                        values,

                        function(data, msg, response) {
                            Ext.Msg.alert('提示', msg, function() {
                                location.href = location.href;
                            });
                        }
                    );
                }
            });
        },

        // 弹出对话框的“计算”按钮点击事件
        addOrEdit: function() {
            var form = formPanel.getForm();

            if (form.isValid()) {
                var values = form.getValues();
                values['_type'] = 'edit';

                if (!isAdd) {
                    values['_type'] = 'edit';
                }

                dataJs.get(
                   '/api/month/tbsyjr.aspx',
                   values,

                   function(data, msg, response) {
                       windowDialog.hide();
                       Ext.Msg.alert('提示', msg, function() {
                           location.href = location.href;
                       });
                   }
               );
            }
        }
    };

    //---------------------------------------------------------创建、编辑对话框--------------------------------------------




    var Panel1 = Ext.create('Ext.form.Panel', {
        frame: false,
        layout: { type: 'table', column: 4 },
        //        layout: 'column',
        border: false,
        bodyPadding: 5,
        defaultType: 'textfield',
        fieldDefaults: {
            msgTarget: 'side',
            labelAlign: 'right',
            labelWidth: 30,
            width: 100

        },

        items: [{
            xtype: 'label',
            text: '原油价格(美元/桶)：',
            name: 'NYJG',
            margin: '0 10 0 0'
        },
           {
               xtype: 'textfield',
               name: 'NUM',
               id: 'NUM',
               value: Num,
               readOnlyCls: 'readOnly',
               readOnly: true,
               allowBlank: false,
               margin: '0 36 0 0'
           },
           {
               xtype: 'label',
               text: '征收比率（%）',
               name: 'ZSBL',
               margin: '0 14 0 0'
           },
           {
               xtype: 'label',
               text: '速算扣除数（美元/桶）',
               name: 'SSKCS',
               margin: '0 25 0 0'

}]

    });

    var Panel2 = Ext.create('Ext.form.Panel', {
        frame: false,
        layout: { type: 'table', column: 4 },
        //        layout: 'column',
        border: false,
        bodyPadding: 5,
        defaultType: 'textfield',
        fieldDefaults: {
            msgTarget: 'side',
            labelAlign: 'right',
            labelWidth: 30,
            width: 100
        },

        items: [{
            xtype: 'textfield',
            name: 'TB1',
            id: 'TB1',
            value: TB1,
            allowBlank: false,
            margin: '0 5 0 0'
        },
           {
               xtype: 'label',
               text: '至',
               name: 'Z1',
               margin: '0 5 0 0'
           },
           {
               xtype: 'textfield',
               name: 'TB2',
               value: TB2,
               id: 'TB2',
               margin: '0 25 0 0',
               allowBlank: false
               
           },
           {
               xtype: 'textfield',
               name: 'TB3',
               id: 'TB3',
               value: TB3,
               margin: '0 10 0 0',
               allowBlank: false
           },
           {
               xtype: 'textfield',
               name: 'TB4',
               id: 'TB4',
               value: TB4,
               margin: '0 10 0 0',
               allowBlank: false
}]

    });

    var Panel3 = Ext.create('Ext.form.Panel', {
        frame: false,
        layout: { type: 'table', column: 4 },
        //        layout: 'column',
        border: false,
        bodyPadding: 5,
        defaultType: 'textfield',
        fieldDefaults: {
            msgTarget: 'side',
            labelAlign: 'right',
            labelWidth: 30,
            width: 100
        },

        items: [{
            xtype: 'textfield',
            name: 'TB5',
            id: 'TB5',
            value: TB5,
            readOnlyCls: 'readOnly',
            readOnly: true,
            allowBlank: false,
            margin: '0 5 0 0'
        },
           {
               xtype: 'label',
               text: '至',
               name: 'Z2',
               margin: '0 5 0 0'

           },
           {
               xtype: 'textfield',
               name: 'TB6',
               id: 'TB6',
               value: TB6,
               margin: '0 25 0 0',
               allowBlank: false
           },
           {
               xtype: 'textfield',
               name: 'TB7',
               id: 'TB7',
               value: TB7,
               margin: '0 10 0 0',
               allowBlank: false
           },
           {
               xtype: 'textfield',
               name: 'TB8',
               id: 'TB8',
               value: TB8,
               margin: '0 10 0 0',
               allowBlank: false
}]

    });

    var Panel4 = Ext.create('Ext.form.Panel', {
        frame: false,
        layout: { type: 'table', column: 4 },
        //        layout: 'column',
        border: false,
        bodyPadding: 5,
        defaultType: 'textfield',
        fieldDefaults: {
            msgTarget: 'side',
            labelAlign: 'right',
            labelWidth: 30,
            width: 100
        },

        items: [{
            xtype: 'textfield',
            name: 'TB9',
            id: 'TB9',
            value: TB9,
            readOnlyCls: 'readOnly',
            readOnly: true,
            allowBlank: false,
            margin: '0 5 0 0'
        },
           {
               xtype: 'label',
               text: '至',
               name: 'Z3',
               margin: '0 5 0 0'

           },
           {
               xtype: 'textfield',
               name: 'TB10',
               id: 'TB10',
               value: TB10,
               margin: '0 25 0 0',
               allowBlank: false
           },
           {
               xtype: 'textfield',
               name: 'TB11',
               id: 'TB11',
               value: TB11,
               margin: '0 10 0 0',
               allowBlank: false
           },
           {
               xtype: 'textfield',
               name: 'TB12',
               id: 'TB12',
               value: TB12,
               margin: '0 10 0 0',
               allowBlank: false
}]

    });

    var Panel5 = Ext.create('Ext.form.Panel', {
        frame: false,
        layout: { type: 'table', column: 4 },
        //        layout: 'column',
        border: false,
        bodyPadding: 5,
        defaultType: 'textfield',
        fieldDefaults: {
            msgTarget: 'side',
            labelAlign: 'right',
            labelWidth: 30,
            width: 100
        },

        items: [{
            xtype: 'textfield',
            name: 'TB13',
            id: 'TB13',
            value: TB13,
            readOnlyCls: 'readOnly',
            readOnly: true,
            allowBlank: false,
            margin: '0 5 0 0'
        },
           {
               xtype: 'label',
               text: '至',
               name: 'Z4',
               margin: '0 5 0 0'

           },
           {
               xtype: 'textfield',
               name: 'TB14',
               id: 'TB14',
               value: TB14,
               margin: '0 25 0 0',
               allowBlank: false
           },
           {
               xtype: 'textfield',
               name: 'TB15',
               id: 'TB15',
               value: TB15,
               margin: '0 10 0 0',
               allowBlank: false
           },
           {
               xtype: 'textfield',
               name: 'TB16',
               id: 'TB16',
               value: TB16,
               margin: '0 10 0 0',
               allowBlank: false
}]

    });

    var Panel6 = Ext.create('Ext.form.Panel', {
        frame: false,
        layout: { type: 'table', column: 4 },
        //        layout: 'column',
        border: false,
        bodyPadding: 5,
        defaultType: 'textfield',
        fieldDefaults: {
            msgTarget: 'side',
            labelAlign: 'right',
            labelWidth: 30,
            width: 100
        },

        items: [{
            xtype: 'textfield',
            name: 'TB17',
            id: 'TB17',
            value: TB17,
            readOnlyCls: 'readOnly',
            readOnly: true,
            allowBlank: false,
            margin: '0 5 0 0'
        },
           {
               xtype: 'label',
               text: '以上',
               name: 'YS',
               margin: '0 118 0 0'
           },
           {
               xtype: 'textfield',
               name: 'TB18',
               id: 'TB18',
               value: TB18,
               margin: '0 10 0 0',
               allowBlank: false
           },
           {
               xtype: 'textfield',
               name: 'TB19',
               id: 'TB19',
               value: TB19,
               margin: '0 10 0 0',
               allowBlank: false
}]

    });

    var formPanel = Ext.create('Ext.form.Panel', {
        frame: false,
        layout: 'column',
        autoScroll: true,
        bodyPadding: 5,
        defaultType: 'textfield',
        fieldDefaults: {
            msgTarget: 'side',
            labelAlign: 'right',
            labelWidth: 30,
            width: 100
        },

        items: [Panel1, Panel2, Panel3, Panel4, Panel5, Panel6],


        buttons: [
            {
                text: '计算',
                iconCls: 'icon-ok',
                handler: events.addOrEdit
            },
            {
                text: '保存',
                iconCls: 'icon-save',
                handler: events.save
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
        height: 280,
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
    var gridStore = Ext.create('Ext.data.Store', {
        proxy: {
            type: 'ajax',
            url: '../../api/month/tbsyjr.aspx',

            extraParams: {
                _type: 'getAllList'
            },

            reader: {
                type: 'json',
                root: 'data'
            }
        },

        fields: ['NY', 'TBSYJ'],
        autoLoad: true
    });
//    var paramsStore = Ext.create('Ext.data.Store', {
//        proxy: {
//            type: 'ajax',
//            url: '../../api/month/tbsyj_bz.aspx',

//            extraParams: {
//                _type: 'getAllList'
//            },

//            reader: {
//                type: 'json',
//                root: 'data'
//            }
//        },

//        fields: ['NY', 'TBSYJ'],
//        autoLoad: true
//    });
    
    var gridPanel = Ext.create('Ext.grid.Panel', {
        store: gridStore,
        region: "center",
        border: false,
        enableColumnHide: false,    //不可隐藏列
        sortableColumns: false,     //不可排序
        columnLines: true,          //表格列竖线
        stripeRows: true,           //隔行换色

        selModel: Ext.create('Ext.selection.CheckboxModel', {   // grid中添加checkbox列
            mode: 'single',                                     // 单选
            showHeaderCheckbox: false                           // 隐藏列顶部的checkbox
        }),

        columns: [
            { text: '年月', dataIndex: 'NY' },
            { text: '特别收益金(元/吨)',width:140, dataIndex: 'TBSYJ' },
            { text: '', dataIndex: 'BZ', hidden: true, hiddenLabel: true },
            { text: '', dataIndex: 'BZ', hidden: true, hiddenLabel: true },
            { text: '', dataIndex: 'BZ', hidden: true, hiddenLabel: true }
           ],

        tbar: [

            '-',
            { xtype: 'button', text: '编辑计算', iconCls: 'icon-edit', handler: events.editClick },
            '-'
        ]
    });


    Ext.create('Ext.container.Viewport', {
        layout: 'border',
        items: [gridPanel],
        renderTo: Ext.getBody()
    });
});