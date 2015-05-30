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
            windowDialog.setTitle('添加区块信息');
            formPanel.getForm().reset();
            formPanel.getForm().findField('QKMC').setReadOnly(false);

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

            Ext.Msg.confirm('确认', '您确定要删除该区块信息？', function(result) {
                if (result == 'yes') {
                    NY: month
                    dataJs.get(
                        '../../api/month/qkxx.aspx',

                        {
                            _type: 'delete',
                            NY: gridSel.data['NY'],
                            // DEP_ID: gridSel.data['DEP_ID'],
                            QKMC: gridSel.data['QKMC']
                        },

                        function(data, msg, response) {
                            Ext.Msg.alert('提示', msg, function() {
                                gridStore.reload();//location.href = location.href;
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
            windowDialog.setTitle('编辑区块信息');

            formPanel.getForm().setValues({
                NY: gridSel.data['NY'],
                QKMC: gridSel.data['QKMC'],
                SSYT: gridSel.data['SSYT'],
                YCLX: gridSel.data['YCLX'],
                DYHYMJ: gridSel.data['DYHYMJ'],
                DYDZCL: gridSel.data['DYDZCL'],
                DYKCCL: gridSel.data['DYKCCL'],
                YCZS: gridSel.data['YCZS'],
                PJSTL: gridSel.data['PJSTL'],
                DXYYND: gridSel.data['DXYYND'],
                YJZJS: gridSel.data['YJZJS'],
                YJKJS: gridSel.data['YJKJS'],
                SJZJS: gridSel.data['SJZJS'],
                SJKJS: gridSel.data['SJKJS'],
                ZSJ: gridSel.data['ZSJ'],
                LJZSL: gridSel.data['LJZSL'],
                ZQJZJS: gridSel.data['ZQJZJS'],
                ZQJKJZ: gridSel.data['ZQJKJZ'],
                ZQL: gridSel.data['ZQL'],
                LJZQL: gridSel.data['LJZQL'],
                CYOUL: gridSel.data['CYOUL'],
                LJCYOUL: gridSel.data['LJCYOUL'],
                CYL: gridSel.data['CYL'],
                LJCYL: gridSel.data['LJCYL'],
                CQL: gridSel.data['CQL'],
                LJCQL: gridSel.data['LJCQL'],
                XSYP: gridSel.data['XSYP'],

                SFPJ: gridSel.data['SFPJ'],
                CYC_ID: gridSel.data['CYC_ID']
            });

            // 只读
            formPanel.getForm().findField('NY').setReadOnly(true);
            // formPanel.getForm().findField('DEP_ID').setReadOnly(true);
            formPanel.getForm().findField('QKMC').setReadOnly(true);

            windowDialog.show();
        },

        //上传文件按钮
        upload: function() {
            var form = sPanel.getForm();
            var file = Ext.getCmp("UPLOADFILE").getValue();
            if (file == "") {
//                Ext.Msg.alert('提示', '请选择文件！');
                return false;
            }
            if (form.isValid()) {

                form.submit({
                    url: '../../api/system/uploadfileqk.aspx',
                    waitMsg: '正在上传...',
                    success: function(msg,response) {
                          var resJson = response.response.responseText;
                           var msg = Ext.decode(resJson?resJson:'{}').msg;
                           Ext.Msg.alert('提示', msg.replace(/\n/g, "<br />"), function() {
                            gridStore.reload();
                        });
                    },
                    failure: function(msg,response) {
                           var resJson = response.response.responseText;
                           var msg = Ext.decode(resJson?resJson:'{}').msg;
                           Ext.Msg.show({
                              title: '提示信息',
                              msg: msg.replace(/\n/g, "<br />"),
                              minWidth: 400,//可以改
                              maxWidth: 600,//可以改
                              buttons: Ext.Msg.YES,
                              fn: gridStore.reload(),
                              icon: Ext.MessageBox.INFO
                            });
                        }
                })
            }
        },
        //下载模板
        download: function() {
            window.location.href = "../../static/dataTemplateExcel/oil/7区块信息.xls";
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
                   '../../api/month/qkxx.aspx',

                   values,

                   function(data, msg, response) {
                       windowDialog.hide();
                       Ext.Msg.alert('提示', msg, function() {
                           gridStore.reload();//location.href = location.href;
                       });
                   }
               );
            }
        }
    };

    //---------------------------------------------------------创建、编辑对话框--------------------------------------------
    // 井别下拉数据
    /*  var jbStore = Ext.create('Ext.data.Store', {
    proxy: {
    type: 'ajax',
    url: '../../api/system/jb_info.aspx',
            
    extraParams: {
    _type: 'getAllList'
    },
            
    reader: {
    type: 'json',
    root: 'data'
    }
    },
        
    fields:['JBH', 'JBNAME'],
    autoLoad: true
    });
    */
    // 作业区下拉数据
    /*  var departmentStore = Ext.create('Ext.data.Store', {
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
        
    fields:['DEP_ID', 'DEP_NAME'],
    autoLoad: true
    });
    */
    // 油气类型下拉数据
    /*    var yqlxStore = Ext.create('Ext.data.Store', {
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
        
    fields:['YQLXDM', 'YQLXMC'],
    autoLoad: true
    });
    */
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

    // 是否评价下拉数据
    var sfpjStore = Ext.create('Ext.data.Store', {
        fields: ['SFPJDM', 'SFPJMC'],
        data: [
            { 'SFPJDM': '0', 'SFPJMC': '否' },
            { 'SFPJDM': '1', 'SFPJMC': '是' }
        ]
    });

    var formPanel = Ext.create('Ext.form.Panel', {
        frame: true,
        bodyPadding: 5,
        defaultType: 'textfield',
        layout: 'column',
        autoScroll:true,
        
        fieldDefaults: {
            msgTarget: 'side',
            labelAlign: 'right',
            labelWidth: 135,
//            width: 180,
            margin: '7 0 0 0'
        },

        items: [
           {
                 xtype: 'monthfield',
               fieldLabel: '年月',
               name: 'NY',
               readOnlyCls: 'readOnly',
               allowBlank: false
           },
           {
               fieldLabel: '区块名称',
               name: 'QKMC',
               allowBlank: false
           },

        /*    {
        xtype: 'combo',
        fieldLabel: '井别',
        name: 'JB',
        store: jbStore,
        displayField: 'JBNAME',
        valueField: 'JBH',
        queryMode: 'local',
        allowBlank: true
        },
        */
        /*   {
        fieldLabel: '投产日期',
        name: 'TCRQ',
        allowBlank: true
        },
        */
           {
           fieldLabel: '所属油田',
           name: 'SSYT',
           allowBlank: true
       },
        /*     {
        fieldLabel: '评价单元',
        name: 'PJDY',
        allowBlank: true
        },
        */
           {
           xtype: 'combo',
           fieldLabel: '油藏类型',
           name: 'YCLX',
           store: yclxStore,
           displayField: 'YCLX',
           valueField: 'YCLX',
           queryMode: 'local',
           allowBlank: true
       },
        /*  {
        xtype: 'combo',
        fieldLabel: '作业区',
        name: 'ZYQ',
        store: departmentStore,
        displayField: 'DEP_NAME',
        valueField: 'DEP_ID',
        queryMode: 'local',
        allowBlank: false
        },
        */
           {
           fieldLabel: '动用含油面积(平方千米)',
           name: 'DYHYMJ',
           allowBlank: true
       },
           {
               fieldLabel: '动用地质储量(万吨)',
               name: 'DYDZCL',
               allowBlank: true
           },
           {
               fieldLabel: '动用可采储量(万吨)',
               name: 'DYKCCL',
               allowBlank: false
           },
           {
               fieldLabel: '油藏中深(米)',
               name: 'YCZS',
               allowBlank: false
           },
           {
               fieldLabel: '平均渗透率(毫达西)',
               name: 'PJSTL',
               allowBlank: true
           },
           {
               fieldLabel: '地下原油粘度(毫帕秒)',
               name: 'DXYYND',
               allowBlank: true
           },
           {
               fieldLabel: '油井总井数(口)',
               name: 'YJZJS',
               allowBlank: true
           },
           {
               fieldLabel: '油井开井数(口)',
               name: 'YJKJS',
               allowBlank: true
           },
            {
                fieldLabel: '注水井总井数(口)',
                name: 'SJZJS',
                allowBlank: true
            },
            {
                fieldLabel: '注水井开井数(口)',
                name: 'SJKJS',
                allowBlank: true
            },
            {
                fieldLabel: '月注水量(万方)',
                name: 'ZSJ',
                allowBlank: true
            },
            {
                fieldLabel: '累计注水量(万方)',
                name: 'LJZSL',
                allowBlank: true
            },
            {
                fieldLabel: '注气井总井数(口)',
                name: 'ZQJZJS',
                allowBlank: true
            },
            {
                fieldLabel: '注气井开井数(口)',
                name: 'ZQJKJZ',
                allowBlank: true
            },
            {
                fieldLabel: '月注气量(万方)',
                name: 'ZQL',
                allowBlank: true
            },
            {
                fieldLabel: '累计注气量(万方)',
                name: 'LJZQL',
                allowBlank: true
            },
            {
                fieldLabel: '月产油量(万吨)',
                name: 'CYOUL',
                allowBlank: true
            },
            {
                fieldLabel: '累计产油量(万吨)',
                name: 'LJCYOUL',
                allowBlank: true
            },
            {
                fieldLabel: '月产液量(万吨)',
                name: 'CYL',
                allowBlank: true
            },
            {
                fieldLabel: '累计产液量(万吨)',
                name: 'LJCYL',
                allowBlank: true
            },
            {
                fieldLabel: '月产气量(万方)',
                name: 'CQL',
                allowBlank: true
            },
            {
                fieldLabel: '累计产气量(亿方)',
                name: 'LJCQL',
                allowBlank: true
            },{
               xtype: 'combo',
               fieldLabel: '销售油品',
               name: 'XSYP',
               store: xsypStore,
               displayField: 'XSYPMC',
               valueField: 'XSYPDM',
               queryMode: 'local',
               allowBlank: false
           },{
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

    var windowDialog = Ext.create('Ext.window.Window', {
        title: 'Hello world!',
        height: 460,
        width: 600,
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
        items: [{ xtype: 'fileuploadfield', buttonText: '导&nbsp入', name: 'UPLOADFILE',buttonOnly:true, id: 'UPLOADFILE', 
        margin: '-2 10 18 0', height: 20 ,   listeners:{
                        change:events.upload
                }}
        ]
    });
    var gridStore = Ext.create('Ext.data.Store', {
        proxy: {
            type: 'ajax',
            url: '../../api/month/qkxx.aspx',

            extraParams: {
                _type: 'getAllList',
                NY:month
            },

            reader: {
                type: 'json',
                root: 'data'
            }
        },

        fields: ['NY', 'QKMC', 'SSYT', 'YCLX', 'DYHYMJ', 'DYDZCL',
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
//        forceFit: true,            //自适应列宽

        selModel: Ext.create('Ext.selection.CheckboxModel', {   // grid中添加checkbox列
            mode: 'single',                                     // 单选
            showHeaderCheckbox: false                           // 隐藏列顶部的checkbox
        }),

        columns: [
            { xtype: 'rownumberer', width: 40, text: '序号' },
            { text: '年月', dataIndex: 'NY' },
            { text: '区块名称', dataIndex: 'QKMC' },
            { text: '所属油田', dataIndex: 'SSYT' },
            { text: '油藏类型', width: 230, dataIndex: 'YCLX' },
            { text: '动用含油面积(平方千米)', dataIndex: 'DYHYMJ',width:200 },
            { text: '动用地质储量(万吨)', dataIndex: 'DYDZCL' ,width:180},
            { text: '动用可采储量(万吨)', dataIndex: 'DYKCCL' ,width:180},
            { text: '油藏中深（米）', dataIndex: 'YCZS',width:150 },
            { text: '平均渗透率（毫达西）', dataIndex: 'PJSTL',width:200 },
            { text: '地下原油粘度（毫帕秒）', dataIndex: 'DXYYND' ,width:200},
            { text: '油井总井数（口）', dataIndex: 'YJZJS',width:180 },
            { text: '油井开井数（口）', dataIndex: 'YJKJS' ,width:180 },
            { text: '注水井总井数（口）', dataIndex: 'SJZJS' ,width:200 },
            { text: '注水井开井数（口）', dataIndex: 'SJKJS'  ,width:200},
            { text: '月注水量（口）', dataIndex: 'ZSJ' ,width:180 },
            { text: '累计注水量（口）', dataIndex: 'LJZSL' ,width:180 },
            { text: '注气井总井数（口）', dataIndex: 'ZQJZJS' ,width:200 },
            { text: '注气井开井数（口）', dataIndex: 'ZQJKJZ' ,width:200 },
            { text: '月注气量（万方）', dataIndex: 'ZQL' ,width:180 },
            { text: '累计注气量（万方）', dataIndex: 'LJZQL'  ,width:180},
            { text: '月产油量（万吨）', dataIndex: 'CYOUL'  ,width:180},
            { text: '累计产油量（万吨）', dataIndex: 'LJCYOUL' ,width:180 },
            { text: '月产液量（万吨）', dataIndex: 'CYL'  ,width:180},
            { text: '累计产液量（万吨）', dataIndex: 'LJCYL' ,width:180 },
            { text: '月产气量（万方）', dataIndex: 'CQL'  ,width:180},
            { text: '累计产气量（亿方）', dataIndex: 'LJCQL'  ,width:180},
            { text: '销售油品', dataIndex: 'XSYPMC'},
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
// -----------------------------------------------------------右侧井组 tree-------------------------------------------------
/*   var treeStore = Ext.create('Ext.data.TreeStore');
    
var treePanel = Ext.create('Ext.tree.Panel', {
title: '可通过点击树节点进行井的筛选',
store: treeStore,
rootVisible: true,
height: '100%',
width: 240,
margin: '0 0 0 0',
autoScroll: true,
border: false,
lines: false,
singleExpand: false,
useArrows: true,
region: 'east',
listeners: {
'itemclick': events.treeNodeClick
}
});
    
// 一次load所有tree数据，并渲染页面
dataJs.get(
'../../api/month/djsj.aspx', 
        
{
_type: 'getZRZTree'
},
        
function(data, msg, response) {
Ext.create('Ext.container.Viewport', {
layout: 'border',
items: [gridPanel, treePanel],
renderTo: Ext.getBody()
});

treeStore.setRootNode(data);
            
treeStore.getRootNode().expandChildren();
            
window.top.sss = gridStore;
}
);
*/   
