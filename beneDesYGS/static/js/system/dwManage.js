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
                Ext.Msg.alert('提示', '请先选中一个部门！');
                return false;
            }

            gridSel = sel[0];

            return true;
        },

        // “添加”按钮点击事件
        addClick: function() {
            isAdd = true;
            windowDialog.setTitle('添加部门');
            formPanel.getForm().reset();
            windowDialog.show();
        },

        // “删除”按钮点击事件
        deleteClick: function() {
            if (!events.getGridSel()) return;

            Ext.Msg.confirm('确认', '您确定要删除该部门？', function(result) {
                if (result == 'yes') {
                    dataJs.get(
                        '../../api/system/danwei.aspx',

                        {
                            _type: 'delete',
                            DEP_ID: gridSel.data['DEP_ID']
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
            windowDialog.setTitle('编辑部门信息');

            formPanel.getForm().setValues({
                DEP_ID: gridSel.data['DEP_ID'],
                DEP_NAME: gridSel.data['DEP_NAME'],
                DEP_TYPE: gridSel.data['DEP_TYPE'],                
                remark: gridSel.data['REMARK']
            });
            formPanel.getForm().findField('DEP_ID').setReadOnly(true);
            formPanel.getForm().findField('DEP_TYPE').setReadOnly(true);
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
                   '../../api/system/danwei.aspx',

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



    var dwTypeStore = new Ext.data.ArrayStore({
        fields: ['DWID', 'DWNAME'],
        data: [
//     ['zyq', '作业区'], ['cyc', '采油厂']
       ['ZYQ', '作业区'], ['CYC', '采油厂']
    ]
    });
    
    var formPanel = Ext.create('Ext.form.Panel', {
        frame: true,
        bodyPadding: 5,
        defaultType: 'textfield',

        fieldDefaults: {
            msgTarget: 'side',
            labelAlign: 'right',
            labelWidth: 75,
            width: 350,
            margin: '7 0 0 0'
        },

        items: [
           {
               xtype: 'combo',
               fieldLabel: '部门类型',
               name: 'DEP_TYPE',
               id:'DEP_TYPE',
               store: dwTypeStore,
               displayField: 'DWNAME',
               valueField: 'DWID',
               queryMode: 'local',
               allowBlank: false
           },
           {
               fieldLabel: '部门标识',
               name: 'DEP_ID',
               allowBlank: false
           },
           {
               fieldLabel: '部门名称',
               name: 'DEP_NAME',
               allowBlank: false
           },         
           {
               fieldLabel: '备注',
               xtype: 'textareafield',
               name: 'remark'
               
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
            url: '../../api/system/danwei.aspx',

            extraParams: {
                _type: 'getAllList'
            },

            reader: {
                type: 'json',
                root: 'data'
            }
        },

        fields: ['DEP_TYPE', 'DEP_NAME', 'DEP_ID', 'REMARK'],
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
            { text: '部门类型', dataIndex: 'DEP_TYPE' },
            { text: '部门标识', dataIndex: 'DEP_ID' },
            { text: '部门名称', dataIndex: 'DEP_NAME' },
//            { text: '所属角色', dataIndex: 'ROLE_NAME' },
            { text: '备注', dataIndex: 'REMARK', flex: 1 }
        ],

        tbar: [
            '-',
            { xtype: 'button', text: '添加', iconCls: 'icon-add', handler: events.addClick },
            '-',
            { xtype: 'button', text: '删除', iconCls: 'icon-delete', handler: events.deleteClick },
            '-',
            { xtype: 'button', text: '编辑', iconCls: 'icon-edit', handler: events.editClick },
            '-',
        ]
    });


    Ext.create('Ext.container.Viewport', {
        layout: 'border',
        items: [gridPanel],
        renderTo: Ext.getBody()
    });
});