Ext.onReady(function() {
    Loading.show();

    //--------------------------------------------------------------------------事件集中处理-------------------------------
    var events = {
        // 检索
        search: function() {

            // 检索的参数通过url的参数来传递给子页面
       //     var cyc = Ext.getCmp('CYC').getValue();
            var dwType = Ext.getCmp('dwType').getValue();
            var zdw = Ext.getCmp('zdw').getValue();
            var lendMonth = Ext.getCmp('lendMonth').getValue();

            document.getElementById('center').setAttribute('src', iframeUrl + '?startMonth=' + startMonth + '&endMonth=' + endMonth + '&lstartMonth=' + lstartMonth + '&lendMonth=' + lendMonth + '&dwType=' + dwType + '&zdw=' + zdw);
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
    var startMonth, endMonth, lstartMonth, lendMonth;
//    var _year = month.substr(0, 4);
//    if (parseInt(month.substr(4)) < 7) startMonth = _year + '01';
//    else startMonth = _year + '07';
    //endMonth = (parseInt(startMonth) + 5).toString();
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

   var dwTypeStore = new Ext.data.ArrayStore({
   fields:['DWID','DWNAME'],
   data:[
     ['zyq','作业区'],['pjdy','评价单元'],['qk','区块']
    ]
});


//    var dwTypeStore = Ext.create('Ext.data.Store', {
////        proxy: {
////            type: 'ajax',
////            url: '../../api/system/dwtype.aspx',

////            extraParams: {
////                _type: 'getDWList'
////            },

////            reader: {
////                type: 'json',
////                root: 'data'
////            }

////        },
//        data:[],
//        fields: ['DWNAME', 'DWID'],
//        autoLoad: true
//    });
    

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
            { xtype: 'label', text: '本次评价时段:', margin: '0 0 0 20' },
            { xtype: 'textfield', name: 'startDate', value: startMonth, readOnlyCls: 'readOnly', readOnly: true, width: 60 },
            '至',
            { xtype: 'textfield', name: 'endDate', value: endMonth, readOnlyCls: 'readOnly', readOnly: true, width: 60},
            '-',
            { xtype: 'label', text: '上次评价时段:', margin: '0 0 0 20' },
            { xtype: 'textfield', name: 'lstartDate', value: lstartMonth, readOnlyCls: 'readOnly', readOnly: true, width: 60 },
            '至',
            { xtype: 'textfield', name: 'lendDate', id: 'lendMonth', value: lendMonth, width: 60 },
            '-',
//            { xtype: 'combo', name: 'CYC', id: 'CYC', store: departmentStore, width: 90, displayField: 'DEP_NAME', valueField: 'DEP_ID', triggerAction: 'all'
//                    , listeners: {
//                        select: {
//                            fn: function(combo, record, index) {
//                                var cyc = Ext.getCmp('CYC').getValue();

//                                if (cyc == "quan") {

//                                    Ext.getCmp('dwType').disabled = true;
//                                }
//                                else {
//                                    Ext.getCmp('dwType').disabled = false;
//                                    Ext.getCmp('dwType').bindStore(dwTypeStore);
//                                }
//                                Ext.getCmp('dwType').clearValue();
//                                Ext.getCmp('zdw').clearValue();

//                            }
//                        }
//                    }
//            },
//            '-',
            { xtype: 'combo', name: 'dwType', id: 'dwType', labelWidth:53, store: dwTypeStore, displayField: 'DWNAME', valueField: 'DWID', triggerAction: 'all'
                , listeners: {
                    select: function(combo, record, index) {
                        var dwType = Ext.getCmp('dwType').getValue();
                     //   var cyc = Ext.getCmp('CYC').getValue();

                        var zdwStore = Ext.create('Ext.data.Store', {
                            proxy: {
                                type: 'ajax',
                                url: '../../api/system/dtstat_djsj1.aspx',
                                extraParams: {
                                    dwType: dwType,
                               //     CYC: cyc,
                                    BNY: month,
                                    ENY: eny
                                },
                                reader: {
                                    type: 'json',
                                    root: 'data'
                                }
                            },
                            fields: ['DEP_NAME', 'DEP_ID'],
                            autoLoad: true
                        });
                        zdwStore.load();
                        Ext.getCmp('zdw').bindStore(zdwStore);
                        //                        Ext.getCmp('zdw').store = zdwStore;
                        Ext.getCmp('zdw').clearValue();
                    }
                }
            },
            '-',
            { xtype: 'combo', name: 'zdw', id: 'zdw', hiddenName: 'zdw', labelWidth:53, displayField: 'DEP_NAME', valueField: 'DEP_ID', queryMode: 'remote', triggerAction: 'all' },
            '-',
            { xtype: 'button', text: '检索', iconCls: 'icon-search', handler: events.search },
            '-',
            { xtype: 'button', text: '导出', iconCls: 'icon-save', handler: events.save }

        ],
        html: '<iframe id="center" src="" scrolling="auto" frameborder=0 width=100% height=100%></iframe>'
    });

   // departmentStore.on('load', function() { Ext.getCmp('CYC').setValue('quan'); });
Ext.getCmp('dwType').setValue('zyq');

    Ext.create('Ext.container.Viewport', {
        layout: 'border',
        items: [panel],
        renderTo: Ext.getBody()
    });

    events.search();

});