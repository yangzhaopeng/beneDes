Ext.onReady(function() {
    Loading.show();

    //--------------------------------------------------------------------------事件集中处理-------------------------------
    var events = {
        // 检索
        search: function() {

            // 检索的参数通过url的参数来传递给子页面
          //  var cyc = Ext.getCmp('CYC').getValue();
            var jh = Ext.getCmp('JH').getValue();

            document.getElementById('center').setAttribute('src', iframeUrl + '?startMonth=' + startMonth + '&endMonth=' + endMonth +  '&JH='+ jh);
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
    startMonth = parseInt(jbny).toString();
    endMonth = parseInt(jeny).toString();


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

 var jhStore = Ext.create('Ext.data.Store', {
             proxy: {
                   type: 'ajax',
                   url: '../../api/system/bjwxy.aspx',

                      extraParams: {
                    //      CYC: cyc
                       },

                     reader: {
                         type: 'json',
                          root: 'data'
                                        }

                        },

                      fields: ['JH_ID', 'JH_NAME'],
                      autoLoad: true
                 });

    var panel = Ext.create('Ext.panel.Panel', {
        region: "center",
        layout: 'fit',
        tbar: [
            '-',
            { xtype: 'label', text: '本次评价时段:', margin: '0 0 0 20' },
            { xtype: 'textfield', name: 'startDate', value: startMonth, readOnlyCls: 'readOnly', readOnly: true, width: 60 },
            '至',
            { xtype: 'textfield', name: 'endDate', value: endMonth, readOnlyCls: 'readOnly', readOnly: true, width: 60 },
            '-',
//            { xtype: 'label', text: '采油厂：' },
//            { xtype: 'combo', name: 'CYC', id: 'CYC', store: departmentStore, width: 100, displayField: 'DEP_NAME', valueField: 'DEP_ID', triggerAction: 'all', queryMode: 'local'
//                    , listeners: {
//                        select: {
//                            fn: function(combo, record, index) {
//                                var cyc = Ext.getCmp('CYC').getValue();
//                               

//                                Ext.getCmp('JH').bindStore(jhStore);
//                                Ext.getCmp('JH').clearValue();

//                            }
//                        }
//                    }
//            },
//            '-',
            { xtype: 'label', text: '井号：' },
            { xtype: 'combo', name: 'JH', id: 'JH',store: jhStore, width: 100, displayField: 'JH_NAME', valueField: 'JH_ID', triggerAction: 'all', queryMode: 'local' },
            '-',
            { xtype: 'button', text: '检索', iconCls: 'icon-search', handler: events.search },
            '-',
            { xtype: 'button', text: '导出', iconCls: 'icon-save', handler: events.save }

        ],
        html: '<iframe id="center" src="" scrolling="auto" frameborder=0 width=100% height=100%></iframe>'
    });

  //  departmentStore.on('load', function() { Ext.getCmp('CYC').setValue('quan'); });


    Ext.create('Ext.container.Viewport', {
        layout: 'border',
        items: [panel],
        renderTo: Ext.getBody()
    });

    events.search();

});