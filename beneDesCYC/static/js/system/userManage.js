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
                Ext.Msg.alert('提示', '请先选中一个用户！');
                return false;
            }
            
            gridSel = sel[0];
            
            return true;
        },
        
        // “添加”按钮点击事件
        addClick: function() {
            isAdd = true;
            windowDialog.setTitle('添加用户（默认密码为123456）');
            formPanel.getForm().reset();
            formPanel.getForm().findField('user_id').setReadOnly(false);
            windowDialog.show();
        },
        
        // “删除”按钮点击事件
        deleteClick: function() {
            if (!events.getGridSel()) return;
            
            Ext.Msg.confirm('确认', '您确定要删除该用户？', function(result) {
                if (result == 'yes') {
                    dataJs.get(
                        '../../api/system/user.aspx',

                        {
                            _type: 'delete',
                            user_id: gridSel.data['USER_ID']
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
            windowDialog.setTitle('编辑用户信息');
            
            formPanel.getForm().setValues({
                user_id: gridSel.data['USER_ID'],
                user_name: gridSel.data['USER_NAME'],
                role_id: gridSel.data['ROLE_ID'],
                dep_id: gridSel.data['DEP_ID'],
                remark: gridSel.data['REMARK']
            });
            
            // 登录名输入框置为可读
            formPanel.getForm().findField('user_id').setReadOnly(true);
            
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
                   '../../api/system/user.aspx',
                   
                   values,
                   
                   function(data, msg, response) {
                       windowDialog.hide();
                       Ext.Msg.alert('提示', msg, function() {
                           location.href = location.href;
                       });
                   }
               );
            }
        },
        
        // “密码重置”按钮点击事件
        resetPassword: function() {
            if (!events.getGridSel()) return;
            
            Ext.Msg.prompt('密码重置', '输入新密码', function(btn, text) {
                if (btn == 'ok') {
                    dataJs.get(
                        '../../api/system/user.aspx',

                        {
                            _type: 'resetPassword',
                            user_id: gridSel.data['USER_ID'],
                            password: text
                        },

                        function(data, msg, response) {
                           Ext.Msg.alert('提示', msg);
                        }
                    );
                }
            });
        }
    };
    
    //---------------------------------------------------------创建、编辑对话框--------------------------------------------
    
    var departmentStore = Ext.create('Ext.data.Store', {
        proxy: {
            type: 'ajax',
            url: '../../api/system/department.aspx',
            
            extraParams: {
                _type: 'getCYCList'
            },
            
            reader: {
                type: 'json',
                root: 'data'
            }
        },
        
        fields:['DEP_ID', 'DEP_NAME'],
        autoLoad: true
    });
    
    var roleStore = Ext.create('Ext.data.Store', {
        proxy: {
            type: 'ajax',
            url: '../../api/system/role.aspx',
            
            extraParams: {
                _type: 'getAllRoles'
            },
            
            reader: {
                type: 'json',
                root: 'data'
            }
        },
        
        fields:['ROLE_ID', 'ROLE_NAME'],
        autoLoad: true
    });
    
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
               fieldLabel: '登陆名',
               name: 'user_id',
               readOnlyCls: 'readOnly',
               allowBlank: false
           },
           {
               fieldLabel: '用户名称',
               name: 'user_name',
               allowBlank: false
           },
           {
               xtype: 'combo',
               fieldLabel: '所属采油厂',
               name: 'dep_id',
               store: departmentStore,
               displayField: 'DEP_NAME',
               valueField: 'DEP_ID',
               queryMode: 'local',
               allowBlank: false
           },
           {
               xtype: 'combo',
               fieldLabel: '所属角色',
               name: 'role_id',
               store: roleStore,
               displayField: 'ROLE_NAME',
               valueField: 'ROLE_ID',
               queryMode: 'local',
               allowBlank: false
           },
           {
               fieldLabel: '备注',
               xtype: 'textareafield',
               name: 'remark',
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
        height: 250,
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
            url: '../../api/system/user.aspx',
            
            extraParams: {
                _type: 'getAllUsers'
            },
            
            reader: {
                type: 'json',
                root: 'data'
            }
        },
        
        fields:['USER_ID', 'USER_NAME', 'DEP_ID', 'ROLE_ID', 'DEP_NAME', 'ROLE_NAME', 'REMARK'],
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
            {text: '登陆名', dataIndex: 'USER_ID'},
            {text: '用户名称', dataIndex: 'USER_NAME'},
            {text: '所属采油厂', dataIndex: 'DEP_NAME'},
            {text: '所属角色', dataIndex: 'ROLE_NAME'},
            {text: '备注', dataIndex: 'REMARK', flex: 1}
        ],
        
        tbar: [
            '-',
            {xtype: 'button', text: '添加', iconCls: 'icon-add', handler: events.addClick},
            '-',
            {xtype: 'button', text: '删除', iconCls: 'icon-delete', handler: events.deleteClick},
            '-',
            {xtype: 'button', text: '编辑', iconCls: 'icon-edit', handler: events.editClick},
            '-',
            '->',
            '-',
            {xtype: 'button', text: '密码重置', iconCls: 'icon-role', handler: events.resetPassword},
            '-'
        ]
    });
    
    
    Ext.create('Ext.container.Viewport', {
        layout: 'border',
        items: [gridPanel],
        renderTo: Ext.getBody()
    });
});