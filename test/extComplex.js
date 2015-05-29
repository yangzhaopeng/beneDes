﻿Ext.onReady(function() {
    
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
        
        // “添加”按钮点击事件
        addClick: function() {
            isAdd = true;
            windowDialog.setTitle('添加天然气体积换算系数信息');
            formPanel.getForm().reset();
           // formPanel.getForm().findField('YQLXDM').setReadOnly(false);
            
            // 年月字段设为不可修改，并且默认值为登录时带进来的年月
            formPanel.getForm().findField('NY').setReadOnly(true);
           // formPanel.getForm().findField('DEP_ID').setReadOnly(Only(true);
            formPanel.getForm().setValues({
             NY:month
           
            });
            
            windowDialog.show();
        },
        
        // “删除”按钮点击事件
        deleteClick: function() {
            if (!events.getGridSel()) return;
            
            Ext.Msg.confirm('确认', '您确定要删除天然气体积换算系数信息？', function(result) {
                if (result == 'yes') {
                NY: month
                    dataJs.get(
                        '../../api/month/params_Value.aspx',

                        {
                            _type: 'delete',
                            NY: gridSel.data['NY'],
                         // DEP_ID: gridSel.data['DEP_ID'],
                           // YQLXDM: gridSel.data['YQLXDM']
                        },

                        function(data, msg, response) {
                           Ext.Msg.alert('提示', msg, function() {
                               location.href = location.href;
                           });
                        }
                    );
                }
            });
        },
        
        // “编辑”按钮点击事件
        editClick: function() {
            if (!events.getGridSel()) return;
            
            isAdd = false;
            windowDialog.setTitle('编辑天然气体积换算系数信息');
            
            formPanel.getForm().setValues({
                NY: gridSel.data['NY'],
             // DEP_ID: gridSel.data['DEP_ID'],
               // YQLXDM: gridSel.data['YQLXDM'],
             // SC: gridSel.data['SC']
                DLY: gridSel.data['DLY'],
                CYC_ID:gridSel.data['CYC_ID']
            });
            
            // 只读
            formPanel.getForm().findField('NY').setReadOnly(true);
         // formPanel.getForm().findField('DEP_ID').setReadOnly(true);
          //  formPanel.getForm().findField('YQLXDM').setReadOnly(true);
            
            windowDialog.show();
        },
           
        // 弹出对话框的“确定”按钮点击事件
        addOrEdit: function() {
            var form = formPanel.getForm();
            
            if (form.isValid()) {
                var values = form.getValues();
                values['_type'] = 'add';
                
                if (!isAdd) {
                    values['_type'] = 'edit';
                }
                
                dataJs.get(
                   '../../api/month/params_Value.aspx',
                   
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
    
  /*  var departmentStore = Ext.create('Ext.data.Store', {
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
        
        fields:['DEP_ID', 'DEP_NAME'],
        autoLoad: true
    });
    */
   /* var yqlxStore = Ext.create('Ext.data.Store', {
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
        
        fields:['YQLXDM', 'YQLXMC'],
        autoLoad: true
    });
    */
    var formPanel = Ext.create('Ext.form.Panel',{
        frame: true,
        bodyPadding: 5,
        defaultType: 'textfield',

        fieldDefaults: {
           msgTarget: 'side',
           labelAlign: 'right',
           labelWidth: 75,
           width: 350
        },

        items: [
           {
               fieldLabel: '年月',
               name: 'NY',
               readOnlyCls: 'readOnly',
               allowBlank: false
           },
         /*{
               xtype: 'combo',
               fieldLabel: '作业区',
               name: 'DEP_ID',
               readOnlyCls: 'readOnly',
               store: departmentStore,
               displayField: 'DEP_NAME',
               valueField: 'DEP_ID',
               queryMode: 'local',
               allowBlank: false
           },
           {
               xtype: 'combo',
               fieldLabel: '油气类型',
               name: 'YQLXDM',
               readOnlyCls: 'readOnly',
               store: yqlxStore,
               displayField: 'YQLXMC',
               valueField: 'YQLXDM',
               queryMode: 'local',
               allowBlank: false
           },
           */
           {
               //fieldLabel: '1吨原油当量=()立方米天然气',
               fieldLabel: '体积换算系数',
               xtype: 'numberfield',
               name: 'DLY',
               allowBlank: true
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
        height: 180,
        width: 380,
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
            url: '../../api/month/params_Value.aspx',
            
            extraParams: {
                _type: 'getAllList'
            },
            
            reader: {
                type: 'json',
                root: 'data'
            }
        },
        
        fields:['NY',   'DLY'],
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
        
        selModel: Ext.create('Ext.selection.CheckboxModel', {   // grid中添加checkbox列
            mode: 'single',                                     // 单选
            showHeaderCheckbox: false                           // 隐藏列顶部的checkbox
        }),
        
        columns: [
            {text: '年月', dataIndex: 'NY'},
          //{text: '作业区', dataIndex: 'DEP_NAME'},
            //{text: '油气类型', dataIndex: 'YQLXMC'},
            {text: '天然气量（立方米天然气）',  width: 400,dataIndex: 'DLY', flex: 1}
        ],
        
        tbar: [
            '-',
            {xtype: 'button', text: '添加', iconCls: 'icon-add', handler: events.addClick},
            '-',
            {xtype: 'button', text: '删除', iconCls: 'icon-delete', handler: events.deleteClick},
            '-',
            {xtype: 'button', text: '编辑', iconCls: 'icon-edit', handler: events.editClick},
            '-'
//            ,
//            {xtype: 'button', text: '年月', iconCls: 'icon-edit', handler: events.editClick},
//            '-'
            
        ]
    });
    
    
    Ext.create('Ext.container.Viewport', {
        layout: 'border',
        items: [gridPanel],
        renderTo: Ext.getBody()
    });
});