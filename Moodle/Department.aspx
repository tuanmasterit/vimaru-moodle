﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="Department.aspx.cs" Inherits="Moodle.Department" %>
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
        TableName="Khoas" Where="Id != @Id" OrderBy="TenKhoa" 
        Select="new (MaKhoa, TenKhoa, Id)">
        <WhereParameters>
            <asp:Parameter DefaultValue="0" Name="Id" Type="Int64" />
        </WhereParameters>
    </asp:LinqDataSource>
    <asp:LinqDataSource ID="LinqDataSourceDepartment" runat="server" 
        ContextTypeName="Moodle.DCVimaruDataContext" EntityTypeName="" TableName="BoMons" 
        Where="MaKhoa == @MaKhoa" onselecting="LinqDataSourceDepartment_Selecting" 
        OrderBy="TenBoMon">
        <WhereParameters>
            <asp:ControlParameter ControlID="cboFilter" Name="MaKhoa" 
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
                        Tạo danh mục bộ môn</td>
                </tr>
                <tr>
                    <td class="tableCell">
                        Khoa:&nbsp;
                        <asp:DropDownList ID="cboFilter" runat="server" AutoPostBack="True" 
                            CssClass="dropDownList" DataSourceID="LinqDataSourceFaculty" 
                            DataTextField="TenKhoa" DataValueField="MaKhoa" style="margin-left: 0px">
                        </asp:DropDownList>
                        <asp:LinkButton ID="btnRedirect" runat="server" Font-Underline="False" 
                            PostBackUrl="~/Subject.aspx">Tới danh mục môn học</asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td class="tableRow">
                        <asp:GridView ID="grvDepartment" runat="server" AllowPaging="True" 
                            AllowSorting="True" AutoGenerateColumns="False" CellPadding="6" 
                            CssClass="DDGridView" DataKeyNames="MaBoMon" 
                            DataSourceID="LinqDataSourceDepartment" EnableModelValidation="False" 
                            onpageindexchanging="grvDepartment_PageIndexChanging" 
                            onprerender="grvDepartment_PreRender" onrowdatabound="grvDepartment_RowDataBound" 
                            onselectedindexchanged="grvDepartment_SelectedIndexChanged" ShowFooter="True" 
                            ShowHeaderWhenEmpty="True" Width="100%">
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
                                <asp:BoundField DataField="MaBoMon" HeaderText="Mã bộ môn" InsertVisible="False" ReadOnly="True" SortExpression="MaBoMon">
                                <FooterStyle Wrap="False" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" 
                                    Wrap="False" />
                                </asp:BoundField>
                                <asp:BoundField DataField="TenBoMon" HeaderText="Tên bộ môn" ReadOnly="True" SortExpression="TenBoMon" />
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
                        <asp:Button ID="btnCreate" runat="server" onclick="btnCreate_Click" 
                            Text="Tạo" />
                    </td>
                </tr>
            </table>
            <asp:Panel ID="Panel1" runat="server" DefaultButton="btnGetDetail">
            <table cellpadding="4" cellspacing="0" class="table">
                <tr>
                    <td class="tableHeader" 
                        colspan="2">
                        Cập nhật và xem danh mục bộ môn</td>
                </tr>
                <tr>
                    <td class="cellHeaderRight">
                        Id</td>
                    <td>
                        <strong>
                        <asp:TextBox ID="txtId" runat="server" CssClass="textBox" MaxLength="10" 
                            ValidationGroup="updateuser" Width="110px"></asp:TextBox>
                        <ajaxToolkit:NumericUpDownExtender ID="txtId_NumericUpDownExtender" 
                            runat="server" Enabled="True" Maximum="1.7976931348623157E+308" Minimum="1" 
                            RefValues="" ServiceDownMethod="" ServiceDownPath="" ServiceUpMethod="" Tag="" 
                            TargetButtonDownID="" TargetButtonUpID="" TargetControlID="txtId" 
                            Width="110">
                        </ajaxToolkit:NumericUpDownExtender>
                        <asp:Label ID="lblUpdateMessage" runat="server" Font-Bold="False" 
                            ForeColor="Red" style="margin-left: 0px"></asp:Label>
                        </strong>
                    </td>
                </tr>
                <tr>
                    <td class="cellHeaderRight">
                        Mã bộ môn</td>
                    <td align="left">
                        <asp:TextBox ID="txtIdnumber" runat="server" Width="200px" 
                            ValidationGroup="updateuser" CssClass="textBox"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="cellHeaderRight">
                        &nbsp; Tên bộ môn</td>
                    <td>
                        <asp:TextBox ID="txtName" runat="server" Width="200px" 
                            ValidationGroup="updateuser" CssClass="textBox"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="cellHeaderRight">
                        &nbsp;</td>
                    <td>
                        <asp:Button ID="btnUpdate" runat="server" onclick="btnUpdate_Click" 
                            Text="Cập nhật" ValidationGroup="update" />
                    </td>
                </tr>
                <tr>
                    <td class="cellHeaderRight">
                        Điều kiện tìm kiếm</td>
                    <td>
                        <asp:DropDownList ID="ddlCriteria" runat="server" CssClass="dropDownList" 
                            Width="180px">
                            <asp:ListItem Selected="True" Value="id">Tìm theo Id</asp:ListItem>
                            <asp:ListItem Value="idnumber">Tìm theo mã bộ môn</asp:ListItem>
                            <asp:ListItem Value="name">Tìm theo tên bộ môn</asp:ListItem>
                        </asp:DropDownList>
                        <asp:CheckBox ID="chkSubCategory" runat="server" Checked="True" 
                            Text="Xem cả các mục con" 
                            ToolTip="Hiển thị thông tin đầy đủ các mục con bên trong mục cần xem thông tin" />
                        <asp:Button ID="btnGetDetail" runat="server" onclick="btnGetDetail_Click" 
                            Text="Chi tiết" ValidationGroup="update" />
                    </td>
                </tr>
                <tr>
                    <td class="cellHeaderRight">
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
