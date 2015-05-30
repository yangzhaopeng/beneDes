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
                        '../../api/system/feiyong.aspx',

                        {
                            _type: 'delete',
                            DL_ID: gridSel.data['DL_ID']
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
                DL_ID: gridSel.data['DL_ID'],
                DL_NAME: gridSel.data['DL_NAME'],
                FY_TYPE: gridSel.data['FY_TYPE'],
                FORMULAS: gridSel.data['FORMULAS'],
                remark: gridSel.data['REMARK']
            });
            formPanel.getForm().findField('DL_ID').setReadOnly(true);
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
                   '../../api/system/feiyong.aspx',

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



    var fyTypeStore = new Ext.data.ArrayStore({
        fields: ['FY_ID', 'FY_NAME'],
        data: [
     ['0', '成本'], ['1', '收入']
    ]
    });

    var formulaStore = Ext.create('Ext.data.Store', {
        proxy: {
            type: 'ajax',
            url: '../../api/system/formulaS.aspx',

            reader: {
                type: 'json',
                root: 'data'
            }

        },

        fields: ['FL_ID', 'FL_NAME'],
        autoLoad: true
    });

    var formPanel = Ext.create('Ext.form.Panel', {
        frame: true,
        bodyPadding: 5,
        defaultType: 'textfield',
        layout: 'column',
        
        fieldDefaults: {
            msgTarget: 'side',
            labelAlign: 'right',
            labelWidth: 75,
            width: 350,
            margin: '7 0 0 0'
        },

        items: [
           {
               fieldLabel: '大类名称',
               name: 'DL_NAME',
               allowBlank: false
           },
           {
               fieldLabel: '大类编号',
               name: 'DL_ID',
               allowBlank: false
           },
           {
               xtype: 'combo',
               fieldLabel: '费用类型',
               name: 'FY_TYPE',
               id: 'FY_TYPE',
               store: fyTypeStore,
               displayField: 'FY_NAME',
               valueField: 'FY_ID',
               queryMode: 'local',
               allowBlank: false
           },
           {
               xtype: 'combo',
               fieldLabel: '分摊公式',
               name: 'FORMULAS',
               id: 'FORMULAS',
               store: formulaStore,
               displayField: 'FL_NAME',
               valueField: 'FL_ID',
               queryMode: 'local',
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
            url: '../../api/system/feiyong.aspx',

            extraParams: {
                _type: 'getAllList'
            },

            reader: {
                type: 'json',
                root: 'data'
            }
        },

        fields: ['DL_NAME', 'DL_ID', 'FY_TYPE', 'FORMULAS', 'REMARK'],
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
            { text: '类型名称', dataIndex: 'DL_NAME' },
            { text: '类型编号', dataIndex: 'DL_ID' },
            { text: '费用类型', dataIndex: 'FY_TYPE' },
            { text: '公式代码', dataIndex: 'FORMULAS' },
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