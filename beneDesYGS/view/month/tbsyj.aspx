<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="tbsyj.aspx.cs" Inherits="beneDesYGS.view.month.tbsyj" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head>
		<title>特别收益金</title>
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
	        .style9
            {
                height: 29px;
            }
		    .style10
            {
                FONT-WEIGHT: bold;
                FONT-SIZE: x-small;
                COLOR: #33536a;
            }
            .style11
            {
                font-size: x-small;
                font-weight: bold;
            }
            .style12
            {
                height: 29px;
                font-size: x-small;
                font-weight: bold;
            }
		    .style13
            {
                width: 716px;
                height: 198px;
            }
		    .style14
            {
                height: 10px;
                width: 506px;
            }
		</style>
		<script type="text/javascript">
		    function ClickEvent(d0,d1)
		    {
		        document.getElementById("txtNY").value = d0;
		        document.getElementById("txtSYJ").value = d1;
		         
		    }
		</script>
	
	</head>
	
	<body style="overflow:hidden" background="/static/image/bg.jpg" >
		<form id="Form1" method="post" runat="server">
		<table id="Table7" style="WIDTH: 724px; HEIGHT: 716px" 
				width="724" border="0" align="center">
				<tr>
					<td valign="top" align="center" class="style13">
						<table id="Table10" width="680" align="center" border="0">
							<tr>
								<td style="width: 684px; height: 45px;">
									<table id="Table11"  width="100%" border="0">
										<tr>
											<td></td>
											<td width="642"></td>
										</tr>
										<tr>
											<td width="39">
												&nbsp;</td>
										</tr>
										<tr>
											<%--<td colspan="2"><IMG height="2" src="../web/image/wh13.jpg" width="681"></td>--%>
										</tr>
									</table>
								</td>
							</tr>
							<tr>
								<td valign="top" style="width: 684px">
									<table id="Table12" cellspacing="0" cellPadding="0" width="680" align="right" border="0">
										<tr>
											<td valign="bottom" width="50%" style="height: 39px"></td>
											<td width="70"></td>
											<td width="70"></td>
											<td width="70"></td>
											<td width="70">
                                                            &nbsp;</td>
											<td width="70">&nbsp;</td>
										</tr>
									</table>
								<asp:Button ID="JS" runat="server" Text="计算" onclick="JS_Click" style="display: none;" />
								
								<asp:Button ID="BC" runat="server" Text="保存" onclick="BC_Click" style="display: none;" />
								
								</td>
							</tr>
							<tr>
								<td valign="top" style="width: 684px; ">
									<table id="Table13"  border="0">
										<tr>
											<td bgcolor="#ebf0f4" style="width: 100%; ">
												<table id="Table14" cellspacing="0" cellpadding="0" width="96%" align="center" border="0" >
													<tr class="css">
													<td class="style14" ></td><td style="height:10px; width: 86%; "></td>
													</tr>
													<tr class="css">
														<td class="style14">
															<div align="right" style="font-family: 黑体">
                                                                时间&nbsp;</div>
														</td>
														<td style="HEIGHT: 10px; width: 86%;" align="left"><asp:textbox id="txtNY" 
                                                                runat="server" Width="184px" Enabled="False"></asp:textbox><FONT face="宋体"></FONT>
															<asp:Label id="labShow" runat="server" ForeColor="Red"></asp:Label></td>
													</tr>
													<tr class="css">
														<td class="style14" style="font-family: 黑体; font-size: small">
															<div align="right">
                                                                石油特别收益金&nbsp;</div>
														</td>
														<td style="HEIGHT: 10px; width: 86%; " align="left">
														<asp:textbox id="txtSYJ" runat="server" Width="184px" ></asp:textbox>
                                                            <FONT face="宋体" style="font-family: 黑体; font-size: small"> 元/吨</FONT>
                                                            <asp:Label id="Labeljs" runat="server" ForeColor="Red"></asp:Label>
                                                            </td>
                                                    </tr>
													<tr class="css">
													<td class="style14" ></td><td style="height: 10px; width: 86%; "></td>
													</tr>												
												</table>
											</td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
						<br>
						<div style="OVERFLOW: auto; WIDTH: 680px; height : 120px; " >
                            <asp:GridView ID="DG" runat="server" AutoGenerateColumns="False" DataKeyNames="ny"
                                 HorizontalAlign="Center" OnRowDataBound="DG_RowDataBound" 
                                Width="641px" BackColor="#EBF0F4" BorderColor="#83B1D3" 
                                BorderStyle="Solid">
                              <Columns>
                                    <asp:BoundField DataField="ny" HeaderText="时间" SortExpression="时间">
                                        <ItemStyle HorizontalAlign="Center" Width="340px" Font-Size="Smaller" Height="30px" BorderColor="#83B1D3" BorderStyle="Solid"/>
                                        <HeaderStyle Width="340px" Font-Size="Smaller" Height="30px" BorderColor="#83B1D3" BorderStyle="Solid" />
                                        <ControlStyle BorderStyle="Solid" BorderColor="#83B1D3"/>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="tbsyj" HeaderText="特别收益金" SortExpression="特别收益金">
                                        <ItemStyle HorizontalAlign="Center" Width="340px" Font-Size="Smaller" Height="30px" BorderColor="#83B1D3" BorderStyle="Solid"/>
                                        <HeaderStyle Width="340px" Font-Size="Smaller" Height="30px" BorderColor="#83B1D3" BorderStyle="Solid" />
                                        <ControlStyle BorderStyle="Solid" BorderColor="#83B1D3"/>
                                    </asp:BoundField>
                             </Columns>
                            </asp:GridView>
                        </div>
                      </td>
				</tr>
                   <div style ="text-align:center">
                   	<td vAlign="top" style="width: 680px; ">
         <table id="Table1"  border="1" align="center"
				style="BORDER-COLLAPSE: collapse; width:680px;" class="css">
            <tr>
                <td align="right" style="vertical-align: text-top; text-align: right"  id="td_ItemName" runat="server"
                    class="style11">原油价格（美元/桶）</td>
                <td class="css">
                     &nbsp; <asp:TextBox ID="Number" runat="server" Height="22px" Width="134px" 
                         Enabled="False"></asp:TextBox>
                </td>
                 <td align="right" style="vertical-align: text-top; text-align: right" class="css">
                     <span class="style11">征收比率（%）</span> </td>
                <td align="right" style="vertical-align: text-top; text-align: right" 
                    class="style10">
                    速算扣除数（美元/桶）</td>
            </tr>

                <tr>
                <td align="right" style="vertical-align: text-top; text-align: right; " 
                    class="css">
                    <asp:TextBox ID="TB1" runat="server" Height="22px" Width="134px" ></asp:TextBox></td>
        
                <td align="left" class="style9">
                   ~ <asp:TextBox ID="TB2" runat="server" Height="22px" Width="134px" 
                        ondisposed="TB2_TextChanged" ontextchanged="TB2_TextChanged "  AutoPostBack="True"></asp:TextBox></td>
                <td align="right" class="css">
                    <asp:TextBox ID="TB3" runat="server" Height="22px" Width="134px"></asp:TextBox></td>
                <td style="text-align: left">
                    <asp:TextBox ID="TB4" runat="server" Height="22px" Width="134px"></asp:TextBox> </td>
                 </tr>
                  <tr>
                <td align="right" style="vertical-align: text-top; text-align: right; " 
                    class="style9">
                    <asp:TextBox ID="TB5" runat="server" Height="22px" Width="134px" 
                         Enabled="False"></asp:TextBox> </td>
                 <%--  <script type="text/javascript">  
                         	   function SyncValue(obj)    
		                       {         document.getElementById("TB5").value = obj.value;     } 
		 </script>   <!--实时变化可以使用onpropertychange, onchange是失去焦点后触发-->
		    <input id = "TB5" onchange="SyncValue(this);" /> 
	      <input id = "TB3"  />--%>
                <td align="left" class="style9">
                    ~ <asp:TextBox ID="TB6" runat="server" Height="22px" Width="134px" 
                        ontextchanged="TB6_TextChanged" AutoPostBack="True" ></asp:TextBox>
                        </td>
                <td align="right" class="style9">
                    <asp:TextBox ID="TB7" runat="server" Height="22px" Width="134px"></asp:TextBox> </td>
                <td style="text-align: left" class="style9">
                    <asp:TextBox ID="TB8" runat="server" Height="22px" Width="134px"></asp:TextBox>
                        </td>
                 </tr>
                 
                 
            <tr>
                <td align="right" style="vertical-align: text-top; text-align: right; " 
                    class="css">
                    <asp:TextBox ID="TB9" runat="server" Height="22px" Width="134px" Enabled="False"></asp:TextBox></td>
                <td align="left" class="css">
                    ~ <asp:TextBox ID="TB10" runat="server" Height="22px" Width="134px" 
                        ontextchanged="TB10_TextChanged"  AutoPostBack="True"></asp:TextBox>
                        </td>
                <td align="right" class="css">
                    <asp:TextBox ID="TB11" runat="server" Height="22px" Width="134px"></asp:TextBox> </td>
                <td style="text-align: left" class="css">
                    <asp:TextBox ID="TB12" runat="server" Height="22px" Width="134px"></asp:TextBox>
                        </td>
                 </tr>
            <tr>
                <td align="right" style="vertical-align: text-top; text-align: right; " 
                    class="css">
                    <asp:TextBox ID="TB13" runat="server" Height="22px" Width="134px" Enabled="False"></asp:TextBox></td>
                <td align="left" class="style9">
                     ~ <asp:TextBox ID="TB14" runat="server" Height="22px" Width="134px" 
                         ontextchanged="TB14_TextChanged"  AutoPostBack="True"></asp:TextBox>
                        </td>
                <td align="right" class="css">
                    <asp:TextBox ID="TB15" runat="server" Height="22px" Width="134px"></asp:TextBox> </td>
                <td style="text-align: left">
                    <asp:TextBox ID="TB16" runat="server" Height="22px" Width="134px"></asp:TextBox> </td>
                 </tr>
               
            <tr>
                <td align="right" style="vertical-align: text-top; text-align: right; " 
                    class="css">
                    <asp:TextBox ID="TB17" runat="server" Height="22px" Width="134px" Enabled="False"></asp:TextBox> </td>
                <td align="left" class="style12">
                 以上</td>
                <td align="right" class="css">
                    <asp:TextBox ID="TB18" runat="server" Height="22px" Width="134px"></asp:TextBox> </td>
                <td style="text-align: left">
                    <asp:TextBox ID="TB19" runat="server" Height="22px" Width="134px"></asp:TextBox>
                        </td>
                 </tr>
            <tr>
         
                <td align="center" colspan="2" class="css">
                   
                </td>
              
                <td align="center" colspan="2" class="css">
                <FONT face="宋体">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:imagebutton id="bt_submit" runat="server" ImageUrl="/static/image/wh8.jpg" OnClick="bt_submit_Click"></asp:imagebutton></FONT>
              <%--  <asp:Button ID="bt_submit" runat="server" Height="30px" Text="保存" Width="100px" 
                        style="font-size: 12px; color: #555555" onclick="bt_submit_Click" />--%>
                 
                <%--<asp:Button ID="bt_reset" runat="server" Text="取消" Height="30px" Width="100px" 
                 CausesValidation="False" OnClick="bt_cancel_Click" 
                 style="font-size: 12px; color: #555555" />--%>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                </td>
              
            </tr>
           </table>
             </div>
   <%-- <div style ="text-align:center">
                  <asp:Table ID="Table1" runat="server">
                   <tr>
                    <td align="right" style="vertical-align: text-top; text-align: right" 
                    class="css">原油价格（美元/桶）</td>
                    <td class="css">
                    <asp:TextBox ID="Number" runat="server" Height="22px" Width="160px"></asp:TextBox>
                    </td>
                    <td align="right" style="vertical-align: text-top; text-align: right" 
                    class="css">征收比率（%） </td>
                    <td style="text-align: left" class="style7">
                     速算扣除数（美元/桶）
                    </td>
                   </tr>
                   
                  </asp:Table>
            </div>--%>
			</table>
</form>
	</body>
</HTML>
