<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Frm_Categories.aspx.cs"
    MasterPageFile="~/AppWebForms/Default.master" Inherits="ApplicationWebForms.AppWebForms.Frm_Categories" %>

<asp:Content ID="contentInicio" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="contentUser" ContentPlaceHolderID="contentMaster" runat="server">
    <table style="margin:auto; padding-top:2%">
        <tr>
            <td>
                <asp:Label ID="lblId" runat="server" Text="ID Categoria">
                </asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtId" runat="server" Enabled="false">
                </asp:TextBox>
            </td>
            <td style="padding-left: 15px">
                <asp:Label ID="lblName" runat="server" Text="Nombre de la Categoria">
                </asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtName" runat="server" MaxLength="80">
                </asp:TextBox>
            </td>
        </tr>
    </table> 
    <table style="margin:auto; padding-top:1%">
        <tr>
            <td>
                <asp:Button id="btnCreate" Text="Crear" runat="server" OnClick="BtnCreate_Click"/>
                <asp:Button id="btnUpdate" Text="Actualizar" runat="server" Visible="false" OnClick="BtnUpdate_Click"/>
                <asp:Button id="btnSearch" Text="Consultar" runat="server"/>
                <asp:Button id="btnClear" Text="Limpiar Datos" runat="server" OnClick="Clear_Properties"/>
            </td>
        </tr>
    </table>
    <table style="padding-top:1%">
        <asp:GridView ID="gvCategories" runat="server" AllowPaging="true" PageSize="5" CellPadding="3"
            OnRowCommand="gvCategories_Row" Visible="false" AutoGenerateColumns="False" style="margin: auto" Width="50%">
            <Columns>
                <asp:TemplateField Visible="False">
                    <ItemTemplate>
                        <asp:Label ID="lblid" runat="server" Text='<%# Eval("id") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Nombre de la Categoria">
                    <ItemTemplate>
                        <asp:Label CausesValidation="false" ID="lblNameGv" runat="server"
                            Font-Underline="True" Font-Bold="True" Text='<%# Eval("name") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Acciones">
                    <ItemTemplate>
                        <table>
                            <tr>
                                <td runat="server" id="tdbtnActualizar">
                                    <asp:ImageButton ID="ibtnGActualizar" runat="server" ImageUrl="../Images/update.png"
                                        CommandName="Actualizar" CausesValidation="False"
                                        CommandArgument='<%# Eval("id") %>' ToolTip="Actualizar" />
                                </td>
                                <td runat="server" id="tdbtnEliminar">
                                    <asp:ImageButton ID="ibtnGEliminar" runat="server" ImageUrl="../Images/delete.png" CommandName="Eliminar"
                                        CausesValidation="False" Style="height: 20px" CommandArgument='<%# Eval("id") %>'
                                        ToolTip="Eliminar" />
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </table>
</asp:Content>