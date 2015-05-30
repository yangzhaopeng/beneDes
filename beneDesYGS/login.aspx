<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="beneDesYGS.login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <title>单井效益评价系统BeneDes V2.0（油公司）</title>
    <style type="text/css">
        body
        {
            margin-left: 0px;
            margin-top: 0px;
            margin-right: 0px;
            margin-bottom: 0px;
        }
    </style>
    <link href="static/Extjs/resources/css/ext-all.css" rel="stylesheet" type="text/css" />
    <link href="static/css/login.css" rel="stylesheet" type="text/css" />

    <script src="static/Extjs/ext-all.js" type="text/javascript"></script>

    <script src="static/js/common/dataJs.js" type="text/javascript"></script>

    <script src="static/Extjs/locale/ext-lang-zh_CN.js" type="text/javascript"></script>

    <script src="static/js/login.js" type="text/javascript"></script>

    <script src="static/Extjs/YearMonthDayCombo.js" type="text/javascript"></script>

    <script src="static/Extjs/MonthField.js" type="text/javascript"></script>

</head>
<body>
    <form id="form1" runat="server">
    <div class="main">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td align="center">
                    <div class="logo">
                    </div>
                    <div class="greenbar">
                    </div>
                    <div class="qinghai">
                    </div>
                    <div class="loginbox">
                        <div class="loginboxl">
                        </div>
                        <div class="loginboxm">
                            <div class="logintitle">
                            </div>
                            <div class="hrbg">
                            </div>
                            <ul class="shuru">
                                <li>用&nbsp;&nbsp;&nbsp;&nbsp;户：<input type="text" id="userName" name="用户名" /></li>
                                <li>密&nbsp;&nbsp;&nbsp;&nbsp;码：<input name="密码" id="word" type="password" /></li>
                                <li style="text-align: left; margin-left: 3px">
                                    <div id="centerMonth">
                                    </div>
                                </li>
                                <li style="text-align: right">
                                    <input type="checkbox" id="rmbCheck" name="记住密码" style="width: auto; vertical-align: middle;
                                        border-width: 0" />
                                    <span style="vertical-align: middle">记住密码</span> </li>
                                <li>
                                    <input type="button" id="login" class="denglu" />
                                </li>
                                <li></li>
                                <li class="intention">注意：如果您忘记了您的用户信息，请联系管理员帮您解决。</li>
                            </ul>
                        </div>
                        <div class="loginboxr">
                        </div>
                    </div>
                    <div class="clear">
                    </div>
                    <div class="footer">
                        恒泰艾普石油天然气技术服务股份有限公司版权所有</div>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
