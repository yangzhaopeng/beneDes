<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="commonSpreadFrameDJDONGTAI.aspx.cs" Inherits="beneDesYGS.view.common.commonSpreadFrameDJDONGTAI" %>

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
    </script>
    <script src="../../static/js/common/loading.js" type="text/javascript"></script>
    <script src="../../static/js/common/commonSpreadFrameDJDONGTAI.js" type="text/javascript"></script>
</head>
<body>
</body>
</html>
