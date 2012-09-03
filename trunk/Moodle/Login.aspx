<%@ Page EnableEventValidation="False" Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Moodle.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">

    .style3
    {
        width: 112px;
    }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" 
        AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            <div style="background-color: #FFF9F9; text-align: center;">
                <img alt="" src="App_Images/loading_icon%20(2).gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table cellpadding="4" cellspacing="0" class="table">
                <tr>
                    <td class="tableHeader" 
                        colspan="2">Login Moodle Server</td>
                </tr>
                <tr>
                    <td align="right" class="style3" style="font-weight: bold">
                        &nbsp;
                        Username</td>
                    <td>
                        <asp:TextBox ID="txtUsername" runat="server" Width="200px" 
                            ValidationGroup="token" CssClass="textBox" ToolTip="Tên đăng nhập"></asp:TextBox>
            &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                            ControlToValidate="txtUsername" ErrorMessage="*" 
                            ForeColor="Red" ValidationGroup="token"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td align="right" class="style3" style="font-weight: bold">
                        &nbsp;
                        Password</td>
                    <td>
                        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" Width="200px" 
                            ValidationGroup="token" CssClass="textBox" ToolTip="Mật khẩu"></asp:TextBox>
            &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                            ControlToValidate="txtPassword" ErrorMessage="*" 
                            ForeColor="Red" ValidationGroup="token"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td align="right" class="style3">
                        <strong>Service Name </strong></td>
                    <td align="left">
                        <asp:DropDownList ID="ddlService" runat="server" CssClass="dropDownList" 
                            ToolTip="Loại dịch vụ">
                            <asp:ListItem Selected="True" Value="all_service">All Service</asp:ListItem>
                            <asp:ListItem Value="course">Course Manager</asp:ListItem>
                            <asp:ListItem Value="enrol">Enrol Manager</asp:ListItem>
                            <asp:ListItem Value="group">Group Manager</asp:ListItem>
                            <asp:ListItem Value="role">Role Manager</asp:ListItem>
                            <asp:ListItem Value="user">User Manager</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="right" class="style3">
                        &nbsp;</td>
                    <td align="left">
                        <asp:Button ID="btnLogin" runat="server" onclick="btnLogin_Click" 
                            Text="Login" ValidationGroup="token" ToolTip="Lấy chuỗi token" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
        </asp:UpdatePanel>
</asp:Content>
