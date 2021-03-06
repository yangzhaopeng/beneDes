﻿Ext.onReady(function() {
    Loading.show();

    //--------------------------------------------------------------------------事件集中处理-------------------------------
    var events = {
        // 检索
        search: function() {

            // 检索的参数通过url的参数来传递给子页面
//  var cyc = Ext.getCmp('CYC').getValue();
        var xzType = Ext.getCmp('xzType').getValue();
        var xzType2 = Ext.getCmp('xzType2').getValue();
        var xzType3 = Ext.getCmp('xzType3').getValue();
        var xzType4 = Ext.getCmp('xzType4').getValue();
//        var xzType5 = Ext.getCmp('xzType5').getValue();
        
        var SJCX = Ext.getCmp('SJCX').getValue();
        var SJCX2 = Ext.getCmp('SJCX2').getValue();
        var SJCX3 = Ext.getCmp('SJCX3').getValue();
        var SJCX4 = Ext.getCmp('SJCX4').getValue();
//        var SJCX5 = Ext.getCmp('SJCX5').getValue();
        
        var ROAND = Ext.getCmp('ROAND').getValue();
        var ROAND2 = Ext.getCmp('ROAND2').getValue();
        var ROAND3 = Ext.getCmp('ROAND3').getValue();
//        var ROAND4 = Ext.getCmp('ROAND4').getValue();
        
       document.getElementById('center').setAttribute('src', iframeUrl +  '?xzType=' + xzType + '&xzType2=' + xzType2 + '&xzType3=' + xzType3 + '&xzType4=' + xzType4 +  '&SJCX=' + SJCX + '&SJCX2=' + SJCX2 + '&SJCX3=' + SJCX3 + '&SJCX4=' + SJCX4 + '&ROAND=' + ROAND + '&ROAND2=' + ROAND2 + '&ROAND3=' + ROAND3 );
     //    document.getElementById('center').setAttribute('src', iframeUrl + '&xzType=' + xzType + '&xzType2=' + xzType2 + '&xzType3=' + xzType3 + '&xzType4=' + xzType4 +  '&SJCX=' + SJCX + '&SJCX2=' + SJCX2 + '&SJCX3=' + SJCX3 + '&SJCX4=' + SJCX4 +  '&ROAND=' + ROAND + '&ROAND2=' + ROAND2 + '&ROAND3=' + ROAND3 );
            // loading的实现
            Loading.onIfrmaLoad('center');
        },

        // 导出
        save: function() {
            // 触发iframe中隐藏的button按钮的click事件
            document.getElementById('center').contentDocument.getElementById("DC").click();
        },

        // 弹出搜索框
        editClick: function() {
            //            if (!events.getGridSel()) return;           
            windowDialog.setTitle('编辑搜索信息');

//            formPanel.getForm().setValues(gridSel.data);

            windowDialog.show();
        }

    };


    // 解析出需要给ifrmae赋予的url
    var iframeUrl = location.search.match(/url=([^&]+)/);
    if (iframeUrl && iframeUrl.length == 2) iframeUrl = iframeUrl[1];
    else iframeUrl = '/404.aspx';


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

        fields: ['DEP_ID', 'DEP_NAME'],
        autoLoad: true
    });

    var xzTypeStore = new Ext.data.ArrayStore({
        fields: ['XZID', 'XZNAME'],
        data: [
    // ['ny', '时间'], ['qk', '区块'], ['jh', '井号'], ['pjdy', '评价单元'], ['yqlxmc', '油气类型'], ['xsypmc', '销售油品'], ['ssyt', '所属油田'], ['yclx', '油藏类型']
     //['ny', '时间'],['ft_type','所属部门'],['source_name','费用来源'],['class_name','费用大类'],['fee_name','费用']
     ['ny','时间'],['jh','井号'],['zyq','作业区'],['qk','区块'],['bscpdm','伴生产品'],['yqlxmc','油气类型'],['xsypmc','销售油品'],['pjdy','所属评价单元'],['ssyt','所属油田'],['yclx','油藏类型']
    ]
    });

    var ORANDStore = new Ext.data.ArrayStore({
        fields: ['ROANDID', 'ROANDNAME'],
        data: [
     ['or', '或者'], ['and', '并且']
    ]
    });

    //第一行
//    var Panel1 = Ext.create('Ext.form.Panel', {
//        frame: false,
//        layout: { type: 'table', column: 4 },
//        //        layout: 'column',
//        border: false,
//        bodyPadding: 5,
//        defaultType: 'textfield',
//        fieldDefaults: {
//            msgTarget: 'side',
//            labelAlign: 'right',
//            labelWidth: 30,
//            width: 100

//        },

//        items: [{
//            xtype: 'label',
//            text: '请选择采油厂:',
//            name: 'label1',
//            margin: '0 20 0 0'
//        },
//        {
//            xtype: 'combo',
//            name: 'CYC',
//            id: 'CYC',
//            store: departmentStore,
//            displayField: 'DEP_NAME',
//            valueField: 'DEP_ID',
//            queryMode: 'local',
//            margin: '0 20 0 0'
//           
//        }
//        ]

//    });
    //第二行
    var Panel2 = Ext.create('Ext.form.Panel', {
        frame: false,
        layout: { type: 'table', column: 4 },
        //        layout: 'column',
        border: false,
        bodyPadding: 5,
         bodyStyle: 'background-color:#dae7f6',
        defaultType: 'textfield',
        fieldDefaults: {
            msgTarget: 'side',
            labelAlign: 'right',
            labelWidth: 30,
            width: 100

        },

        items: [{
            xtype: 'combo',
            name: 'xzType',
            id: 'xzType',
            store: xzTypeStore,
            displayField: 'XZNAME',
            valueField: 'XZID',
            queryMode: 'local',
            emptyText: '请选择类型',
            margin: '0 10 0 0',
            listeners: {
                select: function(combo, record, index) {
                    var xzType = Ext.getCmp('xzType').getValue();
//                    var cyc = Ext.getCmp('CYC').getValue();
                    
                    var sjcxStore = Ext.create('Ext.data.Store', {
                        proxy: {
                            type: 'ajax',
                            url: '../../api/system/bscpftcx.aspx',

                            extraParams: {
                                _type: 'getSJList',
//                                CYC: cyc,
                                xzType: xzType
                            },

                            reader: {
                                type: 'json',
                                root: 'data'
                            }

                        },

                        fields: ['SJ_ID', 'SJ_NAME'],
                        autoLoad: true
                    });
                    
                    Ext.getCmp('SJCX').bindStore(sjcxStore);
                    Ext.getCmp('SJCX').clearValue();
                }
            }
        }, {
            xtype: 'combo',
            name: 'SJCX',
            id: 'SJCX',
            displayField: 'SJ_NAME',
            valueField: 'SJ_ID',
            queryMode: 'local',
            margin: '0 10 0 0'  
            
        }, {
            xtype: 'combo',
            name: 'ROAND',
            id: 'ROAND',
            store: ORANDStore,
            displayField: 'ROANDNAME',
            valueField: 'ROANDID',
            queryMode: 'local',
            margin: '0 10 0 0'
            
}]

        });
        //第三行
        var Panel3 = Ext.create('Ext.form.Panel', {
            frame: false,
            layout: { type: 'table', column: 4 },
            //        layout: 'column',
             bodyStyle: 'background-color:#dae7f6',
            border: false,
            bodyPadding: 5,
            defaultType: 'textfield',
            fieldDefaults: {
                msgTarget: 'side',
                labelAlign: 'right',
                labelWidth: 30,
                width: 100

            },

            items: [{
                xtype: 'combo',
                name: 'xzType2',
                id: 'xzType2',
                store: xzTypeStore,
                displayField: 'XZNAME',
                valueField: 'XZID',
                queryMode: 'local',
                emptyText: '请选择类型',
                margin: '0 10 0 0',
                listeners: {
                    select: function(combo, record, index) {
                        var xzType = Ext.getCmp('xzType2').getValue();
//                        var cyc = Ext.getCmp('CYC').getValue();
                        var sjcxStore2 = Ext.create('Ext.data.Store', {
                            proxy: {
                                type: 'ajax',
                                url: '../../api/system/bscpftcx.aspx',

                                extraParams: {
                                    _type: 'getSJList',
//                                    CYC: cyc,
                                    xzType: xzType
                                },

                                reader: {
                                    type: 'json',
                                    root: 'data'
                                }

                            },

                            fields: ['SJ_ID', 'SJ_NAME'],
                            autoLoad: true
                        });
                        Ext.getCmp('SJCX2').bindStore(sjcxStore2);
                        Ext.getCmp('SJCX2').clearValue();
                    }
                }
            }, {
                xtype: 'combo',
                name: 'SJCX2',
                id: 'SJCX2',
                displayField: 'SJ_NAME',
                valueField: 'SJ_ID',
                queryMode: 'local',
                margin: '0 10 0 0'
            }, {
                xtype: 'combo',
                name: 'ROAND2',
                id: 'ROAND2',
                store: ORANDStore,
                displayField: 'ROANDNAME',
                valueField: 'ROANDID',
                queryMode: 'local',
                margin: '0 10 0 0'
}]

            });
            //第四行
            var Panel4 = Ext.create('Ext.form.Panel', {
                frame: false,
                layout: { type: 'table', column: 4 },
                //        layout: 'column',
                border: false,
                 bodyStyle: 'background-color:#dae7f6',
                bodyPadding: 5,
                defaultType: 'textfield',
                fieldDefaults: {
                    msgTarget: 'side',
                    labelAlign: 'right',
                    labelWidth: 30,
                    width: 100

                },

                items: [{
                    xtype: 'combo',
                    name: 'xzType3',
                    id: 'xzType3',
                    store: xzTypeStore,
                    displayField: 'XZNAME',
                    valueField: 'XZID',
                    queryMode: 'local',
                    emptyText: '请选择类型',
                    margin: '0 10 0 0',
                    listeners: {
                        select: function(combo, record, index) {
                            var xzType = Ext.getCmp('xzType3').getValue();
//                            var cyc = Ext.getCmp('CYC').getValue();
                            var sjcxStore3 = Ext.create('Ext.data.Store', {
                                proxy: {
                                    type: 'ajax',
                                    url: '../../api/system/bscpftcx.aspx',

                                    extraParams: {
                                        _type: 'getSJList',
//                                        CYC: cyc,
                                        xzType: xzType
                                    },

                                    reader: {
                                        type: 'json',
                                        root: 'data'
                                    }

                                },

                                fields: ['SJ_ID', 'SJ_NAME'],
                                autoLoad: true
                            });
                            Ext.getCmp('SJCX3').bindStore(sjcxStore3);
                            Ext.getCmp('SJCX3').clearValue();
                        }
                    }
                }, {
                    xtype: 'combo',
                    name: 'SJCX3',
                    id: 'SJCX3',
                    displayField: 'SJ_NAME',
                    valueField: 'SJ_ID',
                    queryMode: 'local',
                    margin: '0 10 0 0'
                }, {
                    xtype: 'combo',
                    name: 'ROAND3',
                    id: 'ROAND3',
                    store: ORANDStore,
                    displayField: 'ROANDNAME',
                    valueField: 'ROANDID',
                    queryMode: 'local',
                    margin: '0 10 0 0'
}]

                });
                //第五行
                var Panel5 = Ext.create('Ext.form.Panel', {
                    frame: false,
                    layout: { type: 'table', column: 4 },
                    //        layout: 'column',
                    border: false,
                    bodyStyle: 'background-color:#dae7f6',
                    bodyPadding: 5,
                    defaultType: 'textfield',
                    fieldDefaults: {
                        msgTarget: 'side',
                        labelAlign: 'right',
                        labelWidth: 30,
                        width: 100

                    },

                    items: [{
                        xtype: 'combo',
                        name: 'xzType4',
                        id: 'xzType4',
                        store: xzTypeStore,
                        displayField: 'XZNAME',
                        valueField: 'XZID',
                        queryMode: 'local',
                        emptyText: '请选择类型',
                        margin: '0 10 0 0',
                        listeners: {
                            select: function(combo, record, index) {
                                var xzType = Ext.getCmp('xzType4').getValue();
//                                var cyc = Ext.getCmp('CYC').getValue();
                                var sjcxStore4 = Ext.create('Ext.data.Store', {
                                    proxy: {
                                        type: 'ajax',
                                        url: '../../api/system/bscpftcx.aspx',

                                        extraParams: {
                                            _type: 'getSJList',
//                                            CYC: cyc,
                                            xzType: xzType
                                        },

                                        reader: {
                                            type: 'json',
                                            root: 'data'
                                        }

                                    },

                                    fields: ['SJ_ID', 'SJ_NAME'],
                                    autoLoad: true
                                });
                                Ext.getCmp('SJCX4').bindStore(sjcxStore4);
                                Ext.getCmp('SJCX4').clearValue();
                            }
                        }
                    }, {
                        xtype: 'combo',
                        name: 'SJCX4',
                        id: 'SJCX4',
                        displayField: 'SJ_NAME',
                        valueField: 'SJ_ID',
                        queryMode: 'local',
                        margin: '0 10 0 0'
                    } 
//                    {
//                        xtype: 'combo',
//                        name: 'ROAND4',
//                        id: 'ROAND4',
//                        store: ORANDStore,
//                        displayField: 'ROANDNAME',
//                        valueField: 'ROANDID',
//                        queryMode: 'local',
//                        margin: '0 10 0 0'
                    
                   ]

                    });
                    //第六行
//                    var Panel6 = Ext.create('Ext.form.Panel', {
//                        frame: false,
//                        layout: { type: 'table', column: 4 },
//                        //        layout: 'column',
//                        border: false,
//                        bodyPadding: 5,
//                        defaultType: 'textfield',
//                        fieldDefaults: {
//                            msgTarget: 'side',
//                            labelAlign: 'right',
//                            labelWidth: 30,
//                            width: 100

//                        },

//                        items: [{
//                            xtype: 'combo',
//                            name: 'xzType5',
//                            id: 'xzType5',
//                            store: xzTypeStore,
//                            displayField: 'XZNAME',
//                            valueField: 'XZID',
//                            queryMode: 'local',
//                            emptyText: '请选择类型',
//                            margin: '0 10 0 0',
//                            listeners: {
//                                select: function(combo, record, index) {
//                                    var xzType = Ext.getCmp('xzType5').getValue();
//                                    var cyc = Ext.getCmp('CYC').getValue();
//                                    var sjcxStore = Ext.create('Ext.data.Store', {
//                                        proxy: {
//                                            type: 'ajax',
//                                            url: '../../api/system/sjcx.aspx',

//                                            extraParams: {
//                                                _type: 'getSJList',
//                                                CYC: cyc,
//                                                xzType: xzType
//                                            },

//                                            reader: {
//                                                type: 'json',
//                                                root: 'data'
//                                            }

//                                        },

//                                        fields: ['SJ_ID', 'SJ_NAME'],
//                                        autoLoad: true
//                                    });
//                                    Ext.getCmp('SJCX5').bindStore(sjcxStore5);
//                                    Ext.getCmp('SJCX5').clearValue();
//                                }
//                            }
//                        }, {
//                            xtype: 'combo',
//                            name: 'SJCX5',
//                            id: 'SJCX5',
//                            displayField: 'SJ_NAME',
//                            valueField: 'SJ_ID',
//                            queryMode: 'local',
//                            margin: '0 10 0 0'
//}]

//                        });


                        var formPanel = Ext.create('Ext.form.Panel', {
                            frame: false,
                            bodyPadding: 6,
                            bodyStyle: 'background-color:#dae7f6',
                            defaultType: 'textfield',
                            layout: 'column',

                            fieldDefaults: {
                                msgTarget: 'side',
                                labelAlign: 'right',
                                labelWidth: 75,
                                width: 250,
                                margin: '7 0 0 0'
                            },

                            items: [Panel2,Panel3,Panel4,Panel5],

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
                            width: 400,
                            layout: 'fit',
                            modal: true,
                            closeAction: 'hide',
                            constrain: true,
                            resizable: false,
                            border: false,
                            items: [formPanel]
                        });

//--------------------------------------------------------------------------页面布局-----------------------------------



        var panel = Ext.create('Ext.panel.Panel', {
        region: "center",
        layout: 'fit',
        tbar: [
            '-',
            { xtype: 'button', text: '编辑检索', iconCls: 'icon-search', handler: events.editClick },
            '-',
            { xtype: 'button', text: '导出', iconCls: 'icon-save', handler: events.save }
        ],
            html: '<iframe id="center" src="" scrolling="auto" frameborder=0 width=100% height=100%></iframe>'
                        });
//            departmentStore.on('load', function() { Ext.getCmp('CYC').setValue('quan'); });

                        //departmentStore.on('load', function() { Ext.getCmp('CYC').getValue(); });
                        Ext.create('Ext.container.Viewport', {
                            layout: 'border',
                            items: [panel],
                            renderTo: Ext.getBody()
                        });

                        events.search();

                    });