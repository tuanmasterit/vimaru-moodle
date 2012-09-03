<%@ Page EnableEventValidation="False" Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="GetTokenAndServiceList.aspx.cs" Inherits="Moodle.GetTokenAndServiceList" %>
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
                        colspan="2">Get Token &amp; Function List</td>
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
                        <asp:TextBox ID="txtServiceShortName" runat="server" ValidationGroup="token" 
                            Width="200px" CssClass="textBox" ToolTip="Tên viết tắt của dịch vụ"></asp:TextBox>
                        &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                            ControlToValidate="txtServiceShortName" ErrorMessage="*" 
                            ForeColor="Red" ValidationGroup="token"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td align="right" class="style3">
                        &nbsp;</td>
                    <td align="left">
                        <asp:Button ID="btnGetToken" runat="server" onclick="btnGetToken_Click" 
                            Text="Get Token" ValidationGroup="token" ToolTip="Lấy chuỗi token" />
                        <asp:Button ID="btnGetFunctionList" runat="server" 
                            onclick="btnGetFunctionList_Click" Text="Get Function List" 
                            ToolTip="Lấy danh sách các hàm của dịch vụ web" />
                    </td>
                </tr>
                <tr>
                    <td align="right" class="style3" style="font-weight: bold">Token</td>
                    <td>
                        <asp:TextBox ID="txtToken" runat="server" Width="283px" 
                            CssClass="textBox" ToolTip="Chuỗi token"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" class="style3" style="font-weight: bold">
                        &nbsp;</td>
                    <td>
                        <asp:TextBox ID="txtFunctions" runat="server" CssClass="textBox" Height="250px" 
                            TextMode="MultiLine" ToolTip="Các hàm của dịch vụ web" Width="283px"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
        </asp:UpdatePanel>
</asp:Content>
