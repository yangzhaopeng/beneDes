﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="qtqkfy.aspx.cs" Inherits="beneDesCYC.view.month.qtqkfy" %>

<!DOCTYPE html>

<html>
<head>
    <title>区块费用</title>
    <link href="../../static/css/icon.css" rel="stylesheet" type="text/css"/>
    <link href="../../static/Extjs/resources/css/ext-all.css" rel="stylesheet" type="text/css"/>
    <script src="../../static/Extjs/ext-all.js" type="text/javascript"></script>
    <script src="../../static/js/common/dataJs.js" type="text/javascript"></script>
    <script type="text/javascript">
        var userName='<%=Session["userName"] %>';
        var depName='<%=Session["depName"] %>';
        var month = '<%=Session["month"].ToString() %>';
        var totalProperty = '<%=Session["totalProperty"] %>';
    </script>
    <script src="../../static/js/month/qtqkfy.js" type="text/javascript"></script> 
     <script src="../../static/Extjs/MonthField.js" type="text/javascript"></script> 
</head>
<body>
</body>
</html>
