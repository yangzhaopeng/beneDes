Ext.onReady(function() {
    Loading.show();
    var limit = 50;
    var start = 1;
    var page = 1;
    var totalPage = 1;
    var total = 0;
    //--------------------------------------------------------------------------事件集中处理-------------------------------
    var events = {
        // 检索
        search: function() {

            // 检索的参数通过url的参数来传递给子页面
            var cyc = Ext.getCmp('CYC').getValue();
            if (cyc == null) {
                Ext.getCmp('CYC').setValue('[a-zA-Z]');
                cyc = Ext.getCmp('CYC').getValue();
            }
            var jh = Ext.getCmp('JH').getValue();

            document.getElementById('center').setAttribute('src', iframeUrl + '?startMonth=' + startMonth + '&endMonth=' + endMonth + '&CYC=' + cyc + '&JH=' + jh);
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

        fields: ['CYC_ID', 'DEP_NAME'],
        autoLoad: true
    });



    var panel = Ext.create('Ext.panel.Panel', {
        region: "center",
        layout: 'fit',
        tbar: [
            '-',
            { xtype: 'label', text: '本次评价时段:' },
            { xtype: 'textfield', name: 'startDate', value: startMonth, readOnlyCls: 'readOnly', readOnly: true, width: 60 },
            '至',
            { xtype: 'textfield', name: 'endDate', value: endMonth, readOnlyCls: 'readOnly', readOnly: true, width: 60 },
            '-',
            { xtype: 'label', text: '采油厂:' },
            { xtype: 'combo', name: 'CYC', id: 'CYC', store: departmentStore, displayField: 'DEP_NAME', valueField: 'CYC_ID', triggerAction: 'all', queryMode: 'local'
                    , listeners: {
                        select: {
                            fn: function(combo, record, index) {
                                var cyc = Ext.getCmp('CYC').getValue();
                                var jhStore = Ext.create('Ext.data.Store', {
                                    proxy: {
                                        type: 'ajax',
                                        url: '../../api/system/bjwxy.aspx',

                                        extraParams: {
                                            CYC: cyc
                                        },

                                        reader: {
                                            type: 'json',
                                            root: 'data'
                                        }

                                    },

                                    fields: ['JH_ID', 'JH_NAME'],
                                    autoLoad: true
                                });

                                Ext.getCmp('JH').bindStore(jhStore);
                                Ext.getCmp('JH').clearValue();

                            }
                        }
                    }
            },
            '-',
            { xtype: 'label', text: '井号:' },
            { xtype: 'combo', name: 'JH', id: 'JH', displayField: 'JH_NAME', valueField: 'JH_ID', triggerAction: 'all', queryMode: 'local' },
            '-',
            { xtype: 'button', text: '检索', iconCls: 'icon-search', handler: events.search },
            '-',
            { xtype: 'button', text: '导出', iconCls: 'icon-save', handler: events.save }

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