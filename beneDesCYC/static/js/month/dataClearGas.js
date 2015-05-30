Ext.onReady(function() {
    
     Ext.onReady(function() {
           var events = {

        // “添加”按钮点击事件
        dataClear: function() {
            var form = formPanel.getForm();
                
            if (form.isValid()) {
               Ext.Msg.confirm('确认', '您确定要删除该单井基础信息？', function(result) {
                if (result == 'yes') {
                var values = form.getValues();
                dataJs.get(
                   '../../api/month/dataClearOil.aspx',
                  values,
                   function(data, msg, response) {
                    var resJson = response.responseText;
                    var msg = Ext.decode(resJson?resJson:'{}').msg;
//                   Ext.Msg.show({
//                              title: '提示信息',
//                              msg: msg,
//                              minWidth: 400,//可以改
//                              maxWidth: 600,//可以改
//                              buttons: Ext.Msg.YES,
//                              icon: Ext.MessageBox.INFO
//                            });
//                       Ext.Msg.alert('提示', msg, function() {
//                            windowDialog.hide();
//                       });
                   }
               );
              }
            })
        }
        }
        }
    //======================================================================================= 
       var formPanel = Ext.create('Ext.form.Panel', {
        frame: true,
        bodyPadding: 5,
        defaultType: 'textfield',
        id:'checkGroup',
//        fieldDefaults: {
//            msgTarget: 'side',
//            labelAlign: 'right',
//            // labelWidth: 80
//            width: 350
//        },
         width: 320,
     items:[{
        xtype: 'checkboxgroup',
        //fieldLabel: '清空数据',
        columns: 2,
        vertical: true,
        id:'CheckGroupAll',
        items: [
            { boxLabel: '单井基础数据', name: 'djsj', inputValue: '1',height:35 },
            { boxLabel: '单井开发数据', name: 'kfsj', inputValue: '2', height:35 },
            { boxLabel: '区块数据', name: 'qksj', inputValue: '3' ,height:35 },
            { boxLabel: '评价单元数据', name: 'pjdysj', inputValue: '4' ,height:35 },
            { boxLabel: '单井直接费用', name: 'djfy', inputValue: '5' ,height:35 }, 
            { boxLabel: '采气厂费用', name: 'CQC', inputValue: '6',height:35  },
            { boxLabel: '作业区费用', name: 'ZYQ', inputValue: '7',height:35  },
            { boxLabel: '区块费用', name: 'QK', inputValue: '8' ,height:35 ,hidden:true},
            { boxLabel: '集气站费用', name: 'JQZ', inputValue: '9' ,height:35 },   
            { boxLabel: '集气总站费用', name: 'JQZZ', inputValue: '10' ,height:35 },
            { boxLabel: '净化厂费用', name: 'JHC', inputValue: '11' ,height:35 }
        ]
    }],

        buttons: [
            {
                text: '全选',
                handler: function() {
                    var array = Ext.getCmp('CheckGroupAll').items;  
                        array.each(function(item){  
                             item.setValue(true);  
                        });  
                }
            }, {
                text: '反选',
                handler: function() {
                    var array = Ext.getCmp('CheckGroupAll').items;  
                        array.each(function(item){  
//                            alert(item.getValue());  
                            if(item.getValue()==true){  
                                item.setValue(false);  
                            }else{  
                                item.setValue(true);  
                            }  
                        });  
                }
            },{
                text: '清空数据',
                iconCls: 'icon-ok',
                handler: events.dataClear
            }
        ]
    });
     var windowDialog = Ext.create('Ext.window.Window', {
        title: '当前日期：'+ month,
        layout: 'fit',
           x:90,
           y:80,
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