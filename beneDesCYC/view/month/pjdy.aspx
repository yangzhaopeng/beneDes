<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="pjdy.aspx.cs" Inherits="beneDesCYC.view.month.pjdy" %>

<!DOCTYPE html>
<html>
<head>
    <title>评价单元</title>
    <link href="../../static/css/icon.css" rel="stylesheet" type="text/css" />
    <link href="../../static/Extjs/resources/css/ext-all.css" rel="stylesheet" type="text/css" />

    <script src="../../static/Extjs/ext-all.js" type="text/javascript"></script>

    <script src="../../static/Extjs/locale/ext-lang-zh_CN.js" type="text/javascript"></script>

    <script src="../../static/js/common/dataJs.js" type="text/javascript"></script>

    <script type="text/javascript">
        var userName = '<%=Session["userName"] %>';
        var month = '<%=Session["month"].ToString() %>';
    </script>

    <script src="../../static/js/month/pjdy.js" type="text/javascript"></script>

    <script src="../../static/Extjs/MonthField.js" type="text/javascript"></script>

    <!-- Example -->
    <%-- <script type="text/javascript" src="../../static/js/month/multiselect-demo.js"></script>

       <script src="../../static/Extjs/src/ux/form/ItemSelector.js" type="text/javascript"></script>

    <script src="../../static/Extjs/src/ux/form/MultiSelect.js" type="text/javascript"></script>--%>
</head>
<body>
    <div id="itemselector" class="demo-ct">
    </div>
</body>
</html>
