<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="qkft.aspx.cs" Inherits="beneDesCYC.view.month.qkft" %>

<%@ Register Assembly="FarPoint.Web.Spread" Namespace="FarPoint.Web.Spread" TagPrefix="FarPoint" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>区块分摊</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR" />
    <meta content="C#" name="CODE_LANGUAGE" />
    <meta content="JavaScript" name="vs_defaultClientScript" />
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema" />
    <style type="text/css">
        BODY
        {
            margin: 0px;
        }
    </style>
    <link href="css.css" type="text/css" rel="stylesheet" />
    <style type="text/css">
        A:link
        {
            color: #ffffff;
            text-decoration: none;
        }
        A:visited
        {
            color: #ffffff;
            text-decoration: none;
        }
        A:hover
        {
            color: #cc0104;
            text-decoration: underline;
        }
        A:active
        {
            color: #cc0001;
            text-decoration: none;
        }
    </style>
</head>
<body style="overflow: hidden; width: 100%; height: 100%; text-align: center; background-image: url(/static/image/bg.jpg)">
    <form id="Form1" method="post" runat="server">
    <table id="Table7" style="width: 100%; height: 100%; text-align: center" cellspacing="0"
        cellpadding="0" border="0">
        <tr>
            <td valign="top" align="center" style="width: 100%">
                <table id="Table10" cellspacing="0" cellpadding="0" width="100%" style="text-align: center"
                    border="0">
                    <tr>
                        <td style="width: 684px; height: 20px; vertical-align: top">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100%; vertical-align: top">
                            <table id="Table13" style="border-color: #FFFFFF; background-color: #FFFFFF; height: 57px;"
                                cellspacing="1" cellpadding="1" width="100%" border="0">
                                <tr>
                                    <td style="background-color: #FFFFFF; vertical-align: top">
                                        <table id="Table14" cellspacing="0" cellpadding="0" style="text-align: center" border="0">
                                            <tr class="css">
                                                <td style="height: 19px">
                                                    <div style="text-align: left; font-size: medium; height: 11px;">
                                                        选择区块：</div>
                                                </td>
                                            </tr>
                                            <tr class="css">
                                                <td style="height: 19px; text-align: left" valign="middle">
                                                </td>
                                            </tr>
                                            <tr class="css">
                                                <td style="height: 19px; text-align: left" valign="middle">
                                                    <asp:CheckBoxList ID="qkchk" runat="server" Height="21px" RepeatDirection="Horizontal"
                                                        RepeatColumns="5" Font-Size="Small">
                                                    </asp:CheckBoxList>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 20px; width: 684px;">
                        </td>
                    </tr>
                </table>
                <asp:Button ID="QX" runat="server" Text="全选" OnClick="QX_Click" Style="display: none;" />
                <%--<asp:Button ID="CX" runat="server" Text="取消" onclick="CX_Click" style="display: none;" />--%>
                <asp:Button ID="FT" runat="server" Text="分摊" OnClick="FT_Click" Style="display: none;" />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
