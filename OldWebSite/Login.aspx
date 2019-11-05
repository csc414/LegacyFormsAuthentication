<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="OldWebSite.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <% if (User.Identity.IsAuthenticated) { %>
    <span>用户名:<%= User.Identity.Name %></span>
    <form method="post">
        <input type="submit" name="type" value="Logout" />
    </form>
    <% } else { %>
    <form method="post">
        <input type="text" name="name" value="tester" />
        <input type="submit" name="type" value="Login" />
    </form>
    <% } %>
</body>
</html>
