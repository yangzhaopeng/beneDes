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
            windowDialog.setTitle('添加伴生产品');
            formPanel.getForm().reset();
            formPanel.getForm().findField('BSCPDM').setReadOnly(false);
            formPanel.getForm().findField('BSCPMC').setReadOnly(false);
            formPanel.getForm().findField('BZ').setReadOnly(false);
            // 年月字段设为不可修改，并且默认值为登录时带进来的年月
            //formPanel.getForm().findField('NY').setReadOnly(true);
            //formPanel.getForm().setValues({
            // NY: month
            //});

            windowDialog.show();
        },

        // “删除”按钮点击事件
        deleteClick: function() {
            if (!events.getGridSel()) return;

            Ext.Msg.confirm('确认', '您确定要删除该伴生产品？', function(result) {
                if (result == 'yes') {
                    dataJs.get(
                        '../../api/base_info/bscp.aspx',

                        {
                            _type: 'delete',
                            BZ: gridSel.data['BZ'],
                            BSCPDM: gridSel.data['BSCPDM'],
                            BSCPMC: gridSel.data['BSCPMC']
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
            windowDialog.setTitle('编辑伴生产品');

            formPanel.getForm().setValues({
                //NY: gridSel.data['NY'],
                //DEP_ID: gridSel.data['DEP_ID'],
                //YQLXDM: gridSel.data['YQLXDM'],
                //SC: gridSel.data['SC']
                BZ: gridSel.data['BZ'],
                BSCPDM: gridSel.data['BSCPDM'],
                BSCPMC: gridSel.data['BSCPMC']

            });


            formPanel.getForm().findField('BZ').setReadOnly(false);
            formPanel.getForm().findField('BSCPDM').setReadOnly(true);
            formPanel.getForm().findField('BSCPMC').setReadOnly(false);

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
                   '../../api/base_info/bscp.aspx',

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

    /*var dzcsStore = Ext.create('Ext.data.Store', {
    proxy: {
    type: 'ajax',
    url: '../../api/base_info/bscp.aspx',

            extraParams: {
    _type: 'getAllList'
    },

            reader: {
    type: 'json',
    root: 'data'
    }
    },

        fields: ['BSCPDM', 'BSCPMC'],
    autoLoad: true
    });

    var departmentStore = Ext.create('Ext.data.Store', {
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

        fields: ['DEP_ID', 'DEP_NAME'],
    autoLoad: true
    });
    
    /*var yqlxStore = Ext.create('Ext.data.Store', {
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

        fields: ['YQLXDM', 'YQLXMC'],
    autoLoad: true
    });
    */
    var formPanel = Ext.create('Ext.form.Panel', {
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
               fieldLabel: '代码',
               name: 'BSCPDM',
               readOnlyCls: 'readOnly',
               allowBlank: false
           },
           {

               fieldLabel: '名称',
               name: 'BSCPMC',
               //readOnlyCls: 'readOnly',
               //store: dzcsStore,
               //isplayField: 'BSCPMC',
               //valueField: 'BSCPDM',
               //queryMode: 'local',
               allowBlank: false
           },

        /*{
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
        {
        fieldLabel: '输差',
        xtype: 'numberfield',
        name: 'SC',
        allowBlank: true
        }*/
           {
           fieldLabel: '备注',
           name: 'BZ',
           //readOnlyCls: 'readOnly',
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
            url: '../../api/base_info/bscp.aspx',

            extraParams: {
                _type: 'getAllList'
            },

            reader: {
                type: 'json',
                root: 'data'
            }
        },

        //fields: ['NY', 'DEP_ID', 'YQLXDM', 'SC', 'DEP_NAME', 'YQLXMC'],
        fields: ['BSCPDM', 'BSCPMC', 'BZ'],
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
            { text: '代码', dataIndex: 'BSCPDM' },
            { text: '名称', dataIndex: 'BSCPMC', width: 400, fixed: true },
            { text: '备注', dataIndex: 'BZ' },
            { text: ' ', dataIndex: 'DZCSJB', flex: 1 }
        ],

        tbar: [
            '-',
            { xtype: 'button', text: '添加', iconCls: 'icon-add', handler: events.addClick },
            '-',
            { xtype: 'button', text: '删除', iconCls: 'icon-delete', handler: events.deleteClick },
            '-',
            { xtype: 'button', text: '编辑', iconCls: 'icon-edit', handler: events.editClick },
            '-'
        ]
    });


    Ext.create('Ext.container.Viewport', {
        layout: 'border',
        items: [gridPanel],
        renderTo: Ext.getBody()
    });
});