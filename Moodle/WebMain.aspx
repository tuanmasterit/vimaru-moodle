<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="WebMain.aspx.cs" Inherits="Moodle.WebMain" %> 
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style9
        {
            height: 25px;
            width: 102px;
        }
        .style10
        {
        }
    .style11
    {
        height: 25px;
        width: 106px;
        font-weight: 700;
    }
    </style>
    <script language="javascript" type="text/javascript">
        function CheckUncheckAll() {
            var i; CheckBoxs = document.getElementsByTagName("input");

            for (i = 0; i < CheckBoxs.length; i++) {
                if (CheckBoxs[i].type == "checkbox") {
                    CheckBoxs[i].checked = document.getElementById("chkAll").checked;
                    HighlightRow(CheckBoxs[i]);
                }
            }
        }
        function HighlightRow(chkB) {
            var IsChecked = chkB.checked;
            if (IsChecked) {
                chkB.parentElement.parentElement.style.backgroundColor = '#fdffb8';
            } else {
                chkB.parentElement.parentElement.style.backgroundColor = chkB.parentElement.style.backgroundColor;
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server" />
    <asp:LinqDataSource ID="LinqDataSourceTaiKhoan" runat="server" 
        ContextTypeName="Moodle.DCVimaruDataContext" EnableDelete="True" 
        EnableUpdate="True" EntityTypeName="" 
        onselecting="LinqDataSourceTaiKhoan_Selecting" TableName="DangKies">
    </asp:LinqDataSource>
    <asp:LinqDataSource ID="LinqDataSourceHocPhan" runat="server" 
        ContextTypeName="Moodle.DCVimaruDataContext" EntityTypeName="" 
        TableName="HocPhans" onselecting="LinqDataSourceHocPhan_Selecting">
    </asp:LinqDataSource>
    <asp:TextBox ID="txtMaSV" runat="server" Visible="False" Wrap="False"></asp:TextBox>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" 
        AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            <div align="center" style="background-color: #FFFAFA">
                <img alt="" src="App_Images/loading_icon%20(2).gif" align="middle" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="border: 2px solid lavender; width: 100%;" 
        bgcolor="Snow" cellpadding="4" cellspacing="0">
                <tr>
                    <td align="center" class="style1" colspan="2" 
                        style="border: 2px solid #dbddff;">
                        <strong>ĐĂNG NHẬP<asp:Label ID="lblErrorMessage" runat="server" ForeColor="Red" 
                            style="margin-left: 0px" Width="100%"></asp:Label>
                        </strong></td>
                </tr>
                <tr>
                    <td align="right" class="style9" style="font-weight: bold">
                        &nbsp;Tên đăng nhập</td>
                    <td class="style10">
                        <asp:TextBox ID="txtUsername" runat="server" Width="250px" 
                            ValidationGroup="token"></asp:TextBox>
            &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                            ControlToValidate="txtUsername" ErrorMessage="RequiredFieldValidator" 
                            ForeColor="Red" ValidationGroup="token"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td align="right" class="style9" style="font-weight: bold">
                        &nbsp; Mật khẩu</td>
                    <td class="style10">
                        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" Width="250px" 
                            ValidationGroup="token"></asp:TextBox>
            &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                            ControlToValidate="txtPassword" ErrorMessage="RequiredFieldValidator" 
                            ForeColor="Red" ValidationGroup="token"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td align="right" class="style9">
                        &nbsp;</td>
                    <td align="left" class="style10">
                        <asp:Button ID="btnGetToken" runat="server" onclick="btnGetToken_Click" 
                            Text="Đăng nhập" ValidationGroup="token" />
                        &nbsp;<asp:TextBox ID="txtToken" runat="server" ReadOnly="True" 
                            ValidationGroup="token" Visible="False" Width="250px"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <table align="center" bgcolor="Snow" border="1px" cellpadding="4" 
                style="border: thin solid #dbddff; width: 100%;">
                <tr>
                    <td align="center" colspan="2">
                        <strong>TẠO TÀI KHOẢN</strong></td>
                </tr>
                <tr>
                    <td style="border: 2px solid #dbddff; font-size: 14px; " valign="middle" 
                        colspan="2">
                        Lớp học phần:&nbsp;
                        <asp:DropDownList ID="cboAcountFilter" runat="server" AutoPostBack="True" 
                            DataSourceID="LinqDataSourceHocPhan" DataTextField="TenHP" 
                            DataValueField="MaHP" Font-Size="13px" Height="20px" 
                            style="margin-left: 0px" Width="300px">
                        </asp:DropDownList>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td colspan="2" style="border: 2px solid #dbddff;">
                        <asp:GridView ID="grvTaiKhoan" runat="server" AllowPaging="True" 
                            AllowSorting="True" AutoGenerateColumns="False" CellPadding="6" 
                            CssClass="DDGridView" DataKeyNames="MaSV" 
                            DataSourceID="LinqDataSourceTaiKhoan" EnableModelValidation="False" 
                            onprerender="grvTaiKhoan_PreRender" onrowdatabound="grvTaiKhoan_RowDataBound" 
                            ShowFooter="True" ShowHeaderWhenEmpty="True" Width="100%" 
                            onpageindexchanging="grvTaiKhoan_PageIndexChanging" 
                            onselectedindexchanged="grvTaiKhoan_SelectedIndexChanged">
                            <Columns>
                                <asp:TemplateField> 
                                    <HeaderTemplate >
                                    <input type="checkbox" id="chkAll" onclick="CheckUncheckAll();" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                         <asp:CheckBox ID="chk" runat="server" onclick="HighlightRow(this);"/>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle Width="20px" HorizontalAlign="Center" Wrap="False" />
                                </asp:TemplateField>
                                <asp:CommandField SelectText="Sửa" ShowSelectButton="True">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                                </asp:CommandField>
                                <asp:BoundField DataField="Id" HeaderText="Id" ReadOnly="True" 
                                    SortExpression="Id">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="60px" 
                                    Wrap="False" />
                                </asp:BoundField>
                                <asp:BoundField DataField="MaSV" HeaderText="Mã sinh viên" ReadOnly="True" 
                                    SortExpression="MaSV">
                                <FooterStyle Wrap="False" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="110px" 
                                    Wrap="False" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Ho" HeaderText="Họ và đệm" ReadOnly="True" 
                                    SortExpression="Ho">
                                <ItemStyle Width="130px" Wrap="False" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Ten" HeaderText="Tên" ReadOnly="True" 
                                    SortExpression="Ten">
                                <ItemStyle Wrap="False" Width="70px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Email" HeaderText="Email" ReadOnly="True" 
                                    SortExpression="Email">
                                <ItemStyle Wrap="False" />
                                </asp:BoundField>
                            </Columns>
                            <FooterStyle CssClass="DDFooter" Wrap="True" />
                            <HeaderStyle CssClass="th" HorizontalAlign="Center" />
                            <PagerSettings FirstPageImageUrl="~/DynamicData/Content/Images/PgFirst.gif" 
                                FirstPageText="" LastPageImageUrl="~/DynamicData/Content/Images/PgLast.gif" 
                                LastPageText="" Mode="NumericFirstLast" NextPageText="" PageButtonCount="8" 
                                PreviousPageText="" Position="TopAndBottom" />
                            <PagerStyle CssClass="DDPager" />
                            <RowStyle CssClass="td" Wrap="True" />
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td style="border: 2px solid #dbddff; font-size: 14px; " 
                        valign="middle">
                        Phân trang:
                        <asp:DropDownList ID="cboPageSize" runat="server" AutoPostBack="True" 
                            Font-Size="13px" Height="20px" 
                            onselectedindexchanged="cboPageSize_SelectedIndexChanged" 
                            style="margin-right: 0px; margin-left: 0px;" Width="45px">
                            <asp:ListItem Value="1">1</asp:ListItem>
                            <asp:ListItem Selected="True">5</asp:ListItem>
                            <asp:ListItem>10</asp:ListItem>
                            <asp:ListItem>20</asp:ListItem>
                            <asp:ListItem>30</asp:ListItem>
                            <asp:ListItem>50</asp:ListItem>
                            <asp:ListItem>80</asp:ListItem>
                            <asp:ListItem>100</asp:ListItem>
                        </asp:DropDownList>
                        </td>
                    <td style="border: 2px solid #dbddff; font-size: 14px; " 
                        valign="middle">
                        &nbsp;<asp:Button ID="btnCreateUser" runat="server" onclick="btnSubmit_Click" 
                            Text="Tạo" />
                        &nbsp;<asp:Button ID="btnDeleteUser" runat="server" onclick="btnDeleteUser_Click" 
                            Text="Xóa" />
                    </td>
                </tr>
            </table>
            <table style="border: 2px solid lavender; width: 100%;" 
        bgcolor="Snow" cellpadding="4" cellspacing="0">
                <tr>
                    <td align="center" class="style1" 
                        style="border: 2px solid #dbddff;" 
                        colspan="2">
                        <strong>CẬP NHẬT HỒ SƠ NGƯỜI DÙNG<asp:Label ID="lblUpdateUserMessage" 
                            runat="server" ForeColor="Red" style="margin-left: 0px" Width="100%"></asp:Label>
                        </strong></td>
                </tr>
                <tr>
                    <td align="right" class="style11" style="font-weight: bold">
                        Id</td>
                    <td>
                        <asp:TextBox ID="txtId" runat="server" ValidationGroup="updateuser" 
                            Width="250px"></asp:TextBox>
                        <ajaxToolkit:MaskedEditExtender ID="txtId_MaskedEditExtender" runat="server" 
                            CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                            CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                            CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                            InputDirection="RightToLeft" Mask="99999" MaskType="Number" 
                            TargetControlID="txtId">
                        </ajaxToolkit:MaskedEditExtender>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                            ControlToValidate="txtUsername" ErrorMessage="RequiredFieldValidator" 
                            ForeColor="Red" ValidationGroup="updateuser"></asp:RequiredFieldValidator>
                        <asp:TextBox ID="txtNewUsername" runat="server" ValidationGroup="updateuser" 
                            Visible="False" Width="50px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" class="style11" style="font-weight: bold">
                        &nbsp; Mật khẩu mới</td>
                    <td>
                        <asp:TextBox ID="txtNewPassword" runat="server" TextMode="Password" Width="250px" 
                            ValidationGroup="updateuser"></asp:TextBox>
            &nbsp;</td>
                </tr>
                <tr>
                    <td align="right" class="style11">
                        &nbsp;Họ</td>
                    <td align="left">
                        <asp:TextBox ID="txtFirstName" runat="server" ValidationGroup="updateuser" 
                            Width="250px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" class="style11" style="font-weight: bold">
                        &nbsp; Tên</td>
                    <td>
                        <asp:TextBox ID="txtLastName" runat="server" Width="250px" 
                            ValidationGroup="updateuser"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" class="style11" style="font-weight: bold">
                        Email</td>
                    <td>
                        <asp:TextBox ID="txtEmail" runat="server" ValidationGroup="updateuser" 
                            Width="250px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" class="style11" style="font-weight: bold">
                        &nbsp;</td>
                    <td>
                        <asp:Button ID="btnUpdateUser" runat="server" Text="Cập nhật" 
                            onclick="btnUpdateUser_Click" />
                        &nbsp;<asp:Button ID="btnGetUser" runat="server" Text="Chi tiết" 
                            onclick="btnGetUser_Click" />
                    </td>
                </tr>
                <tr>
                    <td align="right" class="style11" style="font-weight: bold">
                        &nbsp;</td>
                    <td>
                        <asp:TreeView ID="treeUserDetail" runat="server" ShowLines="True" 
                            style="text-align: left" Width="100%">
                        </asp:TreeView>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
