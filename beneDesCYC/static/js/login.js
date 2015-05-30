var screenWidth = window.screen.width - 445;
var screenHeight = window.screen.height - 180;//全屏显示
function openFullScreen(url) {
    var date = new Date();
    if (window.name.indexOf("sunking") == -1) {
        var date = new Date();
        var name = "ldocean";
        window.opener = top;
        var windowParam = 'height=' + screen.availHeight + ',width=' + screen.availWidth + ',top=0,left=0,toolbar=no,menubar=no,scrollbars=no,resizable=no,location=no,status=no';
        var winFull = window.open(url, name, 'height=' + screen.availHeight + ', width=' + screen.availWidth + ', top=0, left=0, toolbar=no, menubar=no, scrollbars=no, resizable=no, location=no, status=no');
        winFull.focus();
        window.opener = top;
//        window.open("", "_self");
        window.close();
    }
}
Ext.onReady(function() {

    Ext.create('Ext.ux.form.MonthField', {
        renderTo: Ext.DomQuery.selectNode('#centerMonth'),
        id: 'startMonth',
        fieldLabel: '年&nbsp;&nbsp;&nbsp;&nbsp;月',
        editable: false,
        width: 210,
        left: 6,
        labelWidth: 50,
        labelAlign: 'right',
        value: new Date(),
        format: 'Ym'
    });

    checkCookie();
    setYearMonth();
    function login() {
        var name = Ext.DomQuery.selectNode('#userName').value;
        var word = Ext.DomQuery.selectNode('#word').value;
        //        var month = Ext.DomQuery.selectNode('#month').value;
        var month = Ext.Date.format(Ext.getCmp('startMonth').getValue(), 'Ym');
        if (!name || !word) {
            Ext.Msg.alert('提示', '请输入用户名、密码！');
            return;
        }

        if (!month) {
            Ext.Msg.alert('提示', '请输入年月！');
            return;
        }

        if (!/^20[0-9]{2}[01][0-9]$/.test(month)) {
            Ext.Msg.alert('提示', '年月输入错误！');
            return;
        }

        dataJs.get(
            'api/login.aspx',

            {
                name: name,
                word: word,
                month: month
            },

            function() {
                var rmbCheck = document.getElementById("rmbCheck");
                if (rmbCheck.checked == true) {
                    setCookie('cycusername', name, 'cycpassword', word, 365);
                }
                else {
                    //cleanCookie('username', 'password');
                }
               //    openFullScreen('index.aspx');
             window.location.href = 'index.aspx';
                //window.location.href = 'moduleIndex.aspx';
            }
        );
    }

    // 登陆按钮点击事件
    Ext.EventManager.on(Ext.DomQuery.selectNode('#login'), 'click', login);

    // 绑定键盘事件
    Ext.getBody().on('keypress', function(e) {
        if (e.getKey() == 13) {
            login();
        }
    });

});

//添加记住密码功能
function getCookie(c_name)      //根据分隔符每个变量的值
{
    if (document.cookie.length > 0) {
        c_start = document.cookie.indexOf(c_name + "=")
        if (c_start != -1) {
            c_start = c_start + c_name.length + 1;
            c_end = document.cookie.indexOf("^", c_start);
            if (c_end == -1)
                c_end = document.cookie.length;
            return unescape(document.cookie.substring(c_start, c_end));
        }
    }
    return "";
}

function setCookie(c_name, n_value, p_name, p_value, expiredays)        //设置cookie
{
    var exdate = new Date();
    exdate.setDate(exdate.getDate() + expiredays);
    document.cookie = c_name + "=" + escape(n_value) + "^" + p_name + "=" + escape(p_value) + ((expiredays == null) ? "" : "^;expires=" + exdate.toGMTString());
}

function checkCookie()      //检测cookie是否存在，如果存在则直接读取，否则创建新的cookie
{
  
    var username = getCookie('cycusername');
    var password = getCookie('cycpassword');
    if (username != null && username != "" && password != null && password != "") {
        var rmbCheck = document.getElementById("rmbCheck");
        rmbCheck.checked = "checked"
        Ext.DomQuery.selectNode('#userName').value = username;
        //   alert('Your name: ' + username + '\n' + 'Your password: ' + password);
        Ext.DomQuery.selectNode('#word').value = password;
    }
    else {
        //                        username = "ygs";
        //                     password = "ygs";
        //                     if (username != null && username != "" && password != null && password != "") {
        //                         setCookie('username', username, 'password', password, 365);
        //                     }
    }
}

function cleanCookie(c_name, p_name) {     //使cookie过期
    document.cookie = c_name + "=" + ";" + p_name + "=" + ";expires=Thu, 01-Jan-70 00:00:01 GMT";
}
