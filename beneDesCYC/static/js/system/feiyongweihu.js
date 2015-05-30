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
                Ext.Msg.alert('提示', '请先选中一个维护模板！');
                return false;
            }

            gridSel = sel[0];

            return true;
        },

        // “添加”按钮点击事件
        addClick: function() {
            isAdd = true;
            windowDialog.setTitle('添加维护模板');
            formPanel.getForm().reset();
            windowDialog.show();
        },

        // “删除”按钮点击事件
        deleteClick: function() {
            if (!events.getGridSel()) return;

            Ext.Msg.confirm('确认', '您确定要删除该维护模板？', function(result) {
                if (result == 'yes') {
                    dataJs.get(
                        '../../api/system/feiyongweihu.aspx',

                        {
                            _type: 'delete',
                            TEMPLET_CODE: gridSel.data['TEMPLET_CODE']
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
            windowDialog.setTitle('编辑维护模板信息');

            formPanel.getForm().setValues({
                TEMPLET_CODE: gridSel.data['TEMPLET_CODE'],
                TEMPLET_NAME: gridSel.data['TEMPLET_NAME'],
                TEMPLET_TYPE: gridSel.data['TEMPLET_TYPE'],
                USE_TYPE: gridSel.data['USE_TYPE'],
                TEMPLET_LEVEL: gridSel.data['TEMPLET_LEVEL'],
                TEMPLET_TAG: gridSel.data['TEMPLET_TAG']
            });
            formPanel.getForm().findField('TEMPLET_CODE').setReadOnly(true);
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
                   '../../api/system/feiyongweihu.aspx',

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

//                                                                <asp:ListItem Value="CYC">采油厂</asp:ListItem>
//                                                                <asp:ListItem Value="QK">区块</asp:ListItem>
//                                                                <asp:ListItem Value="ZYQ">作业区</asp:ListItem>
//                                                                <asp:ListItem Value="ZXZ">中心站</asp:ListItem>
//                                                                <asp:ListItem Value="ZRZ">自然站</asp:ListItem>
//                                                                <asp:ListItem Value="DJ">单井</asp:ListItem>

    var weihuStore = new Ext.data.ArrayStore({
        fields: ['WEIHU_ID', 'WEIHU_NAME'],
        data: [
     ['CYC', '采油厂'], ['QK', '区块'], ['ZYQ', '作业区'], ['ZXZ', '中心站'], ['ZRZ', '自然站'], ['DJ', '单井']
    ]
    });

    var useStore = new Ext.data.ArrayStore({
        fields: ['USE_ID', 'USE_NAME'],
        data: [
     ['S', '公共模板'], ['P', '私有模板']
    ]
    });

    var mobanStore = new Ext.data.ArrayStore({
        fields: ['MOBAN_ID', 'MOBAN_NAME'],
        data: [
     ['0', '系统模板'], ['1', '自定义模板']
    ]
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
               fieldLabel: '模板编码',
               name: 'TEMPLET_CODE',
               allowBlank: false
           },
           {
               fieldLabel: '模板名称',
               name: 'TEMPLET_NAME',
               allowBlank: false
           },
           {
               xtype: 'combo',
               fieldLabel: '维护类型',
               name: 'TEMPLET_TYPE',
               id: 'TEMPLET_TYPE',
               store: weihuStore,
               displayField: 'WEIHU_NAME',
               valueField: 'WEIHU_ID',
               queryMode: 'local'
               
           },
           {
               xtype: 'combo',
               fieldLabel: '应用类型',
               name: 'USE_TYPE',
               id: 'USE_TYPE',
               store: useStore,
               displayField: 'USE_NAME',
               valueField: 'USE_ID',
               queryMode: 'local'
               
           },
           {
               xtype: 'combo',
               fieldLabel: '模板级别',
               name: 'TEMPLET_LEVEL',
               id: 'TEMPLET_LEVEL',
               store: mobanStore,
               displayField: 'MOBAN_NAME',
               valueField: 'MOBAN_ID',
               queryMode: 'local'
               
           },
           {
               fieldLabel: '作业区',
               name: 'TEMPLET_TAG'
               
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
            url: '../../api/system/feiyongweihu.aspx',

            extraParams: {
                _type: 'getAllList'
            },

            reader: {
                type: 'json',
                root: 'data'
            }
        },

        fields: ['TEMPLET_CODE', 'TEMPLET_NAME', 'TEMPLET_TYPE', 'USE_TYPE','U_TYPE', 'TEMPLET_LEVEL', 'T_LEVEL', 'TEMPLET_TAG'],
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
            { text: '模板编码', dataIndex: 'TEMPLET_CODE' },
            { text: '模板名称', dataIndex: 'TEMPLET_NAME' },
            { text: '维护类型', dataIndex: 'TEMPLET_TYPE' },
            { text: '应用类型', dataIndex: 'U_TYPE' },
            { text: '模板级别', dataIndex: 'T_LEVEL' },
            { text: '应用对象', dataIndex: 'TEMPLET_TAG', flex: 1 }
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