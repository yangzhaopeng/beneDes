<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="beneDesYGS.login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <title>����Ч������ϵͳBeneDes V2.0���͹�˾��</title>
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
                                <li>��&nbsp;&nbsp;&nbsp;&nbsp;����<input type="text" id="userName" name="�û���" /></li>
                                <li>��&nbsp;&nbsp;&nbsp;&nbsp;�룺<input name="����" id="word" type="password" /></li>
                                <li style="text-align: left; margin-left: 3px">
                                    <div id="centerMonth">
                                    </div>
                                </li>
                                <li style="text-align: right">
                                    <input type="checkbox" id="rmbCheck" name="��ס����" style="width: auto; vertical-align: middle;
                                        border-width: 0" />
                                    <span style="vertical-align: middle">��ס����</span> </li>
                                <li>
                                    <input type="button" id="login" class="denglu" />
                                </li>
                                <li></li>
                                <li class="intention">ע�⣺����������������û���Ϣ������ϵ����Ա���������</li>
                            </ul>
                        </div>
                        <div class="loginboxr">
                        </div>
                    </div>
                    <div class="clear">
                    </div>
                    <div class="footer">
                        ��̩����ʯ����Ȼ����������ɷ����޹�˾��Ȩ����</div>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
