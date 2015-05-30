Ext.onReady(function() {
    Loading.show();

    //--------------------------------------------------------------------------事件集中处理-------------------------------
    var events = {
        // 检索
        search: function() {
            /*var yt = Ext.getCmp('yt').getValue();
            var pjdy = Ext.getCmp('pjdy').getValue();
            var qk = Ext.getCmp('qk').getValue();
            var zyq = Ext.getCmp('zyq').getValue();
            if (yt)
            { var targetType = 'yt'; }
            if (pjdy)
            { var targetType = 'pjdy'; }
            if (qk)
            { var targetType = 'qk'; }
            if (zyq)
            { var targetType = 'zyq'; }
            */
            //? 'yt' : 'pjdy';
            // 检索的参数通过url的参数来传递给子页面
         //   var cyc = Ext.getCmp('CYC').getValue();
          var Dropdl = Ext.getCmp('Dropdl').getValue();

            document.getElementById('center').setAttribute('src', iframeUrl + '?startMonth=' + startMonth + '&endMonth=' + endMonth + '&Dropdl=' + Dropdl);
            // loading的实现
            Loading.onIfrmaLoad('center');
        },

        // 导出
        save: function() {
            // 触发iframe中隐藏的button按钮的click事件
            document.getElementById('center').contentDocument.getElementById("DC").click();
        },
        //前一页
        back: function() {
            // 触发iframe中隐藏的button按钮的click事件
            //var yt = Ext.getCmp('yt').getValue();
            //if (yt)
            if (document.getElementById('center').contentDocument.getElementById("QIAN"))
                document.getElementById('center').contentDocument.getElementById("QIAN").click();
        },
        //后一页
        forward: function() {
            // 触发iframe中隐藏的button按钮的click事件
            //var yt = Ext.getCmp('yt').getValue();
            //if (yt)
            if (document.getElementById('center').contentDocument.getElementById("HOU"))
                document.getElementById('center').contentDocument.getElementById("HOU").click();
        }

    };


    //--------------------------------------------------------------------------页面布局-----------------------------------

    // 解析出需要给ifrmae赋予的url
    var iframeUrl = location.search.match(/url=([^&]+)/);
    if (iframeUrl && iframeUrl.length == 2) iframeUrl = iframeUrl[1];
    else iframeUrl = '/404.aspx';

    // 根据登录时的数据年月，计算出评价时段
    var startMonth, endMonth;
    var _year = month.substr(0, 4);
    if (parseInt(month.substr(4)) < 7) startMonth = _year + '01';
    else startMonth = _year + '07';
    //endMonth = (parseInt(startMonth) + 5).toString();
    endMonth = parseInt(jeny).toString();

   var jdstat_qksjStore = new Ext.data.ArrayStore({
        fields: ['QKID', 'QKMC'],
        data: [
     ['quan', '全部'], ['新民', '新民']
    ]
    });

    var panel = Ext.create('Ext.panel.Panel', {
        region: "center",
        layout: 'fit',
        tbar: [
        //'-',
        //{ xtype: 'radio', boxLabel: '油田', name: 'targetType', margin: '0 10 0 0', checked: true, id: 'yt' },
        //{ xtype: 'radio', boxLabel: '评价单元', name: 'targetType', margin: '0 10 0 0', id: 'pjdy' },
        //{ xtype: 'radio', boxLabel: '区块', name: 'targetType', margin: '0 10 0 0', id: 'qk' },
        //{ xtype: 'radio', boxLabel: '作业区', name: 'targetType', margin: '0 10 0 0', id: 'zyq' },
            '-',
            { xtype: 'label', text: '评价时段:', margin: '0 0 0 20' },
            { xtype: 'textfield', name: 'startDate', value: startMonth, readOnlyCls: 'readOnly', readOnly: true, width: 60 },
            '至',
            { xtype: 'textfield', name: 'endDate', value: endMonth, readOnlyCls: 'readOnly', readOnly: true, width: 60 },
            '-',
             { xtype: 'combo', fieldLabel: '区块', name: 'Dropdl', id: 'Dropdl', store: jdstat_qksjStore,labelWidth:30, displayField: 'QKMC', valueField: 'QKID', queryMode: 'local', allowBlank: false },
            '-',
            { xtype: 'button', text: '检索', iconCls: 'icon-search', handler: events.search },
            '-',
            { xtype: 'button', text: '导出', iconCls: 'icon-save', handler: events.save },
            '-',
        //    { xtype: 'button', text: '前一页', iconCls: 'icon-qian', handler: events.back },
       //     { xtype: 'button', text: '后一页', iconCls: 'icon-hou', handler: events.forward }
        ],
        html: '<iframe id="center" src="" scrolling="auto" frameborder=0 width=100% height=100%></iframe>'
    });

    Ext.getCmp('Dropdl').setValue('quan');

    //departmentStore.on('load', function() { Ext.getCmp('CYC').getValue(); });
    Ext.create('Ext.container.Viewport', {
        layout: 'border',
        items: [panel],
        renderTo: Ext.getBody()
    });

    events.search();

});