Ext.onReady(function() {
    
    // 对话框显示时，当前状态是否是“添加”角色，或者是“编辑”角色
    var isAdd = true;
    // 当前grid选中的行
    var gridSel;
    
    //--------------------------------------------------------------------------事件集中处理-------------------------------
    var events = {
    
        // gird行选中触发事件
        gridSelectionChange: function(thisModel, selected, eOpts) {
            var menuList = selected[0].data['MENU_LIST'];
            events.setChildNode(treeStore.getRootNode(), ',' + menuList + ',');
        },
        
        // 获取当前grid选中的行
        getGridSel: function() {
            var sel = gridPanel.getSelectionModel().getSelection();
            
            if (sel.length < 1) {
                Ext.Msg.alert('提示', '请先选中一个角色！');
                return false;
            }
            
            gridSel = sel[0];
            
            return true;
        },
        
        // 遍历所有的子节点，并选中或取消选中
        setChildNode: function(node, checked) {
            var checkedOrNot = checked;
            
            if (typeof checked == 'string') {
                checkedOrNot = false;
                var nodeId = node.data.id;
                
                if (checked.indexOf(',' + nodeId + ',') >= 0) {
                    checkedOrNot = true;
                }
            }
            
            node.set('checked', checkedOrNot);
            if (node.isNode) {
                node.eachChild(function(child){
                    events.setChildNode(child, checked);
                });
            }
        },
        
        // 遍历所有的父节点，并选中或取消选中
        setParentNode: function(node, checked) {
            if (node) {
                node.set('checked', checked);
                events.setParentNode(node.parentNode, checked);
            }
        },
        
        // tree的复选框勾选事件
        checkChange: function(node, checked) {
            node.eachChild(function(child) {
                events.setChildNode(child, checked);
            });
            
            if (checked) {
                events.setParentNode(node.parentNode, checked);
            }
        },
        
        // tree的节点点击事件
        treeNodeClick: function(view, record, item, index, e) {
            if (e.target.nodeName != 'INPUT' && record.childNodes.length > 0) {
                if (!record.isExpanded()) record.expand();
                else record.collapse();
            }
        },
        
        // “添加”按钮点击事件
        addClick: function() {
            isAdd = true;
            windowDialog.setTitle('添加角色');
            formPanel.getForm().reset();
            windowDialog.show();
        },
        
        // “删除”按钮点击事件
        deleteClick: function() {
            if (!events.getGridSel()) return;
            
            Ext.Msg.confirm('确认', '您确定要删除该角色？', function(result) {
                if (result == 'yes') {
                    dataJs.get(
                        '../../api/system/role.aspx',

                        {
                            _type: 'delete',
                            role_id: gridSel.data['ROLE_ID']
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
            windowDialog.setTitle('编辑角色');
            
            formPanel.getForm().setValues({
                role_name: gridSel.data['ROLE_NAME'],
                remark: gridSel.data['REMARK']
            });
            
            windowDialog.show();
        },
           
        // 弹出对话框的“确定”按钮点击事件
        addOrEdit: function() {
            var form = formPanel.getForm();
            
            if (form.isValid()) {
                var values = form.getValues();
                values['_type'] = 'add';
                
                if (!isAdd) {
                    values['role_id'] = gridSel.data['ROLE_ID'];
                    values['_type'] = 'edit';
                }
                
                dataJs.get(
                   '../../api/system/role.aspx',
                   
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
        
        // “权限保存”按钮点击事件
        saveRightClick: function() {
            if (!events.getGridSel()) return;
            
            var checked = treePanel.getChecked();
            var menu_list = [];
            
            for (var i = 0; i < checked.length; i++) {
                 menu_list.push(checked[i].data.id);
            }
            
            dataJs.get(
                '../../api/system/role.aspx',

                {
                    _type: 'saveRight',
                    role_id: gridSel.data['ROLE_ID'],
                    menu_list: menu_list.join(',')
                },

                function(data, msg, response) {
                   Ext.Msg.alert('提示', msg, function() {
                       location.href = location.href;
                   });
                }
            );
        }
    };
    
    //---------------------------------------------------------创建、编辑对话框--------------------------------------------
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
               fieldLabel: '角色名',
               name: 'role_name',
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
        height: 170,
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
            url: '../../api/system/role.aspx',
            
            extraParams: {
                _type: 'getAllRoles'    // 附带的参数_type=getAllRoles
            },
            
            reader: {
                type: 'json',
                root: 'data'
            }
        },
        
        fields:['ROLE_ID', 'ROLE_NAME', 'REMARK', 'MENU_LIST'],
        autoLoad: true
    });
    
    var gridPanel = Ext.create('Ext.grid.Panel', {
        store: gridStore,
        region: "center",
        enableColumnHide: false,    //不可隐藏列
        sortableColumns: false,     //不可排序
        columnLines: true,          //表格列竖线
        stripeRows: true,           //隔行换色
        
        selModel: Ext.create('Ext.selection.CheckboxModel', {   // grid中添加checkbox列
            mode: 'single',                                     // 单选
            showHeaderCheckbox: false,                          // 隐藏列顶部的checkbox
            listeners: {
                selectionchange: events.gridSelectionChange     // 绑定行选中事件
            }
        }),
        
        columns: [
            {text: '角色ID', dataIndex: 'ROLE_ID'},
            {text: '角色名', dataIndex: 'ROLE_NAME'},
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
            {xtype: 'button', text: '权限保存', iconCls: 'icon-role', handler: events.saveRightClick},
            '-'
        ]
    });
    
    // -----------------------------------------------------------右侧menu tree-------------------------------------------------
    var treeStore = Ext.create('Ext.data.TreeStore');
    
    var treePanel = Ext.create('Ext.tree.Panel', {
        title: '角色的权限---修改后点击左侧“权限保存”',
        store: treeStore,
        rootVisible: true,
        height: '100%',
        width: 300,
        margin: '0 0 0 0',
        autoScroll: true,
        border: false,
        lines: false,
        singleExpand: false,
        useArrows: true,
        region: 'east',
        listeners: {
            'checkchange': events.checkChange,
            'itemclick': events.treeNodeClick
        }
    });
    
    // 一次load所有menu的tree数据，并渲染页面
    dataJs.get(
        '../../api/system/menu.aspx', 
        
        {
            _type: 'getAllMenus'
        },
        
        function(data, msg, response) {
            Ext.create('Ext.container.Viewport', {
                layout: 'border',
                items: [gridPanel, treePanel],
                renderTo: Ext.getBody()
            });

            treeStore.setRootNode({
                expanded: true,
                children: data,
                text: '所有功能',
                id: 0,
                checked: false
            });
            
            treeStore.getRootNode().expandChildren();
        }
    );
    
});