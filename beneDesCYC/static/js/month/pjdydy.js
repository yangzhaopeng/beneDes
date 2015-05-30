Ext.Loader.setConfig({ enabled: true });
Ext.Loader.setPath('Ext.ux', '../../static/Extjs/src/ux');
Ext.require([
    'Ext.form.Panel',
    'Ext.ux.form.MultiSelect',
    'Ext.ux.form.ItemSelector',
    'Ext.ux.ajax.SimManager'
]);


Ext.onReady(function() {

    // 对话框显示时，当前状态是否是“添加”角色，或者是“编辑”角色
    var isAdd = true;
    // 当前grid选中的行
    var gridSel;
    var gridQkSel;
    var  PJDYMC;
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
            PJDYMC =gridSel.data["PJDYMC"];
            return true;
        },
        getQkGridSel:function() {
            var qksel = msEditPanel.getSelectionModel().getSelection();

            if (qksel.length < 1) {
                Ext.Msg.alert('提示', '请先选中一个区块！');
                return false;
            }

            gridQkSel = qksel[0];
            return true;
        },
        // “添加”按钮点击事件
        addClick: function() {
            //if (!events.getGridSel()) return;
            isAdd = true;
            windowAddDialog.setTitle('添加评价单元');
            formAddPanel.getForm().reset();
            // formAddPanel.getForm().findField('PJDYMC').setReadOnly(false);

            // 年月字段设为不可修改，并且默认值为登录时带进来的年月
            //formAddPanel.getForm().findField('NY').setReadOnly(true);
            formAddPanel.getForm().setValues({
                NY: month
            });

            windowAddDialog.show();
        },
        
        // “删除”按钮点击事件
        deleteClick: function() {
            if (!events.getGridSel()) return;

            Ext.Msg.confirm('确认', '您确定要删除该评价单元信息？', function(result) {
                if (result == 'yes') {
//                    NY: month
                    dataJs.get(
                        '../../api/month/pjdy.aspx',

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
            var month = gridSel.data['NY'];
            var pjdy  = gridSel.data['PJDYMC'];
            qkstore.load({ 
            params:{pjdy: pjdy,month:month},
            callback:function(records, options, success){
                 windowSetDialog.show();
            }
            });           
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
                    url: '../../api/system/uploadfilepjdy.aspx',
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
//        //下载模板
//        download: function() {
////            window.location.href = "excel/油/评价单元.xls";
//        },
        
        addPJDY: function() {
            var form = formAddPanel.getForm();
            
            if (form.isValid()) {
                var values = form.getValues();
                 values['_type'] = 'add';
                };
           dataJs.get(
                   '../../api/month/pjdy.aspx',
                   values,
                   function(data, msg, response) {
                       windowAddDialog.hide();
                       Ext.Msg.alert('提示', msg, function() {
                           gridStore.reload();
                           //location.href = location.href;
                       });
                   }
               );
            },
          addQK:function(){
            if (!events.getGridSel()) return;
                var month = gridSel.data['NY'];
                var pjdy  = gridSel.data['PJDYMC'];
                jsonstore.load({ 
                params:{pjdy: pjdy,month:month},
                callback:function(records, options, success){
                     windowAddQKDialog.show();
                }
                });
            },
            
            deleteQK:function(){
              if (!events.getQkGridSel('delete')) return;
            var records=msEditPanel.getSelectionModel().getSelection();
            var qks="";
            var len =records.length;
          
            for(var i = 0 ;i<len;i++){
            var data = records[i].data;
                  if (i == len - 1){
                    qks+=data['QK'];
                 }else{
                    qks+=data['QK']+',';
                    }
            }
            Ext.Msg.confirm('确认', '您确定要删除该区块？', function(result) {
                if (result == 'yes') {
                    dataJs.get(
                         '../../api/month/pjdyqk.aspx',
                        {
                            _type: 'delete',
                            NY:month,
                            PJDYMC:PJDYMC,
                            qks:qks
                        },
                        function(data, msg, response) {
                            Ext.Msg.alert('提示', msg, function() {
                                qkstore.reload();
                            });
                        }
                        )
                    }; 
            });
            },
         //添加选中区块到评价单元   
         addQKToPJDY: function() {
            var records=gridQKPanel.getSelectionModel().getSelection();
            var QKS="";
            var len =records.length;
            for(var i = 0 ;i<len;i++){
            var data = records[i].data;
            
           if (i == len - 1){
                    QKS+=data['QKMC'];
            }else{
                    QKS+=data['QKMC']+',';
                    }
                    
            };
            dataJs.get(
            '../../api/month/pjdyqk.aspx',
            {
                _type: 'add',
                NY:month,
                pjdymc:PJDYMC,
                QKS:QKS
            },
            function(data, msg, response) {
                windowAddQKDialog.hide();
                Ext.Msg.alert('提示', msg, function() {
                    windowAddQKDialog.hide();
                    qkstore.reload();
                });
            }
            )
            },
            //计算评价单元信息
            calcData:function(){
                    dataJs.get(
                        '../../api/month/pjdyqk.aspx',
                        {
                            _type: 'calc',
                            NY: gridSel.data['NY'],
                            PJDYMC: gridSel.data['PJDYMC']
                        },

                        function(data, msg, response) {
                            Ext.Msg.alert('提示', msg, function() {
                               gridStore.reload();
                            });
                        }
                    );
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
                           gridStore.reload();
                           //location.href = location.href;
                       });
                   }
               );
            }
        }
    };

    //---------------------------------------------------------数据获取--------------------------------------------
     // 全部区块
    var jsonstore = Ext.create('Ext.data.Store', {
        proxy: {
            type: 'ajax',
            url: '../../api/month/pjdyqk.aspx',

            extraParams: {
                _type: 'getUnSelectQkList',
                 NY:month,
                 PJDYMC:PJDYMC                 
            },

            reader: {
                type: 'json',
                root: 'data'
            }
        },
        fields: ['NY', 'QKMC'],
        autoLoad: true
    });
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

    // 销售油品下拉数据
    var xsypStore = Ext.create('Ext.data.Store', {
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
        // 销售油品下拉数据
    var yxqkStore = Ext.create('Ext.data.Store', {
        proxy: {
            type: 'ajax',
            url: '../../api/system/pjdy.aspx',

            extraParams: {
                _type: 'getQKList',
                NY:month
            },

            reader: {
                type: 'json',
                root: 'data'
            }
        },

        fields: ['QKMC'],
        autoLoad: true
    });
    
    var pjdyStore=Ext.create('Ext.data.Store', {
        proxy: {
            type: 'ajax',
            url: '../../api/month/pjdy.aspx',
            extraParams: {
                _type: 'getAllPJDY',
                NY:month
            },

            reader: {
                type: 'json',
                root: 'data'
            }
        },
        fields: ['NY','PJDY','CYC_ID'],
        autoLoad: true
    });
    
    
    // 是否评价下拉数据
    var sfpjStore = Ext.create('Ext.data.Store', {
        fields: ['SFPJDM', 'SFPJMC'],
        data: [
            { 'SFPJDM': '0', 'SFPJMC': '否' },
            { 'SFPJDM': '1', 'SFPJMC': '是' }
        ]
    });

    var ds = Ext.create('Ext.data.ArrayStore', {
        fields: ['ID', 'PJDY'],
        proxy: {
            type: 'ajax',
            url: 'Numbers1',
            reader: 'array'
        },
        autoLoad: true,
        sortInfo: {
            field: 'PJDY',
            direction: 'ASC'
        }
    });
       
   
   //====================================================创建、编辑对话框=================================================================
      var formAddPanel = Ext.create('Ext.form.Panel', {
        frame: true,
        bodyPadding: 5,
        defaultType: 'textfield',

        fieldDefaults: {
            msgTarget: 'side',
            labelAlign: 'right'
        },
        items: [
        {
            xtype: 'monthfield',
            id: 'month',
            format: 'Ym',
            value: month,
            editable: true,
            fieldLabel: '年月',
            name: 'NY',
            allowBlank: false
        }, {
            fieldLabel: '评价单元名称',
            name: 'PJDYMC',
            allowBlank: false
        },{
                xtype: 'combo',
                fieldLabel: '销售油品',
                name: 'XSYP',
                store: xsypStore,
                displayField: 'XSYPMC',
                valueField: 'XSYPDM',
                queryMode: 'local',
                allowBlank: false
            }
            ],

        buttons: [
            {
                text: '添加',
                iconCls: 'icon-ok',
                handler: events.addPJDY
            }, {
                text: '取消',
                iconCls: 'icon-close',
                handler: function() {
                    windowAddDialog.hide();
                }
            }
        ]
    });
    
  
    
    //添加区块 区块列表
           var gridQKPanel =  new Ext.grid.GridPanel({
//                title: 'GridPanel',
                 width: 250,
                height:300,
                store: jsonstore,
               
                selModel: Ext.create('Ext.selection.CheckboxModel', {   // grid中添加checkbox列
                    mode: 'multi',                                     // 单选
                    showHeaderCheckbox: false                           // 隐藏列顶部的checkbox
                    }),
                    
                columns: [
                    { xtype: 'rownumberer', width: 40, text: '序号' },
                    { text: '年月', dataIndex: 'NY',width:60 },
                    { text: '区块名称', dataIndex: 'QKMC',width:180 }
                ],
                bbar:[
                    '->',
                    { xtype: 'button', text: '提交', iconCls: 'icon-add', handler: events.addQKToPJDY },
                    '-'
                ]
                });
  
//========================================================================================================
    //列表
    var grid = Ext.create('Ext.grid.Panel', {
        store: gridStore,
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

        ]

        //        , tbar: [
        //            '-',
        //            { xtype: 'button', text: '添加', iconCls: 'icon-add', handler: events.addClick },
        //            '-',
        //            { xtype: 'button', text: '删除', iconCls: 'icon-delete', handler: events.deleteClick },
        //            '-',
        //            { xtype: 'button', text: '编辑', iconCls: 'icon-edit', handler: events.editClick },
        //            '-',
        //            sPanel,
        //            '-',
        //            '->',
        //            { xtype: 'button', text: '下载模板', iconCls: 'icon-save', handler: events.download }
        //            ] 

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
                    width: 250,
                    margin: '7 0 0 0'
                },

                items: [
           {
               fieldLabel: '年月',
               name: 'NY',
               readOnlyCls: 'readOnly',
               allowBlank: false,
               readOnly: true
           }, {
               fieldLabel: '评价单元名称',
               name: 'PJDYMC',
               allowBlank: false
           }, {
               fieldLabel: '所属油田',
               name: 'SSYT',
               allowBlank: true
           }, {
               xtype: 'combo',
               fieldLabel: '油藏类型',
               name: 'YCLX',
               store: yclxStore,
               displayField: 'YCLX',
               valueField: 'YCLX',
               queryMode: 'local',
               allowBlank: true
           }, {
               fieldLabel: '动用含油面积（平方千米）',
               name: 'DYHYMJ',
               allowBlank: true
           }, {
               fieldLabel: '动用地质储量（万吨）',
               name: 'DYDZCL',
               allowBlank: true
           }, {
               fieldLabel: '动用可采储量（万吨）',
               name: 'DYKCCL',
               allowBlank: false
           }, {
               fieldLabel: '油藏中深（米）',
               name: 'YCZS',
               allowBlank: false
           },
           {
               fieldLabel: '平均渗透率（毫达西）',
               name: 'PJSTL',
               allowBlank: true
           },
           {
               fieldLabel: '地下原油粘度（毫帕秒）',
               name: 'DXYYND',
               allowBlank: true
           },
           {
               fieldLabel: '油井总井数（口）',
               name: 'YJZJS',
               allowBlank: true
           },
           {
               fieldLabel: '油井开井数（口）',
               name: 'YJKJS',
               allowBlank: true
           },
            {
                fieldLabel: '注水井总井数（口）',
                name: 'SJZJS',
                allowBlank: true
            },
            {
                fieldLabel: '注水井开井数（口）',
                name: 'SJKJS',
                allowBlank: true
            },
            {
                fieldLabel: '月注水量（万方）',
                name: 'ZSJ',
                allowBlank: true
            },
            {
                fieldLabel: '累计注水量（万方）',
                name: 'LJZSL',
                allowBlank: true
            },
            {
                fieldLabel: '注气井总井数（口）',
                name: 'ZQJZJS',
                allowBlank: true
            },
            {
                fieldLabel: '注气井开井数（口）',
                name: 'ZQJKJZ',
                allowBlank: true
            },
            {
                fieldLabel: '月注气量（万方）',
                name: 'ZQL',
                allowBlank: true
            },
            {
                fieldLabel: '累计注气量（万方）',
                name: 'LJZQL',
                allowBlank: true
            },
            {
                fieldLabel: '月产油量（万吨）',
                name: 'CYOUL',
                allowBlank: true
            },
            {
                fieldLabel: '累计产油量（万吨）',
                name: 'LJCYOUL',
                allowBlank: true
            },
            {
                fieldLabel: '月产液量（万吨）',
                name: 'CYL',
                allowBlank: true
            },
            {
                fieldLabel: '累计产液量（万吨）',
                name: 'LJCYL',
                allowBlank: true
            },
            {
                fieldLabel: '月产气量（万方）',
                name: 'CQL',
                allowBlank: true
            },
            {
                fieldLabel: '累计产气量（万方）',
                name: 'LJCQL',
                allowBlank: true
            }, {
                xtype: 'combo',
                fieldLabel: '销售油品',
                name: 'XSYP',
                store: xsypStore,
                displayField: 'XSYPMC',
                valueField: 'XSYPDM',
                queryMode: 'local',
                allowBlank: false
            },
            {
                xtype: 'combo',
                fieldLabel: '是否评价',
                name: 'SFPJ',
                store: sfpjStore,
                displayField: 'SFPJMC',
                valueField: 'SFPJDM',
                queryMode: 'local',
                allowBlank: false
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
            
               // 已选区块
            var qkstore = Ext.create('Ext.data.Store', {
                proxy: {
                    type: 'ajax',
                    url: '../../api/month/pjdyqk.aspx',

                    extraParams: {
                        _type: 'getAllList'
                    },

                    reader: {
                        type: 'json',
                        root: 'data'
                    }
                },

                fields: ['NY', 'PJDY', 'QK'],

                autoLoad: true
            });
            
            //编辑
          var msEditPanel =new Ext.grid.GridPanel({
              width: 280,
              height:300,
              store: qkstore,
              border: false,
              enableColumnHide: false,    //不可隐藏列
              sortableColumns: false,     //不可排序
              columnLines: true,          //表格列竖线
              stripeRows: true,           //隔行换色
               selModel: Ext.create('Ext.selection.CheckboxModel', {   // grid中添加checkbox列
                    mode: 'multi',                                     // 单选
                    showHeaderCheckbox: false                           // 隐藏列顶部的checkbox
                    }),
              columns: [
                  { xtype: 'rownumberer', width: 40, text: '序号' },
                  { text: '年月', dataIndex: 'NY',width:60 },
                  { text: '区块名称', dataIndex: 'QK',width:160 },
                  { text: '评价单元名称',hidden:true, dataIndex: 'PJDY',width:149}
              ],
              bbar:[
                  '-',
                  { xtype: 'button', text: '添加', iconCls: 'icon-add', handler: events.addQK }, 
                  '-',
                  { xtype: 'button', text: '删除', iconCls: 'icon-delete', handler: events.deleteQK }, 
                   '->',
                  {text: '计算',iconCls: 'icon-ok',handler: events.calcData},
                 '-',
                  {text: '取消',iconCls: 'icon-close',handler: function() { windowSetDialog.hide();}},
                  '-'
              ]
              
            });  
    
            var windowAddDialog = Ext.create('Ext.window.Window', {
                title: 'Hello world!',
//                      height: 460,
//                      width: 800,
                layout: 'fit',
                modal: true,
                closeAction: 'hide',
                constrain: true,
                resizable: false,
                border: false,
                items: [formAddPanel]
            });
            var windowSetDialog = Ext.create('Ext.window.Window', {
                title: '已选区块',
                layout: 'fit',
                modal: true,
                closeAction: 'hide',
                constrain: true,
                resizable: false,
                border: false,
                items: [msEditPanel]
            });
            var windowAddQKDialog = Ext.create('Ext.window.Window', {
                title: '区块列表',
//               height: 260,
                width: 320,
                layout: 'fit',
                modal: true,
                closeAction: 'hide',
                constrain: true,
                resizable: true,
                border: false,
                items: [gridQKPanel]
            });
            var windowDialog = Ext.create('Ext.window.Window', {
                title: 'Hello world!',
                height: 460,
                width: 800,
                layout: 'fit',
                modal: true,
                closeAction: 'hide',
                constrain: true,
                resizable: false,
                border: false,
                items: [formPanel]
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
  
            
            
            var gridWestPanel = Ext.create('Ext.grid.Panel', {
                store: gridStore,
               region: "west",
                width:'30%',  
                split:true,
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
            { text: '评价单元名称', dataIndex: 'PJDYMC' }
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
            var gridCenterPanel = Ext.create('Ext.grid.Panel', {
                store: gridStore,
               region: "center",
                width:'30%',  
               
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
            { text: '评价单元名称', dataIndex: 'PJDYMC' }
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
             var gridEastPanel = Ext.create('Ext.grid.Panel', {
                store: gridStore,
                region: "east",
                width:'30%',  
                split:true,
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
            { text: '评价单元名称', dataIndex: 'PJDYMC' }
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
               layout:'border',  
                items:[gridWestPanel,gridCenterPanel,gridEastPanel]  
            });
        });
        // -----------------------------------------------------------右侧井组 tree-------------------------------------------------
