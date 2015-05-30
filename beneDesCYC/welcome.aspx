<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="welcome.aspx.cs" Inherits="beneDesCYC.welcome" %>

<!DOCTYPE html >

<html>
<head runat="server">
    <title>欢迎</title>
    <link href="static/css/welcome.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <div class="tip">
        您好！<span><%=Session["userName"] %></span>，欢迎您使用该系统！
    </div>
</body>
</html>
