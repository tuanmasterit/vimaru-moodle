<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="Grouping.aspx.cs" Inherits="Moodle.Grouping" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server" />
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
        onselecting="LinqDataSourceGroup_Selecting" TableName="Nhoms" 
        OrderBy="ID_Nhom">
    </asp:LinqDataSource>
    <asp:LinqDataSource ID="LinqDataSourceGrouping" runat="server" 
        ContextTypeName="Moodle.DCVimaruDataContext" EntityTypeName="" 
        TableName="Tos" OrderBy="ID_To" Select="new (ID_To, TenTo, MaTKB)" 
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
                    <td class="tableHeader">
                        Tạo, xóa, cập nhật và phân tổ nhóm học viên</td>
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
                    </td>
                </tr>
                <tr>
                    <td class="tableCell">
                        Tổ:
                        <asp:DropDownList ID="cboGrouping" runat="server" CssClass="dropDownList" 
                            DataSourceID="LinqDataSourceGrouping" DataTextField="TenTo" 
                            DataValueField="ID_To" style="margin-left: 0px">
                        </asp:DropDownList>
                        <asp:TextBox ID="txtGroupingName" runat="server" CssClass="textBox" 
                            ValidationGroup="cg" Width="120px"></asp:TextBox>
                        <ajaxToolkit:TextBoxWatermarkExtender ID="txtGroupingName_TextBoxWatermarkExtender" 
                            runat="server" Enabled="True" TargetControlID="txtGroupingName" 
                            WatermarkCssClass="waterMark" WatermarkText="Tên tổ">
                        </ajaxToolkit:TextBoxWatermarkExtender>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                            ControlToValidate="txtGroupingName" ErrorMessage="*" SetFocusOnError="True" 
                            ValidationGroup="cg"></asp:RequiredFieldValidator>
                        <asp:TextBox ID="txtDescription" runat="server" CssClass="textBox" 
                            ValidationGroup="cg" Width="200px"></asp:TextBox>
                        <ajaxToolkit:TextBoxWatermarkExtender ID="txtDescription_TextBoxWatermarkExtender" 
                            runat="server" Enabled="True" TargetControlID="txtDescription" 
                            WatermarkCssClass="waterMark" WatermarkText="Mô tả tổ">
                        </ajaxToolkit:TextBoxWatermarkExtender>
                        <asp:Button ID="btnCreateGrouping" runat="server" 
                            onclick="btnCreateGrouping_Click" Text="Tạo tổ" ValidationGroup="cg" />
                        <asp:Button ID="btnUpdateGrouping" runat="server" 
                            onclick="btnUpdateGrouping_Click" Text="Cập nhật tổ" />
                        <asp:Button ID="btnDeleteGrouping" runat="server" 
                            onclick="btnDeleteGrouping_Click" Text="Xóa tổ" />
                    </td>
                </tr>
                <tr>
                    <td class="tableRow">
                        <asp:GridView ID="grvGroup" runat="server" AllowPaging="True" 
                            AllowSorting="True" AutoGenerateColumns="False" CellPadding="6" 
                            CssClass="DDGridView" DataKeyNames="ID_Nhom" 
                            DataSourceID="LinqDataSourceGroup" EnableModelValidation="False" 
                            onprerender="grvGroup_PreRender" onrowdatabound="grvGroup_RowDataBound" 
                            ShowFooter="True" ShowHeaderWhenEmpty="True" Width="100%" 
                            onpageindexchanging="grvGroup_PageIndexChanging">
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
                                <asp:BoundField DataField="ID_Nhom" HeaderText="Mã nhóm" 
                                    ReadOnly="True" SortExpression="ID_Nhom">
                                <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                                <ItemStyle HorizontalAlign="Center" Width="50px" Wrap="False" 
                                    VerticalAlign="Middle" />
                                </asp:BoundField>
                                <asp:BoundField DataField="TenNhom" HeaderText="Tên nhóm" ReadOnly="True" 
                                    SortExpression="TenNhom">
                                <FooterStyle Wrap="False" />
                                <HeaderStyle Wrap="False" />
                                <ItemStyle Width="100px" 
                                    Wrap="False" />
                                </asp:BoundField>
                                <asp:BoundField DataField="MoTa" HeaderText="Mô tả" ReadOnly="True" 
                                    SortExpression="MoTa">
                                <FooterStyle Wrap="False" />
                                <ItemStyle Width="200px" 
                                    Wrap="False" />
                                </asp:BoundField>
                                <asp:BoundField DataField="TenTo" HeaderText="Tổ" ReadOnly="True" 
                                    SortExpression="TenTo">
                                <HeaderStyle Wrap="False" />
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
                        <asp:Button ID="btnAssignGroup" runat="server" Text="Thêm nhóm vào tổ" />
                        <asp:Button ID="btnUnsignGrouping" runat="server" Text="Bớt nhóm khỏi tổ" />
                    </td>
                </tr>
            </table>
            <asp:Panel ID="Panel1" runat="server">
                <table cellpadding="4" cellspacing="0" class="table">
                    <tr>
                        <td class="tableHeader" 
                        colspan="2">
                            &nbsp;</td>
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
