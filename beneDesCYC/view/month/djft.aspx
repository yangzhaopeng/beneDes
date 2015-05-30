<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="djft.aspx.cs" Inherits="beneDesCYC.view.month.djft" %>

<%@ Register Assembly="FarPoint.Web.Spread" Namespace="FarPoint.Web.Spread" TagPrefix="FarPoint" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>单井分摊</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR"/>
		<meta content="C#" name="CODE_LANGUAGE"/>
		<meta content="JavaScript" name="vs_defaultClientScript"/>
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema"/>
		 <script src="../../static/Extjs/MonthField.js" type="text/javascript"></script>
		<style type="text/css">BODY { MARGIN: 0px }
		</style>
		<link href="css.css" type="text/css" rel="stylesheet"/>
		<style type="text/css">
	A:hover { COLOR: #cc0104; TEXT-DECORATION: underline }
	A:active { COLOR: #cc0001; TEXT-DECORATION: none }
	        .style1
            {
                height: 2px;
                width: 458px;
            }
		</style>
   
	</head>
	<body style="overflow: hidden; WIDTH: 974px; HEIGHT: 700px; text-align:center;background-image:url(/static/image/bg.jpg)" >
		<form id="Form1" method="post" runat="server">
			<table id="Table7" style="WIDTH: 710px; HEIGHT: 700px; text-align:center" cellspacing="0" cellpadding="0"
				width="705" border="0">
				<tr>
					<td valign="top" align="center"  style="width: 716px">
						<table id="Table10" cellspacing="0" cellpadding="0" width="680" 
                            style="text-align:center; background-color: #FFFFFF;"  border="0">
							<tr>
								<td style="width: 684px; height: 45px;">
									&nbsp;</td>
							</tr>
							<tr>
								<td  style="width: 684px; vertical-align:top; ">
									<table id="Table13"  style="border-color:#FFFFFF; background-color:#FFFFFF" cellspacing="1" cellpadding="1" width="100%"
										 border="0">
										<tr>
											<td style="background-color:#FFFFFF; vertical-align:top; height: 102px;" >
												<table id="Table14" cellspacing="0" cellpadding="0" 
                                                    style="text-align:center; width: 96%; height: 126px; background-color: #FFFFFF;"  
                                                    border="0">
													<tr class="css">
														<td style="HEIGHT: 10px; font-size: small;" align="left" colspan="4" >
                                                            作业区<asp:DropDownList ID="DropDownListZYQ" runat="server" Width="209px" 
                                                                OnSelectedIndexChanged="DropDownListZYQ_SelectedIndexChanged" Font-Size="Small" 
                                                                Height="26px">
                                                            </asp:DropDownList>
                                                            </td>
													</tr>
													<tr class="css">
														<%--<td style="HEIGHT: 10px; width: 208px;" align="left" >
                                                            年月<asp:TextBox ID="TextBoxNY" runat="server" Width="150px"></asp:TextBox></td>--%>
														<td style="HEIGHT: 10px; width: 259px; font-size: small;" align="left" 
                                                            colspan="2" >
                                                            井&nbsp;&nbsp;&nbsp; 号<asp:DropDownList ID="DropDownListJH" runat="server" 
                                                                Width="155px" OnSelectedIndexChanged="DropDownListJH_SelectedIndexChanged" 
                                                                Font-Size="Small" Height="26px">
                                                            </asp:DropDownList></td>
                                                                <td style="HEIGHT: 10px; width: 148px;" align="right" >
                                                                    <%--<asp:imagebutton id="fentan" runat="server" ImageUrl="~/web/image/wh15.jpg" OnClick="fentan_Click"></asp:imagebutton>
                                                                    <asp:imagebutton id="bc" runat="server" ImageUrl="~/web/image/wh8.jpg" OnClick="bc_Click"></asp:imagebutton>--%>
                                                        <asp:Button ID="btnHiddenPostButton" runat="server" Text="HiddenPostButton" style="display:none" />
                                                        </td>
													</tr>
													<tr class="css">
														<td style="font-size: small;" align="left" class="style1" >
                                                            费用名<asp:DropDownList ID="DropDownListFEE"  AutoPostBack ="true" runat="server" 
                                                                Width="130px" OnSelectedIndexChanged="DropDownListFEE_SelectedIndexChanged" 
                                                                Font-Size="Small" Height="26px" >
                                                            </asp:DropDownList>
                                                            <asp:Label ID="LabelFEE" runat="server"  Width="142px" Font-Size="Small"></asp:Label></td>
														<td style="HEIGHT: 2px; width: 259px;" align="left" colspan="2" >
                                                            &nbsp;</td>
                                                    </tr>
												</table>
											</td>
										</tr>
									</table>
								</td>
							</tr>
							</table>
						 <%--<asp:Button ID="QX" runat="server" Text="全选" onclick="QX_Click" style="display: none;" />--%>
								
							<%--<asp:Button ID="CX" runat="server" Text="取消" onclick="CX_Click" style="display: none;" />--%>
                            
                            <asp:Button ID="FT" runat="server" Text="分摊" onclick="FT_Click" style="display: none;" />
                            
                            <asp:Button ID="BC" runat="server" Text="保存" onclick="bc_Click" style="display: none;" />
                            
                            
					</td>
				</tr>
			</table>
			</form>
	</body>
</html>



