<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="commonSpreadFrame17.aspx.cs" Inherits="beneDesYGS.view.common.commonSpreadFrame17" %>

<!DOCTYPE html>

<html>
<head>
    <title>farPointSpread外层通用框架</title>
    <link href="../../static/css/icon.css" rel="stylesheet" type="text/css"/>
    <link href="../../static/Extjs/resources/css/ext-all.css" rel="stylesheet" type="text/css"/>
    
    <script src="../../static/Extjs/ext-all.js" type="text/javascript"></script>
    <script src="../../static/js/common/dataJs.js" type="text/javascript"></script>
    <script type="text/javascript">
        var userName='<%=Session["userName"] %>';
        var month = '<%=Session["month"].ToString() %>';
        var jeny = '<%=Session["jeny"].ToString() %>';
        var bny = '<%=Session["bny"].ToString() %>'; //添加开始时间@yzp
        var eny = '<%=Session["eny"].ToString() %>';
        var jbny = '<%=Session["jbny"].ToString() %>';
        
                var enys = jeny;
                var daten = enys.substr(0, 4);
                var datey = enys.substr(4, 2);
                var n = 0;
                switch (datey)
                {
                    case "02": datey = "01"; break;
                    case "03": datey = "02"; break;
                    case "04": datey = "03"; break;
                    case "05": datey = "04"; break;
                    case "06": datey = "05"; break;
                    case "07": datey = "06"; break;
                    case "08": datey = "07"; break;
                    case "09": datey = "08"; break;
                    case "10": datey = "09"; break;
                    case "11": datey = "10"; break;
                    case "12": datey = "11"; break;
                    case "01": datey = "12"; n = parseInt(daten.toString()) - 1; daten = n.toString(); break;
                    default: break;
                }
                var leny = (daten + datey).toString();
        
    </script>
    <script src="../../static/js/common/loading.js" type="text/javascript"></script>
    <script src="../../static/js/common/commonSpreadFrame17.js" type="text/javascript"></script>
</head>
<body>
</body>
</html>

