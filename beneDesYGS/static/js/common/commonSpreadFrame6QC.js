﻿Ext.onReady(function() {
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
            var cycname = Ext.getCmp('CYC').getRawValue();
            //var jgsz = Ext.getCmp('jgsz').getValue();
            var qclx = Ext.getCmp('QCLX').getValue();

            document.getElementById('center').setAttribute('src', iframeUrl + '?startMonth=' + startMonth + '&endMonth=' + endMonth + '&CYC=' + cyc + '&QCLX=' + qclx + '&CYC_NAME=' + cycname);
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
    startMonth = parseInt(jbny).toString();
    endMonth = parseInt(jeny).toString();
    //var jgsz = '16.5,20,25,30,35,40,45,50,70,90,120';
    var qclxStore = Ext.create('Ext.data.Store', {
        proxy: {
            type: 'ajax',
            url: '../../api/system/qclx.aspx',

            extraParams: {
                _type: 'getAllList'
            },

            reader: {
                type: 'json',
                root: 'data'
            }
        },

        fields: ['QCLX'],
        autoLoad: true
    });


    var departmentStore = Ext.create('Ext.data.Store', {
        proxy: {
            type: 'ajax',
            url: '../../api/system/department.aspx',

            extraParams: {
                _type: 'getCQCList'
            },

            reader: {
                type: 'json',
                root: 'data'
            }

        },

        fields: ['CYC_ID', 'DEP_NAME'],
        autoLoad: true
    });

    var panel = Ext.create('Ext.panel.Panel', {
        region: "center",
        layout: 'fit',
        tbar: [
            '-',
        //{ xtype: 'radio', boxLabel: '油田', name: 'targetType', checked: true, margin: '0 20 0 30', id: 'yt' },
        //{ xtype: 'radio', boxLabel: '评价单元', name: 'targetType', margin: '0 10 0 0', id: 'pjdy' },
        //'-',
            {xtype: 'label', text: '评价时段:', margin: '0 0 0 20' },
            { xtype: 'textfield', name: 'startDate', value: startMonth, readOnlyCls: 'readOnly', readOnly: true, width: 60 },
            '至',
            { xtype: 'textfield', name: 'endDate', value: endMonth, readOnlyCls: 'readOnly', readOnly: true, width: 60 },
            '-',
        //{ xtype: 'label', text: '价格数值参数, 不同价格之间以“，”分割', margin: '0 0 0 20' },
        //{ xtype: 'textfield', name: 'jgsz', id: 'jgsz', value: jgsz, emptyText: '16.5,20,25,30,35,40,45,50,70,90,120', width: 300 },
            {xtype: 'combo', fieldLabel: '气藏类型', name: 'QCLX', id: 'QCLX', value: '凝析气藏', labelWidth: 60, labelAlign: 'right', store: qclxStore, displayField: 'QCLX', valueField: 'QCLX', queryMode: 'local', allowBlank: true },
            '-',
            { xtype: 'combo', fieldLabel: '采气厂', name: 'CYC', id: 'CYC',  labelWidth: 40, labelAlign: 'right', store: departmentStore, displayField: 'DEP_NAME', valueField: 'CYC_ID', queryMode: 'local', allowBlank: false },  
            '-',
            { xtype: 'button', text: '检索', iconCls: 'icon-search', handler: events.search },
            '-',
            { xtype: 'button', text: '导出', iconCls: 'icon-save', handler: events.save }
        ],
        html: '<iframe id="center" src="" scrolling="auto" frameborder=0 width=100% height=100%></iframe>'
    });

    //    departmentStore.on('load', function() { Ext.getCmp('CYC').setValue('quan'); });
    //    qclxStore.on('load', function() { Ext.getCmp('QCLX').setValue('凝析气藏'); });

    //departmentStore.on('load', function() { Ext.getCmp('CYC').getValue(); });
    Ext.create('Ext.container.Viewport', {
        layout: 'border',
        items: [panel],
        renderTo: Ext.getBody()
    });

    events.search();

});