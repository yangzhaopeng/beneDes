Ext.onReady(function() {
    Loading.show();

    //--------------------------------------------------------------------------事件集中处理-------------------------------
    var events = {
        // 检索
        search: function() {
            var yt = Ext.getCmp('yt').getValue();
            var pjdy = Ext.getCmp('pjdy').getValue();
            var qk = Ext.getCmp('qk').getValue();
            var Tbtgcb = Ext.getCmp('Tbtgcb').getValue();
            if (yt)
            { var targetType = 'yt'; }
            if (pjdy)
            { var targetType = 'pjdy'; }
            if (qk)
            { var targetType = 'qk'; }
            
            
            document.getElementById('center').setAttribute('src', iframeUrl + '?targetType=' + targetType + '&startMonth=' + startMonth + '&endMonth=' + endMonth + '&Tbtgcb=' + Tbtgcb);
            // loading的实现
            Loading.onIfrmaLoad('center');
        },

        // 导出
        save: function() {
            // 触发iframe中隐藏的button按钮的click事件
            document.getElementById('center').contentDocument.getElementById("DC").click();
        }
    };


    //--------------------------------------------------------------------------页面布局-----------------------------------

    // 解析出需要给ifrmae赋予的url
    var iframeUrl = location.search.match(/url=([^&]+)/);
    if (iframeUrl && iframeUrl.length == 2) iframeUrl = iframeUrl[1];
    else iframeUrl = '/404.aspx';

    // 根据登录时的数据年月，计算出评价时段
    var startMonth, endMonth;
    startMonth = parseInt(jbny).toString();
    endMonth = parseInt(jeny).toString();
    var Tbtgcb=400;

    var departmentStore = Ext.create('Ext.data.Store', {
        proxy: {
            type: 'ajax',
            url: '../../../api/system/department.aspx',

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
            { xtype: 'radio', boxLabel: '气井', name: 'targetType', checked: true, margin: '0 20 0 30', id: 'yt' },
            { xtype: 'radio', boxLabel: '区块', name: 'targetType',  margin: '0 10 0 0', id: 'qk' },
            { xtype: 'radio', boxLabel: '评价单元', name: 'targetType', margin: '0 10 0 0', id: 'pjdy' },
        //{ xtype: 'radio', boxLabel: '作业区', name: 'targetType', margin: '0 10 0 0', id: 'zyq' },
            '-',
            { xtype: 'label', text: '评价时段:', margin: '0 0 0 20' },
            { xtype: 'textfield', name: 'startDate', value: startMonth, readOnlyCls: 'readOnly', readOnly: true, width: 60 },
            '至',
            { xtype: 'textfield', name: 'endDate', value: endMonth, readOnlyCls: 'readOnly', readOnly: true, width: 60 },
            '-',
        //    { xtype: 'combo', fieldLabel: '采油厂', name: 'CYC', id: 'CYC', store: departmentStore, displayField: 'DEP_NAME', valueField: 'DEP_ID', queryMode: 'local', allowBlank: false },
        //   '-',
            {xtype: 'label', text: '特高成本= ' },
            { xtype: 'textfield', name: 'Tbtgcb', value: Tbtgcb, readOnlyCls: 'readOnly', readOnly: false, width: 60 ,id:'Tbtgcb'},
             '-',
            { xtype: 'button', text: '检索', iconCls: 'icon-search', handler: events.search },
            '-',
            { xtype: 'button', text: '导出', iconCls: 'icon-save', handler: events.save }
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

});