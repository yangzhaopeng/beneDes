<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="jianbao.aspx.cs" Inherits="beneDesYGS.view.oilAssessment.jianbao" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>无标题页</title>
</head>
<body  background="/static/image/bg.jpg";>
    <form id="form1" runat="server" style="text-align :center ">
    <div>
    
    </div>
        <table style="width: 690px">
            <tr>
                <td style="width: 460px; height: 26px; text-align: left; vertical-align :middle ;">
                    &nbsp;</td>
                    
                    <td style="width: 660px; height: 26px; text-align: left; vertical-align :middle ;">
                    <asp:Label ID="Lbcb" runat="server" Font-Size="8pt" ForeColor="Blue" Text="特高成本="></asp:Label>
                    <asp:TextBox ID="Tbtgcb" runat="server" Width="60px"></asp:TextBox>
                    </td>
                <td style="width: 100px; height: 26px; text-align: left">
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="width: 260px; height: 17px; text-align: left">
                    <asp:Label ID="Label1" runat="server" ForeColor="Red" Text="Label" Font-Size="10pt"></asp:Label></td>
                <td style="width: 430px; height: 17px; text-align: left">
                </td>
            </tr>
            <tr>
                <td style="vertical-align: top; height: 426px; text-align: left" colspan="2">
                    <asp:HyperLink ID="HyperLink1" runat="server" Font-Size="10pt">效益评价简报（点击保存）</asp:HyperLink></td>
            </tr>
        </table>
    </form>
</body>
</html>
