<%@ Page EnableEventValidation="False" Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Moodle.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table cellpadding="4" cellspacing="0" class="table">
        <tr>
            <td class="tableHeader" 
                colspan="2">Login Moodle Server</td>
        </tr>
        <tr>
            <td class="cellHeaderRight">
                Username:</td>
            <td>
                <asp:TextBox ID="txtUsername" runat="server" Width="180px" 
                    ValidationGroup="token" CssClass="textBox" ToolTip="Tên đăng nhập"></asp:TextBox>
    &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                    ControlToValidate="txtUsername" ErrorMessage="*" 
                    ForeColor="Red" ValidationGroup="token"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td align="left" class="cellHeaderRight" style="font-weight: bold">
                Password:</td>
            <td>
                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" Width="180px" 
                    ValidationGroup="token" CssClass="textBox" ToolTip="Mật khẩu"></asp:TextBox>
    &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                    ControlToValidate="txtPassword" ErrorMessage="*" 
                    ForeColor="Red" ValidationGroup="token"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td align="left" class="cellHeaderRight" style="font-weight: bold">Service Name:</td>
            <td align="left" class="style1" valign="bottom">
        <asp:DropDownList ID="cboService" runat="server" CssClass="dropDownList" 
                    DataTextField="FullName" DataValueField="ShortName" Width="188px">
        </asp:DropDownList>
                <asp:Button ID="btnLogin" runat="server" onclick="btnLogin_Click" Text="Login" 
                    ToolTip="Lấy chuỗi token" ValidationGroup="token" Width="60px" />
                        <asp:LinkButton ID="btnRedirectDefault" runat="server" Font-Underline="False" 
                            PostBackUrl="~/Default.aspx">Redirect Default</asp:LinkButton>
            </td>
        </tr>
        <tr>
            <td align="right" class="style3">
                &nbsp;</td>
            <td align="left">
                &nbsp;</td>
        </tr>
    </table>
</asp:Content>
