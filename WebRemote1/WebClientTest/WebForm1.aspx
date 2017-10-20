<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="WebClientTest.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Button ID="btnSyncTime" runat="server" Text="Button" OnClick="btnSyncTime_Click" />
        <br />
        <asp:TextBox ID="Message" runat="server" TextMode="MultiLine" Height="200px" Width="600px"></asp:TextBox>
    </div>
    </form>
</body>
</html>
