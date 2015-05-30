<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ydsjinit.aspx.cs" Inherits="beneDesCYC.view.month.ydsjinit" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">



<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>月度数据汇总--采油厂级月度数据</title>
    </head>
<body style="overflow: hidden;  WIDTH: 950px; HEIGHT: 665px; text-align:center; background-image:url(/static/image/bg.jpg);">
		<form id="Form1" method="post" runat="server">
			<table id="Table7" style=" WIDTH: 700px; HEIGHT: 665px" cellspacing="0" cellpadding="0" border="0">
				<tr>
					<td valign="top" align="center" style="width: 716px; ">
        <table style="width: 630px" >
            <tr>
                <td style="width: 630px; height: 17px; text-align: left">
                    <asp:Label ID="Label13" runat="server" Font-Size="10pt" ForeColor="Red" Text="Label"></asp:Label></td>
            </tr>
            <tr>
                <td style="width: 630px; height: 426px; text-align: left; vertical-align : top">
                    <asp:Panel ID="Panel1" runat="server"  Width="630px">
                        <div style="text-align: left; ">
                            <table style="width: 100%;border-color:#83b1d3;" border="0">
                                <tr>
                                    <td style="height: 28px; vertical-align :top ">
                                        <strong><span style="font-size: 10pt"> 初始化项目选择:</span></strong></td>
                                    <td style="width: 450px; height: 28px;">
                                        <%--<asp:Button ID="Btnall" runat="server" Font-Size="Smaller" OnClick="Btnall_Click"
                                            Text="全选" Width="60px" />
                                        &nbsp;
                                        <asp:Button ID="Btncanc" runat="server" Font-Size="10pt" OnClick="Btncanc_Click"
                                            Text="取消" Width="60px" />
                                        &nbsp;
                    <asp:Button ID="Btnqd" runat="server" Text="初始化" Width="60px" OnClick="Btnqd_Click" Font-Size="10pt" />--%>
                    
                                        <asp:Label ID="thisSE" runat="server" Font-Size="Smaller" ForeColor="Red" 
                                            Text="Label" Visible="false" ></asp:Label>
                    
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 220px; height: 26px;vertical-align :top">
                                        <span style="font-size: 10pt">
                                            <asp:CheckBox ID="CheckBox1" runat="server" BorderColor="Red" Text="油井月度基础信息" /></span></td>
                                    <td style="width: 450px; height: 26px;">
                                    <asp:Label ID="Label1" runat="server" Font-Size="Smaller" Text="Label1" ForeColor="Red"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td style="width: 220px; height: 26px;vertical-align :top">
                                        <span style="font-size: 10pt">
                                            <asp:CheckBox ID="CheckBox6" runat="server" BorderColor="Red" Text="气井月度基础信息" /></span></td>
                                    <td style="width: 450px; height: 26px;">
                                    <asp:Label ID="Label6" runat="server" Font-Size="Smaller" Text="Label1" ForeColor="Red"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td style="width: 220px; height: 26px;vertical-align :top">
                                        <span style="font-size: 10pt">
                                            <asp:CheckBox ID="CheckBox2" runat="server" Text="月度油藏评价单元信息" /></span></td>
                                    <td style="width: 450px; height: 26px;">
                                    <asp:Label ID="Label2" runat="server" Font-Size="Smaller" Text="Label" ForeColor="Red"></asp:Label></td>
                                </tr>
                                 <tr>
                                    <td style="width: 220px; height: 26px;vertical-align :top">
                                        <span style="font-size: 10pt">
                                            <asp:CheckBox ID="CheckBox8" runat="server" Text="月度气藏评价单元信息" /></span></td>
                                    <td style="width: 450px; height: 26px;">
                                    <asp:Label ID="Label8" runat="server" Font-Size="Smaller" Text="Label" ForeColor="Red"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td style="width: 220px; height: 26px;vertical-align :top">
                                        <span style="font-size: 10pt">
                                            <asp:CheckBox ID="CheckBox3" runat="server" Text="月度油藏区块信息" /></span></td>
                                    <td style="width: 450px; height: 26px;">
                                    <asp:Label ID="Label3" runat="server" Font-Size="Smaller" Text="Label" ForeColor="Red"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td style="width: 220px; height: 26px;vertical-align :top">
                                        <span style="font-size: 10pt">
                                            <asp:CheckBox ID="CheckBox9" runat="server" Text="月度气藏区块信息" /></span></td>
                                    <td style="width: 450px; height: 26px;">
                                    <asp:Label ID="Label9" runat="server" Font-Size="Smaller" Text="Label" ForeColor="Red"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td style="width: 220px; height: 26px;vertical-align :top">
                                        <span style="font-size: 10pt">
                                            <asp:CheckBox ID="CheckBox4" runat="server" Text="月度输差信息" /></span></td>
                                    <td style="width: 450px; height: 26px;">
                                    <asp:Label ID="Label4" runat="server" Font-Size="Smaller" Text="Label" ForeColor="Red"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td style="width: 220px; height: 26px;vertical-align :top">
                                        <span style="font-size: 10pt">
                                            <asp:CheckBox ID="CheckBox5" runat="server" Text="月度商品率信息" /></span></td>
                                    <td style="width: 450px; height: 26px;">
                                    <asp:Label ID="Label5" runat="server" Font-Size="Smaller" Text="Label" ForeColor="Red"></asp:Label></td>
                                </tr>
                               <%-- <tr>
                                    <td style="width: 5px; height: 26px">
                                    </td>
                                    <td style="width: 220px; height: 26px;vertical-align :top">
                                        <span style="font-size: 10pt">
                                            <asp:CheckBox ID="CheckBox6" runat="server" Text="初始化月度销售参数" /></span></td>
                                    <td style="width: 450px; height: 26px">
                                        <asp:Label ID="Label6" runat="server" Font-Size="10pt" ForeColor="Red" Text="Label"></asp:Label></td>
                                    <td style="width: 10px; height: 26px">
                                    </td>
                                </tr>--%>
                                <tr>
                                    <td style="width: 220px; height: 26px;vertical-align :top">
                                        <span style="font-size: 10pt">
                                            <asp:CheckBox ID="CheckBox7" runat="server" Text="月度天然气体积换算系数" /></span></td>
                                    <td style="width: 450px; height: 26px">
                                        <asp:Label ID="Label7" runat="server" Font-Size="10pt" ForeColor="Red" Text="Label"></asp:Label></td>
                                </tr>
                                <%--<tr>
                                    <td style="width: 5px; height: 26px">
                                    </td>
                                    <td style="width: 220px; height: 26px;vertical-align :top">
                                        </td>
                                    <td style="width: 450px; height: 26px">
                                        </td>
                                    <td style="width: 10px; height: 26px">
                                    </td>
                                </tr>--%>
                                <%--<tr>
                                    <td style="width: 5px; height: 26px">
                                    </td>
                                    <td style="width: 220px; height: 26px;vertical-align :top">
                                        <span style="font-size: 10pt">
                                            <asp:CheckBox ID="CheckBox8" runat="server" Text="初始化月度吨桶比信息" /></span></td>
                                    <td style="width: 450px; height: 26px">
                                        <asp:Label ID="Label8" runat="server" Font-Size="10pt" ForeColor="Red" Text="Label"></asp:Label></td>
                                    <td style="width: 10px; height: 26px">
                                    </td>
                                </tr>--%>
                                <%--<tr>
                                    <td style="width: 5px; height: 26px">
                                    </td>
                                    <td style="width: 220px; height: 26px;vertical-align :top">
                                        <span style="font-size: 10pt">
                                            <asp:CheckBox ID="CheckBox9" runat="server" Text="初始化月度人民币汇率" /></span></td>
                                    <td style="width: 450px; height: 26px">
                                        <asp:Label ID="Label9" runat="server" Font-Size="10pt" ForeColor="Red" Text="Label"></asp:Label></td>
                                    <td style="width: 10px; height: 26px">
                                    </td>
                                </tr>--%>
                                <%--<tr>
                                    <td style="width: 5px; height: 26px">
                                    </td>
                                    <td style="width: 220px; height: 26px;vertical-align :top">
                                        <span style="font-size: 10pt">
                                            <asp:CheckBox ID="CheckBox10" runat="server" Text="初始化月度期间勘探费" /></span></td>
                                    <td style="width: 450px; height: 26px">
                                        <asp:Label ID="Label10" runat="server" Font-Size="10pt" ForeColor="Red" Text="Label"></asp:Label></td>
                                    <td style="width: 10px; height: 26px">
                                    </td>
                                </tr>--%>
                                <tr>
                                    <td style="width: 220px; height: 26px;vertical-align :top">
                                        <span style="font-size: 10pt">
                                            </span></td>
                                    <td style="width: 450px; height: 26px">
                                        </td>
                                </tr>
                                <tr>
                                    <td style="width: 220px; height: 26px;vertical-align :top">
                                        <span style="font-size: 10pt"></span>
                                    </td>
                                    <td style="width: 450px; height: 26px">
                                    </td>
                                </tr>
                            </table>
                            
                            <asp:Button ID="QX" runat="server" Text="全选" onclick="QX_Click" style="display: none;" />
								
							<asp:Button ID="CX" runat="server" Text="取消" onclick="CX_Click" style="display: none;" />
                            
                            <asp:Button ID="CSH" runat="server" Text="初始化" onclick="CSH_Click" style="display: none;" />
                        </div>
                    </asp:Panel>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>


