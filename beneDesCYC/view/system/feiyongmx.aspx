<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="feiyongmx.aspx.cs" Inherits="beneDesCYC.view.system.feiyongmx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
	<head>
		<title>费用明细</title>
<%--		<link href="../../static/css/commonSpread.css" rel="stylesheet" type="text/css" />
        <script src="../../static/js/common/commonSpread.js" type="text/javascript"></script>--%>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR"/>
		<meta content="C#" name="CODE_LANGUAGE"/>
		<meta content="JavaScript" name="vs_defaultClientScript"/>
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema"/>
		<style type="text/css">BODY { MARGIN: 0px }
		</style>
		<link href="../web/css.css" type="text/css" rel="stylesheet" />
		<style type="text/css">
	A:link { COLOR: #ffffff; TEXT-DECORATION: none }
	A:visited { COLOR: #ffffff; TEXT-DECORATION: none }
	A:hover { COLOR: #cc0104; TEXT-DECORATION: underline }
	A:active { COLOR: #cc0001; TEXT-DECORATION: none }
	.style7 { FONT-WEIGHT: bold; FONT-SIZE: 13px; COLOR: #33536a }
	        .style8
            {
                width: 61px;
            }
            .style10
            {
                width: 217px;
            }
		</style>
		<script type="text/javascript">
		    function ClickEvent(a, b, c, d, e, f, g) {

		        document.getElementById("ddlclass").value = a;
		        document.getElementById("txtcode").value = b;
		        document.getElementById("txtname").value = c;

		        document.getElementById("ddlgrade").value = d;
		        document.getElementById("ddlformula").value = e;

		        document.getElementById("txtbeizhu").value = g;
		        //alert(document.getElementById("chk1").checked);
		        if (f == 1) {
		            //document.getElementById("ttt").value=1;
		            document.getElementById("chk1").checked = true;

		        }
		        else {
		            //document.getElementById("ttt").value=0;
		            document.getElementById("chk1").checked = false;
		        }

		    }
		</script>
	</head>
	<body style=" overflow: auto; WIDTH: 974px; HEIGHT: 584px; text-align:center;background-image:url(../static/image/bg.jpg)">
		<form id="Form1" method="post" runat="server">
            <table style="height: 335px" width="750">
                 <tr><td  valign="top"  style="height: 700px; width: 750px;">
                 <table id="Table10" cellSpacing="0" cellPadding="0" width="750" align="center" border="0">
                <%--<tr>
                    <td width="680" colspan="2" style="height: 45px">
                        &nbsp;</td>
                </tr>--%>
                <tr>
								<td  style="width: 611px; vertical-align:bottom; height: 15px;" colspan="2">
									<table id="Table12" cellspacing="0" cellpadding="0" width="740" style="text-align:right"  border="0">
										<tr class="css">
											<%--<td valign="bottom" style="height: 39px; text-align:left" colspan="2"><img height="27px" src="../web/image/wh-31.jpg" alt="" ></td>--%>
                                            <td  style="height: 6px; width:70px" valign="middle">
                                                <asp:Label ID="Label" runat="server" Text="Label" Width="107px" Font-Size="10pt" Height="20px" Visible="False"></asp:Label></td>
											<td  style="height: 6px; width:70px" valign="middle">&nbsp;</td>
											<td  style="height: 6px; width:70px" valign="middle">&nbsp;</td>
											<td  style="height: 6px; width:70px" valign="middle">&nbsp;</td>
											<td  style="height: 6px; width:70px" valign="middle">&nbsp;</td>
										</tr>
									</table>
								</td>
							</tr>
                <tr>
                    <td style="width: 237px" valign="top" >
                        <table class="css" width="180px" cellpadding="0" cellspacing="0">
                            <tr>
                                <td style="width: 180px; background:#EBF0F4" align="center">
                                    <div style="border-style:solid;border-width:1px; border-color:#83b1d3; height: 418px;"  >
                                        <br />
                                        <asp:Label ID="Label1" runat="server" CssClass="css style7" Font-Size="11pt" Height="35px"
                                            Text="请选择费用大类" Width="116px"></asp:Label><br />
                                        <asp:RadioButtonList ID="listbox" runat="server" AutoPostBack="True" CssClass="css"
                                            Font-Size="10pt" OnSelectedIndexChanged="listbox_SelectedIndexChanged1">
                                        </asp:RadioButtonList></div>
                                </td>
                             </tr>
                        </table>
                    </td>
                    <td style="width: 100px" valign="top">
                        <table class="css" style="width: 442px; height: 314px" cellpadding="0" cellspacing="0">
                                
							<tr>
								<td  style="width: 560px; height: 298px; vertical-align:top" align="left">
									<table id="Table13" style="width: 555px;border-width:1px; border-color:#83b1d3; background-color:#6b90ab "  cellspacing="0" cellpadding="0"
										 border="1">
										<tr>
											<td  style=" height: 150px; width: 555px; background-color:#ebf0f4" valign="top" align="center">
												<table id="Table14" cellspacing="0" cellpadding="0" border="0" style="width: 530px; text-align:center" >
													<tr class="css">
													   <td style="height: 20px; " colspan="4"></td>
													</tr>
													<tr class="css">
                                                        <td style="width: 61px; height: 28px">
                                                        <div style="text-align:right; font-size: 9pt;"  >
                                                            费用代号：</div>
                                                        </td>
														<td style="height: 28px; width: 158px ;">
															<div style="text-align:left" >
                                                                <asp:TextBox ID="txtcode" runat="server"  Width="155px"></asp:TextBox>&nbsp;</div>
														</td>
														<td style="HEIGHT: 28px; width: 62px;">
														<div style="text-align:right; font-size: 9pt;" >
                                                            费用名称：</div>
														</td>
                                                        <td style="height: 28px; width: 217px ;">
															<div style="text-align:left" >
                                                                <asp:TextBox ID="txtname" runat="server"  Width="160px"></asp:TextBox>&nbsp;</div> 
                                                         </td>
													</tr>
													<tr class="css">
														<td style="height: 28px; width: 61px;">
															<div style="text-align:right; font-size: 9pt;" >
                                                                所属大类：</div>
                                                                </td>
                                                              <td style="height: 28px; width: 158px ;">
															<div style="text-align:left"  >  <asp:DropDownList
                                                                    ID="ddlclass" runat="server" Width="155px">
                                                                </asp:DropDownList>&nbsp;</div>
														</td>
														<td style="height: 28px; width: 62px;">
															<div style="text-align:right; font-size: 9pt;" >
                                                                费用级别：</div>
                                                                </td>
                                                              <td style="height: 28px; width: 217px ;">
															<div style="text-align:left" >  <asp:DropDownList ID="ddlgrade" runat="server">
                                                                        <asp:ListItem Value="0">系统级</asp:ListItem>
                                                                        <asp:ListItem Value="1">用户级</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                <asp:CheckBox
                                                                ID="chk1" runat="server" BorderColor="Red" Checked="false" Height="20px" 
                                                                    Font-Size="9pt" />
                                                                <span
                                                                    style="color: #ff0000; font-size: 9pt;">是措施性费用</span></div> 
															</td>
													</tr>
													<tr class="css">
														<td valign="middle" class="style8">
															<div style="text-align:right; font-size: 9pt;" >
                                                                分摊公式：</div>
                                                          </td> 
                                                        <td colspan="2" valign="middle">
															<div style="text-align:left" >  
                                                                <asp:DropDownList ID="ddlformula" runat="server" Height="25px" Width="214px">
                                                                </asp:DropDownList>&nbsp;</div>
                                                        </td>
                                                          <td valign="middle" class="style10">
														</td>
													</tr>
													<tr class="css">
													   <td style="height: 28px; width: 61px; text-align:right; font-size: 9pt;" >
                                                           备注：</td>
													   <td style="height: 28px; text-align:left " colspan="3">
                                                           <asp:TextBox ID="txtbeizhu" runat="server" Height="25px" Width="420px"></asp:TextBox></td>
													</tr>
													</table>
											</td>
										</tr>
									</table>
									<br />
									<div style="overflow:auto; height: 335px; width: 555px;text-align:left" >
                                    <asp:GridView ID="GV" runat="server" AutoGenerateColumns="False" BackColor="#EBF0F4"
                                        BorderColor="#83B1D3"  HorizontalAlign="left"  
                                            OnRowDataBound="DG_RowDataBound" DataKeyNames="fee_code"
                                          CssClass="css" Font-Size="9pt">
                                        <Columns>
                                             <asp:BoundField DataField="fee_class" HeaderText="费用大类" >
                                                <ItemStyle BorderColor="#83B1D3" BorderStyle="Solid" Font-Names="宋体" Font-Size="X-Small" Height="30px"
                                                    HorizontalAlign="Center" Width="70px" />
                                                <HeaderStyle BorderColor="#83B1D3" BorderStyle="Solid"  Height="30px"
                                                    Width="70px" />
                                                <ControlStyle BorderColor="#83B1D3" BorderStyle="Solid" />
                                            </asp:BoundField>
                                           <asp:BoundField DataField="fee_code" HeaderText="费用代号" >
                                                <ItemStyle BorderColor="#83B1D3" BorderStyle="Solid" Font-Names="宋体" Font-Size="X-Small" Height="30px"
                                                    HorizontalAlign="Center" Width="70px" />
                                                <HeaderStyle BorderColor="#83B1D3" BorderStyle="Solid"  Height="30px"
                                                    Width="70px" />
                                                <ControlStyle BorderColor="#83B1D3" BorderStyle="Solid" />
                                            </asp:BoundField>
                                           <asp:BoundField DataField="fee_name" HeaderText="费用名称" >
                                                <ItemStyle BorderColor="#83B1D3" BorderStyle="Solid" Font-Names="宋体" Font-Size="X-Small" Height="30px"
                                                    HorizontalAlign="Center" Width="100px" />
                                                <HeaderStyle BorderColor="#83B1D3" BorderStyle="Solid"  Height="30px"
                                                    Width="100px" />
                                                <ControlStyle BorderColor="#83B1D3" BorderStyle="Solid" />
                                            </asp:BoundField>
                                           <asp:BoundField DataField="fee_level" HeaderText="费用级别" >
                                                <ItemStyle BorderColor="#83B1D3" BorderStyle="Solid" Font-Names="宋体" Font-Size="X-Small" Height="30px"
                                                    HorizontalAlign="Center" Width="70px" />
                                                <HeaderStyle BorderColor="#83B1D3" BorderStyle="Solid"  Height="30px"
                                                    Width="70px" />
                                                <ControlStyle BorderColor="#83B1D3" BorderStyle="Solid" />
                                            </asp:BoundField>
                                          <asp:BoundField DataField="formula_code" HeaderText="分摊公式">
                                                <ItemStyle BorderColor="#83B1D3" BorderStyle="Solid" Font-Names="宋体" Font-Size="X-Small" Height="30px"
                                                    HorizontalAlign="Center" Width="70px" />
                                                <HeaderStyle BorderColor="#83B1D3" BorderStyle="Solid"  Height="30px"
                                                    Width="70px" />
                                                <ControlStyle BorderColor="#83B1D3" BorderStyle="Solid" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="has_cuoshi" HeaderText="是措施性费用">
                                                <ItemStyle BorderColor="#83B1D3" BorderStyle="Solid" Font-Names="宋体" Font-Size="X-Small" Height="30px"
                                                    HorizontalAlign="Center" Width="100px" />
                                                <HeaderStyle BorderColor="#83B1D3" BorderStyle="Solid"  Height="30px"
                                                    Width="100px" />
                                                <ControlStyle BorderColor="#83B1D3" BorderStyle="Solid" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="remark" HeaderText="备注">
                                                <ItemStyle BorderColor="#83B1D3" BorderStyle="Solid" Font-Names="宋体" Font-Size="X-Small" Height="30px"
                                                    HorizontalAlign="Center" Width="40px" />
                                                <HeaderStyle BorderColor="#83B1D3" BorderStyle="Solid"  Height="30px"
                                                    Width="40px" />
                                                <ControlStyle BorderColor="#83B1D3" BorderStyle="Solid" />
                                            </asp:BoundField>
                                        </Columns>
                                    </asp:GridView>
									<asp:Button ID="ImgButZj" runat="server" Text="增加" onclick="ImgButZj_Click" style="display: none;" />
									<asp:Button ID="ImgButXg" runat="server" Text="修改" onclick="ImgButXg_Click" style="display: none;" />
									<asp:Button ID="ImgButSc" runat="server" Text="删除" onclick="ImgButSc_Click" style="display: none;" />
									<asp:Button ID="ImgButBc" runat="server" Text="保存" onclick="ImgButBc_Click" style="display: none;" />
									    <asp:Label ID="labShow" runat="server" ForeColor="Red" Width="16px" 
                                            Font-Size="X-Small"></asp:Label>
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
