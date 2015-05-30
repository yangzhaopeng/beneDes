﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="commonSpreadFrameXYOil.aspx.cs" Inherits="beneDesYGS.view.common.commonSpreadFrameXYOil" %>

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
        var bny = '<%=Session["bny"].ToString() %>'; //添加开始时间@yzp
        var eny = '<%=Session["eny"].ToString() %>';
    </script>
    <script src="../../static/js/common/loading.js" type="text/javascript"></script>
    <script src="../../static/Extjs/locale/ext-lang-zh_CN.js" type="text/javascript"></script>
    <script src="../../static/js/common/commonSpreadFrameXYOil.js" type="text/javascript"></script>
    <script src="../../static/Extjs/MonthField.js" type="text/javascript"></script>
</head>
<body>
</body>
</html>
