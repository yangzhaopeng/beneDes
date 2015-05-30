Ext.onReady(function() {
    Loading.show();

    //--------------------------------------------------------------------------事件集中处理-------------------------------
    var events = {
        // 检索
        search: function() {

            // 检索的参数通过url的参数来传递给子页面


            document.getElementById('center').setAttribute('src', iframeUrl);
            // loading的实现
            Loading.onIfrmaLoad('center');
        },

        // 计算
        modify: function() {
            // 触发iframe中隐藏的button按钮的click事件
            document.getElementById('center').contentDocument.getElementById("XG").click();
        },

        // 保存
        save: function() {
            var doc;
            // 触发iframe中隐藏的button按钮的click事件
            if(document.all){
            doc = document.frames["center"].document;
            }else{
            doc = document.getElementById('center').contentDocument;
            }
            doc.getElementById("DC").click();
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
//            '-',
//            { xtype: 'button', text: '检索', iconCls: 'icon-search', handler: events.search, hidden: true, hiddenLabel: true },
            '-',
            { xtype: 'button', text: '修改', iconCls: 'icon-send' , handler: events.modify },
            '-',
            { xtype: 'button', text: '保存', iconCls: 'icon-save' , handler: events.save }
        ],
        html: '<iframe id="center" src="" scrolling="auto" frameborder=0 width=100% height=100%></iframe>'
    });

    //    departmentStore.on('load', function() { Ext.getCmp('CYC').setValue('quan'); });

    Ext.create('Ext.container.Viewport', {
        layout: 'border',
        items: [panel],
        renderTo: Ext.getBody()
    });

    events.search();

});