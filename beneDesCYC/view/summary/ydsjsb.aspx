<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ydsjsb.aspx.cs" Inherits="beneDesCYC.view.summary.ydsjsb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>月度数据上报</title>
</head>
<body style="width: 974px; text-align: center;" background="../../static/image/bg.jpg">
    <form id="form1" runat="server">
    <div>
    </div>
    <table style="width: 685px">
        <tr>
            <td style="width: 685px; height: 35px; text-align: left;">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td style="width: 685px; height: 20px; text-align: left">
                <asp:Label ID="Label5" runat="server" Font-Size="Smaller" Text="Label" ForeColor="Red"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 685px; height: 426px; text-align: left; vertical-align: top">
                <asp:Panel ID="Panel1" runat="server" Width="685px">
                    <div style="text-align: left">
                        <table style="width: 100%">
                            <tr>
                                <td colspan="2" style="height: 28px">
                                    <strong><span style="font-size: 10pt">&nbsp;&nbsp; 步骤列表:</span></strong>
                                </td>
                                <td style="width: 500px; height: 28px;">
                                </td>
                                <td style="width: 10px; height: 28px;">
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 5px; height: 26px;">
                                </td>
                                <td style="width: 161px; height: 26px; vertical-align: top">
                                    <span style="font-size: 10pt">上报单井数据</span>
                                </td>
                                <td style="width: 500px; height: 26px; vertical-align: top">
                                    <asp:Label ID="Label6" runat="server" Font-Size="Smaller" ForeColor="Red" Text="Label"></asp:Label>
                                </td>
                                <td style="width: 10px; height: 26px;">
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 5px; height: 26px;">
                                </td>
                                <td style="width: 161px; height: 26px; vertical-align: top">
                                    <span style="font-size: 10pt">上报开发数据</span>
                                </td>
                                <td style="width: 500px; height: 26px; vertical-align: top">
                                    <asp:Label ID="Label7" runat="server" Font-Size="Smaller" ForeColor="Red" Text="Label"></asp:Label>
                                </td>
                                <td style="width: 10px; height: 26px;">
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 5px; height: 26px;">
                                </td>
                                <td style="width: 161px; height: 26px; vertical-align: top">
                                    <span style="font-size: 10pt">上报单井月度数据</span>
                                </td>
                                <td style="width: 500px; height: 26px; vertical-align: top">
                                    <asp:Label ID="Label1" runat="server" Font-Size="Smaller" Text="Label1" ForeColor="Red"></asp:Label>
                                </td>
                                <td style="width: 10px; height: 26px;">
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 5px; height: 26px;">
                                </td>
                                <td style="width: 161px; height: 26px; vertical-align: top">
                                    <span style="font-size: 10pt">上报评价单元月度数据</span>
                                </td>
                                <td style="width: 500px; height: 26px; vertical-align: top">
                                    <asp:Label ID="Label2" runat="server" Font-Size="Smaller" Text="Label" ForeColor="Red"></asp:Label>
                                </td>
                                <td style="width: 10px; height: 26px;">
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 5px; height: 26px;">
                                </td>
                                <td style="width: 161px; height: 26px; vertical-align: top">
                                    <span style="font-size: 10pt">上报区块月度数据</span>
                                </td>
                                <td style="width: 500px; height: 26px; vertical-align: top">
                                    <asp:Label ID="Label3" runat="server" Font-Size="Smaller" Text="Label" ForeColor="Red"></asp:Label>
                                </td>
                                <td style="width: 10px; height: 26px;">
                                </td>
                            </tr>
                            <%-- <tr>
                                    <td style="width: 5px; height: 26px">
                                    </td>
                                    <td style="width: 161px; height: 26px;vertical-align :top">
                                        <span style="font-size: 10pt">上报月度参数数据</span></td>
                                    <td style="width: 500px; height: 26px;vertical-align :top ">
                                        <asp:Label ID="Label4" runat="server" Font-Size="Smaller" ForeColor="Red" Text="Label"></asp:Label></td>
                                    <td style="width: 10px; height: 26px">
                                    </td>
                                </tr>--%>
                            <tr>
                                <td style="width: 5px">
                                </td>
                                <td style="width: 161px">
                                </td>
                                <td style="width: 500px">
                                </td>
                                <td style="width: 10px">
                                </td>
                            </tr>
                        </table>
                    </div>
                </asp:Panel>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
