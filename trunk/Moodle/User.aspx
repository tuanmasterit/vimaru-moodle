<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="User.aspx.cs" Inherits="Moodle.User" %> 
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
            <div class="centerDiv">
                <img alt="" src="App_Images/loading_icon%20(2).gif" align="middle" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table cellpadding="4" class="table" cellspacing="0">
                <tr>
                    <td class="tableHeader">Tạo tài khoản người dùng</td>
                </tr>
                <tr>
                    <td class="tableCell">
                        Lớp học phần:&nbsp;
                        <asp:DropDownList ID="cboAcountFilter" runat="server" AutoPostBack="True" 
                            DataSourceID="LinqDataSourceHocPhan" DataTextField="TenHP" 
                            DataValueField="MaHP" 
                            style="margin-left: 0px" CssClass="dropDownList">
                        </asp:DropDownList>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="tableRow">
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
                                <FooterStyle Wrap="False" />
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
                    <td class="tableCell">
                        Phân trang:
                        <asp:DropDownList ID="cboPageSize" runat="server" AutoPostBack="True" 
                            onselectedindexchanged="cboPageSize_SelectedIndexChanged" 
                            style="margin-right: 0px; margin-left: 0px;" CssClass="dropDownList">
                            <asp:ListItem Value="1">1</asp:ListItem>
                            <asp:ListItem Selected="True">5</asp:ListItem>
                            <asp:ListItem>10</asp:ListItem>
                            <asp:ListItem>20</asp:ListItem>
                            <asp:ListItem>30</asp:ListItem>
                            <asp:ListItem>50</asp:ListItem>
                            <asp:ListItem>80</asp:ListItem>
                            <asp:ListItem>100</asp:ListItem>
                        </asp:DropDownList>
                        <asp:Button ID="btnCreateUser" runat="server" onclick="btnSubmit_Click" 
                            Text="Tạo" />
                        <asp:Button ID="btnDeleteUser" runat="server" onclick="btnDeleteUser_Click" 
                            Text="Xóa" />
                        &nbsp;&nbsp;</td>
                </tr>
            </table>
            <table cellpadding="4" cellspacing="0" class="table">
                <tr>
                    <td class="tableHeader" 
                        style="border: 2px solid #dbddff;" 
                        colspan="2">Cập nhật hồ sơ người dùng</td>
                </tr>
                <tr>
                    <td class="cellHeaderRight">
                        Id</td>
                    <td>
                        <asp:TextBox ID="txtId" runat="server" ValidationGroup="updateuser" 
                            Width="250px" CssClass="textBox"></asp:TextBox>
                        <ajaxToolkit:MaskedEditExtender ID="txtId_MaskedEditExtender" runat="server" 
                            CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                            CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                            CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                            InputDirection="RightToLeft" Mask="99999" MaskType="Number" 
                            TargetControlID="txtId">
                        </ajaxToolkit:MaskedEditExtender>
                        <asp:TextBox ID="txtNewUsername" runat="server" ValidationGroup="updateuser" 
                            Visible="False" Width="50px"></asp:TextBox>
                        <strong>
                        <asp:Label ID="lblUpdateUserMessage" runat="server" Font-Bold="False" 
                            ForeColor="Red" style="margin-left: 0px"></asp:Label>
                        </strong>
                    </td>
                </tr>
                <tr>
                    <td class="cellHeaderRight">
                        &nbsp; Mật khẩu mới</td>
                    <td>
                        <asp:TextBox ID="txtNewPassword" runat="server" TextMode="Password" Width="250px" 
                            ValidationGroup="updateuser" CssClass="textBox"></asp:TextBox>
            &nbsp;</td>
                </tr>
                <tr>
                    <td class="cellHeaderRight">
                        &nbsp;Họ</td>
                    <td align="left">
                        <asp:TextBox ID="txtFirstName" runat="server" ValidationGroup="updateuser" 
                            Width="250px" CssClass="textBox"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="cellHeaderRight">
                        &nbsp; Tên</td>
                    <td>
                        <asp:TextBox ID="txtLastName" runat="server" Width="250px" 
                            ValidationGroup="updateuser" CssClass="textBox"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="cellHeaderRight">
                        Email</td>
                    <td>
                        <asp:TextBox ID="txtEmail" runat="server" ValidationGroup="updateuser" 
                            Width="250px" CssClass="textBox"></asp:TextBox>
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
