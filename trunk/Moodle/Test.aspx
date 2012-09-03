<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Test.aspx.cs" Inherits="Moodle.MoodleWebserviceTest" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Moodle Service Test</title>
    <style type="text/css">
        .style1
        {
        }
        .style2
        {
            width: 86px;
        }
        .style3
        {
            width: 83px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
<table style="border: 2px solid lavender; width: 100%;" 
        bgcolor="Snow" cellpadding="4" cellspacing="0">
    <tr>
        <td align="center" class="style1" 
            style="border-style: solid; border-width: 1px; border-color: lavender; font-weight: bold" 
            colspan="2">
            GET TOKEN</td>
    </tr>
    <tr>
        <td align="right" class="style3" style="font-weight: bold">
            &nbsp;
            UserName</td>
        <td>
            <asp:TextBox ID="txtUsername" runat="server" Width="250px" 
                ValidationGroup="token">admin</asp:TextBox>
&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                ControlToValidate="txtUsername" ErrorMessage="RequiredFieldValidator" 
                ForeColor="Red" ValidationGroup="token"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td align="right" class="style3" style="font-weight: bold">
            &nbsp;
            Password</td>
        <td>
            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" Width="250px" 
                ValidationGroup="token">Trung8290/</asp:TextBox>
&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                ControlToValidate="txtPassword" ErrorMessage="RequiredFieldValidator" 
                ForeColor="Red" ValidationGroup="token"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td align="right" class="style3">
            &nbsp;</td>
        <td align="left">
            <asp:Button ID="btnGetToken" runat="server" onclick="btnGetToken_Click" 
                Text="Get Token" ValidationGroup="token" />
        </td>
    </tr>
    <tr>
        <td align="right" class="style3" style="font-weight: bold">
            &nbsp;
            Token</td>
        <td>
            <asp:TextBox ID="txtToken" runat="server" ReadOnly="True" Width="250px"></asp:TextBox>
        </td>
    </tr>
</table>
<table style="border: 2px solid lavender; width: 100%; height: 100%;" 
        bgcolor="Snow" cellpadding="4" cellspacing="0">
    <tr>
        <td align="center" class="style1" 
            style="border-style: solid; border-width: 2px; border-color: lavender; font-weight: bold" 
            colspan="2">
            REST TEST</td>
    </tr>
    <tr>
        <td align="right" class="style2" style="font-weight: bold">
            &nbsp; Wsfunction</td>
        <td>
            <asp:DropDownList ID="ddlWsfuntion" runat="server" Height="22px" 
                ValidationGroup="REST" Width="248px">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td align="right" class="style2" style="font-weight: bold">
            &nbsp; Post data</td>
        <td>
            <asp:TextBox ID="txtPostData" runat="server" Width="100%"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td align="right" class="style2" style="font-weight: bold">
            XPath</td>
        <td align="left">
            <asp:TextBox ID="txtXPath" runat="server" Width="100%"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td align="right" class="style2" style="font-weight: bold">
            &nbsp;</td>
        <td align="left">
            &nbsp;<asp:Button ID="btnGetTreeView" runat="server" onclick="btnGetTreeView_Click" 
                Text="Get tree view" />
        </td>
    </tr>
    <tr>
        <td align="right" class="style2" 
            style="border-style: solid; border-width: 2px; border-color: lavender;" 
            height="100px" valign="top">
            <strong>Result</strong></td>
        <td height="100px" 
            style="border-style: solid; border-width: 2px; border-color: lavender" 
            valign="top">
            <asp:TreeView ID="resultTree" runat="server" ShowLines="True" Width="100%">
            </asp:TreeView>
        </td>
    </tr>
</table>
    <asp:TextBox ID="txtList" runat="server" Height="500px" TextMode="MultiLine" 
        Width="100%"></asp:TextBox>
    </form>
</body>
</html>

