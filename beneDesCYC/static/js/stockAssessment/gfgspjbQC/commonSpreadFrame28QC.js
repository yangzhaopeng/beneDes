Ext.onReady(function() {
    Loading.show();
    var isSelected = '';
    var page='';
    var sumPages='';
    var pageAndSum='';
    //--------------------------------------------------------------------------事件集中处理-------------------------------
    var events = {
        // 检索
        search: function() {

            // 检索的参数通过url的参数来传递给子页面
            document.getElementById('center').setAttribute('src', iframeUrl + '?startMonth=' + startMonth + '&endMonth=' + endMonth);
            // loading的实现
            Loading.onIfrmaLoad('center');
            events.getPages();
        },
        
        //查询选择
        querySelect: function() {
            var ifChecked = '';
            var queryItems = Ext.getCmp("querySelect").items;
            for (var i = 0; i < queryItems.length; i++) {
                    if (queryItems.get(i).checked) {
                        ifChecked += '1,';
                    }
                    else {
                        ifChecked += '0,';
                    }
            }
            if(ifChecked.length!=0)
            {
                ifChecked = ifChecked.substring(0,ifChecked.length-1);
            }
            dataJs.get(
                        '../../view/stockAssessment/gfgspjbQC/xylbqjbQC.aspx',

                        {
                            _type:'querySelect',
                            querySelect:ifChecked
                        },

                        function(data, msg, response) {
                            location.href = location.href;
                        },
                        '',
                        true
                    );
            
            windowDialog.hide();
//            Loading.onIfrmaLoad('center');
        },
        
        getSelected:function(){
            
            dataJs.get(
                        '../../view/stockAssessment/gfgspjbQC/xylbqjbQC.aspx',

                        {
                            _type:'selectedItem'
                        },

                        function(data, msg, response) {
                            isSelected = msg;
                        },                      
                        '',
                        true
                    );
        },
        
        //显示对话框
        showPanel:function(){          
            var strs = new Array();
            if(isSelected.length==0)
            {
                return;
            }
            strs = isSelected.split(',');
            var queryItems = Ext.getCmp("querySelect").items;
            for(var i = 0; i<strs.length; i++)
            {
                if(strs[i]=="1")
                {
                    queryItems.get(i).setValue(true);
                }
                else
                {
                    queryItems.get(i).setValue(false);
                }
            }
            windowDialog.show();
        },
        
        //得到当前页数和总页数
        getPages:function(){
            
            dataJs.get(
                        '../../view/stockAssessment/gfgspjbQC/xylbqjbQC.aspx',

                        {
                            _type:'getPages'
                        },

                        function(data, msg, response) {
                            pageAndSum = msg;
                            var pstrs = new Array()
                            pstrs = pageAndSum.split(',');
                            page = pstrs[0];
                            Ext.getCmp('perPage').setValue(page);
                            sumPages = pstrs[1];
                            Ext.getCmp('sumPages').setValue(sumPages);
                        },                      
                        '',
                        true
                    );
        },
        
        // 导出
        save: function() {
            // 触发iframe中隐藏的button按钮的click事件
            document.getElementById('center').contentDocument.getElementById("DC").click();
        },
        //前一页
        back: function() {
            // 触发iframe中隐藏的button按钮的click事件
                if (document.getElementById('center').contentDocument.getElementById("QIAN"))
                document.getElementById('center').contentDocument.getElementById("QIAN").click();
                events.getPages();
        },
        //后一页
        forward: function() {
            // 触发iframe中隐藏的button按钮的click事件     
                if (document.getElementById('center').contentDocument.getElementById("HOU"))
                document.getElementById('center').contentDocument.getElementById("HOU").click();
                events.getPages();
        }
    };


    //--------------------------------------------------------------------------页面布局-----------------------------------

    // 解析出需要给ifrmae赋予的url
    var iframeUrl = location.search.match(/url=([^&]+)/);
    if (iframeUrl && iframeUrl.length == 2) iframeUrl = iframeUrl[1];
    else iframeUrl = '/404.aspx';

    // 根据登录时的数据年月，计算出评价时段
    //    var startMonth, endMonth;
    //    var _year = month.substr(0, 4);
    //    if (parseInt(month.substr(4)) < 7) startMonth = _year + '01';
    //    else startMonth = _year + '07';
    //    //endMonth = (parseInt(startMonth) + 5).toString();
    startMonth = parseInt(jbny).toString();
    endMonth = parseInt(jeny).toString();
    //----------------------------------------------------------创建对话框---------------------------------------------------
    var formPanel = Ext.create('Ext.form.Panel', {
        frame: true,
        bodyPadding: 5,

        items: [
            {
        xtype: 'checkboxgroup',
        name: 'querySelect',
        id:'querySelect',
        width: 550,
        columns: 4,
        items: [
            { boxLabel: '井号', name: 'JH', inputValue:"1", checked: true, disabled:true },
            { boxLabel: '效益类别', name: 'XYLB', inputValue:"1", checked: true, disabled:true },
            { boxLabel: '单元名称', name: 'DYMC', inputValue:"1", checked:true, disabled:true },
            { boxLabel: '生产时间', name: 'SCSJ', inputValue:"0" },
            { boxLabel: '井口月产气量', name: 'JKCQL', inputValue:"0"},
            { boxLabel: '放空气量', name: 'FKQL', inputValue:"0"},
            { boxLabel: '天然气商品量', name: 'TRQSPL', inputValue:"0"},
            { boxLabel: '凝析油产量', name: 'JKCYL', inputValue:"0" },
            { boxLabel: '凝析油商品量', name: 'YYSPL', inputValue:"0" },
            { boxLabel: '月产水量', name: 'ZSL', inputValue:"0" },
            { boxLabel: '直接材料费', name: 'ZJCLF', inputValue:"0" },
            { boxLabel: '直接燃料费', name: 'ZJRLF', inputValue:"0" },
            { boxLabel: '直接动力费', name: 'ZJDLF', inputValue:"0" },
            { boxLabel: '直接人员费', name: 'ZJRYF', inputValue:"0" },
            { boxLabel: '井下作业费', name: 'JXZYF', inputValue:"0" },
            { boxLabel: '维护性井下作业费', name: 'WHXJXZYF', inputValue:"0" },
            { boxLabel: '测井试井费', name: 'CJSJF', inputValue:"0" },
            { boxLabel: '维护修理费', name: 'WHXLF', inputValue:"0" },
            { boxLabel: '油气处理费', name: 'YQCLF', inputValue:"0" },
            { boxLabel: '拉油费', name: 'LYF', inputValue:"0" },
            { boxLabel: '天然气净化费', name: 'TRQJHF', inputValue:"0" },
            { boxLabel: '运输费', name: 'YSF', inputValue:"0" },
            { boxLabel: '其它直接费', name: 'QTZJF', inputValue:"0" },
            { boxLabel: '厂矿管理费', name: 'CKGLF', inputValue:"0" },
            { boxLabel: '减自用气产品', name: 'ZYYQCP', inputValue:"0" },
            { boxLabel: '折旧折耗', name: 'ZJZH', inputValue:"0" },
            { boxLabel: '期间费', name: 'QJF' , inputValue:"0"},
            { boxLabel: '勘探费', name: 'KTF', inputValue:"0" },
            { boxLabel: '操作成本', name: 'CZCB', inputValue:"0" },
            { boxLabel: '最低运行费', name: 'ZDYXF', inputValue:"0" },
            { boxLabel: '生产成本', name: 'SCCB', inputValue:"0" },
            { boxLabel: '营运成本', name: 'YYCB', inputValue:"0" },
            { boxLabel: '总销售收入', name: 'XSSR', inputValue:"0" },
            { boxLabel: '天然气销售收入', name: 'TRQXSSR', inputValue:"0" },
            { boxLabel: '凝析油销售收入', name: 'YYXSSR', inputValue:"0" },
            { boxLabel: '伴生产品销售收入', name: 'BSCPXSSR', inputValue:"0" },
            { boxLabel: '总销售税金', name: 'XSSJ', inputValue:"0" },
            { boxLabel: '天然气销售税金', name: 'TRQXSSJ', inputValue:"0" },
            { boxLabel: '凝析油销售税金', name: 'YYXSSJ', inputValue:"0" },
            { boxLabel: '伴生产品销售税金', name: 'BSCPXSSJ', inputValue:"0" },
            { boxLabel: '总税后收入', name: 'SHSR', inputValue:"0" }
        ]
    }
        ],
        buttons: [
            {
                text: '确定', iconCls: 'icon-ok',
                handler: events.querySelect
            },
            {
                text: '取消', iconCls: 'icon-close',
                handler: function() {
                    windowDialog.hide();
                }
            },
        ]
    });

    var windowDialog = Ext.create('Ext.window.Window', {
        title: '查询选择',
        height: 350,
        width: 550,
        layout: 'fit',
        modal: true,
        closeAction: 'hide',
        constrain: true,
        resizable: false,
        border: false,
        items: [formPanel]
    });
    //--------------------------------------------------------------------------------------------------------------------------
    var queryCheckboxGroup = Ext.create('Ext.form.CheckboxGroup', {
        xtype: 'checkboxgroup',
        name: 'querySelect',
        width: 300,
        columns: 5,
        fieldLabel: '查询选择',
        items: [
            { boxLabel: '井号', name: 'JH', inputValue:"1", checked: true },
            { boxLabel: '效益类别', name: 'XYLB', inputValue:"1", checked: true },
            { boxLabel: '单元名称', name: 'DYMC', inputValue:"1", checked:true },
            { boxLabel: '生产时间', name: 'SCSJ', inputValue:"0" },
            { boxLabel: '井口月产气量', name: 'JKCQL', inputValue:"0" },
            { boxLabel: '放空气量', name: 'FKQL', inputValue:"0" },
            { boxLabel: '天然气商品量', name: 'TRQSPL', inputValue:"0" },
            { boxLabel: '凝析油产量', name: 'JKCYL', inputValue:"0" },
            { boxLabel: '凝析油商品量', name: 'YYSPL', inputValue:"0" },
            { boxLabel: '月产水量', name: 'ZSL', inputValue:"0" },
            { boxLabel: '直接材料费', name: 'ZJCLF', inputValue:"0" },
            { boxLabel: '直接燃料费', name: 'ZJRLF', inputValue:"0" },
            { boxLabel: '直接动力费', name: 'ZJDLF', inputValue:"0" },
            { boxLabel: '直接人员费', name: 'ZJRYF', inputValue:"0" },
            { boxLabel: '井下作业费', name: 'JXZYF', inputValue:"0" },
            { boxLabel: '维护性井下作业费', name: 'WHXJXZYF', inputValue:"0" },
            { boxLabel: '测井试井费', name: 'CJSJF', inputValue:"0" },
            { boxLabel: '维护修理费', name: 'WHXLF', inputValue:"0" },
            { boxLabel: '油气处理费', name: 'YQCLF', inputValue:"0" },
            { boxLabel: '拉油费', name: 'LYF', inputValue:"0" },
            { boxLabel: '天然气净化费', name: 'TRQJHF', inputValue:"0" },
            { boxLabel: '运输费', name: 'YSF', inputValue:"0" },
            { boxLabel: '其它直接费', name: 'QTZJF', inputValue:"0" },
            { boxLabel: '厂矿管理费', name: 'CKGLF', inputValue:"0" },
            { boxLabel: '减自用气产品', name: 'ZYYQCP', inputValue:"0" },
            { boxLabel: '折旧折耗', name: 'ZJZH', inputValue:"0" },
            { boxLabel: '期间费', name: 'QJF' , inputValue:"0"},
            { boxLabel: '勘探费', name: 'KTF', inputValue:"0" },
            { boxLabel: '操作成本', name: 'CZCB', inputValue:"0" },
            { boxLabel: '最低运行费', name: 'ZDYXF', inputValue:"0" },
            { boxLabel: '生产成本', name: 'SCCB', inputValue:"0" },
            { boxLabel: '营运成本', name: 'YYCB', inputValue:"0" },
            { boxLabel: '总销售收入', name: 'XSSR', inputValue:"0" },
            { boxLabel: '天然气销售收入', name: 'TRQXSSR', inputValue:"0" },
            { boxLabel: '凝析油销售收入', name: 'YYXSSR', inputValue:"0" },
            { boxLabel: '伴生产品销售收入', name: 'BSCPXSSR', inputValue:"0" },
            { boxLabel: '总销售税金', name: 'XSSJ', inputValue:"0" },
            { boxLabel: '天然气销售税金', name: 'TRQXSSJ', inputValue:"0" },
            { boxLabel: '凝析油销售税金', name: 'YYXSSJ', inputValue:"0" },
            { boxLabel: '伴生产品销售税金', name: 'BSCPXSSJ', inputValue:"0" },
            { boxLabel: '总税后收入', name: 'SHSR', inputValue:"0" }
        ]
    });

    var panel = Ext.create('Ext.panel.Panel', {
        region: "center",
        layout: 'fit',
        tbar: [
            { xtype: 'label', text: '评价时段:', margin: '0 0 0 20' },
            { xtype: 'textfield', name: 'startDate', value: startMonth, readOnlyCls: 'readOnly', readOnly: true, width: 60 },
            '至',
            { xtype: 'textfield', name: 'endDate', value: endMonth, readOnlyCls: 'readOnly', readOnly: true, width: 60 },
           '-',
            { xtype: 'button', text: '查询选择', iconCls: 'icon-search', handler: events.showPanel },
            '-',
            { xtype: 'button', text: '检索', iconCls: 'icon-search', handler: events.search },
            '-',
            { xtype: 'button', text: '导出', iconCls: 'icon-save', handler: events.save },
            '-',
            { xtype: 'button', text: '前一页', iconCls: 'icon-qian', handler: events.back },
            { xtype: 'button', text: '后一页', iconCls: 'icon-hou', handler: events.forward },
            '-',
            { xtype: 'label', text: '当前页: '},
            { xtype: 'displayfield', name:'perPage', id:'perPage', margin: '10 0 0 0',width: 10 },
            { xtype: 'label', text: '/'},
            { xtype: 'displayfield', name:'sumPages',id:'sumPages', margin: '5 0 0 0',width: 30 },
        ],
        html: '<iframe id="center" src="" scrolling="auto" frameborder=0 width=100% height=100%></iframe>'
    });

    //   departmentStore.on('load', function() { Ext.getCmp('CYC').setValue('quan'); });

    //departmentStore.on('load', function() { Ext.getCmp('CYC').getValue(); });
    Ext.create('Ext.container.Viewport', {
        layout: 'border',
        items: [panel],
        renderTo: Ext.getBody()
    });

    events.search();
    events.getSelected();
    events.getPages();
});