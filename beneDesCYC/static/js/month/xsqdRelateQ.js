Ext.onReady(function() {

    // 对话框显示时，当前状态是否是“添加”，或者是“编辑”
    var isAdd = true;
    // 当前grid选中的行
    var gridSel;

    //--------------------------------------------------------------------------事件集中处理-------------------------------
    var events = {

        // 获取当前grid选中的行
        getGridSel: function(type) {
            var sel = gridPanel.getSelectionModel().getSelection();
            if(type=='edit'){
                if (sel.length != 1) {
                    Ext.Msg.alert('提示', '请选中一行记录编辑！');
                    return false;
                }
                gridSel = sel[0];
            }else{
                if (sel.length < 1) {
                    Ext.Msg.alert('提示', '请至少选中一行记录删除！');
                    return false;
                }
            }
        

            return true;
        },

        // “添加”按钮点击事件
        addClick: function() {
            isAdd = true;
            windowDialog.setTitle('添加销售参数信息');
            formPanel.getForm().reset();
            formPanel.getForm().findField('XSYPDM').setReadOnly(false);
            formPanel.getForm().findField('JG').setReadOnly(false);
            formPanel.getForm().findField('SJ').setReadOnly(false);
            // 年月字段设为不可修改，并且默认值为登录时带进来的年月
            formPanel.getForm().findField('NY').setReadOnly(false);
            // formPanel.getForm().findField('DEP_ID').setReadOnly(Only(true);
            formPanel.getForm().setValues({
                NY: month

            });

            windowDialog.show();
        },

        // “删除”按钮点击事件
        deleteClick: function() {
            if (!events.getGridSel('delete')) return;
            var records=gridPanel.getSelectionModel().getSelection();
            var nys ="";
            var xsypdms="";
            var len =records.length;
          
            for(var i = 0 ;i<len;i++){
            var data = records[i].data;
                  if (i == len - 1){
                       nys+=data['NY'];
                    xsypdms+=data['XSYPDM'];
          }else{
                    nys+=data['NY']+',';
                    xsypdms+=data['XSYPDM']+',';
                    }
                    
            }
            Ext.Msg.confirm('确认', '您确定要删除该销售参数信息？', function(result) {
                if (result == 'yes') {
                    dataJs.get(
                        '../../api/month/xsypRelateY.aspx',
                        {
                            _type: 'delete',
//                            NY: gridSel.data['NY'],
//                            XSYPDM: gridSel.data['XSYPDM']
                            NYS:nys,
                            XSYPDMS:xsypdms
                        },
                        function(data, msg, response) {
                            Ext.Msg.alert('提示', msg, function() {
                                gridStore.reload();
                            });
                        }
                        )
                    }; 
            });
        },

        // “编辑”按钮点击事件
        editClick: function() {
        
            if (!events.getGridSel('edit')) return;

            isAdd = false;
            windowDialog.setTitle('编辑销售参数信息');

            formPanel.getForm().setValues({
                NY: gridSel.data['NY'],
                XSYPDM: gridSel.data['XSYPDM'],
                 DTB: gridSel.data['DTB'],
                YQSPL: gridSel.data['YQSPL'],
                ZYS: gridSel.data['ZYS'],
                JG: gridSel.data['JG'],
                SJ: gridSel.data['SJ'],
                CYC_ID: gridSel.data['CYC_ID']
            });

            // 只读
            formPanel.getForm().findField('NY').setReadOnly(true);
            formPanel.getForm().findField('XSYPDM').setReadOnly(true);

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
                   '../../api/month/xsqdRelateQ.aspx',

                   values,

                   function(data, msg, response) {
                       windowDialog.hide();
                       Ext.Msg.alert('提示', msg, function() {
                       gridStore.reload();
//                           gridStore.reload();//location.href = location.href;
                       });
                   }
               );
            }
        }
    };

    //---------------------------------------------------------创建、编辑对话框--------------------------------------------

    var yqlxStore = Ext.create('Ext.data.Store', {
        proxy: {
            type: 'ajax',
            url: '../../api/system/xsyp_info.aspx',

            extraParams: {
                _type: 'getAllList'
            },

            reader: {
                type: 'json',
                root: 'data'
            }
        },

        fields: ['XSYPDM', 'XSYPMC'],
        autoLoad: true
    });

    var formPanel = Ext.create('Ext.form.Panel', {
        frame: true,
        bodyPadding: 5,
        defaultType: 'textfield',
        
        fieldDefaults: {
            msgTarget: 'side',
            labelAlign: 'right',
          labelWidth: 160
//            width: 350
        },

        items: [
           {
               xtype: 'monthfield',
               fieldLabel: '年月',
               name: 'NY',
               readOnlyCls: 'readOnly',
               allowBlank: false
           },{
           xtype: 'combo',
           fieldLabel: '销售渠道',
           name: 'XSYPDM',
           readOnlyCls: 'readOnly',
           store: yqlxStore,
           displayField: 'XSYPMC',
           valueField: 'XSYPDM',
           queryMode: 'local',
           allowBlank: false
       },
            {
               fieldLabel: '油气商品率（%）',
               xtype: 'numberfield',
               name: 'YQSPL',
               allowBlank: true
           }, {
               fieldLabel: '吨桶比（桶/吨）',
               xtype: 'numberfield',
               name: 'DTB',
               decimalPrecision: 3,
               allowBlank: true
           }, 
           {
               fieldLabel: '税金（元/千方）',
               xtype: 'numberfield',
               name: 'SJ',
               allowBlank: true
           }, {
               fieldLabel: '资源税（%）',
               xtype: 'numberfield',
               name: 'ZYS',
               allowBlank: true
           }, {
               fieldLabel: '价格（元/千方）',
               xtype: 'numberfield',
               name: 'JG',
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
//        height: 180,
//        width: 380,
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
            url: '../../api/month/xsqdRelateQ.aspx',

            extraParams: {
                _type: 'getAllList'
            },

            reader: {
                type: 'json',
                root: 'data'
            }
        },

        fields: ['NY', 'XSYPDM', 'JG', 'SJ', 'XSYPMC', 'DTB', 'YQSPL','ZYS'],
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
            //mode: 'single',                                     // 单选
            mode: 'multi',                                     // 多选
            showHeaderCheckbox: false                           // 隐藏列顶部的checkbox
        }),

        columns: [
            { text: '年月', dataIndex: 'NY' },
            { text: '销售渠道', dataIndex: 'XSYPMC' },
            { text: '油气商品率（%）',width:160, dataIndex: 'YQSPL' },
             { text: '吨桶比（桶/吨）',width:130, dataIndex: 'DTB' },
            { text: '资源税（%）',width:130,dataIndex: 'ZYS' },
            { text: '税金（元/千方）',width:150,dataIndex: 'SJ' },
            { text: '价格（元/千方）',width:150, dataIndex: 'JG' }
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