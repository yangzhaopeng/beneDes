<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="dataClearGas.aspx.cs" Inherits="beneDesCYC.view.month.dataClearGas" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
    <link href="../../static/css/icon.css" rel="stylesheet" type="text/css" />
    <link href="../../static/Extjs/resources/css/ext-all.css" rel="stylesheet" type="text/css" />

    <script src="../../static/Extjs/ext-all.js" type="text/javascript"></script>

    <script src="../../static/Extjs/locale/ext-lang-zh_CN.js" type="text/javascript"></script>

    <script src="../../static/js/common/dataJs.js" type="text/javascript"></script>

    <script src="../../static/Extjs/MonthField.js" type="text/javascript"></script>

    <script src="../../static/js/month/dataClearGas.js" type="text/javascript"></script>

    <script type="text/javascript">
        var userName='<%=Session["userName"] %>';
        var month = '<%=Session["month"].ToString() %>';
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
    </div>
    </form>
</body>
</html>
