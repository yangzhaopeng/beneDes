Ext.onReady(function() {
   Ext.QuickTips.init();
   
   Ext.create('Ext.form.Panel',{
       renderTo: Ext.getBody(),
       title: '修改密码',
       frame: true,
       bodyPadding: 5,
       margin: '20 20 20 20',
       defaultType: 'textfield',
       buttonAlign: 'left',
       
       fieldDefaults: {
           msgTarget: 'side',
           labelWidth: 75,
           width: 350
       },
       
       items: [
           {
               fieldLabel: '旧密码',
               name: 'old',
               allowBlank: false,
               inputType: 'password'
           },
           {
               fieldLabel: '新密码',
               name: 'new',
               allowBlank: false,
               inputType: 'password'
           },
           {
               fieldLabel: '确认新密码',
               name: 'newAgain',
               allowBlank: false,
               inputType: 'password'
           }
       ],
       
       buttons: [
           {
               text: '确定',
               margin: '0 0 0 80',
               handler: function() {
                   var form = this.up('form').getForm();
                   if (form.isValid()) {
                       // 获取form中的所有key-value字段值
                       var values = form.getValues();
                       
                       if (values['new'] != values['newAgain']) {
                           Ext.Msg.alert('提示', '两次输入的密码不一致！');
                           return;
                       }
                       
                       // 指定api的锚点
                       values._type = 'changePassword';
                       
                       dataJs.get(
                           '../../api/system/user.aspx',
                           
                           values,
                           
                           function(data, msg, response) {
                               Ext.Msg.alert('提示', '修改密码成功！');
                               form.reset();
                           }
                       );
                   }
               }
           }    
       ]
       
   }); 
   
});