﻿Ext.onReady(function() {
    //Loading.show();

    //--------------------------------------------------------------------------事件集中处理-------------------------------
    var events = {
        // 检索
        confirmClick: function() {
            var startMonth = Ext.Date.format(Ext.getCmp('startMonth').getValue(), 'Ym');
            var endMonth = Ext.Date.format(Ext.getCmp('endMonth').getValue(), 'Ym');
            //if (targetType == true)
            //targetType = 'yt';
            // 检索的参数通过url的参数来传递给子页面
            var cyc = Ext.getCmp('CYC').getValue();
            if (cyc == null) {
                Ext.getCmp('CYC').setValue('[a-zA-Z]');
                cyc = Ext.getCmp('CYC').getValue();
            }
            document.getElementById('center').setAttribute('src', iframeUrl + '?startMonth=' + startMonth + '&endMonth=' + endMonth + '&CYC=' + cyc);
            // loading的实现
            Loading.onIfrmaLoad('center');
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
    startMonth = parseInt(jbny).toString();
    endMonth = parseInt(jeny).toString();
    //endMonth = parseInt(month).toString();

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
          { xtype: 'label', text: '评价时段：', margin: '0 0 0 20' },
            {
                xtype: 'monthfield', id: 'startMonth', editable: false, width: 150, labelWidth: 50, labelAlign: 'right',
                format: 'Ym', value: startMonth
            },
            '至',
            {
                xtype: 'monthfield', id: 'endMonth', editable: false, width: 150, labelWidth: 50, labelAlign: 'right',
                format: 'Ym', value: endMonth
            },
        //            { xtype: 'textfield', id: 'endMonth', name: 'endDate', value: endMonth, width: 60 },
        //            { xtype: 'textfield', id: 'year', name: 'endDate', value: endMonth, width: 60 },
            '-',
            { xtype: 'combo', fieldLabel: '采油厂', name: 'CYC', id: 'CYC', value: 'quan', labelWidth: 40, labelAlign: 'right', store: departmentStore, displayField: 'DEP_NAME', valueField: 'DEP_ID', queryMode: 'local', hidden: true, hiddenLabel: true, allowBlank: false },
        //'-',
            {xtype: 'button', text: '确定', iconCls: 'icon-send', handler: events.confirmClick },
        //            '-',
        //            { xtype: 'button', text: '导出', iconCls: 'icon-save', margin: '0 0 0 15', handler: events.save }
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

    //events.confirmClick();
});