Ext.onReady(function() {
    Loading.show();

    //--------------------------------------------------------------------------事件集中处理-------------------------------
    var events = {
        // 检索
        search: function() {
            //var targetType = Ext.getCmp('yt').getValue();
            //if (targetType == true)
            //targetType = 'yt';
            // 检索的参数通过url的参数来传递给子页面
            var cyc = Ext.getCmp('CYC').getValue();
            if (cyc == null) {
                Ext.getCmp('CYC').setValue('[a-zA-Z]');
                cyc = Ext.getCmp('CYC').getValue();
            }
            document.getElementById('center').setAttribute('src', iframeUrl + '?startMonth=' + startMonth + '&endMonth=' + endMonth + '&CYC=' + cyc + '&lstartMonth=' + lstartMonth + '&lendMonth=' + lendMonth);
            // loading的实现
            Loading.onIfrmaLoad('center');
        },

        // 导出
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
        var startMonth, endMonth;
    //    var _year = month.substr(0, 4);
    //    if (parseInt(month.substr(4)) < 7) startMonth = _year + '01';
    //    else startMonth = _year + '07';
    //endMonth = (parseInt(startMonth) + 5).toString();
    //修改开始时间获取方式@yzp
        startMonth = parseInt(bny).toString();
    endMonth = parseInt(eny).toString();
    lstartMonth = parseInt(bny).toString();
    lendMonth = parseInt(leny).toString();
    

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
        //{ xtype: 'radio', boxLabel: '油田', name: 'targetType', checked: true, margin: '0 20 0 30', id: 'yt' },
        //{ xtype: 'radio', boxLabel: '评价单元', name: 'targetType', margin: '0 10 0 0', id: 'pjdy' },
            '-',
            { xtype: 'label', text: '本次评价时段:', margin: '0 0 0 20' },
            { xtype: 'textfield', name: 'startDate', value: startMonth, readOnlyCls: 'readOnly', readOnly: true, width: 60 },
            '至',
            { xtype: 'textfield', name: 'endDate', value: endMonth, readOnlyCls: 'readOnly', readOnly: true, width: 60},
            '-',
            { xtype: 'label', text: '上次评价时段:', margin: '0 0 0 20' },
            { xtype: 'textfield', name: 'lstartDate', value: lstartMonth, readOnlyCls: 'readOnly', readOnly: true, width: 60 },
            '至',
            { xtype: 'textfield', name: 'lendDate', value: lendMonth, readOnlyCls: 'readOnly', readOnly: true, width: 60 },
            '-',
            { xtype: 'combo', fieldLabel: '采油厂', name: 'CYC', id: 'CYC', value: 'quan', labelWidth: 40, labelAlign: 'right', store: departmentStore, displayField: 'DEP_NAME', valueField: 'DEP_ID', queryMode: 'local', allowBlank: false },
            '-',
            { xtype: 'button', text: '检索', iconCls: 'icon-search', handler: events.search },
            '-',
            { xtype: 'button', text: '导出', iconCls: 'icon-save', handler: events.save }
        ],
        html: '<iframe id="center" src="" scrolling="auto" frameborder=0 width=100% height=100%></iframe>'
    });

//    departmentStore.on('load', function() { Ext.getCmp('CYC').setValue('quan'); });

    //departmentStore.on('load', function() { Ext.getCmp('CYC').getValue(); });
    Ext.create('Ext.container.Viewport', {
        layout: 'border',
        items: [panel],
        renderTo: Ext.getBody()
    });

    events.search();

});