<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="zhcx.aspx.cs" Inherits="beneDesCYC.view.query.zhcx" %>


<%@ Register Assembly="FarPoint.Web.Spread" Namespace="FarPoint.Web.Spread" TagPrefix="FarPoint" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>综合查询</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR" />
    <meta content="C#" name="CODE_LANGUAGE" />
    <meta content="JavaScript" name="vs_defaultClientScript" />
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema" />
    <style type="text/css">BODY { MARGIN: 0px }
		</style>
    <link href="../../web/css.css" type="text/css" rel="stylesheet" />
    <style type="text/css">
	A:link { COLOR: #ffffff; TEXT-DECORATION: none }
	A:visited { COLOR: #ffffff; TEXT-DECORATION: none }
	A:hover { COLOR: #cc0104; TEXT-DECORATION: underline }
	A:active { COLOR: #cc0001; TEXT-DECORATION: none }
	.style7 { FONT-WEIGHT: bold; FONT-SIZE: 13px; COLOR: #33536a }
	.freezing{ position:relative ;table-layout:fixed }
		.style9
        {
            height: 19px;
            width: 11px;
        }
        .style10
        {
            height: 21px;
            width: 11px;
        }
        .style14
        {
            height: 21px;
            width: 5px;
        }
        .style15
        {
            height: 21px;
            width: 4px;
        }
        .style17
        {
            height: 21px;
            width: 2px;
        }
		</style>
</head>
<body style="overflow: auto; width: 974px; height: 584px; text-align: center; background-image: url(../web/image/ibj1-1.JPG)">
    <form id="Form1" method="post" runat="server">
        <table id="Table7" style="width: 700px; height: 584px" cellspacing="0" cellpadding="0"
            border="0">
            <tr>
                <td valign="top" align="center" style="width: 716px">
                    <table id="Table10" cellspacing="0" cellpadding="0" width="680" style="text-align: center"
                        border="0">
                        <tr>
                            <td style="width: 684px; height: 45px">
                                <table id="Table11" cellspacing="0" cellpadding="0" width="100%" border="0">
                                    <tr>
                                        <td style="width: 13px; height: 19px">
                                            &nbsp;</td>
                                        <td style="height: 19px;" colspan="2">
                                            &nbsp;&nbsp;</td>
                                    </tr>
                                   <%-- <tr>
                                        <td style="width: 13px; height: 21px;" align="right">
                                            <div style="text-align: center">
                                                <img height="21" src="../web/image/wh14.jpg" width="21" alt="" /></div>
                                        </td>
                                        <td class="css style7" align="left" colspan="2" style="height: 21px">
                                            数据查询&gt;综合查询&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </td>
                                    </tr>--%>
                                    <tr>
                                        <td colspan="3" style="height: 21px">
                                            <img height="2" src="../web/image/wh13.jpg" width="681" alt="" /></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td valign="bottom" style="width: 684px">
                                <table id="Table12" cellspacing="0" cellpadding="0" width="680" style="text-align: right"
                                    border="0">
                                    <tr class="css">
                                        <td style="height: 6px; width: 332px; text-align: left;">
                                                    <asp:RadioButtonList ID="RadioButtonList1" runat="server" 
                                                        RepeatDirection="Horizontal" 
                                                        style="font-size: 8pt; font-weight: 700;" 
                                                        Width="215px" AutoPostBack="True"
                                                        onselectedindexchanged="RadioButtonList1_SelectedIndexChanged">
                                                        <asp:ListItem Value="1">油井</asp:ListItem>
                                                        <asp:ListItem Value="2">评价单元</asp:ListItem>
                                                        <asp:ListItem Value="3">区块</asp:ListItem>
                                                    </asp:RadioButtonList>
                                        </td>
                                        <td valign="middle" style="height: 39px;" align="right" colspan="6">
                                                   
                                               
                                                <asp:imagebutton id="searching" runat="server" ImageUrl="/static/image/js.jpg" 
                                                                    onclick="searching_Click" ></asp:imagebutton>
                                            &nbsp; &nbsp;<asp:ImageButton ID="daochu" runat="server" ImageUrl="/static/image/dc.jpg"
                                                OnClick="daochu_Click"></asp:ImageButton>
                                            &nbsp; &nbsp;</td>
                                    </tr>
                                </table>
                               <%-- <asp:Button ID="DC" runat="server" Text="导出" onclick="daochu_Click" style="display: none;" />--%>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" style="width: 684px;">
                                <table id="Table13" style="border-color: #83b1d3; background-color: #6b90ab" cellspacing="1"
                                    cellpadding="1" width="100%" border="0" class="newStyle4">
                                    <tr>
                                        <td style="width: 100%; background-color: #ebf0f4">
                                            <table id="nihao" runat="server" style="width: 100%; background-color: #ebf0f4" 
                                                class="newStyle3">
                                                <tr style="width: 710px; height: 30px" align="center">
                                                    <td style="width: 246px; height: 20px;font-size:11px" >
                                                    显示字段                            
                                                      </td>
                                                    <td style="width: 271px; height: 20px">
                                                        &nbsp;</td>
                                                    <td style="width: 62px; height: 20px">
                                                    </td>
                                                    <td style="height: 20px; width: 269px;">
                                                    </td>
                                                </tr>
                                                <tr align="center" style="width: 710px; height: 30px">
                                                    <td style="width: 246px; height: 30px">
                                                <asp:DropDownList ID="link_tables" runat="server" style="margin-bottom: 0px; text-align: center;" 
                                                    Height="26px" Width="130px" AutoPostBack="True"
                                                        onselectedindexchanged="link_tables_SelectedIndexChanged" >
                                                   
                                                </asp:DropDownList>
                                               
                                                    </td>
                                                    <td style="width: 271px; height: 30px">
                                                    <asp:ListBox ID="zd" runat="server" Height="89px" Width="203px" 
                                                        AutoPostBack="True">
                                                    </asp:ListBox>
                                               
                                                    </td>
                                                    <td style="height: 30px; width: 62px;" align=center >
                                               
                                                <asp:Button ID="bt_add" runat="server" Text="添加"   
                                                style="height: 26px" Height="24px" Width="40px" Font-Size="11px"  onclick="bt_add_Click"  />
                                               
                                                <asp:Button ID="Bt_clear" runat="server" Text="清除" Font-Size="11px" onclick="Bt_clear_Click" 
                                                        style="height: 26px" Height="24px" Width="40px" />
                                               
                                                    </td>
                                                      <td style="height: 30px; width: 269px;" >
                                                      <asp:ListBox ID="ChoosedJoinTypes" runat="server" Width="186px" Height="89px"></asp:ListBox>
                                                    </td>
                                                </tr>
                                               
                                               
                                            </table>
                                            </td>
                                     </tr>
                                     <tr>
                                       <td style="background-color: #ebf0f4" class="style18">
                                       </td>
                                     </tr>
                                      <tr>
                                       <td style="width: 100%; background-color: #ebf0f4">
                                            <table id="Table1" runat="server" 
                                                style="width: 100%; background-color: #ebf0f4" class="newStyle1">
                                                <tr style="width: 710px; height: 30px" align="center">
                                                    <td style="width: 194px; height: 20px; font-size:11px">
                                                   
                                                  查询条件</td>
                                                    <td style="width: 155px; height: 20px">
                                                        &nbsp;</td>
                                                    <td style="width: 98px; height: 20px">
                                                        &nbsp;</td>
                                                    <td style="height: 20px; width: 98px;">
                                                        &nbsp;</td>
                                                        <td style="height: 20px; width: 165px;">
                                                        &nbsp;</td>
                                                </tr>
                                                <tr align="center" style="width: 710px; height: 20px">
                                                    <td style="width: 194px; height: 20px; font-size:11px">
                                                     字段</td>
                                                    <td style="width: 155px; height: 20px; font-size:11px">
                                                        条件</td>
                                                    <td style="width: 98px; height: 20px; font-size:11px">
                                               
                                                           值</td>
                                                      <td style="height: 20px; width: 90px;font-size:11px">
                                                     连接词</td>
                                                     <td style="height: 20px; width: 165px;font-size:11px">
                                                     </td>
                                                </tr>
                                                   <tr align="center" style="width: 710px; height: 30px">
                                                    <td class="style14">
                                                        <asp:DropDownList ID="dp_zd" runat="server" Height="26px" 
                                                        Width="190px" AutoPostBack="True"
                                                        style="text-align: center"  >
                                                    </asp:DropDownList>
                                                            </td>
                                                    <td class="style15">
                                                    <asp:DropDownList ID="dp_relation" runat="server" Height="26px" Width="151px" 
                                                        AutoPostBack="True">
                                                        <asp:ListItem></asp:ListItem>
                                                        <asp:ListItem Value=">">大于</asp:ListItem>
                                                        <asp:ListItem Value="<">小于</asp:ListItem>
                                                        <asp:ListItem Value="=">等于</asp:ListItem>
                                                        <asp:ListItem Value=">=">大于等于</asp:ListItem>
                                                        <asp:ListItem Value="<=">小于等于</asp:ListItem>
                                                    </asp:DropDownList>
                                                       </td>
                                                    <td class="style17">
                                               
                                                <asp:TextBox ID="tb_value" runat="server" Width="94px" Height="26px" 
                                                    style="text-align: center"></asp:TextBox>
                                                       </td>
                                                      <td style="height: 30px; width: 98px;">
                                                <asp:DropDownList ID="dp_link" runat="server" Height="26px" Width="94px">
                                                    <asp:ListItem Value="and">并且</asp:ListItem>
                                                    <asp:ListItem Value="or">或</asp:ListItem>
                                                    <asp:ListItem></asp:ListItem>
                                                </asp:DropDownList>
                                                </td>
                                                <td style="height: 30px; width: 165px;" align ="right" >
                                                <asp:Button ID="bt_addzfc" runat="server"  Text="添加" onclick="bt_addzfc_Click"
                                                    style="height: 26px" Height="24px" Width="40px" Font-Size="11px"/>
                                                <asp:Button ID="Bt_clear1" runat="server" Text="清除" onclick="Bt_clear1_Click" 
                                                          style="height: 26px" Height="24px" Width="40px" Font-Size="11px"/>
                                                       </td>
                                                </tr>
                                                    <tr align="center" style="width: 710px; height: 30px">
                                                    <td colspan="5" class="style9">
                                                      
                                                  <asp:TextBox ID="zfc" runat="server" 
                                                    TextMode="MultiLine" Height="100px" Width="674px"></asp:TextBox>
                                                                                    
                                            </td>
                                                </tr>
                                               
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                             </td>
                        </tr>
                    </table>
                    <br />
		                                 
                             
    <div style="OVERFLOW: auto; WIDTH: 680px; HEIGHT: 280px" class="css">                                               
     <FarPoint:FpSpread ID="FpSpread1" runat="server" BorderColor="Black" BorderStyle="Solid"
            BorderWidth="1px" Width="2000px" Height="2000px" ActiveSheetViewIndex="0" DesignString="&lt;?xml version=&quot;1.0&quot; encoding=&quot;utf-8&quot;?&gt;&lt;Spread /&gt;"
            CommandBar-Visible="false" EnableAjaxCall="False">
            <TitleInfo BackColor="#E7EFF7" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                Font-Size="X-Large" Font-Strikeout="False" Font-Underline="False" ForeColor=""
                HorizontalAlign="Center" VerticalAlign="NotSet" Text="">
            </TitleInfo>
            <HierBar Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                Font-Underline="False" />
            <Pager Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                Font-Underline="False" />
            <CommandBar BackColor="Control" ButtonFaceColor="Control" ButtonHighlightColor="ControlLightLight"
                ButtonShadowColor="ControlDark">
                <Background BackgroundImageUrl="SPREADCLIENTPATH:/img/cbbg.gif" />
            </CommandBar>
            <Sheets>
                <FarPoint:SheetView SheetName="Sheet1" ColumnHeaderVisible="False" DesignString="&lt;?xml version=&quot;1.0&quot; encoding=&quot;utf-8&quot;?&gt;&lt;Sheet&gt;&lt;Data&gt;&lt;RowHeader class=&quot;FarPoint.Web.Spread.Model.DefaultSheetDataModel&quot; rows=&quot;3&quot; columns=&quot;1&quot;&gt;&lt;AutoCalculation&gt;True&lt;/AutoCalculation&gt;&lt;AutoGenerateColumns&gt;True&lt;/AutoGenerateColumns&gt;&lt;ReferenceStyle&gt;A1&lt;/ReferenceStyle&gt;&lt;Iteration&gt;False&lt;/Iteration&gt;&lt;MaximumIterations&gt;1&lt;/MaximumIterations&gt;&lt;MaximumChange&gt;0.001&lt;/MaximumChange&gt;&lt;/RowHeader&gt;&lt;ColumnHeader class=&quot;FarPoint.Web.Spread.Model.DefaultSheetDataModel&quot; rows=&quot;1&quot; columns=&quot;4&quot;&gt;&lt;AutoCalculation&gt;True&lt;/AutoCalculation&gt;&lt;AutoGenerateColumns&gt;True&lt;/AutoGenerateColumns&gt;&lt;ReferenceStyle&gt;A1&lt;/ReferenceStyle&gt;&lt;Iteration&gt;False&lt;/Iteration&gt;&lt;MaximumIterations&gt;1&lt;/MaximumIterations&gt;&lt;MaximumChange&gt;0.001&lt;/MaximumChange&gt;&lt;/ColumnHeader&gt;&lt;DataArea class=&quot;FarPoint.Web.Spread.Model.DefaultSheetDataModel&quot; rows=&quot;3&quot; columns=&quot;4&quot;&gt;&lt;AutoCalculation&gt;True&lt;/AutoCalculation&gt;&lt;AutoGenerateColumns&gt;True&lt;/AutoGenerateColumns&gt;&lt;ReferenceStyle&gt;A1&lt;/ReferenceStyle&gt;&lt;Iteration&gt;False&lt;/Iteration&gt;&lt;MaximumIterations&gt;1&lt;/MaximumIterations&gt;&lt;MaximumChange&gt;0.001&lt;/MaximumChange&gt;&lt;SheetName&gt;Sheet1&lt;/SheetName&gt;&lt;/DataArea&gt;&lt;SheetCorner class=&quot;FarPoint.Web.Spread.Model.DefaultSheetDataModel&quot; rows=&quot;1&quot; columns=&quot;1&quot;&gt;&lt;AutoCalculation&gt;True&lt;/AutoCalculation&gt;&lt;AutoGenerateColumns&gt;True&lt;/AutoGenerateColumns&gt;&lt;ReferenceStyle&gt;A1&lt;/ReferenceStyle&gt;&lt;Iteration&gt;False&lt;/Iteration&gt;&lt;MaximumIterations&gt;1&lt;/MaximumIterations&gt;&lt;MaximumChange&gt;0.001&lt;/MaximumChange&gt;&lt;/SheetCorner&gt;&lt;ColumnFooter class=&quot;FarPoint.Web.Spread.Model.DefaultSheetDataModel&quot; rows=&quot;1&quot; columns=&quot;4&quot;&gt;&lt;AutoCalculation&gt;True&lt;/AutoCalculation&gt;&lt;AutoGenerateColumns&gt;True&lt;/AutoGenerateColumns&gt;&lt;ReferenceStyle&gt;A1&lt;/ReferenceStyle&gt;&lt;Iteration&gt;False&lt;/Iteration&gt;&lt;MaximumIterations&gt;1&lt;/MaximumIterations&gt;&lt;MaximumChange&gt;0.001&lt;/MaximumChange&gt;&lt;/ColumnFooter&gt;&lt;/Data&gt;&lt;Presentation&gt;&lt;AxisModels&gt;&lt;Column class=&quot;FarPoint.Web.Spread.Model.DefaultSheetAxisModel&quot; orientation=&quot;Horizontal&quot; count=&quot;4&quot;&gt;&lt;Items&gt;&lt;Item index=&quot;-1&quot;&gt;&lt;SortIndicator&gt;Ascending&lt;/SortIndicator&gt;&lt;/Item&gt;&lt;/Items&gt;&lt;/Column&gt;&lt;RowHeaderColumn class=&quot;FarPoint.Web.Spread.Model.DefaultSheetAxisModel&quot; defaultSize=&quot;40&quot; orientation=&quot;Horizontal&quot; count=&quot;1&quot;&gt;&lt;Items&gt;&lt;Item index=&quot;-1&quot;&gt;&lt;Size&gt;40&lt;/Size&gt;&lt;SortIndicator&gt;Ascending&lt;/SortIndicator&gt;&lt;/Item&gt;&lt;/Items&gt;&lt;/RowHeaderColumn&gt;&lt;ColumnHeaderRow class=&quot;FarPoint.Web.Spread.Model.DefaultSheetAxisModel&quot; defaultSize=&quot;22&quot; orientation=&quot;Vertical&quot; count=&quot;1&quot;&gt;&lt;Items&gt;&lt;Item index=&quot;-1&quot;&gt;&lt;Size&gt;22&lt;/Size&gt;&lt;/Item&gt;&lt;/Items&gt;&lt;/ColumnHeaderRow&gt;&lt;ColumnFooterRow class=&quot;FarPoint.Web.Spread.Model.DefaultSheetAxisModel&quot; defaultSize=&quot;22&quot; orientation=&quot;Vertical&quot; count=&quot;1&quot;&gt;&lt;Items&gt;&lt;Item index=&quot;-1&quot;&gt;&lt;Size&gt;22&lt;/Size&gt;&lt;/Item&gt;&lt;/Items&gt;&lt;/ColumnFooterRow&gt;&lt;/AxisModels&gt;&lt;StyleModels&gt;&lt;RowHeader class=&quot;FarPoint.Web.Spread.Model.DefaultSheetStyleModel&quot; Rows=&quot;3&quot; Columns=&quot;1&quot;&gt;&lt;AltRowCount&gt;2&lt;/AltRowCount&gt;&lt;DefaultStyle class=&quot;FarPoint.Web.Spread.NamedStyle&quot; Parent=&quot;RowHeaderDefault&quot; /&gt;&lt;ConditionalFormatCollections /&gt;&lt;/RowHeader&gt;&lt;ColumnHeader class=&quot;FarPoint.Web.Spread.Model.DefaultSheetStyleModel&quot; Rows=&quot;1&quot; Columns=&quot;4&quot;&gt;&lt;AltRowCount&gt;2&lt;/AltRowCount&gt;&lt;DefaultStyle class=&quot;FarPoint.Web.Spread.NamedStyle&quot; Parent=&quot;ColumnHeaderDefault&quot;&gt;&lt;Background class=&quot;FarPoint.Web.Spread.Background&quot;&gt;&lt;BackgroundImageUrl&gt;SPREADCLIENTPATH:/img/chbg.gif&lt;/BackgroundImageUrl&gt;&lt;SelectedBackgroundImageUrl&gt;SPREADCLIENTPATH:/img/chm.png&lt;/SelectedBackgroundImageUrl&gt;&lt;/Background&gt;&lt;/DefaultStyle&gt;&lt;ConditionalFormatCollections /&gt;&lt;/ColumnHeader&gt;&lt;DataArea class=&quot;FarPoint.Web.Spread.Model.DefaultSheetStyleModel&quot; Rows=&quot;3&quot; Columns=&quot;4&quot;&gt;&lt;AltRowCount&gt;2&lt;/AltRowCount&gt;&lt;DefaultStyle class=&quot;FarPoint.Web.Spread.NamedStyle&quot; Parent=&quot;DataAreaDefault&quot; /&gt;&lt;ConditionalFormatCollections /&gt;&lt;/DataArea&gt;&lt;SheetCorner class=&quot;FarPoint.Web.Spread.Model.DefaultSheetStyleModel&quot; Rows=&quot;1&quot; Columns=&quot;1&quot;&gt;&lt;AltRowCount&gt;2&lt;/AltRowCount&gt;&lt;DefaultStyle class=&quot;FarPoint.Web.Spread.NamedStyle&quot; Parent=&quot;CornerDefault&quot;&gt;&lt;Background class=&quot;FarPoint.Web.Spread.Background&quot;&gt;&lt;BackgroundImageUrl&gt;SPREADCLIENTPATH:/img/chbg.gif&lt;/BackgroundImageUrl&gt;&lt;SelectedBackgroundImageUrl&gt;SPREADCLIENTPATH:/img/chm.png&lt;/SelectedBackgroundImageUrl&gt;&lt;/Background&gt;&lt;/DefaultStyle&gt;&lt;ConditionalFormatCollections /&gt;&lt;/SheetCorner&gt;&lt;ColumnFooter class=&quot;FarPoint.Web.Spread.Model.DefaultSheetStyleModel&quot; Rows=&quot;1&quot; Columns=&quot;4&quot;&gt;&lt;AltRowCount&gt;2&lt;/AltRowCount&gt;&lt;DefaultStyle class=&quot;FarPoint.Web.Spread.NamedStyle&quot; Parent=&quot;ColumnFooterDefault&quot; /&gt;&lt;ConditionalFormatCollections /&gt;&lt;/ColumnFooter&gt;&lt;/StyleModels&gt;&lt;MessageRowStyle class=&quot;FarPoint.Web.Spread.Appearance&quot;&gt;&lt;BackColor&gt;LightYellow&lt;/BackColor&gt;&lt;ForeColor&gt;Red&lt;/ForeColor&gt;&lt;/MessageRowStyle&gt;&lt;SheetCornerStyle class=&quot;FarPoint.Web.Spread.NamedStyle&quot; Parent=&quot;CornerDefault&quot;&gt;&lt;Background class=&quot;FarPoint.Web.Spread.Background&quot;&gt;&lt;BackgroundImageUrl&gt;SPREADCLIENTPATH:/img/chbg.gif&lt;/BackgroundImageUrl&gt;&lt;SelectedBackgroundImageUrl&gt;SPREADCLIENTPATH:/img/chm.png&lt;/SelectedBackgroundImageUrl&gt;&lt;/Background&gt;&lt;/SheetCornerStyle&gt;&lt;AllowLoadOnDemand&gt;false&lt;/AllowLoadOnDemand&gt;&lt;LoadRowIncrement &gt;10&lt;/LoadRowIncrement &gt;&lt;LoadInitRowCount &gt;30&lt;/LoadInitRowCount &gt;&lt;TopRow&gt;0&lt;/TopRow&gt;&lt;PreviewRowStyle class=&quot;FarPoint.Web.Spread.PreviewRowInfo&quot; /&gt;&lt;/Presentation&gt;&lt;Settings&gt;&lt;Name&gt;Sheet1&lt;/Name&gt;&lt;Categories&gt;&lt;Appearance&gt;&lt;GridLineColor&gt;#d0d7e5&lt;/GridLineColor&gt;&lt;SelectionBorder class=&quot;FarPoint.Web.Spread.Border&quot; /&gt;&lt;SelectionBackColor&gt;#eaecf5&lt;/SelectionBackColor&gt;&lt;RowHeaderVisible&gt;False&lt;/RowHeaderVisible&gt;&lt;ColumnHeaderVisible&gt;False&lt;/ColumnHeaderVisible&gt;&lt;/Appearance&gt;&lt;Behavior&gt;&lt;AllowPage&gt;False&lt;/AllowPage&gt;&lt;PageSize&gt;20&lt;/PageSize&gt;&lt;AllowInsert&gt;True&lt;/AllowInsert&gt;&lt;AllowDelete&gt;True&lt;/AllowDelete&gt;&lt;/Behavior&gt;&lt;Layout&gt;&lt;RowHeaderColumnCount&gt;1&lt;/RowHeaderColumnCount&gt;&lt;ColumnHeaderRowCount&gt;1&lt;/ColumnHeaderRowCount&gt;&lt;/Layout&gt;&lt;/Categories&gt;&lt;SelectionModel class=&quot;FarPoint.Web.Spread.Model.DefaultSheetSelectionModel&quot;&gt;&lt;CellRange Row=&quot;-1&quot; Column=&quot;0&quot; RowCount=&quot;-1&quot; ColumnCount=&quot;1&quot; /&gt;&lt;/SelectionModel&gt;&lt;ActiveRow&gt;0&lt;/ActiveRow&gt;&lt;ActiveColumn&gt;0&lt;/ActiveColumn&gt;&lt;ColumnHeaderRowCount&gt;1&lt;/ColumnHeaderRowCount&gt;&lt;ColumnFooterRowCount&gt;1&lt;/ColumnFooterRowCount&gt;&lt;TitleInfo class=&quot;FarPoint.Web.Spread.TitleInfo&quot;&gt;&lt;Style class=&quot;FarPoint.Web.Spread.StyleInfo&quot;&gt;&lt;BackColor&gt;#e7eff7&lt;/BackColor&gt;&lt;HorizontalAlign&gt;Right&lt;/HorizontalAlign&gt;&lt;/Style&gt;&lt;Value type=&quot;System.String&quot; whitespace=&quot;&quot; /&gt;&lt;/TitleInfo&gt;&lt;LayoutTemplate class=&quot;FarPoint.Web.Spread.LayoutTemplate&quot;&gt;&lt;Layout&gt;&lt;ColumnCount&gt;4&lt;/ColumnCount&gt;&lt;RowCount&gt;1&lt;/RowCount&gt;&lt;/Layout&gt;&lt;Data&gt;&lt;LayoutData class=&quot;FarPoint.Web.Spread.Model.DefaultSheetDataModel&quot; rows=&quot;1&quot; columns=&quot;4&quot;&gt;&lt;AutoCalculation&gt;True&lt;/AutoCalculation&gt;&lt;AutoGenerateColumns&gt;True&lt;/AutoGenerateColumns&gt;&lt;ReferenceStyle&gt;A1&lt;/ReferenceStyle&gt;&lt;Iteration&gt;False&lt;/Iteration&gt;&lt;MaximumIterations&gt;1&lt;/MaximumIterations&gt;&lt;MaximumChange&gt;0.001&lt;/MaximumChange&gt;&lt;Cells&gt;&lt;Cell row=&quot;0&quot; column=&quot;0&quot;&gt;&lt;Data type=&quot;System.Int32&quot;&gt;0&lt;/Data&gt;&lt;/Cell&gt;&lt;Cell row=&quot;0&quot; column=&quot;1&quot;&gt;&lt;Data type=&quot;System.Int32&quot;&gt;1&lt;/Data&gt;&lt;/Cell&gt;&lt;Cell row=&quot;0&quot; column=&quot;2&quot;&gt;&lt;Data type=&quot;System.Int32&quot;&gt;2&lt;/Data&gt;&lt;/Cell&gt;&lt;Cell row=&quot;0&quot; column=&quot;3&quot;&gt;&lt;Data type=&quot;System.Int32&quot;&gt;3&lt;/Data&gt;&lt;/Cell&gt;&lt;/Cells&gt;&lt;/LayoutData&gt;&lt;/Data&gt;&lt;AxisModels&gt;&lt;LayoutColumn class=&quot;FarPoint.Web.Spread.Model.DefaultSheetAxisModel&quot; orientation=&quot;Horizontal&quot; count=&quot;4&quot;&gt;&lt;Items&gt;&lt;Item index=&quot;-1&quot;&gt;&lt;SortIndicator&gt;Ascending&lt;/SortIndicator&gt;&lt;/Item&gt;&lt;/Items&gt;&lt;/LayoutColumn&gt;&lt;LayoutRow class=&quot;FarPoint.Web.Spread.Model.DefaultSheetAxisModel&quot; orientation=&quot;Vertical&quot; count=&quot;1&quot;&gt;&lt;Items&gt;&lt;Item index=&quot;-1&quot; /&gt;&lt;/Items&gt;&lt;/LayoutRow&gt;&lt;/AxisModels&gt;&lt;/LayoutTemplate&gt;&lt;LayoutMode&gt;CellLayoutMode&lt;/LayoutMode&gt;&lt;/Settings&gt;&lt;/Sheet&gt;"
                    RowHeaderVisible="False" AllowDelete="True" AllowInsert="True" AllowPage="False">
                    <ColumnHeader Visible="False" />
                    <RowHeader Visible="False" />
                </FarPoint:SheetView>
            </Sheets>
        </FarPoint:FpSpread>
                            <FarPoint:FpSpread ID="FpSpread2" runat="server" ActiveSheetViewIndex="0" BorderColor="Black"
                                BorderStyle="Solid" BorderWidth="1px" DesignString='<?xml version="1.0" encoding="utf-8"?><Spread />'
                                Height="250px" RenderCSSClass="False" Visible="false" Width="900px">
                                <Sheets>
                                    <FarPoint:SheetView AllowPage="False" ColumnHeaderVisible="False" DesignString="&lt;?xml version=&quot;1.0&quot; encoding=&quot;utf-8&quot;?&gt;&lt;Sheet&gt;&lt;Data&gt;&lt;RowHeader class=&quot;FarPoint.Web.Spread.Model.DefaultSheetDataModel&quot; rows=&quot;3&quot; columns=&quot;1&quot;&gt;&lt;AutoCalculation&gt;True&lt;/AutoCalculation&gt;&lt;AutoGenerateColumns&gt;True&lt;/AutoGenerateColumns&gt;&lt;ReferenceStyle&gt;A1&lt;/ReferenceStyle&gt;&lt;Iteration&gt;False&lt;/Iteration&gt;&lt;MaximumIterations&gt;1&lt;/MaximumIterations&gt;&lt;MaximumChange&gt;0.001&lt;/MaximumChange&gt;&lt;/RowHeader&gt;&lt;ColumnHeader class=&quot;FarPoint.Web.Spread.Model.DefaultSheetDataModel&quot; rows=&quot;1&quot; columns=&quot;4&quot;&gt;&lt;AutoCalculation&gt;True&lt;/AutoCalculation&gt;&lt;AutoGenerateColumns&gt;True&lt;/AutoGenerateColumns&gt;&lt;ReferenceStyle&gt;A1&lt;/ReferenceStyle&gt;&lt;Iteration&gt;False&lt;/Iteration&gt;&lt;MaximumIterations&gt;1&lt;/MaximumIterations&gt;&lt;MaximumChange&gt;0.001&lt;/MaximumChange&gt;&lt;/ColumnHeader&gt;&lt;DataArea class=&quot;FarPoint.Web.Spread.Model.DefaultSheetDataModel&quot; rows=&quot;3&quot; columns=&quot;4&quot;&gt;&lt;AutoCalculation&gt;True&lt;/AutoCalculation&gt;&lt;AutoGenerateColumns&gt;True&lt;/AutoGenerateColumns&gt;&lt;ReferenceStyle&gt;A1&lt;/ReferenceStyle&gt;&lt;Iteration&gt;False&lt;/Iteration&gt;&lt;MaximumIterations&gt;1&lt;/MaximumIterations&gt;&lt;MaximumChange&gt;0.001&lt;/MaximumChange&gt;&lt;SheetName&gt;Sheet1&lt;/SheetName&gt;&lt;/DataArea&gt;&lt;/Data&gt;&lt;Presentation&gt;&lt;AxisModels&gt;&lt;Row class=&quot;FarPoint.Web.Spread.Model.DefaultSheetAxisModel&quot; defaultSize=&quot;22&quot; orientation=&quot;Vertical&quot; count=&quot;3&quot;&gt;&lt;Items&gt;&lt;Item index=&quot;-1&quot;&gt;&lt;Size&gt;22&lt;/Size&gt;&lt;/Item&gt;&lt;/Items&gt;&lt;/Row&gt;&lt;Column class=&quot;FarPoint.Web.Spread.Model.DefaultSheetAxisModel&quot; defaultSize=&quot;90&quot; orientation=&quot;Horizontal&quot; count=&quot;4&quot;&gt;&lt;Items&gt;&lt;Item index=&quot;-1&quot;&gt;&lt;Size&gt;90&lt;/Size&gt;&lt;SortIndicator&gt;Ascending&lt;/SortIndicator&gt;&lt;/Item&gt;&lt;/Items&gt;&lt;/Column&gt;&lt;RowHeaderColumn class=&quot;FarPoint.Web.Spread.Model.DefaultSheetAxisModel&quot; defaultSize=&quot;40&quot; orientation=&quot;Horizontal&quot; count=&quot;1&quot;&gt;&lt;Items&gt;&lt;Item index=&quot;-1&quot;&gt;&lt;Size&gt;40&lt;/Size&gt;&lt;SortIndicator&gt;Ascending&lt;/SortIndicator&gt;&lt;/Item&gt;&lt;/Items&gt;&lt;/RowHeaderColumn&gt;&lt;ColumnHeaderRow class=&quot;FarPoint.Web.Spread.Model.DefaultSheetAxisModel&quot; defaultSize=&quot;22&quot; orientation=&quot;Vertical&quot; count=&quot;1&quot;&gt;&lt;Items&gt;&lt;Item index=&quot;-1&quot;&gt;&lt;Size&gt;22&lt;/Size&gt;&lt;/Item&gt;&lt;/Items&gt;&lt;/ColumnHeaderRow&gt;&lt;/AxisModels&gt;&lt;StyleModels&gt;&lt;RowHeader class=&quot;FarPoint.Web.Spread.Model.DefaultSheetStyleModel&quot; Rows=&quot;3&quot; Columns=&quot;1&quot;&gt;&lt;AltRowCount&gt;2&lt;/AltRowCount&gt;&lt;DefaultStyle class=&quot;FarPoint.Web.Spread.NamedStyle&quot; Parent=&quot;HeaderDefault&quot;&gt;&lt;BackColor&gt;Control&lt;/BackColor&gt;&lt;Border class=&quot;FarPoint.Web.Spread.Border&quot; Size=&quot;1&quot; Style=&quot;Solid&quot;&gt;&lt;Bottom Color=&quot;ControlDark&quot; /&gt;&lt;Left Size=&quot;0&quot; /&gt;&lt;Right Color=&quot;ControlDark&quot; /&gt;&lt;Top Size=&quot;0&quot; /&gt;&lt;/Border&gt;&lt;HorizontalAlign&gt;Center&lt;/HorizontalAlign&gt;&lt;/DefaultStyle&gt;&lt;ConditionalFormatCollections /&gt;&lt;/RowHeader&gt;&lt;ColumnHeader class=&quot;FarPoint.Web.Spread.Model.DefaultSheetStyleModel&quot; Rows=&quot;1&quot; Columns=&quot;4&quot;&gt;&lt;AltRowCount&gt;2&lt;/AltRowCount&gt;&lt;DefaultStyle class=&quot;FarPoint.Web.Spread.NamedStyle&quot; Parent=&quot;HeaderDefault&quot;&gt;&lt;BackColor&gt;Control&lt;/BackColor&gt;&lt;Border class=&quot;FarPoint.Web.Spread.Border&quot; Size=&quot;1&quot; Style=&quot;Solid&quot;&gt;&lt;Bottom Color=&quot;ControlDark&quot; /&gt;&lt;Left Size=&quot;0&quot; /&gt;&lt;Right Color=&quot;ControlDark&quot; /&gt;&lt;Top Size=&quot;0&quot; /&gt;&lt;/Border&gt;&lt;HorizontalAlign&gt;Center&lt;/HorizontalAlign&gt;&lt;/DefaultStyle&gt;&lt;ConditionalFormatCollections /&gt;&lt;/ColumnHeader&gt;&lt;DataArea class=&quot;FarPoint.Web.Spread.Model.DefaultSheetStyleModel&quot; Rows=&quot;3&quot; Columns=&quot;4&quot;&gt;&lt;AltRowCount&gt;2&lt;/AltRowCount&gt;&lt;DefaultStyle class=&quot;FarPoint.Web.Spread.NamedStyle&quot; Parent=&quot;DataAreaDefault&quot; /&gt;&lt;ConditionalFormatCollections /&gt;&lt;/DataArea&gt;&lt;/StyleModels&gt;&lt;MessageRowStyle class=&quot;FarPoint.Web.Spread.Appearance&quot;&gt;&lt;BackColor&gt;LightYellow&lt;/BackColor&gt;&lt;ForeColor&gt;Red&lt;/ForeColor&gt;&lt;/MessageRowStyle&gt;&lt;SheetCornerStyle class=&quot;FarPoint.Web.Spread.NamedStyle&quot; Parent=&quot;HeaderDefault&quot; /&gt;&lt;AllowLoadOnDemand&gt;false&lt;/AllowLoadOnDemand&gt;&lt;LoadRowIncrement &gt;10&lt;/LoadRowIncrement &gt;&lt;LoadInitRowCount &gt;30&lt;/LoadInitRowCount &gt;&lt;TopRow&gt;0&lt;/TopRow&gt;&lt;/Presentation&gt;&lt;Settings&gt;&lt;Name&gt;Sheet1&lt;/Name&gt;&lt;Categories&gt;&lt;Appearance&gt;&lt;RowHeaderVisible&gt;False&lt;/RowHeaderVisible&gt;&lt;ColumnHeaderVisible&gt;False&lt;/ColumnHeaderVisible&gt;&lt;SelectionBorder class=&quot;FarPoint.Web.Spread.Border&quot; /&gt;&lt;/Appearance&gt;&lt;Behavior&gt;&lt;AllowPage&gt;False&lt;/AllowPage&gt;&lt;/Behavior&gt;&lt;Layout&gt;&lt;ColumnHeaderRowCount&gt;1&lt;/ColumnHeaderRowCount&gt;&lt;RowHeaderColumnCount&gt;1&lt;/RowHeaderColumnCount&gt;&lt;/Layout&gt;&lt;/Categories&gt;&lt;ColumnHeaderRowCount&gt;1&lt;/ColumnHeaderRowCount&gt;&lt;/Settings&gt;&lt;/Sheet&gt;"
                                        RowHeaderVisible="False" SheetName="Sheet1">
                                    </FarPoint:SheetView>
                                </Sheets>
                                <Pager Align="Left" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Mode="Number" PageCount="20" Position="Bottom" />
                            </FarPoint:FpSpread>
                            </div>
                     </td>
            </tr>
        </table>
    </form>
</body>
</html>

