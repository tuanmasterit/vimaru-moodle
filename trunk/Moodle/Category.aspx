<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="Category.aspx.cs" Inherits="Moodle.Category" %>
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
            <table cellpadding="4" cellspacing="0" class="table">
                <tr>
                    <td colspan="2" class="tableHeader">
                        Tạo, cập nhật và lấy thông tin mục<asp:Label ID="lblCategoryMessage" runat="server" 
                            CssClass="statusLabel" style="margin-left: 0px"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="cellHeaderRight">
                        Id</td>
                    <td>
                        <asp:TextBox ID="txtId" runat="server" CssClass="textBox" MaxLength="10" 
                            ValidationGroup="update" Width="110px"></asp:TextBox>
                        <ajaxToolkit:NumericUpDownExtender ID="txtId_NumericUpDownExtender" 
                            runat="server" Enabled="True" Maximum="1.7976931348623157E+308" Minimum="1" 
                            RefValues="" ServiceDownMethod="" ServiceDownPath="" ServiceUpMethod="" Tag="" 
                            TargetButtonDownID="" TargetButtonUpID="" TargetControlID="txtId" Width="110">
                        </ajaxToolkit:NumericUpDownExtender>
                    </td>
                </tr>
                <tr>
                    <td class="cellHeaderRight">
                        Tên mục</td>
                    <td align="left">
                        <asp:TextBox ID="txtName" runat="server" ValidationGroup="update" 
                            Width="200px" CssClass="textBox"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="cellHeaderRight">
                        &nbsp; Id mục cấp trên</td>
                    <td>
                        <asp:TextBox ID="txtParent" runat="server" ValidationGroup="update" 
                            Width="110px" CssClass="textBox" MaxLength="10"></asp:TextBox>
                        <ajaxToolkit:NumericUpDownExtender ID="txtParent_NumericUpDownExtender" 
                            runat="server" Enabled="True" Maximum="1.7976931348623157E+308" Minimum="1" 
                            RefValues="" ServiceDownMethod="" ServiceDownPath="" ServiceUpMethod="" Tag="" 
                            TargetButtonDownID="" TargetButtonUpID="" TargetControlID="txtParent" 
                            Width="110">
                        </ajaxToolkit:NumericUpDownExtender>
                    </td>
                </tr>
                <tr>
                    <td class="cellHeaderRight">
                        Idnumber</td>
                    <td>
                        <asp:TextBox ID="txtIdnumber" runat="server" ValidationGroup="updateuser" 
                            Width="100px" CssClass="textBox"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="cellHeaderRight">
                        Mô tả</td>
                    <td class="style14">
                        <asp:TextBox ID="txtDecscription" runat="server" Height="60px" 
                            TextMode="MultiLine" ValidationGroup="updateuser" Width="400px" 
                            CssClass="textBox"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="cellHeaderRight">
                        Định dạng mô tả</td>
                    <td>
                        <asp:TextBox ID="txtDescriptionFormat" runat="server" 
                            ValidationGroup="update" Width="50px" 
                            CssClass="textBox" MaxLength="2">1</asp:TextBox>
                        <ajaxToolkit:NumericUpDownExtender ID="txtDescriptionFormat_NumericUpDownExtender" 
                            runat="server" Enabled="True" Maximum="99" Minimum="0" RefValues="" 
                            ServiceDownMethod="" ServiceDownPath="" ServiceUpMethod="" Tag="" 
                            TargetButtonDownID="" TargetButtonUpID="" 
                            TargetControlID="txtDescriptionFormat" Width="50">
                        </ajaxToolkit:NumericUpDownExtender>
                    </td>
                </tr>
                <tr>
                    <td class="cellHeaderRight">
                        Theme</td>
                    <td>
                        <asp:TextBox ID="txtTheme" runat="server" ValidationGroup="update" 
                            Width="200px" 
                            ToolTip="The new category theme. This option must be enabled on moodle" 
                            CssClass="textBox"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" class="style24" style="font-weight: bold">
                        &nbsp;</td>
                    <td valign="middle">
                        <asp:Button ID="btnCreate" runat="server" onclick="btnCreateCategory_Click" 
                            Text="Tạo" Width="65px" ValidationGroup="update" />
                        &nbsp;<asp:Button ID="btnUpdate" runat="server" onclick="btnUpdate_Click" 
                            Text="Cập nhật" Width="65px" ValidationGroup="update" />
                        &nbsp;&nbsp;</td>
                </tr>
                <tr>
                    <td class="cellHeaderRight">
                        Điều kiện tìm kiếm</td>
                    <td>
                        <asp:DropDownList ID="ddlCriteria" runat="server" CssClass="dropDownList">
                            <asp:ListItem Value="All">Tất cả</asp:ListItem>
                            <asp:ListItem Selected="True" Value="id">Tìm theo Id</asp:ListItem>
                            <asp:ListItem Value="name">Tìm theo tên</asp:ListItem>
                            <asp:ListItem Value="parent">Tìm theo id mục cấp trên</asp:ListItem>
                            <asp:ListItem Value="idnumber">Tìm theo idnumber</asp:ListItem>
                        </asp:DropDownList>
                        <asp:CheckBox ID="chkSubCategory" runat="server" Checked="True" 
                            Text="Xem cả các mục con" 
                            ToolTip="Hiển thị thông tin đầy đủ các mục con bên trong mục cần xem thông tin" />
                        <asp:Button ID="btnDetail" runat="server" onclick="btnDetail_Click" 
                            Text="Chi tiết" Width="65px" />
                    </td>
                </tr>
                <tr>
                    <td class="tableCell" valign="top">
                        &nbsp;</td>
                    <td class="tableCell">
                        <asp:TreeView ID="treeCategoryDetail" runat="server" ShowLines="True" 
                            style="text-align: left" Width="100%">
                        </asp:TreeView>
                    </td>
                </tr>
            </table>
            <asp:Panel ID="Panel2" runat="server" DefaultButton="btnDelete">
            <table cellpadding="4" cellspacing="0" class="table">
                <tr>
                    <td colspan="2" class="tableHeader">Xóa mục<asp:Label ID="lblDeleteCategoryMessage" 
                            runat="server" style="margin-left: 0px" CssClass="statusLabel"></asp:Label>
                        </strong>
                    </td>
                </tr>
                <tr>
                    <td class="cellHeaderRight">
                        Id</td>
                    <td>
                        <asp:TextBox ID="txtDeleteId" runat="server" ValidationGroup="updateuser" 
                            Width="100px" CssClass="textBox"></asp:TextBox>
                        <ajaxToolkit:MaskedEditExtender ID="txtDeleteId_MaskedEditExtender" 
                            runat="server" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                            CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                            CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                            InputDirection="RightToLeft" Mask="99999" MaskType="Number" 
                            TargetControlID="txtDeleteId">
                        </ajaxToolkit:MaskedEditExtender>
                    </td>
                </tr>
                <tr>
                    <td class="cellHeaderRight">
                        &nbsp; Id mục cấp trên mới</td>
                    <td>
                        <asp:TextBox ID="txtNewParent" runat="server" ValidationGroup="updateuser" 
                            Width="100px" CssClass="textBox"></asp:TextBox>
                        <ajaxToolkit:MaskedEditExtender ID="txtNewParent_MaskedEditExtender" 
                            runat="server" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                            CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                            CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                            InputDirection="RightToLeft" Mask="99999" MaskType="Number" 
                            TargetControlID="txtNewParent">
                        </ajaxToolkit:MaskedEditExtender>
                    </td>
                </tr>
                <tr>
                    <td class="cellHeaderRight" valign="top">
                        Kiểu xóa</td>
                    <td class="style19">
                        <asp:Panel ID="Panel1" runat="server" Height="40px">
                            <asp:RadioButton ID="rdbDeleteOnlyParent" runat="server" Checked="True" 
                                style="font-weight: 700" 
                                Text="Chỉ xóa mục này, đẩy toàn bộ mục con vào mục cấp trên mới" />
                            <br />
                            <asp:RadioButton ID="rdbDeleteAllChildren" runat="server" 
                                style="font-weight: 700" 
                                Text="Xóa toàn bộ mục này và các mục con bên trong nó" />
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td class="cellHeaderRight">
                        &nbsp;</td>
                    <td>
                        <asp:Button ID="btnDelete" runat="server" onclick="btnDelete_Click" 
                            Text="Xóa" Width="65px" />
                    </td>
                </tr>
                <tr>
                    <td align="right" class="style23" style="font-weight: bold">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
            </table>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
