Ext.onReady(function() {
    Loading.show();

    //--------------------------------------------------------------------------事件集中处理-------------------------------
    var events = {
        // 初始化
        search: function() {

            // 检索的参数通过url的参数来传递给子页面

        var month = Ext.getCmp('month').getValue();
         var Dropdl = Ext.getCmp('Dropdl').getValue();
         
        document.getElementById('center').setAttribute('src', iframeUrl + '?month=' + month + '&Dropdl=' + Dropdl );
            // loading的实现
            Loading.onIfrmaLoad('center');
        },
        
         upload: function() {
         
           var month = Ext.getCmp('month').getValue();
//         var Dropdl = Ext.getCmp('Dropdl').getValue();
         
//        document.getElementById('center').setAttribute('src', iframeUrl + '?month=' + month + '&Dropdl=' + Dropdl );
//            // loading的实现
//            Loading.onIfrmaLoad('center');
            
            
         
            var form = sPanel.getForm();
            var file = Ext.getCmp("UPLOADFILE").getValue();
            if (file == "") {
                Ext.Msg.alert('提示', '请选择文件！');
                return false;
            }
            if (form.isValid()) {

                form.submit({
                    url: '../../api/system/drsjjc.aspx',
                    waitMsg: '正在上传...',
                    success: function(response) {
                        Ext.Msg.alert('提示', '导入成功！',function() {
                            location.href = location.href;
                        });
                    },
                    failure: function(response) {
                        Ext.Msg.alert('提示', '导入失败！', function() {
                            location.href = location.href;
                        });

                    }
                })
            }
        },

//        // 全选
//        allSelect: function() {
//            // 触发iframe中隐藏的button按钮的click事件
//            document.getElementById('center').contentDocument.getElementById("QX").click();
//        },

//        // 取消
//        cancel: function() {
//            // 触发iframe中隐藏的button按钮的click事件
//            document.getElementById('center').contentDocument.getElementById("CX").click();
//        },
//        
//        // 初始化
//        initialise: function() {
//        // 触发iframe中隐藏的button按钮的click事件

//        document.getElementById('center').contentDocument.getElementById("CSH").click();
//            Loading.onIfrmaLoad('center');
//        }
        
    };


    //--------------------------------------------------------------------------页面布局-----------------------------------

    // 解析出需要给ifrmae赋予的url
    var iframeUrl = location.search.match(/url=([^&]+)/);
    if (iframeUrl && iframeUrl.length == 2) iframeUrl = iframeUrl[1];
    else iframeUrl = '/404.aspx';

    // 根据登录时的数据年月，计算出评价时段
    //    var startMonth, endMonth, lstartMonth, lendMonth;
    //    var _year = month.substr(0, 4);
    //    if (parseInt(month.substr(4)) < 7) startMonth = _year + '01';
    //    else startMonth = _year + '07';
    //    //endMonth = (parseInt(startMonth) + 5).toString();
    //    endMonth = parseInt(jeny).toString();
    //    lstartMonth = parseInt(jbny).toString();
    //    lendMonth = parseInt(leny).toString();
    var inputMonth= month;
    
    var jdstat_qksjStore = new Ext.data.ArrayStore({
        fields: ['QKID', 'QKMC'],
        data: [
     ['1', '单井基础数据表'], ['2', '单井开发数据表'],['3','单井直接费用表']
    ]
    });
    
    
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
        items: [{ xtype: 'combo', fieldLabel: '导入数据表类型', labelWidth: 90, width:210, name: 'Dropdl', id: 'Dropdl', store: jdstat_qksjStore, displayField: 'QKMC', valueField: 'QKID', queryMode: 'local',margin: '-2 10 18 0', allowBlank: false },
        { xtype: 'fileuploadfield', buttonText: '浏览', name: 'UPLOADFILE', id: 'UPLOADFILE', margin: '-2 10 18 0', height: 20 },
        { xtype: 'button', text: '导入', iconCls: 'icon-add', handler: events.upload, margin: '-2 5 18 0' }
        ]
    });    
    

//    var departmentStore = Ext.create('Ext.data.Store', {
//        proxy: {
//            type: 'ajax',
//            url: '../../api/system/department.aspx',

//            extraParams: {
//                _type: 'getCYCList'
//            },

//            reader: {
//                type: 'json',
//                root: 'data'
//            }

//        },

//        fields: ['DEP_ID', 'DEP_NAME'],
//        autoLoad: true
//    });

    var panel = Ext.create('Ext.panel.Panel', {
        region: "center",
        layout: 'fit',
        tbar: [
            '-',
            { xtype: 'label',  text: '请输入年月:', margin: '0 0 0 20' },
            { xtype: 'textfield', name: 'month', id: 'month', value: inputMonth, width: 60 },
            '-',
            sPanel,
            '-',
            { xtype: 'button',hidden:true, text: '数据检查', iconCls: 'icon-add', handler: events.search },
            '-'         
        ],
        html: '<iframe id="center" src="" scrolling="auto" frameborder=0 width=100% height=100%></iframe>'
    });

        Ext.getCmp('Dropdl').setValue('1');

    Ext.create('Ext.container.Viewport', {
        layout: 'border',
        items: [panel],
        renderTo: Ext.getBody()
    });

    events.search();

});