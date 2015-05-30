<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="cycfy1.aspx.cs" Inherits="beneDesCYC.view.month.cycfy1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
	<head id="Head1" runat="server">
		<title>采油厂费用</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<style type="text/css">BODY { MARGIN: 0px }
		</style>
		<LINK href="../../web/css.css" type="text/css" rel="stylesheet">
		<style type="text/css">
	       
	        A:hover { COLOR: #cc0104; TEXT-DECORATION: underline }
	        A:active { COLOR: #cc0001; TEXT-DECORATION: none }
	        .style7 { FONT-WEIGHT: bold; FONT-SIZE: 13px; COLOR: #33536a }
	        .txtbox { TEXT-ALIGN: right}
		</style>
	</head>
	<body style="OVERFLOW-X: hidden; WIDTH: 974px; HEIGHT: 716px;"  background=#FFFFFF  >
		<form id="Form1" method="post" runat="server">
			<table id="Table7" style="WIDTH: 724px; HEIGHT: 716px" cellSpacing="0" cellPadding="0"	width="724" border="0" align="center">
				<tr>
					<td style="width: 200px" valign="top" align ="left">
                       <table ><tr><td vAlign="bottom" style="height: 39px; width: 200px;">
                           </td></tr>
                       <tr><td style="width: 200px" >
                       <div style="border: 1px solid #83b1d3; overflow:auto; height: 650px; background-color: #FFFFFF; width: 164px;">
                            <asp:TreeView ID="TreeView1" runat="server" ImageSet="Faq" ShowLines="True"
                               ForeColor="RoyalBlue" MaxDataBindDepth="4" OnSelectedNodeChanged="TreeView1_SelectedNodeChanged">
                               <ParentNodeStyle Font-Bold="False" />
                               <HoverNodeStyle Font-Underline="True" ForeColor="Purple" />
                               <SelectedNodeStyle Font-Underline="True" HorizontalPadding="0px" VerticalPadding="0px" />
                               <NodeStyle Font-Names="Tahoma" Font-Size="8pt" ForeColor="DarkBlue" HorizontalPadding="5px"
                                   NodeSpacing="0px" VerticalPadding="0px" />
                           </asp:TreeView>
                       </div>
                       </td></tr>
                       </table>
                     </td>
					<td vAlign="top" align="center" style="width: 716px">
						<table id="Table10" cellSpacing="0" cellPadding="0" width="680" align="center" border="0">
							<tr>
								<td height="45" style="width: 684px">
									<table id="Table11" cellSpacing="0" cellPadding="0" width="100%" border="0">
										<tr>
											<td>&nbsp;</td>
											<td width="642">&nbsp;</td>
										</tr>
										<tr>
											<td width="39">
												<%--<div align="center"><IMG height="21" src="../../web/image/wh14.jpg" width="21"></div>--%>
											</td>
											<td class="css style7" align=left>
                                                <asp:Label ID="Labelid" runat="server" Visible="False"></asp:Label>
                                                <asp:Label ID="Labeltype" runat="server" Visible="False"></asp:Label></td>
										</tr>
										<tr>
											<td colSpan="2"><IMG height="2" src="../../web/image/wh13.jpg" width="680"></td>
										</tr>
									</table>
								</td>
							</tr>
							<tr>
								<td vAlign="bottom" style="width: 684px">
									<table id="Table12" cellSpacing="0" cellPadding="0" width="680" align="right" border="0">
										<tr>
											<td vAlign="bottom" width="50%" style="height: 39px" align="left"></td>
											<td width="70" style="height: 39px" align="left"></td>
											<td width="70" style="height: 39px" align="left"></td>
											<td width="70" style="height: 39px" align="left">
                                                <asp:FileUpload ID="FileUpload1" runat="server" Height="25px" /></td>
											<td width="70" style="height: 39px" align="left"><asp:imagebutton id="ImgButDr" runat="server" ImageUrl="/static/image/wh9.jpg" OnClick="ImgButDr_Click"></asp:imagebutton></td>
											<td width="70" style="height: 39px" align="left"><FONT face="宋体"><asp:imagebutton id="ImgButBc" runat="server" ImageUrl="/static/image/wh8.jpg" OnClick="ImgButBc_Click"></asp:imagebutton></FONT></td>
										</tr>
									</table>
								</td>
							</tr>
							<tr>
								<td vAlign="bottom" style="width: 684px">
									<table id="Table13" borderColor="#83b1d3" cellSpacing="1" cellPadding="1" width="100%"
										bgColor="#6b90ab" border="0">
										<tr>
											<td bgColor="#ebf0f4" 
                                                style="height: 600px; width: 680px; background-color: #FFFFFF;" valign="top">
												<table id="Table14" cellSpacing="0" cellPadding="0" width="96%" align="center" border="0" style="height: 396px">
                                                    <tr class="css">
                                                        <td align="left" colspan="1" rowspan="1" style="height: 29px">
                                                        </td>
                                                        <td align="left" colspan="1" rowspan="1" style="height: 29px">
                                                        </td>
                                                        <td align="left" colspan="2" rowspan="1" style="height: 29px; font-size: 9pt;" 
                                                            bordercolor="#ebf0f4">
                                                            年月—
                                                            <asp:TextBox ID="txtNY" runat="server" Width="80px"></asp:TextBox>
                                                            &nbsp;&nbsp; 当前编辑—<asp:TextBox
                                                                ID="txtID" runat="server" Width="180px"></asp:TextBox><FONT face="宋体">&nbsp;&nbsp;(费用单位：元)</FONT><asp:Label ID="labShow" runat="server" ForeColor="Red"></asp:Label></td>
                                                    </tr>
													<tr class="css">
                                                        <td align="left" colspan="1" rowspan="3" style="height: 311px" valign="top">
                                                        </td>
                                                        <td align="left" colspan="1" rowspan="3" style="height: 311px" valign="top">
                                                        </td>
														<td valign="top" style="height: 311px;" colspan="2" rowspan="1" align="left">
															<asp:DataList ID="DataList1" runat="server" Font-Bold="False" 
                                                                Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                                                                Font-Underline="False" Height="65px" RepeatColumns="3" 
                                                                RepeatDirection="Horizontal" Width="620px" Font-Size="9pt">
                                                                    <ItemTemplate >
                                                                        <table style="width: 197px; height: 30px">
                                                                            <tr class="css">
                                                                                <td align="right" style="width: 110px">
                                                                                    <asp:Label ID="labFN1" runat="server" Width="100px" Text='<%# DataBinder.Eval(Container.DataItem,"FEE_NAME") %>'></asp:Label></td>
                                                                                <td align="left" colspan="2" style="width: 110px">
                                                                                    <asp:TextBox ID="txtFEE1" runat="server" Width="100px" CssClass ="txtbox" Text='<%# DataBinder.Eval(Container.DataItem,"FEE") %>'></asp:TextBox></td>
                                                                            </tr>
                                                                        </table>
                                                                    </ItemTemplate>
                                                                </asp:DataList>
														 </td>
													</tr>
													</table>
											</td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
						<br>
					</td>
				</tr>
			</table>
</form>
	</body>
</HTML>

