<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="beneDesCYC.index" %>

<!DOCTYPE html>
<html>
<head>
    <title>单井效益评价系统BeneDes V2.0（厂级）</title>
    <link href="static/css/index.css" rel="stylesheet" type="text/css" />
    <link href="static/Extjs/resources/css/ext-all.css" rel="stylesheet" type="text/css" />

    <script src="static/Extjs/ext-all.js" type="text/javascript"></script>

    <script src="static/Extjs/locale/ext-lang-zh_CN.js" type="text/javascript"></script>

    <script src="static/js/common/dataJs.js" type="text/javascript"></script>

    <script type="text/javascript">
        var userName = '<%=Session["userName"] %>';
        var month = '<%=Session["month"].ToString() %>';
    </script>

    <script src="static/js/index.js" type="text/javascript"></script>

    <meta http-equiv="x-ua-compatible" content="IE=7" />
</head>
<body>
</body>
</html>
