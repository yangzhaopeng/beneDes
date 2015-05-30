<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="csdy.aspx.cs" Inherits="beneDesYGS.view.month.csdy" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>参数定义</title>
<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<style type="text/css">BODY { MARGIN: 0px }
		</style>
		<LINK href="../web/css.css" type="text/css" rel="stylesheet">
		<style type="text/css">
	A:link { COLOR: #ffffff; TEXT-DECORATION: none }
	A:visited { COLOR: #ffffff; TEXT-DECORATION: none }
	A:hover { COLOR: #cc0104; TEXT-DECORATION: underline }
	A:active { COLOR: #cc0001; TEXT-DECORATION: none }
		    .style11
            {
                height: 35px;
            }
            .style12
            {
                width: 160px;
                height: 35px;
            }
            .style13
            {
                width: 484px;
                height: 35px;
            }
		</style>
	</head>
	<body background="/static/image/bg.jpg" >
		<form id="Form1" method="post" runat="server">
		<table id="Table7" style="WIDTH: 724px; HEIGHT: 716px" cellSpacing="0" cellPadding="0"
				width="724" border="0" align="center">
				<tr>
					<td vAlign="top" align="center" style="width: 716px">
						<table id="Table10" cellSpacing="0" cellPadding="0" width="680" align="center" border="0">
							<tr>
								<td height="45" style="width: 684px">
									&nbsp;</td>
							</tr>
							<tr>
								<td vAlign="bottom" style="width: 684px">
									<table id="Table12" cellSpacing="0" cellPadding="0" width="680" align="right" border="0">
										<tr>
											<td vAlign="bottom" width="50%" style="height: 39px" align="left">&nbsp;</td>
                                            <td style="height: 39px" align="right" colspan="2">&nbsp;</td>
											<td width="70" style="height: 39px" align="right">&nbsp;</td>
										</tr>
									</table>
								</td>
							</tr>
							<tr>
								<td vAlign="top" style="width: 684px; ">
									<table id="Table13" borderColor="#83b1d3" cellSpacing="1" cellPadding="1" width="100%"
										bgColor="#6b90ab" border="0">
										<tr>
											<td bgColor="#ebf0f4" style="width: 100% ">
												<table id="Table14" cellSpacing="0" cellPadding="0" width="96%" align="center" border="0" >
													<tr class="css">
													   <td style="height: 10px; width: 160px "></td>
													   <td style="height: 10px; width: 222px "></td>
													   <td style="height: 10px; width: 100px "></td>
													   <td style="height: 10px; width: 223px "></td>
													</tr>
													<tr class="css">
                                                        <td class="style12">
                                                        <div align="right" style="font-size: 9pt" >
                                                            时间 &nbsp;
                                                        </div>
                                                        </td>
                                                        <td colspan="4" class="style13">
															<div align="left" >&nbsp;&nbsp;&nbsp;
                                                                <asp:textbox id="txtNY" runat="server" Width="184px" style="margin-left: 1px"></asp:textbox>
                                                    <asp:Label ID="labShow" runat="server" ForeColor="Red" style="font-size: 9pt"></asp:Label>
                                                                <asp:Label ID="Label" runat="server" Visible="False" style="font-size: 9pt"></asp:Label></div>
                                                        </td>
                                                        <td colspan="1" class="style11">
                                                        </td>
													</tr>
													<tr class="css">
														<td class="style12">
															<div align="right" style="font-size: 9pt" >
                                                                特高成本井 &nbsp;</div>
                                                                </td>
                                                        <td colspan="4" class="style13">
															<div align="left"  >  &gt;&nbsp;
                                                            <asp:TextBox ID="txtCBJ" runat="server" Width="184px"></asp:TextBox><FONT face="宋体" 
                                                                    style="font-size: 9pt"> 元/吨</FONT></div>
                                                        </td>
													</tr>
													<tr class="css">
														<td class="style12">
															<div align="right" style="font-size: 9pt" >
                                                                高含水 &nbsp;
                                                            </div>
                                                          </td> 
                                                        <td colspan="4" class="style13">
															<div align="left" >  &gt;&nbsp;
															<asp:textbox id="txtGHS" runat="server" Width="184px"></asp:textbox><FONT face="宋体" 
                                                                    style="font-size: 9pt"> %</FONT></div>
                                                        </td>
													</tr>
													<tr class="css">
														<td class="style12">
															<div align="right" style="font-size: 9pt" >
                                                                低产能 &nbsp;
                                                            </div>
                                                          </td> 
                                                        <td colspan="4" class="style13">
															<div align="left" >  &lt;&nbsp;
															<asp:textbox id="txtDCN" runat="server" Width="184px"></asp:textbox><FONT face="宋体" 
                                                                    style="font-size: 9pt"> 吨</FONT></div>
                                                        </td>
													</tr>
													<tr class="css">
													   <td style="height: 10px; width: 160px "></td>
													   <td style="height: 10px; width: 222px "></td>
													   <td style="height: 10px; width: 100px "></td>
													   <td style="height: 10px; width: 223px "></td>
													</tr>
													</table>
													
													<asp:Button ID="XG" runat="server" Text="修改" onclick="XG_Click" style="display: none;" />
								
	                                                <asp:Button ID="BC" runat="server" Text="保存" onclick="BC_Click" style="display: none;" />
											</td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
</form>
	</body>
</HTML>

