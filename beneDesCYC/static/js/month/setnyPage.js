Ext.onReady(function() {
    
     Ext.onReady(function() {
           var events = {

        // “添加”按钮点击事件
        setClick: function() {
            var form = formPanel.getForm();
                
            if (form.isValid()) {
                var values = form.getValues();
                month=values["NY"];
                dataJs.get(
                   '../../api/month/setnyPage.aspx',
                  values,

                   function(data, msg, response) {
                            windowDialog.hid
//                       Ext.Msg.alert('提示', msg, function() {
                            top.location.href=top.location.href;
//                       });
                   }
               );
            }
            }
        }
            
       var formPanel = Ext.create('Ext.form.Panel', {
        frame: true,
        bodyPadding: 5,
        defaultType: 'textfield',

        fieldDefaults: {
            msgTarget: 'side',
            labelAlign: 'right',
              labelWidth: 60
            //            width: 350
        },

        items: [
           {
               xtype: 'monthfield',
               fieldLabel: '年月',
               name: 'NY',
               value:month,
               allowBlank: false
           }
        ],

        buttons: [
            {
                text: '确定',
                iconCls: 'icon-ok',
                handler: events.setClick
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
        title: '当前日期：'+month,
        layout: 'fit',
        modal: true,
        closeAction: 'hide',
        constrain: true,
        resizable: false,
        border: false,
        items: [formPanel],
        autoload:true
    });
    windowDialog.show();
        });
});