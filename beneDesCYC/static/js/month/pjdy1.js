Ext.onReady(function() {

    // 对话框显示时，当前状态是否是“添加”角色，或者是“编辑”角色
    var isAdd = true;
    // 当前grid选中的行
    var gridSel;

    //--------------------------------------------------------------------------事件集中处理-------------------------------
    var events = {

        // 获取当前grid选中的行
        getGridSel: function() {
            var sel = gridPanel.getSelectionModel().getSelection();

            if (sel.length < 1) {
                Ext.Msg.alert('提示', '请先选中一个区块！');
                return false;
            }

            gridSel = sel[0];

            return true;
        },

        // “添加”按钮点击事件
        addClick: function() {
            //if (!events.getGridSel()) return;
            isAdd = true;
            windowDialog.setTitle('添加评价单元信息');
            formPanel.getForm().reset();
            formPanel.getForm().findField('PJDYMC').setReadOnly(false);

            // 年月字段设为不可修改，并且默认值为登录时带进来的年月
            formPanel.getForm().findField('NY').setReadOnly(true);


            // formPanel.getForm().findField('DEP_ID').setReadOnly(Only(true);
            formPanel.getForm().setValues({
                NY: month


            });

            windowDialog.show();
        },

        // “删除”按钮点击事件
        deleteClick: function() {
            if (!events.getGridSel()) return;

            Ext.Msg.confirm('确认', '您确定要删除该评价单元信息？', function(result) {
                if (result == 'yes') {
                    NY: month
                    dataJs.get(
                        '/api/month/pjdy.aspx',

                        {
                            _type: 'delete',
                            NY: gridSel.data['NY'],
                            // DEP_ID: gridSel.data['DEP_ID'],
                            PJDYMC: gridSel.data['PJDYMC']
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
        editClick: function(params) {
            if (!events.getGridSel()) return;
            isAdd = false;
            windowDialog.setTitle('编辑评价单元信息');
            //加载  已选择区块列表  
            var month = gridSel.data['NY'];
            var pjdy  = gridSel.data['PJDYMC'];
            qkStore.load({ params:{pjdy: pjdy,month:month}});
            windowDialog.show();
        },

        //上传文件按钮
        upload: function() {
            var form = sPanel.getForm();
            var file = Ext.getCmp("UPLOADFILE").getValue();
            if (file == "") {
                Ext.Msg.alert('提示', '请选择文件！');
                return false;
            }
            if (form.isValid()) {

                form.submit({
                    url: '/api/system/uploadfilepjdy.aspx',
                    waitMsg: '正在上传...',
                    success: function(response) {
                        Ext.Msg.alert('提示', '上传成功！', function() {
                            location.href = location.href;
                        });
                    },
                    failure: function(response) {
                        Ext.Msg.alert('提示', '上传失败！', function() {
                            location.href = location.href;
                        });

                    }
                })
            }
        },
        //下载模板
        download: function() {
            window.location.href = "excel/油/评价单元.xls";
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
                   '../../api/month/pjdy.aspx',

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
    // 油藏类型下拉数据
    var yclxStore = Ext.create('Ext.data.Store', {
        proxy: {
            type: 'ajax',
            url: '../../api/system/yclx.aspx',

            extraParams: {
                _type: 'getAllList'
            },

            reader: {
                type: 'json',
                root: 'data'
            }
        },

        fields: ['YCLX'],
        autoLoad: true
    });
    
   
    
    var formPanel = Ext.create('Ext.grid.Panel', {
        store: qkStore,
        region: "center",
        border: false,
        enableColumnHide: false,    //不可隐藏列
        sortableColumns: false,     //不可排序
        columnLines: true,          //表格列竖线
        stripeRows: true,           //隔行换色

        columns: [
                    { xtype: 'rownumberer', width: 40, text: '序号' },
                    { text: '年月', dataIndex: 'NY',width:60 },
                    { text: '区块名称', dataIndex: 'QK',width:160 },
                    { text: '评价单元名称', dataIndex: 'PJDY',width:149}
                ],
         bbar:[
                    { xtype: 'button', text: '添加', iconCls: 'icon-add', handler: function(){windowAddQKDialog.show()} }, 
                     '->',
                    {text: '确定',iconCls: 'icon-ok',handler: events.calcData},
                   '-',
                    {text: '取消',iconCls: 'icon-close',handler: function() { windowSetDialog.hide();}},
                    '-'
                ]
    });

    var windowDialog = Ext.create('Ext.window.Window', {
        title: 'Hello world!',
        height: 260,
        width: 400,
        layout: 'fit',
        modal: true,
        closeAction: 'hide',
        constrain: true,
        resizable: false,
        border: false,
        items: [formPanel]
    });
     // 已选区块
    var qkStore = Ext.create('Ext.data.Store', {
        proxy: {
            type: 'ajax',
            url: '../../api/month/pjdyqk.aspx',

            extraParams: {
                _type: 'getAllList',
                 NY:month
            },

            reader: {
                type: 'json',
                root: 'data'
            }
        },
        fields: ['NY', 'QK','PJDY'],
        autoLoad: true
    });
    // -----------------------------------------------------------------中间grid表格-----------------------------------------
    var sPanel = Ext.create('Ext.form.Panel', {
        frame: false,
        layout: 'table',
        border: false,
        bodyPadding: 5,
        height: 28,
        bodyStyle: 'background-color:#dae7f6',

        defaultType: 'textfield',
        fieldDefaults: {
            msgTarget: 'side',
            labelAlign: 'right',
            labelWidth: 30,
            width: 200,
            margin: '0 0 15 0'
        },
        items: [{ xtype: 'fileuploadfield', buttonText: '浏览', name: 'UPLOADFILE', id: 'UPLOADFILE', margin: '-2 10 18 0', height: 20 },
        { xtype: 'button', text: '导入', iconCls: 'icon-add', handler: events.upload, margin: '-2 5 18 0' }
        ]
    });
    //--------------------------store-----------------------------------------------------------------
    var gridStore = Ext.create('Ext.data.Store', {
        proxy: {
            type: 'ajax',
            url: '../../api/month/pjdy.aspx',

            extraParams: {
                _type: 'getAllList'
            },

            reader: {
                type: 'json',
                root: 'data'
            }
        },

        fields: ['NY', 'PJDYMC', 'SSYT', 'YCLX', 'DYHYMJ', 'DYDZCL',
                'DYKCCL', 'YCZS', 'PJSTL', 'DXYYND', 'YJZJS',
                'YJKJS', 'SJZJS', 'SJKJS', 'ZSJ', 'LJZSL',
                'ZQJZJS', 'ZQJKJZ', 'ZQL', 'LJZQL', 'CYOUL',
                'LJCYOUL', 'CYL', 'LJCYL', 'CQL', 'LJCQL', 'XSYP', 'XSYPMC', 'SFPJ'
        ],

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
            { xtype: 'rownumberer', width: 40, text: '序号' },
            { text: '年月', dataIndex: 'NY' },
            { text: '评价单元名称', dataIndex: 'PJDYMC' },
            { text: '所属油田', dataIndex: 'SSYT' },
            { text: '油藏类型', width: 130, dataIndex: 'YCLX' },
            { text: '动用含油面积', dataIndex: 'DYHYMJ' },
            { text: '动用地质储量', dataIndex: 'DYDZCL' },
            { text: '动用可采储量', dataIndex: 'DYKCCL' },
            { text: '油藏中深', dataIndex: 'YCZS' },
            { text: '平均渗透率', dataIndex: 'PJSTL' },
            { text: '地下原油粘度', dataIndex: 'DXYYND' },
            { text: '油井总井数', dataIndex: 'YJZJS' },
            { text: '油井开井数', dataIndex: 'YJKJS' },
            { text: '注水井总井数', dataIndex: 'SJZJS' },
            { text: '注水井开井数', dataIndex: 'SJKJS' },
            { text: '月注水量', dataIndex: 'ZSJ' },
            { text: '累计注水量', dataIndex: 'LJZSL' },
            { text: '注气井总井数', dataIndex: 'ZQJZJS' },
            { text: '注气井开井数', dataIndex: 'ZQJKJZ' },
            { text: '月注气量', dataIndex: 'ZQL' },
            { text: '累计注气量', dataIndex: 'LJZQL' },
            { text: '月产油量', dataIndex: 'CYOUL' },
            { text: '累计产油量', dataIndex: 'LJCYOUL' },
            { text: '月产液量', dataIndex: 'CYL' },
            { text: '累计产液量', dataIndex: 'LJCYL' },
            { text: '月产气量', dataIndex: 'CQL' },
            { text: '累计产气量', dataIndex: 'LJCQL' },
            { text: '销售油品', dataIndex: 'XSYPMC' },
            { text: '是否评价', dataIndex: 'SFPJ', renderer: function(value) {
                if (value == '0') return '否';
                else if (value == '1') return '是';
            }
            }

        ],

        tbar: [
            '-',
            { xtype: 'button', text: '添加', iconCls: 'icon-add', handler: events.addClick },
            '-',
            { xtype: 'button', text: '删除', iconCls: 'icon-delete', handler: events.deleteClick },
            '-',
            { xtype: 'button', text: '编辑', iconCls: 'icon-edit', handler: events.editClick },
            '-',
            sPanel,
            '-',
            '->',
            { xtype: 'button', text: '下载模板', iconCls: 'icon-save', handler: events.download }
            ]

    });
    Ext.create('Ext.container.Viewport', {
        layout: 'border',
        items: [gridPanel],
        renderTo: Ext.getBody()
    });
});
