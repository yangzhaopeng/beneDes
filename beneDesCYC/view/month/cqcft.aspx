<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="cqcft.aspx.cs" Inherits="beneDesCYC.view.month.cqcft" %>

<%@ Register Assembly="FarPoint.Web.Spread" Namespace="FarPoint.Web.Spread" TagPrefix="FarPoint" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>采气厂分摊</title>
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
<body style="overflow: hidden; width: 974px; height: 700px; text-align: center; background-image: url(/static/image/bg.jpg)">
    <form id="Form1" method="post" runat="server">
    <table id="Table7" style="width: 710px; height: 700px; text-align: center" cellspacing="0"
        cellpadding="0" width="705" border="0">
        <tr>
            <td valign="top" align="center" style="width: 716px">
                <table id="Table10" cellspacing="0" cellpadding="0" width="680" style="text-align: center"
                    border="0">
                    <tr>
                        <td style="width: 684px; height: 20px; vertical-align: top">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 684px; vertical-align: top">
                            <table id="Table13" style="border-color: #FFFFFF; background-color: #FFFFFF; height: 57px;"
                                cellspacing="1" cellpadding="1" width="100%" border="0">
                                <tr>
                                    <td style="background-color: #FFFFFF; vertical-align: top">
                                        <table id="Table14" cellspacing="0" cellpadding="0" width="96%" style="text-align: center"
                                            border="0">
                                            <%--<tr class="css">
														<td style="HEIGHT: 10px; width:14%" >
															<div style="text-align:right" >
															
															<div style="text-align:right; height: 15px;" >
															
															<div style="text-align:right; font-size: small;">
                                                                选择作业区</div>
                                                                </div>
                                                            </div>
														</td>
														<td style="HEIGHT: 10px; width:86%; text-align:left" >
                                                            <asp:DropDownList ID="zyqddl" runat="server" AutoPostBack="true" 
                                                                Width="188px" OnSelectedIndexChanged="zyqddl_SelectedIndexChanged" 
                                                                Font-Size="Small">
                                                            </asp:DropDownList></td>
													</tr>--%>
                                            <%--<tr class="css">
														<td style="HEIGHT: 10px; width:14%">
															<div style="text-align:right" >
                                                                年月</div>
														</td>
														<td style="HEIGHT: 10px; width:86%; text-align:left" >
                                                          <asp:TextBox ID="txtdate" runat="server" Height="15px" Width="184px"></asp:TextBox>
                                                           &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                                            <asp:imagebutton id="all" runat="server" ImageUrl="~/web/image/wh17.jpg" OnClick="all_Click" ></asp:imagebutton>
                                                            &nbsp;&nbsp;<asp:imagebutton id="fentan" runat="server" ImageUrl="~/web/image/wh15.jpg" OnClick="fentan_Click" ></asp:imagebutton>
                                                        </td>
													</tr>--%>
                                            <tr class="css">
                                                <td style="height: 19px">
                                                    <div style="text-align: right; font-size: small; height: 11px;">
                                                        选择采气厂</div>
                                                </td>
                                                <td style="height: 19px; text-align: left" valign="middle">
                                                    <asp:CheckBoxList ID="cqcchklist" runat="server" Height="21px" RepeatDirection="Horizontal"
                                                        RepeatColumns="5" Width="549px" Font-Size="Small">
                                                    </asp:CheckBoxList>
                                                </td>
                                            </tr>
                                            <tr class="css">
                                                <td style="height: 2px" colspan="2">
                                                </td>
                                            </tr>
                                            <tr class="css">
                                                <td style="height: 19px">
                                                    &nbsp;
                                                </td>
                                                <td style="height: 19px; text-align: left">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr class="css">
                                                <td style="height: 10px; width: 14%">
                                                    <div style="text-align: right">
                                                        &nbsp;</div>
                                                </td>
                                                <td style="height: 10px; width: 86%; text-align: left">
                                                    &nbsp;
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
                <div style="overflow: auto; width: 689px; height: 450px">
                    <table style="border-color: #83B1D3">
                        <tr>
                            <td align="left" valign="top" style="height: 178px; width: 110px;">
                                <%--  <asp:ListBox ID="listzxz" runat="server" Height="439px" Width="128px"  AutoPostBack="True" OnSelectedIndexChanged="listzxz_SelectedIndexChanged" ></asp:ListBox>--%>
                            </td>
                            <td valign="top" style="height: 178px; width: 539px;">
                                <%-- <FarPoint:FpSpread  ID="FpSpread1" runat="server" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" CssClass="css" Width="548px" Height="430px"
                                         ActiveSheetViewIndex="0" DesignString='<?xml version="1.0" encoding="utf-8"?><Spread />' RenderCSSClass="False" >
                                        <sheets><FarPoint:SheetView SheetName="Sheet1" 
                                                DesignString="&lt;?xml version=&quot;1.0&quot; encoding=&quot;utf-8&quot;?&gt;&lt;Sheet&gt;&lt;Data&gt;&lt;RowHeader class=&quot;FarPoint.Web.Spread.Model.DefaultSheetDataModel&quot; rows=&quot;3&quot; columns=&quot;0&quot;&gt;&lt;AutoCalculation&gt;True&lt;/AutoCalculation&gt;&lt;AutoGenerateColumns&gt;True&lt;/AutoGenerateColumns&gt;&lt;ReferenceStyle&gt;A1&lt;/ReferenceStyle&gt;&lt;Iteration&gt;False&lt;/Iteration&gt;&lt;MaximumIterations&gt;1&lt;/MaximumIterations&gt;&lt;MaximumChange&gt;0.001&lt;/MaximumChange&gt;&lt;/RowHeader&gt;&lt;ColumnHeader class=&quot;FarPoint.Web.Spread.Model.DefaultSheetDataModel&quot; rows=&quot;1&quot; columns=&quot;4&quot;&gt;&lt;AutoCalculation&gt;True&lt;/AutoCalculation&gt;&lt;AutoGenerateColumns&gt;True&lt;/AutoGenerateColumns&gt;&lt;ReferenceStyle&gt;A1&lt;/ReferenceStyle&gt;&lt;Iteration&gt;False&lt;/Iteration&gt;&lt;MaximumIterations&gt;1&lt;/MaximumIterations&gt;&lt;MaximumChange&gt;0.001&lt;/MaximumChange&gt;&lt;/ColumnHeader&gt;&lt;DataArea class=&quot;FarPoint.Web.Spread.Model.DefaultSheetDataModel&quot; rows=&quot;3&quot; columns=&quot;4&quot;&gt;&lt;AutoCalculation&gt;True&lt;/AutoCalculation&gt;&lt;AutoGenerateColumns&gt;True&lt;/AutoGenerateColumns&gt;&lt;ReferenceStyle&gt;A1&lt;/ReferenceStyle&gt;&lt;Iteration&gt;False&lt;/Iteration&gt;&lt;MaximumIterations&gt;1&lt;/MaximumIterations&gt;&lt;MaximumChange&gt;0.001&lt;/MaximumChange&gt;&lt;SheetName&gt;Sheet1&lt;/SheetName&gt;&lt;/DataArea&gt;&lt;/Data&gt;&lt;Presentation&gt;&lt;AxisModels&gt;&lt;Row class=&quot;FarPoint.Web.Spread.Model.DefaultSheetAxisModel&quot; defaultSize=&quot;22&quot; orientation=&quot;Vertical&quot; count=&quot;3&quot;&gt;&lt;Items&gt;&lt;Item index=&quot;-1&quot;&gt;&lt;Size&gt;22&lt;/Size&gt;&lt;/Item&gt;&lt;/Items&gt;&lt;/Row&gt;&lt;Column class=&quot;FarPoint.Web.Spread.Model.DefaultSheetAxisModel&quot; defaultSize=&quot;90&quot; orientation=&quot;Horizontal&quot; count=&quot;4&quot;&gt;&lt;Items&gt;&lt;Item index=&quot;-1&quot;&gt;&lt;Size&gt;90&lt;/Size&gt;&lt;SortIndicator&gt;Ascending&lt;/SortIndicator&gt;&lt;/Item&gt;&lt;/Items&gt;&lt;/Column&gt;&lt;RowHeaderColumn class=&quot;FarPoint.Web.Spread.Model.DefaultSheetAxisModel&quot; defaultSize=&quot;40&quot; orientation=&quot;Horizontal&quot; count=&quot;0&quot;&gt;&lt;Items&gt;&lt;Item index=&quot;-1&quot;&gt;&lt;Size&gt;40&lt;/Size&gt;&lt;SortIndicator&gt;Ascending&lt;/SortIndicator&gt;&lt;/Item&gt;&lt;/Items&gt;&lt;/RowHeaderColumn&gt;&lt;ColumnHeaderRow class=&quot;FarPoint.Web.Spread.Model.DefaultSheetAxisModel&quot; defaultSize=&quot;22&quot; orientation=&quot;Vertical&quot; count=&quot;1&quot;&gt;&lt;Items&gt;&lt;Item index=&quot;-1&quot;&gt;&lt;Size&gt;22&lt;/Size&gt;&lt;/Item&gt;&lt;/Items&gt;&lt;/ColumnHeaderRow&gt;&lt;/AxisModels&gt;&lt;StyleModels&gt;&lt;RowHeader class=&quot;FarPoint.Web.Spread.Model.DefaultSheetStyleModel&quot; Rows=&quot;3&quot; Columns=&quot;0&quot;&gt;&lt;AltRowCount&gt;2&lt;/AltRowCount&gt;&lt;DefaultStyle class=&quot;FarPoint.Web.Spread.NamedStyle&quot; Parent=&quot;HeaderDefault&quot;&gt;&lt;BackColor&gt;Control&lt;/BackColor&gt;&lt;Border class=&quot;FarPoint.Web.Spread.Border&quot; Size=&quot;1&quot; Style=&quot;Solid&quot;&gt;&lt;Bottom Color=&quot;ControlDark&quot; /&gt;&lt;Left Size=&quot;0&quot; /&gt;&lt;Right Color=&quot;ControlDark&quot; /&gt;&lt;Top Size=&quot;0&quot; /&gt;&lt;/Border&gt;&lt;HorizontalAlign&gt;Center&lt;/HorizontalAlign&gt;&lt;/DefaultStyle&gt;&lt;ConditionalFormatCollections /&gt;&lt;/RowHeader&gt;&lt;ColumnHeader class=&quot;FarPoint.Web.Spread.Model.DefaultSheetStyleModel&quot; Rows=&quot;1&quot; Columns=&quot;4&quot;&gt;&lt;AltRowCount&gt;2&lt;/AltRowCount&gt;&lt;DefaultStyle class=&quot;FarPoint.Web.Spread.NamedStyle&quot; Parent=&quot;HeaderDefault&quot;&gt;&lt;BackColor&gt;Control&lt;/BackColor&gt;&lt;Border class=&quot;FarPoint.Web.Spread.Border&quot; Size=&quot;1&quot; Style=&quot;Solid&quot;&gt;&lt;Bottom Color=&quot;ControlDark&quot; /&gt;&lt;Left Size=&quot;0&quot; /&gt;&lt;Right Color=&quot;ControlDark&quot; /&gt;&lt;Top Size=&quot;0&quot; /&gt;&lt;/Border&gt;&lt;HorizontalAlign&gt;Center&lt;/HorizontalAlign&gt;&lt;/DefaultStyle&gt;&lt;ConditionalFormatCollections /&gt;&lt;/ColumnHeader&gt;&lt;DataArea class=&quot;FarPoint.Web.Spread.Model.DefaultSheetStyleModel&quot; Rows=&quot;3&quot; Columns=&quot;4&quot;&gt;&lt;AltRowCount&gt;2&lt;/AltRowCount&gt;&lt;DefaultStyle class=&quot;FarPoint.Web.Spread.NamedStyle&quot; Parent=&quot;DataAreaDefault&quot; /&gt;&lt;ConditionalFormatCollections /&gt;&lt;/DataArea&gt;&lt;/StyleModels&gt;&lt;MessageRowStyle class=&quot;FarPoint.Web.Spread.Appearance&quot;&gt;&lt;BackColor&gt;LightYellow&lt;/BackColor&gt;&lt;ForeColor&gt;Red&lt;/ForeColor&gt;&lt;/MessageRowStyle&gt;&lt;SheetCornerStyle class=&quot;FarPoint.Web.Spread.NamedStyle&quot; Parent=&quot;HeaderDefault&quot; /&gt;&lt;AllowLoadOnDemand&gt;false&lt;/AllowLoadOnDemand&gt;&lt;LoadRowIncrement &gt;10&lt;/LoadRowIncrement &gt;&lt;LoadInitRowCount &gt;30&lt;/LoadInitRowCount &gt;&lt;TopRow&gt;0&lt;/TopRow&gt;&lt;/Presentation&gt;&lt;Settings&gt;&lt;Name&gt;Sheet1&lt;/Name&gt;&lt;Categories&gt;&lt;Appearance&gt;&lt;RowHeaderVisible&gt;False&lt;/RowHeaderVisible&gt;&lt;ColumnHeaderVisible&gt;False&lt;/ColumnHeaderVisible&gt;&lt;SelectionBorder class=&quot;FarPoint.Web.Spread.Border&quot; /&gt;&lt;/Appearance&gt;&lt;Behavior&gt;&lt;PageSize&gt;80&lt;/PageSize&gt;&lt;/Behavior&gt;&lt;Layout&gt;&lt;ColumnHeaderRowCount&gt;1&lt;/ColumnHeaderRowCount&gt;&lt;RowHeaderColumnCount&gt;0&lt;/RowHeaderColumnCount&gt;&lt;/Layout&gt;&lt;/Categories&gt;&lt;ColumnHeaderRowCount&gt;1&lt;/ColumnHeaderRowCount&gt;&lt;/Settings&gt;&lt;/Sheet&gt;" 
                                                PageSize="80">--%>
                                <%--<ColumnHeader Visible="False"></ColumnHeader>--%>
                                <%--    <RowHeader ColumnCount="0" />
                                        </FarPoint:SheetView></sheets>
                                        <Pager Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" PageCount="80" />
                                    </FarPoint:FpSpread> --%>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
