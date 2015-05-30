Ext.Loader.setConfig({ enabled: true });
Ext.Loader.setPath('Ext.ux', '../../static/Extjs/src/ux');
Ext.require([
    'Ext.form.Panel',
    'Ext.ux.form.MultiSelect',
    'Ext.ux.form.ItemSelector',
    'Ext.ux.ajax.SimManager'
]);

Ext.onReady(function() {
    //    Ext.tip.QuickTipManager.init();
    function createDockedItems(fieldId) {
        return [{
            xtype: 'toolbar',
            dock: 'top',
            items: {
                text: 'Options',
                menu: [{
                    text: 'Get value',
                    handler: function() {
                        var value = Ext.getCmp(fieldId).getValue();
                        Ext.Msg.alert('Value is a split array', value.join(', '));
                    }
                }
                    ]
            }
        }, {
            xtype: 'toolbar',
            dock: 'bottom',
            ui: 'footer',
            defaults: {
                minWidth: 75
            },
            items: ['->', {
                text: 'Clear',
                handler: function() {
                    var field = Ext.getCmp(fieldId);
                    if (!field.disabled) {
                        field.clearValue();
                    }
                }
            }, {
                text: 'Reset',
                handler: function() {
                    Ext.getCmp(fieldId).up('form').getForm().reset();
                }
            }, {
                text: 'Save',
                handler: function() {
                    var form = Ext.getCmp(fieldId).up('form').getForm();
                    form.getValues(true);
                    if (form.isValid()) {
                        Ext.Msg.alert('Submitted Values', 'The following will be sent to the server: <br />' +
                            form.getValues(true));
                    }
                }
            }
                ]
        }
                ];
    };

    var windowDialog = Ext.create('Ext.window.Window', {
        id: "myWin",
        title: "示例窗口",
        width: 500,
        height: 300,
        layout: "fit",
        autoShow: true,
        items: [
               {
                   xtype: "form",
                   //            width: 700,
                   //            bodyPadding: 10,
                   //            height: 300,
                   layout: 'fit',
                   items: [{
                       //                       xtype: 'itemselector',
                       //                       name: 'itemselector',
                       //                       id: 'itemselector-field',
                       //                       anchor: '100%',
                       //                       fieldLabel: 'ItemSelector',
                       //                       imagePath: '../ux/images/',
                       //                       store: ds,
                       //                       displayField: 'text',
                       //                       valueField: 'value',
                       //                       value: ['3', '4', '6'],
                       //                       allowBlank: false,
                       //                       msgTarget: 'side',
                       fromTitle: '区块名称',
                       toTitle: '已选区块'}],
                       //                dockedItems: createDockedItems('itemselector-field'),
                       buttons: [
                          { xtype: "button", text: "确定", handler: function() { this.up("window").close(); } },
                          { xtype: "button", text: "取消", handler: function() { this.up("window").close(); } }]
}]
    });

    Ext.ux.ajax.SimManager.init({
        delay: 300,
        defaultSimlet: null
    }).register({
        'Numbers': {
            data: [[123, 'One Hundred Twenty Three'],
                    ['1', 'One'], ['2', 'Two'], ['3', 'Three'], ['4', 'Four'], ['5', 'Five'],
                    ['6', 'Six'], ['7', 'Seven'], ['8', 'Eight'], ['9', 'Nine']],
            stype: 'json'
        }
    });

    var ds = Ext.create('Ext.data.ArrayStore', {
        fields: ['value', 'text'],
        proxy: {
            type: 'ajax',
            url: 'Numbers',
            reader: 'array'
        },
        autoLoad: true,
        sortInfo: {
            field: 'value',
            direction: 'ASC'
        }
    });


    /*
    * Ext.ux.form.ItemSelector Example Code
    */
    var isForm = Ext.widget('form', {
        //        title: 'ItemSelector Test',
        width: 700,
        bodyPadding: 10,
        height: 300,
        renderTo: 'itemselector',
        layout: 'fit',
        items: [{
            xtype: 'itemselector',
            name: 'itemselector',
            id: 'itemselector-field',
            anchor: '100%',
            fieldLabel: 'ItemSelector',
            imagePath: '../ux/images/',
            store: ds,
            displayField: 'text',
            valueField: 'value',
            value: ['3', '4', '6'],
            allowBlank: false,
            msgTarget: 'side',
            fromTitle: '区块名称',
            toTitle: '已选区块'
}],
            dockedItems: createDockedItems('itemselector-field')
        });
    });
