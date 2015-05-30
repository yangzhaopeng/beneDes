<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="xypj.aspx.cs" Inherits="beneDesCYC.view.oilAssessment.xypj" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>效益评价</title>
</head>
<body style="width: 974px;text-align: center;  " background="/static/image/bg.jpg">
    <form id="form1" runat="server">
    <div>
    </div>
        <div style="text-align: center">
            <div style="text-align: center">
                <div style="text-align: center">
                    <table style="width: 690px">
                        <tr>
                            <td style="width: 21px">
                                &nbsp;</td>
                            <td colspan="2" style="width: 669px; text-align: left">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td colspan="3" align ="left" >
                                &nbsp;</td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
        <table style="width: 685px">
            <tr>
                <td style="width: 685px; height: 35px; text-align: left;">
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="width: 685px; height: 20px; text-align: left">
                                    <asp:Label ID="Label5" runat="server" Font-Size="Smaller" Text="Label" 
                                        ForeColor="Red" Visible="False"></asp:Label></td>
            </tr>
            <tr>
                <td style="width: 685px; height: 426px; text-align: left; vertical-align : top">
                    <asp:Panel ID="Panel1" runat="server"  Width="685px">
                        <div style="text-align: left">
                            <table style="width: 100%">
                                <tr>
                                    <td colspan="2" style="height: 28px">
                                        <strong><span style="font-size: 10pt">步骤列表:</span></strong></td>
                                    <td style="width: 500px; height: 28px;">
                                    </td>
                                    <td style="width: 10px; height: 28px;">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 5px; height: 26px;">
                                    </td>
                                    <td style="width: 200px; height: 26px; vertical-align :top">
                                        <span style="font-size: 10pt">创建单井动态评价数据</span></td>
                                    <td style="width: 500px; height: 26px;vertical-align :top">
                                    <asp:Label ID="Label1" runat="server" Font-Size="Smaller" Text="Label1" ForeColor="Red"></asp:Label></td>
                                    <td style="width: 10px; height: 26px;">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 5px; height: 26px;">
                                    </td>
                                    <td style="width: 200px; height: 26px; vertical-align :top">
                                        <span style="font-size: 10pt">汇总计算评价单元动态数据</span></td>
                                    <td style="width: 500px; height: 26px;vertical-align :top">
                                    <asp:Label ID="Label2" runat="server" Font-Size="Smaller" Text="Label" ForeColor="Red"></asp:Label></td>
                                    <td style="width: 10px; height: 26px;">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 5px; height: 26px;">
                                    </td>
                                    <td style="width: 200px; height: 26px; vertical-align :top">
                                        <span style="font-size: 10pt">汇总计算区块动态数据</span></td>
                                    <td style="width: 500px; height: 26px;vertical-align :top">
                                    <asp:Label ID="Label3" runat="server" Font-Size="Smaller" Text="Label" ForeColor="Red"></asp:Label></td>
                                    <td style="width: 10px; height: 26px;">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 5px; height: 26px;">
                                    </td>
                                    <td style="width: 161px; height: 26px;">
                                        <span style="font-size: 10pt">
                                            <asp:Label ID="Labelctime" runat="server" Font-Size="10pt" Text="Label"></asp:Label></span></td>
                                    <td style="width: 500px; height: 26px;">
                                        <asp:Label ID="Label4" runat="server" Font-Size="10pt" ForeColor="#FF0000" Text="Label"></asp:Label></td>
                                    <td style="width: 10px; height: 26px;">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 5px">
                                    </td>
                                    <td style="width: 161px">
                                    </td>
                                    <td style="width: 500px">
                                    </td>
                                    <td style="width: 10px">
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </asp:Panel>
                </td>
            </tr>
        </table>
    </form>
    <%--<script language="javascript">
    function   ShowBar() 
{ 
       if(document.getElementById("<%=Panel1.ClientID%>")!=null)
       {
       document.getElementById("<%=Panel1.ClientID%>").style.visibility="hidden";  
       }
   
        document.getElementById( "Load_Div").style.display= "block";
}
 </script>
 <div   style= "height:   24px;   width:   167px;   padding:5px;z-index:10;left:350px; top:115px; position:   absolute;display:none; background-color:#eeeeee;filter:alpha(opacity=80); background-image: url('41.gif'); background-repeat: no-repeat;"   
    id= "Load_Div"> 
                <img   src= "loading.gif "   />  <span style="position:relative;color:Black"> 正在处理中... </span>
</div> --%>
</body>
</html>

