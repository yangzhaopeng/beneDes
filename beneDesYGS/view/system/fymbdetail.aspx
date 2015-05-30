<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="fymbdetail.aspx.cs" Inherits="beneDesYGS.view.system.fymbdetail" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
	<head>
		<title>费用模扳明细</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR"/>
		<meta content="C#" name="CODE_LANGUAGE"/>
		<meta content="JavaScript" name="vs_defaultClientScript"/>
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema"/>
		<style type="text/css">BODY { MARGIN: 0px }
		</style>
		
		<script type="text/javascript">
		     function ClickEvent(a)
		    {
		        document.getElementById("test").value = a;
		        
		    }

		</script>
	</head>
	<body style="overflow: hidden; WIDTH: 974px; HEIGHT: 720px; text-align:center;background-image: url(../web/image/ibj1-1.JPG)">
		<form id="Form1" method="post" runat="server">
            <table id="Table7" style="WIDTH: 750px; HEIGHT: 716px" cellspacing="0" cellpadding="0"	width="724" border="0" >
                 <tr><td  valign="top" align="center"  style="height: 700px; width: 716px;">
                 <table style="width: 750px" cellspacing="0px" cellpadding="0">
                <tr>
                    <td colspan="2" style="height: 45px">
                        <table id="Table11" cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>						
                                                <td  style="width: 300px; text-align:right">
                                                    &nbsp;<asp:Label ID="labShow" runat="server" ForeColor="Red" Width="250px" Font-Size="X-Small"></asp:Label></td>
										</tr>
									</table>
                    </td>
                </tr>
                <tr>
								<td  style="width: 611px; height: 39px; vertical-align:bottom" colspan="2">
									<table id="Table12" cellspacing="0" cellpadding="0" width="740" style=" text-align:right"  border="0">
										<tr>
											<td style="width: 95px; vertical-align:bottom; height: 39px;" align="left">&nbsp;</td>
											<td style="height: 30px; width: 393px;" valign="middle">
                                                <asp:Label ID="Label2" runat="server" Font-Size="10pt" ForeColor="Red" Height="15px"
                                                    Text="您选择修改的模板为" Width="151px"></asp:Label><asp:TextBox ID="test" runat="server"></asp:TextBox></td>
                                            <td style="height: 30px; width:70px">&nbsp;</td>
											<td  style="height: 30px; width:70px">&nbsp;</td>
										</tr>
									</table>
								</td>
							</tr>
                <tr>
                    <td style="width: 237px" valign="top" >
                        <table width="180px" cellpadding="0" cellspacing="0">
                            <tr>
                                <td style="width: 180px;background:#EBF0F4" align="center">
                                    <div style="border-style:solid;border-width:1px; border-color:#83b1d3; height: 418px; "  >
                                        <br />
                                        <asp:Label ID="Label1" runat="server" CssClass="css style7" Font-Size="11pt" Height="35px"
                                            Text="请选择费用大类" Width="116px"></asp:Label><br />
                                        <asp:RadioButtonList ID="listbox1" runat="server" AutoPostBack="True" CssClass="css"
                                            Font-Size="10pt" OnSelectedIndexChanged="listbox1_SelectedIndexChanged1">
                                        </asp:RadioButtonList></div>
                                </td>
                             </tr>
                        </table>
                    </td>
                    <td style="width: 393px" valign="top">
                        <table class="css" style="width: 522px; height: 314px">
                                
							<tr>
								<td  style="width: 555px; height: 298px; vertical-align:top">
									<table id="Table13"  style="border-color:#83b1d3; background-color:#6b90ab;width: 555px" cellspacing="0" cellpadding="0"
										 border="1">
										<tr>
											<td style=" height: 150px; width: 538px;background-color:#ebf0f4" valign="top">
											    <div style="overflow:auto;height: 150px; width: 555px;">
                                                <asp:CheckBoxList ID="chklist1" runat="server" CssClass="css" Font-Size="Smaller" Height="129px"
                                                    RepeatColumns="3" RepeatDirection="Horizontal" Width="497px">
                                                </asp:CheckBoxList>
                                                </div>
                                          </td>
										</tr>
									</table>
									<br />
									<div style="overflow:auto; height: 350px; width: 555px; text-align:left" >
                                    <asp:GridView ID="DG" runat="server"  AutoGenerateColumns="False" BackColor="#EBF0F4"
                                        BorderColor="#83B1D3"  HorizontalAlign="Center"  OnRowDataBound="DG_RowDataBound" 
                                         Width="555px">
                                        <Columns>
                                             <asp:BoundField DataField="templet_code" HeaderText="模板编码">
                                                <ItemStyle BorderColor="#83B1D3" BorderStyle="Solid" Font-Size="Smaller" Height="40px"
                                                    HorizontalAlign="Center" Width="200px" />
                                                <HeaderStyle BorderColor="#83B1D3" BorderStyle="Solid" Font-Size="Smaller" Height="40px"
                                                    Width="200px" />
                                                <ControlStyle BorderColor="#83B1D3" BorderStyle="Solid" />
                                            </asp:BoundField>
                                           <asp:BoundField DataField="templet_name" HeaderText="模板名称">
                                                <ItemStyle BorderColor="#83B1D3" BorderStyle="Solid" Font-Size="Smaller" Height="40px"
                                                    HorizontalAlign="Center" Width="350px" />
                                                <HeaderStyle BorderColor="#83B1D3" BorderStyle="Solid" Font-Size="Smaller" Height="40px"
                                                    Width="200px" />
                                                <ControlStyle BorderColor="#83B1D3" BorderStyle="Solid" />
                                            </asp:BoundField>
                                           <asp:BoundField DataField="templet_type" HeaderText="维护类型">
                                                <ItemStyle BorderColor="#83B1D3" BorderStyle="Solid" Font-Size="Smaller" Height="40px"
                                                    HorizontalAlign="Center" Width="150px" />
                                                <HeaderStyle BorderColor="#83B1D3" BorderStyle="Solid" Font-Size="Smaller" Height="40px"
                                                    Width="200px" />
                                                <ControlStyle BorderColor="#83B1D3" BorderStyle="Solid" />
                                            </asp:BoundField>
                                           <asp:BoundField DataField="use_type" HeaderText="应用类型">
                                                <ItemStyle BorderColor="#83B1D3" BorderStyle="Solid" Font-Size="Smaller" Height="40px"
                                                    HorizontalAlign="Center" Width="200px" />
                                                <HeaderStyle BorderColor="#83B1D3" BorderStyle="Solid" Font-Size="Smaller" Height="40px"
                                                    Width="200px" />
                                                <ControlStyle BorderColor="#83B1D3" BorderStyle="Solid" />
                                            </asp:BoundField>
                                         <asp:BoundField DataField="templet_level" HeaderText="模板级别">
                                                <ItemStyle BorderColor="#83B1D3" BorderStyle="Solid" Font-Size="Smaller" Height="40px"
                                                    HorizontalAlign="Center" Width="100px" />
                                                <HeaderStyle BorderColor="#83B1D3" BorderStyle="Solid" Font-Size="Smaller" Height="40px"
                                                    Width="200px" />
                                                <ControlStyle BorderColor="#83B1D3" BorderStyle="Solid" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="templet_tag" HeaderText="应用对象">
                                                <ItemStyle BorderColor="#83B1D3" BorderStyle="Solid" Font-Size="Smaller" Height="40px"
                                                    HorizontalAlign="Center" Width="200px" />
                                                <HeaderStyle BorderColor="#83B1D3" BorderStyle="Solid" Font-Size="Smaller" Height="40px"
                                                    Width="200px" />
                                                <ControlStyle BorderColor="#83B1D3" BorderStyle="Solid" />
                                            </asp:BoundField>
                                           
                                        </Columns>
                                    </asp:GridView>
									<asp:Button ID="xsfeiyong" runat="server" Text="显示包含费用" onclick="xsfeiyong_Click" style="display: none;" />
									<asp:Button ID="ImgButBc" runat="server" Text="保存" onclick="ImgButBc_Click" style="display: none;" />
									</div>
								</td>
							</tr>
                        
                        </table>
                    </td>
                </tr>
            </table>
            </td></tr>
            </table>
</form>
	</body>
</html>

