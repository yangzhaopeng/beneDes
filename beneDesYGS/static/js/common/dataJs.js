(function() {
    
    function ajax(method, url, params, success, error, notShowWaiting) {
        if (!notShowWaiting) Ext.Msg.wait('loading...');
        
        Ext.Ajax.request({
            url: url,
            method: method,
            params: params,
            
         success: function(response, options) {
                if (!notShowWaiting) Ext.Msg.hide();
                var obj = Ext.decode(response.responseText ? response.responseText : '{}');
                if (obj.success) {
                   success(obj.data, obj.msg || "", response);
                } else {
//                    if (error) error(obj.msg || "api is error！", obj, response);
//                    else Ext.Msg.alert('提示', obj.msg || "api is error！");
                     Ext.Msg.alert('提示', obj.msg);
                }
            },
            failure: function(response,options) {
               if (!notShowWaiting) Ext.Msg.hide();
//                if (error) error('网络连接错误！', null, response);
//                else Ext.Msg.alert('提示', '网络连接错误！');
            }
        });
    }
    
    window.dataJs = {
        get: function(url, params, success, error, notShowWaiting) {
            ajax('GET', url, params, success, error, notShowWaiting);
        },
        post: function(url, params, success, error, notShowWaiting) {
            ajax('POST', url, params, success, error, notShowWaiting);
        }
    };
})();