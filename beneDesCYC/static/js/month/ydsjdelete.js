Ext.onReady(function() {
    Loading.show();

    //--------------------------------------------------------------------------事件集中处理-------------------------------
    var events = {
        // 初始化
        search: function() {

            // 检索的参数通过url的参数来传递给子页面

        var Date = Ext.getCmp('Date').getValue();
        
        document.getElementById('center').setAttribute('src', iframeUrl + '?Date=' + Date );
            // loading的实现
            Loading.onIfrmaLoad('center');
        },

        // 全选
        allSelect: function() {
            // 触发iframe中隐藏的button按钮的click事件
            document.getElementById('center').contentDocument.getElementById("QX").click();
        },

        // 取消
        cancel: function() {
            // 触发iframe中隐藏的button按钮的click事件
            document.getElementById('center').contentDocument.getElementById("CX").click();
        },
        
        // 初始化
        initialise: function() {
        // 触发iframe中隐藏的button按钮的click事件

        document.getElementById('center').contentDocument.getElementById("QK").click();
            Loading.onIfrmaLoad('center');
        }
        
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
    

    var departmentStore = Ext.create('Ext.data.Store', {
        proxy: {
            type: 'ajax',
            url: '../../api/system/department.aspx',

            extraParams: {
                _type: 'getCYCList'
            },

            reader: {
                type: 'json',
                root: 'data'
            }

        },

        fields: ['DEP_ID', 'DEP_NAME'],
        autoLoad: true
    });

    var panel = Ext.create('Ext.panel.Panel', {
        region: "center",
        layout: 'fit',
        tbar: [
            '-',
            { xtype: 'label', text: '输入时间:', margin: '0 0 0 20' },
            { xtype: 'textfield', name: 'Date', id: 'Date', value: inputMonth, width: 60 },
            '-',
//            { xtype: 'button', text: '清空', iconCls: 'icon-search', margin: '0 0 0 15', handler: events.search, hidden: true, hiddenLabel: true  },
//            '-',
            { xtype: 'button', text: '全选', iconCls: 'icon-send', handler: events.allSelect },
            '-',
            { xtype: 'button', text: '取消', iconCls: 'icon-close', handler: events.cancel },
            '-',
            { xtype: 'button', text: '清空', iconCls: 'icon-init', handler: events.initialise }
            
        ],
        html: '<iframe id="center" src="" scrolling="auto" frameborder=0 width=100% height=100%></iframe>'
    });

       

    Ext.create('Ext.container.Viewport', {
        layout: 'border',
        items: [panel],
        renderTo: Ext.getBody()
    });

    events.search();

});