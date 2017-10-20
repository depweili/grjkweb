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
        <asp:Button ID="btnSyncTime" runat="server" Text="同步时间" OnClick="btnSyncTime_Click" />
        <asp:Button ID="btnShowReLst" runat="server" Text="结果列表" OnClick="btnShowReLst_Click" />
        <asp:Button ID="btnClose" runat="server" Text="close" OnClick="btnClose_Click" />
        <asp:Button ID="btnReRegister" runat="server" Text="ReRegister" OnClick="btnReRegister_Click" />
        <br />
        <asp:TextBox ID="CodeBox1" runat="server" ></asp:TextBox>
        <br />
        <asp:TextBox ID="Message" runat="server" TextMode="MultiLine" Height="200px" Width="600px"></asp:TextBox>
        <asp:TextBox ID="Message2" runat="server" TextMode="MultiLine" Height="200px" Width="600px"></asp:TextBox>
        <asp:Repeater ID="Repeater1" runat="server"></asp:Repeater>
    </div>
    </form>
</body>
</html>
