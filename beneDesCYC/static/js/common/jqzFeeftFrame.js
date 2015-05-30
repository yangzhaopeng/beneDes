Ext.onReady(function() {
    Loading.show();
    var limit = 50;
    var start = 1;
    var page = 1;
    var totalPage = 1;
    var total = 0;
    var ft_type;
    //--------------------------------------------------------------------------事件集中处理-------------------------------
    var events = {
        // 检索
        search: function() {

            // 检索的参数通过url的参数来传递给子页面
//            var cyc = Ext.getCmp('CYC').getValue();
//            if (cyc == null) {
//                Ext.getCmp('CYC').setValue('[a-zA-Z]');
//                cyc = Ext.getCmp('CYC').getValue();
//            }

            //获取数据总条数
            Ext.Ajax.request({
                url: iframeUrl + '?ft_type='+ft_type,
                success: function(data) {
                    total = Ext.JSON.decode(data.responseText).data.total;
                    totalPage = Math.ceil(total / limit);
                    document.getElementById('center').setAttribute('src', iframeUrl + '?ft_type='+ft_type+'&limit=' + limit + '&start=' + start);
                    // loading的实现                    Loading.onIfrmaLoad('center');
                },
                params: { type: 'total' }
            });
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
        },

        // 弹出搜索框
        editClick: function() {
            //            if (!events.getGridSel()) return;           
            windowDialog.setTitle('编辑搜索信息');

            //            formPanel.getForm().setValues(gridSel.data);

            windowDialog.show();
        },
        fistPage: function() {
            start = 1;
            events.search();
        },
        nextPage: function() {
            if (total < page * limit) {
                alert("已经是最后一页");
            }
            else {
                start = page * limit + 1;
                page = page + 1;
                events.search();
            }

        },
        prePage: function() {
            if (page == 1) {
                alert("这是第一页");
            } else {
                start = (page - 2) * limit + 1;
                page = page - 1;
                events.search();
            }
        }

    };
///========================================================

    // 解析出需要给ifrmae赋予的url
    var iframeUrl = location.search.match(/url=([^&]+)/);
       if (iframeUrl && iframeUrl.length == 2) {
        iframeUrl = iframeUrl[1];
    }else
         iframeUrl = '/404.aspx';
         
     function parse_url(url){ //定义函数
         var pattern = /(\w+)=(\w+)/ig;//定义正则表达式
         var parames = {};//定义数组
         url.replace(pattern, function(a, b, c){
          parames[b] = c;
         });
         return parames;//返回这个数组.
    }
    
    var parames = parse_url(location.search);
//    alert(parames['ft_type']);//最后打印.根据key值来打印数组对应的值
    ft_type=parames['ft_type'];

    //---------------------------------------------------------创建、编辑对话框--------------------------------------------

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

 

                        var formPanel = Ext.create('Ext.form.Panel', {
                            frame: false,
                            bodyStyle: 'background-color:#dae7f6',
                            bodyPadding: 6,
                            defaultType: 'textfield',
                            layout: 'column',

                            fieldDefaults: {
                                msgTarget: 'side',
                                labelAlign: 'right',
                                labelWidth: 75,
                                width: 250,
                                margin: '7 0 0 0'
                            },

                            buttons: [
                    {
                        text: '检索',
                        iconCls: 'icon-search',
                        handler: events.search
                    },
                    {
                        text: '取消',
                        iconCls: 'icon-close',
                        handler: function() {
                            windowDialog.hide();
                        }
                    }
               ]
                        });



                        var windowDialog = Ext.create('Ext.window.Window', {
                            title: 'Hello world!',
                            height: 320,
                            width: 360,
                            layout: 'fit',
                            modal: true,
                            closeAction: 'hide',
                            constrain: true,
                            resizable: false,
                            border: false,
                            items: []
                        });

                        //--------------------------------------------------------------------------页面布局-----------------------------------


                        var panel = Ext.create('Ext.panel.Panel', {
                            region: "center",
                            layout: 'fit',
                            tbar: [
                                '-',
                                { xtype: 'button', text: '分摊', iconCls: 'icon-search', handler: events.editClick },
                                '-', 
                                { xtype: 'button', text: '浏览', iconCls: 'icon-search', handler: events.editClick },
                                '-',
                                { xtype: 'button', text: '导出', iconCls: 'icon-save', handler: events.save }
                                    ],
                            bbar: ['-',
                                { xtype: 'button', text: '首页', iconCls: 'icon-search', handler: events.firstPage },
                                '-',
                                { xtype: 'button', text: '上一页', iconCls: 'icon-search', handler: events.prePage },
                                  '-',
                                { xtype: 'button', text: '下一页', iconCls: 'icon-search', handler: events.nextPage },
                                 '->'
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