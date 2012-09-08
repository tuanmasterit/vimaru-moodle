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
    <asp:LinqDataSource ID="LinqDataSourceUser" runat="server" 
        ContextTypeName="Moodle.DCVimaruDataContext" EnableDelete="True" 
        EnableUpdate="True" EntityTypeName="" 
        onselecting="LinqDataSourceUser_Selecting" TableName="DangKies">
    </asp:LinqDataSource>
    <asp:LinqDataSource ID="LinqDataSourceFaculty" runat="server" 
        ContextTypeName="Moodle.DCVimaruDataContext" EntityTypeName="" 
        TableName="Khoas" Where="Id != @Id" Select="new (MaKhoa, TenKhoa)" 
        OrderBy="TenKhoa">
        <WhereParameters>
            <asp:Parameter DefaultValue="0" Name="Id" Type="Int64" />
        </WhereParameters>
    </asp:LinqDataSource>
    <asp:LinqDataSource ID="LinqDataSourceDepartment" runat="server" 
        ContextTypeName="Moodle.DCVimaruDataContext" EntityTypeName="" TableName="BoMons" 
        Where="MaKhoa == @MaKhoa &amp;&amp; Id != @Id" 
        Select="new (MaBoMon, TenBoMon)" OrderBy="TenBoMon">
        <WhereParameters>
            <asp:ControlParameter ControlID="cboFilterFaculty" Name="MaKhoa" 
                PropertyName="SelectedValue" Type="String" />
            <asp:Parameter DefaultValue="0" Name="Id" Type="Int64" />
        </WhereParameters>
    </asp:LinqDataSource>
    <asp:LinqDataSource ID="LinqDataSourceSubject" runat="server" 
        ContextTypeName="Moodle.DCVimaruDataContext" EntityTypeName="" TableName="HocPhans" 
        Where="Id != @Id &amp;&amp; MaBoMon == @MaBoMon" 
        Select="new (MaHP, TenHP)" OrderBy="TenHP">
        <WhereParameters>
            <asp:Parameter DefaultValue="0" Name="Id" Type="Int64" />
            <asp:ControlParameter ControlID="cboFilterDepartment" DefaultValue="0" 
                Name="MaBoMon" PropertyName="SelectedValue" Type="Int32" />
        </WhereParameters>
    </asp:LinqDataSource>
    <asp:LinqDataSource ID="LinqDataSourceCourse" runat="server" 
        ContextTypeName="Moodle.DCVimaruDataContext" EntityTypeName="" TableName="ThoiKhoaBieus" 
        Where="MaHP == @MaHP" onselecting="LinqDataSourceCourse_Selecting">
        <WhereParameters>
            <asp:ControlParameter ControlID="cboFilterSubject" Name="MaHP" 
                PropertyName="SelectedValue" Type="String" />
        </WhereParameters>
    </asp:LinqDataSource>
    <asp:LinqDataSource ID="LinqDataSourceGroup" runat="server" 
        ContextTypeName="Moodle.DCVimaruDataContext" EntityTypeName="" 
        TableName="Nhoms" OrderBy="ID_Nhom desc" Select="new (ID_Nhom, TenNhom, MaTKB)" 
        Where="MaTKB == @MaTKB">
        <WhereParameters>
            <asp:ControlParameter ControlID="cboFilterCourse" DefaultValue="0" Name="MaTKB" 
                PropertyName="SelectedValue" Type="Int64" />
        </WhereParameters>
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
                    <td class="tableHeader">Tạo, xóa, ghi danh và phân nhóm học viên</td>
                </tr>
                <tr>
                    <td class="tableCell">
                        Khoa:
                        <asp:DropDownList ID="cboFilterFaculty" runat="server" AutoPostBack="True" 
                            CssClass="dropDownList" DataSourceID="LinqDataSourceFaculty" 
                            DataTextField="TenKhoa" DataValueField="MaKhoa" style="margin-left: 0px">
                        </asp:DropDownList>
                        &nbsp;Bộ môn:
                        <asp:DropDownList ID="cboFilterDepartment" runat="server" AutoPostBack="True" 
                            CssClass="dropDownList" DataSourceID="LinqDataSourceDepartment" 
                            DataTextField="TenBoMon" DataValueField="MaBoMon" 
                            ondatabound="cboFilterDepartment_DataBound" style="margin-left: 0px">
                        </asp:DropDownList>
                        &nbsp;Môn học:
                        <asp:DropDownList ID="cboFilterSubject" runat="server" AutoPostBack="True" 
                            CssClass="dropDownList" DataSourceID="LinqDataSourceSubject" 
                            DataTextField="TenHP" DataValueField="MaHP" 
                            ondatabound="cboFilterSubject_DataBound" style="margin-left: 0px">
                        </asp:DropDownList>
                        &nbsp;Khóa học:&nbsp;<asp:DropDownList ID="cboFilterCourse" runat="server" AutoPostBack="True" 
                            DataSourceID="LinqDataSourceCourse" DataTextField="TenHP" 
                            DataValueField="STT" 
                            style="margin-left: 0px" CssClass="dropDownList" 
                            ondatabound="cboFilterCourse_DataBound">
                        </asp:DropDownList>
                        <asp:Button ID="btnGetEnrolledUsers" runat="server" 
                            onclick="btnGetEnrolledUsers_Click" Text="Xem thành viên" />
                    </td>
                </tr>
                <tr>
                    <td class="tableCell">
                        <asp:Panel ID="Panel2" runat="server" DefaultButton="btnSearch">
                            Bộ Lọc:
                        <asp:DropDownList ID="cboFilter" runat="server" AutoPostBack="True" 
                            CssClass="dropDownList" 
                            style="margin-left: 0px">
                            <asp:ListItem>Tất cả</asp:ListItem>
                            <asp:ListItem Value="Đã có tài khoản">Đã có tài khoản</asp:ListItem>
                            <asp:ListItem Value="Chưa có tài khoản">Chưa có tài khoản</asp:ListItem>
                            <asp:ListItem Value="Đã ghi danh khóa học">Đã ghi danh khóa học</asp:ListItem>
                            <asp:ListItem Value="Chưa ghi danh khóa học">Chưa ghi danh khóa học</asp:ListItem>
                            <asp:ListItem Value="Đã vào nhóm">Đã vào nhóm</asp:ListItem>
                            <asp:ListItem Value="Chưa vào nhóm">Chưa vào nhóm</asp:ListItem>
                        </asp:DropDownList>
                        &nbsp;Cột:
                        <asp:DropDownList ID="cboField" runat="server" AutoPostBack="True" 
                            CssClass="dropDownList" 
                            style="margin-left: 0px">
                            <asp:ListItem>Tên</asp:ListItem>
                            <asp:ListItem>Họ và đệm</asp:ListItem>
                            <asp:ListItem>Lớp</asp:ListItem>
                            <asp:ListItem>Nhóm</asp:ListItem>
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
                                <asp:BoundField DataField="STT" HeaderText="Mã ĐK" InsertVisible="False" 
                                    ReadOnly="True" SortExpression="STT">
                                <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                                <ItemStyle HorizontalAlign="Center" Width="40px" Wrap="False" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Id" HeaderText="Moodle ID" ReadOnly="True" 
                                    SortExpression="Id">
                                <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" 
                                    Wrap="False" />
                                </asp:BoundField>
                                <asp:CheckBoxField DataField="GhiDanh" HeaderText="Ghi Danh" 
                                    SortExpression="GhiDanh">
                                <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                                <ItemStyle HorizontalAlign="Center" Width="50px" Wrap="False" />
                                </asp:CheckBoxField>
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
                                <asp:BoundField DataField="TenLop" HeaderText="Lớp" ReadOnly="True" 
                                    SortExpression="TenLop">
                                <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                                <ItemStyle HorizontalAlign="Center" Width="60px" Wrap="False" />
                                </asp:BoundField>
                                <asp:BoundField DataField="TenNhom" HeaderText="Nhóm" ReadOnly="True" 
                                    SortExpression="TenNhom" />
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
                        <asp:Button ID="btnEnrolUsers" runat="server" onclick="btnEnrolUsers_Click" 
                            Text="Ghi danh" />
                        <asp:Button ID="btnSuspendUsers" runat="server" onclick="btnSuspendUsers_Click" 
                            Text="Đình chỉ ghi danh" />
                    </td>
                </tr>
                                <tr>
                    <td class="tableCell">
                    <asp:Panel ID="Panel3" runat="server" DefaultButton="btnCreateGroup">
                        Nhóm:
                        <asp:DropDownList ID="cboGroup" runat="server" 
                            CssClass="dropDownList" 
                            style="margin-left: 0px" DataSourceID="LinqDataSourceGroup" 
                            DataTextField="TenNhom" DataValueField="ID_Nhom">
                        </asp:DropDownList>
                        <asp:TextBox ID="txtGroupName" runat="server" CssClass="textBox" Width="120px" 
                            ValidationGroup="cg"></asp:TextBox>
                        <ajaxToolkit:TextBoxWatermarkExtender ID="txtGroupName_TextBoxWatermarkExtender" 
                            runat="server" Enabled="True" TargetControlID="txtGroupName" 
                            WatermarkCssClass="waterMark" WatermarkText="Tên nhóm">
                        </ajaxToolkit:TextBoxWatermarkExtender>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                            ControlToValidate="txtGroupName" ErrorMessage="*" SetFocusOnError="True" 
                            ValidationGroup="cg"></asp:RequiredFieldValidator>
                        <asp:TextBox ID="txtDescription" runat="server" CssClass="textBox" 
                            Width="200px" ValidationGroup="cg"></asp:TextBox>
                        <ajaxToolkit:TextBoxWatermarkExtender ID="txtDescription_TextBoxWatermarkExtender" 
                            runat="server" Enabled="True" TargetControlID="txtDescription" 
                            WatermarkCssClass="waterMark" WatermarkText="Mô tả nhóm">
                        </ajaxToolkit:TextBoxWatermarkExtender>
                        <asp:Button ID="btnCreateGroup" runat="server" Text="Tạo nhóm" 
                            onclick="btnCreateGroup_Click" ValidationGroup="cg" />
                        <asp:Button ID="btnDeleteGroup" runat="server" onclick="btnDeleteGroup_Click" 
                            Text="Xóa nhóm" />
                        <asp:Button ID="btnAddGroupMember" runat="server" 
                            onclick="btnAddGroupMember_Click" Text="Thêm thành viên" />
                        <asp:Button ID="btnDeleteGroupMember" runat="server" 
                            onclick="btnDeleteGroupMember_Click" Text="Bớt thành viên" />
                        </asp:Panel>
                        </td>
                </tr>
            </table>
            <asp:Panel ID="Panel1" runat="server" DefaultButton="btnGetDetail">
            <table cellpadding="4" cellspacing="0" class="table">
                <tr>
                    <td class="tableHeader" 
                        colspan="2">Cập nhật và xem hồ sơ học viên</td>
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
                            onclick="btnUpdate_Click" />
                        &nbsp;<asp:Button ID="btnGetDetail" runat="server" Text="Xem hồ sơ" 
                            onclick="btnGetDetail_Click" />
                        <asp:Button ID="btnGetCourses" runat="server" onclick="btnGetCourses_Click" 
                            Text="Xem các khóa học" />
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
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
