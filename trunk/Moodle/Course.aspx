<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="Course.aspx.cs" Inherits="Moodle.Course" %>
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
    <asp:LinqDataSource ID="LinqDataSourceFaculty" runat="server" 
        ContextTypeName="Moodle.DCVimaruDataContext" EntityTypeName="" 
        TableName="Khoas" Where="Id != @Id" Select="new (MaKhoa, TenKhoa)">
        <WhereParameters>
            <asp:Parameter DefaultValue="0" Name="Id" Type="Int64" />
        </WhereParameters>
    </asp:LinqDataSource>
    <asp:LinqDataSource ID="LinqDataSourceDepartment" runat="server" 
        ContextTypeName="Moodle.DCVimaruDataContext" EntityTypeName="" TableName="BoMons" 
        Where="MaKhoa == @MaKhoa &amp;&amp; Id != @Id" 
        Select="new (MaBoMon, TenBoMon)">
        <WhereParameters>
            <asp:ControlParameter ControlID="cboFilterFaculty" Name="MaKhoa" 
                PropertyName="SelectedValue" Type="String" />
            <asp:Parameter DefaultValue="0" Name="Id" Type="Int64" />
        </WhereParameters>
    </asp:LinqDataSource>
    <asp:LinqDataSource ID="LinqDataSourceSubject" runat="server" 
        ContextTypeName="Moodle.DCVimaruDataContext" EntityTypeName="" TableName="HocPhans" 
        Where="Id != @Id &amp;&amp; MaBoMon == @MaBoMon" 
        Select="new (MaHP, TenHP)">
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
    <asp:TextBox ID="txtListId" runat="server" Visible="False" Wrap="False"></asp:TextBox>
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
                        Tạo khóa học</td>
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
                            DataTextField="TenBoMon" DataValueField="MaBoMon" style="margin-left: 0px" 
                            ondatabound="cboFilterDepartment_DataBound">
                        </asp:DropDownList>
                        &nbsp;Môn học:
                        <asp:DropDownList ID="cboFilterSubject" runat="server" AutoPostBack="True" 
                            CssClass="dropDownList" DataSourceID="LinqDataSourceSubject" 
                            DataTextField="TenHP" DataValueField="MaHP" style="margin-left: 0px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="tableRow">
                        <asp:GridView ID="grvCourse" runat="server" AllowPaging="True" 
                            AllowSorting="True" AutoGenerateColumns="False" CellPadding="6" 
                            CssClass="DDGridView" DataKeyNames="STT" 
                            DataSourceID="LinqDataSourceCourse" EnableModelValidation="False" 
                            onpageindexchanging="grvCourse_PageIndexChanging" 
                            onprerender="grvCourse_PreRender" onrowdatabound="grvCourse_RowDataBound" ShowFooter="True" 
                            ShowHeaderWhenEmpty="True" Width="100%" 
                            onselectedindexchanged="grvCourse_SelectedIndexChanged">
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
                                <asp:CommandField SelectText="Chi tiết" ShowSelectButton="True">
                                <FooterStyle Wrap="False" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                                </asp:CommandField>
                                <asp:BoundField DataField="Id" HeaderText="Moodle ID" ReadOnly="True" 
                                    SortExpression="Id">
                                <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" 
                                    Wrap="False" />
                                </asp:BoundField>
                                <asp:BoundField DataField="STT" HeaderText="STT" ReadOnly="True" 
                                    SortExpression="STT">
                                <HeaderStyle Wrap="False" />
                                <ItemStyle Width="50px" Wrap="False" />
                                </asp:BoundField>
                                <asp:BoundField DataField="TenHP" HeaderText="Tên học phần" ReadOnly="True" 
                                    SortExpression="TenHP" >
                                <FooterStyle Wrap="False" />
                                <ItemStyle Wrap="False" />
                                </asp:BoundField>
                                <asp:BoundField DataField="MaNH" HeaderText="Nhóm học" InsertVisible="False" 
                                    ReadOnly="True" SortExpression="MaNH">
                                <FooterStyle Wrap="False" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                <ItemStyle Width="60px" 
                                    Wrap="False" />
                                </asp:BoundField>
                                <asp:BoundField DataField="NgayBD" DataFormatString="{0:dd-MM-yyyy}" 
                                    HeaderText="Ngày bắt đầu" ReadOnly="True" SortExpression="NgayBD">
                                <FooterStyle Wrap="False" />
                                <HeaderStyle Wrap="True" />
                                <ItemStyle Wrap="False" />
                                </asp:BoundField>
                            </Columns>
                            <FooterStyle CssClass="DDFooter" Wrap="True" />
                            <HeaderStyle CssClass="th" HorizontalAlign="Center" />
                            <PagerSettings FirstPageImageUrl="~/DynamicData/Content/Images/PgFirst.gif" 
                                FirstPageText="" LastPageImageUrl="~/DynamicData/Content/Images/PgLast.gif" 
                                LastPageText="" Mode="NumericFirstLast" NextPageText="" PageButtonCount="8" 
                                Position="TopAndBottom" PreviousPageText="" />
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
                        <asp:Button ID="btnCreate" runat="server" 
                            Text="Tạo" Height="26px" onclick="btnCreate_Click" />
                        <asp:Button ID="btnDelete" runat="server" Height="26px" 
                            onclick="btnDelete_Click" Text="Xóa" />
                    </td>
                </tr>
            </table>
            <table cellpadding="4" cellspacing="0" class="table">
                <tr>
                    <td class="tableHeader" 
                        colspan="2">
                        Thông tin chi tiết về khóa học</td>
                </tr>
                <tr>
                    <td class="columnNoBoderLeft" width="50%">
                        Thông tin khóa học</td>
                    <td class="columnNoBoderRight">
                        Các nội dung của khóa học</td>
                </tr>
                <tr>
                    <td class="cellNoBorderLeft" width="50%" valign="top">
                        <asp:TreeView ID="treeDetail" runat="server" ShowLines="True" 
                            style="text-align: left" Width="100%">
                        </asp:TreeView>
                    </td>
                    <td class="cellNoBorderRight" valign="top">
                        <asp:TreeView ID="treeContent" runat="server" ShowLines="True" 
                            style="text-align: left" Width="100%">
                        </asp:TreeView>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
