Ext.onReady(function() {
    
    var commonLoading = new Ext.LoadMask(Ext.getBody(), { msg: "正在努力加载中..." });

    var Loading = {
        show: function() {
            commonLoading.show();
        },

        hide: function() {
            commonLoading.hide();
        },

        // 监听页面中的某iframe的load事件，实现hide loading
        onIfrmaLoad: function(ifrmaeId) {
            Loading.show();

            var ele = Ext.DomQuery.selectNode('#' + ifrmaeId);

            function loaded() {
                Loading.hide();
                Ext.EventManager.un(ele, 'load', loaded);
            }

            Ext.EventManager.on(ele, 'load', loaded);
        }
    };

    window.Loading = Loading;
});