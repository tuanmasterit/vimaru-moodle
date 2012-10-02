<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="Student.aspx.cs" Inherits="Moodle.Student" %>
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
    <asp:LinqDataSource ID="LinqDataSourceUser" runat="server" 
        ContextTypeName="Moodle.DCVimaruDataContext" EntityTypeName="" 
        onselecting="LinqDataSourceUser_Selecting" TableName="SinhViens">
        <WhereParameters>
            <asp:ControlParameter ControlID="cboClass" DefaultValue="0" Name="MaLop" 
                PropertyName="SelectedValue" Type="Int32" />
        </WhereParameters>
    </asp:LinqDataSource>
    <asp:LinqDataSource ID="LinqDataSourceClass" runat="server" 
        ContextTypeName="Moodle.DCVimaruDataContext" EntityTypeName="" TableName="Lops" 
        Select="new (MaLop, TenLop)" OrderBy="TenLop">
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
                    <td class="tableHeader">Tạo, xóa tài khoản sinh viên</td>
                </tr>
                <tr>
                    <td class="tableCell">
                        <asp:Panel ID="Panel2" runat="server" DefaultButton="btnSearch">
                            Lớp:
                            <asp:DropDownList ID="cboClass" runat="server" AutoPostBack="True" 
                                CssClass="dropDownList" DataSourceID="LinqDataSourceClass" 
                                DataTextField="TenLop" DataValueField="MaLop" style="margin-left: 0px">
                            </asp:DropDownList>
                            &nbsp;Bộ Lọc:
                        <asp:DropDownList ID="cboFilter" runat="server" AutoPostBack="True" 
                            CssClass="dropDownList" 
                            style="margin-left: 0px">
                            <asp:ListItem>Tất cả</asp:ListItem>
                            <asp:ListItem Value="Đã có tài khoản">Đã có tài khoản</asp:ListItem>
                            <asp:ListItem Value="Chưa có tài khoản">Chưa có tài khoản</asp:ListItem>
                        </asp:DropDownList>
                        &nbsp;Cột:
                        <asp:DropDownList ID="cboField" runat="server" AutoPostBack="True" 
                            CssClass="dropDownList" 
                            style="margin-left: 0px">
                            <asp:ListItem>Tên</asp:ListItem>
                            <asp:ListItem>Họ và đệm</asp:ListItem>
                            <asp:ListItem>Mã sinh viên</asp:ListItem>
                        </asp:DropDownList>
                        <asp:TextBox ID="txtKeyword" runat="server" CssClass="textBox" Width="150px"></asp:TextBox>
                        <ajaxToolkit:TextBoxWatermarkExtender ID="txtKeyword_TextBoxWatermarkExtender" 
                            runat="server" Enabled="True" TargetControlID="txtKeyword" 
                            WatermarkText="Từ khóa" WatermarkCssClass="waterMark">
                        </ajaxToolkit:TextBoxWatermarkExtender>
                        <asp:Button ID="btnSearch" runat="server" Text="Tìm kiếm" 
                                onclick="btnSearch_Click" />
                        </asp:Panel>
                    </td>
                                            
                </tr>
                <tr>
                    <td class="tableRow">
                        <asp:GridView ID="grvUser" runat="server" AllowPaging="True" 
                            AllowSorting="True" AutoGenerateColumns="False" CellPadding="6" 
                            CssClass="DDGridView" DataKeyNames="MaSV" 
                            DataSourceID="LinqDataSourceUser" EnableModelValidation="False" 
                            onprerender="grvUser_PreRender" onrowdatabound="grvUser_RowDataBound" 
                            ShowFooter="True" ShowHeaderWhenEmpty="True" Width="100%" 
                            onpageindexchanging="grvUser_PageIndexChanging" 
                            onselectedindexchanged="grvUser_SelectedIndexChanged">
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
                                <asp:BoundField DataField="Id" HeaderText="Moodle ID" ReadOnly="True" 
                                    SortExpression="Id">
                                <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" 
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
                        <asp:Button ID="btnCreate" runat="server" onclick="btnCreate_Click" 
                            Text="Tạo tài khoản" style="height: 26px" />
                        <asp:Button ID="btnDelete" runat="server" onclick="btnDelete_Click" 
                            Text="Xóa tài khoản" />
                    </td>
                </tr>
            </table>
            <asp:Panel ID="Panel1" runat="server" DefaultButton="btnGetDetail">
            <table cellpadding="4" cellspacing="0" class="table">
                <tr>
                    <td class="tableHeader" 
                        colspan="2">Cập nhật và xem hồ sơ sinh viên</td>
                </tr>
                <tr>
                    <td class="cellHeaderRight">
                        Moodle Id</td>
                    <td>
                        <asp:TextBox ID="txtId" runat="server" ValidationGroup="update" 
                            Width="110px" CssClass="textBox" MaxLength="10"></asp:TextBox>
                        <ajaxToolkit:NumericUpDownExtender ID="txtId_NumericUpDownExtender" 
                            runat="server" Enabled="True" Maximum="1.7976931348623157E+308" Minimum="1" 
                            RefValues="" ServiceDownMethod="" ServiceDownPath="" ServiceUpMethod="" 
                            Tag="" TargetButtonDownID="" TargetButtonUpID="" 
                            TargetControlID="txtId" Width="110">
                        </ajaxToolkit:NumericUpDownExtender>
                        <asp:TextBox ID="txtNewUsername" runat="server" ValidationGroup="updateuser" 
                            Visible="False" Width="50px"></asp:TextBox>
                        <asp:Label ID="lblUpdateUserMessage" runat="server" Font-Bold="False" 
                            ForeColor="Red" style="margin-left: 0px"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="cellHeaderRight">
                        &nbsp; Mật khẩu mới</td>
                    <td>
                        <asp:TextBox ID="txtNewPassword" runat="server" TextMode="Password" Width="200px" 
                            ValidationGroup="update" CssClass="textBox"></asp:TextBox>
            &nbsp;</td>
                </tr>
                <tr>
                    <td class="cellHeaderRight">
                        &nbsp;Họ</td>
                    <td align="left">
                        <asp:TextBox ID="txtFirstName" runat="server" ValidationGroup="update" 
                            Width="200px" CssClass="textBox"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="cellHeaderRight">
                        &nbsp; Tên</td>
                    <td>
                        <asp:TextBox ID="txtLastName" runat="server" Width="200px" 
                            ValidationGroup="update" CssClass="textBox"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="cellHeaderRight">
                        Email</td>
                    <td>
                        <asp:TextBox ID="txtEmail" runat="server" ValidationGroup="update" 
                            Width="200px" CssClass="textBox"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" class="style11" style="font-weight: bold">
                        &nbsp;</td>
                    <td>
                        <asp:Button ID="btnUpdateUser" runat="server" Text="Cập nhật" 
                            onclick="btnUpdate_Click" ValidationGroup="update" />
                        &nbsp;<asp:Button ID="btnGetDetail" runat="server" Text="Xem hồ sơ" 
                            onclick="btnGetDetail_Click" ValidationGroup="update" />
                        <asp:Button ID="btnGetCourses" runat="server" onclick="btnGetCourses_Click" 
                            Text="Xem các khóa học" ValidationGroup="update" />
                    </td>
                </tr>
                <tr>
                    <td align="right" class="style11" style="font-weight: bold">
                        &nbsp;</td>
                    <td>
                        <asp:TreeView ID="treeDetail" runat="server" ShowLines="True" 
                            style="text-align: left" Width="100%">
                        </asp:TreeView>
                    </td>
                </tr>
            </table>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
