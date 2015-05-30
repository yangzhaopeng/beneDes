/*
*开采方式相关参数
*/
Ext.onReady(function() {

    // 对话框显示时，当前状态是否是“添加”，或者是“编辑”
    var isAdd = true;
    // 当前grid选中的行
    var gridSel;
    var pjdy;

    //--------------------------------------------------------------------------事件集中处理-------------------------------
    var events = {

        // 获取当前grid选中的行
        getGridSel: function() {
            var sel = gridWestPanel.getSelectionModel().getSelection();

            if (sel.length < 1) {
                Ext.Msg.alert('提示', '请先选中一行记录！');
                return false;
            }

            gridSel = sel[0];

            return true;
        },
        
        // “添加”按钮点击事件
        addClick: function() {
            var cyc = Ext.getCmp('CYC').getValue();
            if(cyc=='全选'){
                 Ext.Msg.alert('提示', '请选择一个单位！');
                return false;
            }
            isAdd = true;
             formPanel.getForm().reset();
             formPanel.getForm().setValues({
                CYC_ID: cyc
            });
            windowDialog.setTitle('添加评价单元');
        
            // 年月字段设为不可修改，并且默认值为登录时带进来的年月            windowDialog.show();
        },

        // “删除”按钮点击事件
        deleteClick: function() {
            if (!events.getGridSel()) return;

            Ext.Msg.confirm('确认', '您确定要删除该评价单元？', function(result) {
                if (result == 'yes') {
                    NY: month
                    dataJs.get(
                        '../../api/month/pjdydy.aspx',

                        {
                            _type: 'delete',
                            ID:gridSel.data['ID']
                        },

                        function(data, msg, response) {
                             gridWestStore.reload();
                        }
                    );
                }
            });
        },
        // “编辑”按钮点击事件
        editClick: function() {
            if (!events.getGridSel()) return;
             isAdd = false;
             
//            var cyc = Ext.getCmp('CYC').getValue();
            windowDialog.setTitle('编辑评价单元');
            formPanel.getForm().reset();
            formPanel.getForm().setValues({
                ID:gridSel.data['ID'],
                PJDYMC:gridSel.data['PJDYMC']
            });

            windowDialog.show();
        },
        //选择 单位 
       comboSelect: function() {
            // 检索的参数通过url的参数来传递给子页面            var cyc = Ext.getCmp('CYC').getValue();
            if (cyc == null) {
                Ext.getCmp('CYC').setValue('[a-zA-Z]');
                cyc = Ext.getCmp('CYC').getValue();
          }
          
            gridWestStore.load({ 
                params:{cyc_id: cyc},
                callback:function(records, options, success){
                    
                }
            });
            gridQKStore.load({ 
                params:{cyc_id: cyc},
                callback:function(records, options, success){
                    
                }
            });
        },
        //选择区块
         comboQKSelect: function() {
            // 检索的参数通过url的参数来传递给子页面            var QKID = Ext.getCmp('QKMC').getValue();
            if (QKID == null) {
                Ext.getCmp('QKMC').setValue('[a-zA-Z]');
                QKMC = Ext.getCmp('QKMC').getValue();
          }
          
            gridQkWellStore.load({ 
                params:{QKID: QKID,pjdy:pjdy},
                callback:function(records, options, success){
                   var selModel  = gridCenterPanel.selModel;
                   if(records.length>0){ 
                       for(var i=0 ;i<=records.length;i++){
                            if(records[i]!=undefined){
                                var pjdy = records[i].data['PJDY'];
                                if(pjdy){
                                    selModel.select(i,true);
                                }
                            }
                       }
                   }
                }
            });
        },
        //评价单元Panel点击事件
        comboPjdySelect: function(obj) {
        pjdy = obj.lastSelected.data["ID"];
        var cyc = obj.lastSelected.data["CYC_ID"];
            // 检索的参数通过url的参数来传递给子页面            gridPjdyWellStore.load({ 
                params:{pjdy: pjdy},
                callback:function(records, options, success){
                    
                }
            });
            gridQKStore.load({ 
                params:{cyc_id: cyc},
                callback:function(records, options, success){
                    
                }
            });
            
        },
        updateWell:function(){
            var selWell = gridCenterPanel.getSelectionModel().getSelection();
            if (selWell.length < 1) {
                Ext.Msg.alert('提示', '请先选中一行记录！');
                return false;
            };
            var param=new Object();
            param['_type']="update";
            param['PJDY']=pjdy;
            param['QK']= Ext.getCmp('QKMC').getValue();;
            param['IDS']="";
            for(i=0;i<selWell.length;i++){
                param['IDS']+=selWell[i].data['ID']+',';
            }
            
            
           dataJs.post(
             '../../api/month/pc_well.aspx',
             param,
             function(data,msg,response){
                   Ext.Msg.alert('提示',msg,function(){
                       gridQkWellStore.reload();
                       gridPjdyWellStore.reload();
                   })
             })
        },
        // 弹出对话框的“确定”按钮点击事件        addOrEdit: function() {
            var form = formPanel.getForm();
           
            if (form.isValid()) {
                var values = form.getValues();
                values['_type'] = 'add';
                if (!isAdd) {
                    values['_type'] = 'edit';
                }

                dataJs.get(
                   '../../api/month/pjdydy.aspx',
                   values,
                   function(data, msg, response) {
                       windowDialog.hide();
                       gridWestStore.reload();
                   }
               );
            }
        }
    };

    //---------------------------------------------------------创建、编辑对话框--------------------------------------------
    
    
    
    
    
    
    
    var formPanel = Ext.create('Ext.form.Panel', {
        frame: true,
        bodyPadding: 5,
        defaultType: 'textfield',

        fieldDefaults: {
            msgTarget: 'side',
            labelAlign: 'right',
            labelWidth: 100,
            width:50
        },

        items: [
           {
               fieldLabel: '评价单元名称',
               name: 'PJDYMC',
               width:'40px',
               allowBlank: false
           },{
               fieldLabel: '单位名称',
               name: 'CYC_ID',
               width:'40px',
               allowBlank: true
              ,hidden:true
           },{
               fieldLabel: 'ID',
               name: 'ID',
               width:'40px',
               allowBlank: true
              ,hidden:true
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
   //加载单位combobox下拉列表
   var departmentStore = Ext.create('Ext.data.Store', {
    proxy: {
    type: 'ajax',
    url: '../../api/system/department.aspx',
            
    extraParams: {
    _type: 'getDepList'
    },
            
    reader: {
    type: 'json',
    root: 'data'
    }
    },
        
    fields:['DEP_ID', 'DEP_NAME' ],
    autoLoad: true
    });
    //加载 选定单位下 评价单元列表
    var gridWestStore = Ext.create('Ext.data.Store', {
        proxy: {
            type: 'ajax',
            url: '../../api/month/pjdydy.aspx',

            extraParams: {
                _type: 'getPjdyList',
                cyc_id:'[a-zA-Z]'
            },

            reader: {
                type: 'json',
                root: 'data'
            }
        },

        fields: ['PJDYMC','ID','CYC_ID','DEP_NAME'],
        autoLoad: true
    });
    //加载 QK combobox 下拉菜单
     var gridQKStore = Ext.create('Ext.data.Store', {
        proxy: {
            type: 'ajax',
            url: '../../api/month/qkdy.aspx',

            extraParams: {
                _type: 'getQKList'
            },

            reader: {
                type: 'json',
                root: 'data'
            }
        },

        fields: ['ID','QKMC'],
        autoLoad: false
    });
    //加载区块及井所属关系表 以及   已选择单井列表
     var gridQkWellStore = Ext.create('Ext.data.Store', {
        proxy: {
            type: 'ajax',
            url: '../../api/month/pc_well.aspx',

            extraParams: {
                _type: 'getWellList'
            },

            reader: {
                type: 'json',
                root: 'data'
            }
        },

        fields: ['PJDY','ID','QK','QKMC','JH'],
        autoLoad: false
    });
    //加载区块及井所属关系表 以及   已选择单井列表
     var gridPjdyWellStore = Ext.create('Ext.data.Store', {
        proxy: {
            type: 'ajax',
            url: '../../api/month/pc_well.aspx',

            extraParams: {
                _type: 'getPjdyWellList'
            },

            reader: {
                type: 'json',
                root: 'data'
            }
        },

        fields: ['PJDY','ID','QK','JH','QKMC'],
        autoLoad: false
    });
    //加载 评价单元列表
   var gridWestPanel = Ext.create('Ext.grid.Panel', {
                store: gridWestStore,
               region: "west",
                width:'40%',  
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
            { text: '所属单位', dataIndex: 'DEP_NAME' },
            { text: '单位ID', dataIndex: 'CYC_ID',hidden:true },
            { text: '评价单元名称', dataIndex: 'PJDYMC' },
            { text: '唯一标识', dataIndex: 'ID' ,hidden:true}
          ],  listeners:{
                'select': events.comboPjdySelect
            },

                tbar: [
            '-', { xtype: 'combo', fieldLabel: '单位选择', name: 'CYC', id: 'CYC', value: '全选',lableWidth:30, labelAlign: 'right',width:220, store: departmentStore, displayField: 'DEP_NAME', valueField: 'DEP_ID', queryMode: 'local',allowBlank: false ,  
            listeners:{
                "select": events.comboSelect
            }},
            { xtype: 'button', text: '添加', iconCls: 'icon-add', handler: events.addClick },
            '-',
            { xtype: 'button', text: '删除', iconCls: 'icon-delete', handler: events.deleteClick },
            '-',
            { xtype: 'button', text: '编辑', iconCls: 'icon-edit', handler: events.editClick },
            '-',
           
            '-'
            ]
            });
            
      var gridCenterPanel = Ext.create('Ext.grid.Panel', {
                store: gridQkWellStore,
                region: "center",
                width:'40%',  
               
                border: false,
                enableColumnHide: false,    //不可隐藏列
                sortableColumns: false,     //不可排序
                columnLines: true,          //表格列竖线
                stripeRows: true,           //隔行换色

                selModel: Ext.create('Ext.selection.CheckboxModel', {   // grid中添加checkbox列
                    mode: 'multi',                                     // 多选
                    showHeaderCheckbox: false,                         // 隐藏列顶部的checkbox
                    checkOnly:false
//                    , renderer: function(value) {
//                            alert(pjdy);
//                        }
                }),

                columns: [
            { xtype: 'rownumberer', text: '序号',width:40 },
            { text: '区块名称', dataIndex: 'QKMC' },
            { text: '井名称', dataIndex: 'JH' },
            { text: 'ID', dataIndex: 'ID',hidden:true }
          ],

                tbar: [
              '-',
            { xtype: 'combo', fieldLabel: '区块选择', name: 'QKMC', id: 'QKMC', value: '', labelAlign: 'right',width:240, store: gridQKStore, displayField: 'QKMC', valueField: 'ID', queryMode: 'local',     listeners:{
                "select": events.comboQKSelect
            }
            },
             '-',
            { xtype: 'button', text: '提交', iconCls: 'icon-ok' , handler: events.updateWell },
            '-',
            { xtype: 'checkbox', name:'checkboxgroup',  id: 'checkboxgroup', fieldLabel: '全选'  , 
                handler: function(){
                   var selModel  = gridCenterPanel.selModel;
                   if(this.checked){
                        selModel.selectAll()
                   }else{
                        
                   }
            }},
            '-'
            ]
            });
          var gridEastPanel = Ext.create('Ext.grid.Panel', {
                store: gridPjdyWellStore,
                region: "east",
                width:'20%',  
                split:true,
                enableColumnHide: false,    //不可隐藏列
                sortableColumns: false,     //不可排序
                columnLines: true,          //表格列竖线
                stripeRows: true,           //隔行换色

//                selModel: Ext.create('Ext.selection.CheckboxModel', {   // grid中添加checkbox列
//                    mode: 'single',                                     // 单选
//                    showHeaderCheckbox: false                           // 隐藏列顶部的checkbox
//                }),

                columns: [
            { xtype: 'rownumberer', width: 40, text: '序号' },
            { text: '区块名称', dataIndex: 'QKMC' },
            { text: '井名称', dataIndex: 'JH' },
            { text: 'ID', dataIndex: 'ID',hidden:true }
          ],

                tbar: [
            '-',
             { xtype: 'label', text: '已选择井列表', margin: '0 0 0 20' }
            ]
            });
            Ext.create('Ext.container.Viewport', {
               layout:'border',  
                items:[gridWestPanel,gridCenterPanel,gridEastPanel]  
            });
});