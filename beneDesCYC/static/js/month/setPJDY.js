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
            dock: 'bottom',
            ui: 'footer',
            defaults: {
                minWidth: 75
            },
            items: ['->', {
                text: '保存',
                handler: function() {
                    var form = Ext.getCmp(fieldId).up('form').getForm();
                    form.getValues(true);
                    if (form.isValid()) {
                        Ext.Msg.alert('Submitted Values', 'The following will be sent to the server: <br />' +
                            form.getValues(true));
                    }
                }
            },{
                text: '取消',
                handler: function() {
                    var field = Ext.getCmp(fieldId);
                    if (!field.disabled) {
                        field.clearValue();
                    }
                }
            }
                ]
        }
                ];
    };

   

    Ext.ux.ajax.SimManager.init({
        delay: 300,
        defaultSimlet: null
    }).register({
        'Numbers1': {
            data: [[123, 'One Hundred '],
                    ['1', '评价单元1'], ['2', '评价单元2'], ['3', '评价单元3'], ['4', '评价单元4'], ['5', '评价单元5'],
                    ['6', 'Six'], ['7', 'Seven'], ['8', 'Eight'], ['9', 'Nine']],
            stype: 'json'
        }
    });

    var ds = Ext.create('Ext.data.ArrayStore', {
        fields: ['ID', 'PJDY'],
        proxy: {
            type: 'ajax',
            url: 'Numbers1',
            reader: 'array'
        },
        autoLoad: true,
        sortInfo: {
            field: 'PJDY',
            direction: 'ASC'
        }
    });

    /*
     * Ext.ux.form.MultiSelect Example Code
     */
    var msForm = Ext.widget('form', {
     title: '评价单元',
        width: 200,
        bodyPadding: 10,
        height: 300,
        items:[{
            anchor: '100%',
            xtype: 'multiselect',
            msgTarget: 'side',
            name: 'multiselect',
            id: 'multiselect-field',
            maxSelections:1,
//            allowBlank: true,
            store: {
                fields: [ 'number', 'numberName' ],
                proxy: {
                    type: 'ajax',
                    url: 'Numbers1',
                    reader: 'array'
                },
                autoLoad: true
            },
            listeners:{
            click:function(){
            alert("click me!");
            }},
            valueField: 'number',
            displayField: 'numberName',
            value: ['3', '4', '6'],
            ddReorder: true
        }]
//        ,dockedItems: createDockedItems('multiselect-field')
    });
    
    /*
    * Ext.ux.form.ItemSelector Example Code
    */
    var isForm = Ext.widget('form', {
        width: 400,
        bodyPadding: 10,
        height: 300,
        layout: 'fit',
        items: [{
            xtype: 'itemselector',
            name: 'itemselector',
            id: 'itemselector-field',
            anchor: '100%',
//            fieldLabel: 'ItemSelector',
            imagePath: '../ux/images/',
            store: ds,
            displayField: 'PJDY',
            valueField: 'ID',
            value: ['3', '4', '6'],
            allowBlank: false,
            msgTarget: 'side',
            fromTitle: '区块名称',
            toTitle: '已选区块'
}],
            dockedItems: createDockedItems('itemselector-field')
        });
          
       var windowDialog = Ext.create('Ext.window.Window', {
        id: "myWin",
        title: "示例窗口",
//        width: 500,
//        height: 300,
        layout: 'column',
        autoShow: true,
        items: [msForm,isForm]
    });
    });
