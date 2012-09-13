<%@ Page EnableEventValidation="False" Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Moodle.Token" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style1
        {
            font-weight: bold;
            text-align: right;
            padding-right: 4px;
            width: 16%;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" 
        AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            <div class="centerDiv">
                <img alt="" src="App_Images/loading_icon%20(2).gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table cellpadding="4" cellspacing="0" class="table">
                <tr>
                    <td class="tableHeader" 
                        colspan="3">Get Token &amp; Function List</td>
                </tr>
                <tr>
                    <td class="style1">Username</td>
                    <td colspan="2">
                        <asp:TextBox ID="txtUsername" runat="server" Width="195px" 
                            ValidationGroup="token" CssClass="textBox" ToolTip="Tên đăng nhập"></asp:TextBox>
            &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                            ControlToValidate="txtUsername" ErrorMessage="*" 
                            ForeColor="Red" ValidationGroup="token"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="style1">Password</td>
                    <td colspan="2">
                        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" Width="195px" 
                            ValidationGroup="token" CssClass="textBox" ToolTip="Mật khẩu"></asp:TextBox>
            &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                            ControlToValidate="txtPassword" ErrorMessage="*" 
                            ForeColor="Red" ValidationGroup="token"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="style1">Service Name</td>
                    <td align="left">
                        <asp:DropDownList ID="cboService" runat="server" CssClass="dropDownList" 
                            DataTextField="FullName" DataValueField="ShortName" Width="203px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="style1">
                        &nbsp;</td>
                    <td align="left" colspan="2">
                        <asp:Button ID="btnGetToken" runat="server" onclick="btnGetToken_Click" 
                            Text="Get Token" ValidationGroup="token" ToolTip="Lấy chuỗi token" />
                        <asp:Button ID="btnGetFunctionList" runat="server" 
                            onclick="btnGetFunctionList_Click" Text="Get Function List" 
                            ToolTip="Lấy danh sách các hàm của dịch vụ web" />
                        <asp:LinkButton ID="btnRedirectLogin" runat="server" Font-Underline="False" 
                            PostBackUrl="~/Login.aspx">Redirect Login</asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td class="style1">Token</td>
                    <td colspan="2">
                        <asp:TextBox ID="txtToken" runat="server" Width="283px" 
                            CssClass="textBox" ToolTip="Chuỗi token"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style1">
                        &nbsp;</td>
                    <td colspan="2">
                        <asp:TextBox ID="txtFunctions" runat="server" CssClass="textBox" Height="250px" 
                            TextMode="MultiLine" ToolTip="Các hàm của dịch vụ web" Width="283px"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
        </asp:UpdatePanel>
</asp:Content>
