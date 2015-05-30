Ext.onReady(function() {
 
    //---------------头部------------------------
    var header = new Ext.Panel({
        frame: false,
        border: false,
        region: 'north',
        autoHeight: true,
        html: '<div class="header-container"><div class="header"></div></div>'
        ,bbar: [
            '&nbsp;<img src="static/image/icon/用户2.png" height="16px" width="16px"><b> <span style="color:#2c6db0">' + userName + '&nbsp;&nbsp; （年月：' + month + '）</span>', 
            '->'
//            , 
//            { 
//                text: '首&nbsp页', 
//                icon: 'static/image/icon/首页.png', 
//                handler: function() { 
//                    window.location.href = "index.aspx";
//                }
//            }, 
//            { 
//                text: '退&nbsp出',
//                icon: 'static/image/icon/退出.png', 
//                handler: function() {
//                    location.href="logout.aspx";
//                }
//            }
        ]
    });
    
    //---------------功能菜单------------------
    var west;
    
    //-------------------中心tab页-----------------
    var center = new Ext.TabPanel({
        region: "center",
        activeItem: 0,
        margin: '2 2 2 2',
        border: '0 0 0 0',
        enableTabScroll: true,
        layoutOnTabChange: true,
        items: [{
            xtype: "panel",
            id: "index",
            title: "&nbsp&nbsp首&nbsp&nbsp页&nbsp&nbsp",
            html: "<iframe id='center' src='welcome.aspx' scrolling='auto' frameborder=0 width=100% height=100%></iframe>"
        }]
    });
    
    // 树节点的点击事件
    function treeNodeClick (view, record, item, index, e){
        if (record.childNodes.length > 0) {
            if (!record.isExpanded()) record.expand();
            else record.collapse();
        } else {
            var url = record.raw.url;
            if (!url) url = '404.aspx';
            
            var tab = center.getComponent("tab_default");
            if (tab) {
               center.remove(tab);
            }
            tab = center.add({
                id: "tab_default",
                xtype: "panel",
                title: record.raw.text,
                closable: true,
                html: "<iframe id='center_content' src=" + url + " scrolling='auto' frameborder=0 width=100% height=100%></iframe>"
            });
            center.setActiveTab(tab);
        }
    }
    
    // 生成导航目录
    function addMenu(data) {
        var items = [];
        
        for (var i = 0; i < data.length; i++) {
            var store = {
                root: {
                    expanded: true,
                    children: data[i].children
                }
            };
            var item = Ext.create('Ext.tree.Panel', {
                title: data[i].text,
                store: store,
                rootVisible: false,
                height: '100%',
                width: 200,
                margin: '0 0 0 0',
                autoScroll: true,
                border: false,
                lines: false,
                singleExpand: true
//               , useArrows: true
            });
            item.on('itemclick', treeNodeClick);
            items.push(item);
        }
        
        west = new Ext.Panel({
//            collapsible: true,
            border: true,
            margins: '2 2 2 2',
             width: '16%',
            id: 'west',
            layout: "accordion",
            region: "west",
            title: '功能菜单',
            split:true,
            items: items
        });
    }
    
    // 渲染整个页面
    function renderAll() {
        Ext.create('Ext.container.Viewport', {
            layout: 'border',
            items: [header, west, center],
            renderTo: Ext.getBody()
        });
    }
    
    // 异步请求导航目录数据，并渲染整个页面
    dataJs.get(
        'api/system/menu.aspx', 
        
        {
            _type: 'getUserMenus'
        },
        
        function(data, msg, response) {
            addMenu(data);
            renderAll();
        }
    );
    
});