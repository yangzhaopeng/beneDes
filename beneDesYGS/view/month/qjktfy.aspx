<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="qjktfy.aspx.cs" Inherits="beneDesYGS.view.month.qjktfy" %>

<!DOCTYPE html>

<html>
<head>
    <title>期间勘探费用</title>
    <link href="../../static/css/icon.css" rel="stylesheet" type="text/css"/>
    <link href="../../static/Extjs/resources/css/ext-all.css" rel="stylesheet" type="text/css"/>
    <script src="../../static/Extjs/ext-all.js" type="text/javascript"></script>
    <script src="../../static/Extjs/locale/ext-lang-zh_CN.js" type="text/javascript"></script>
    <script src="../../static/js/common/dataJs.js" type="text/javascript"></script>
    <script type="text/javascript">
        var userName='<%=Session["userName"] %>';
        var month = '<%=Session["month"].ToString() %>';
    </script>
    <script src="../../static/js/month/qjktfy.js" type="text/javascript"></script>
</head>
<body>
</body>
</html>
