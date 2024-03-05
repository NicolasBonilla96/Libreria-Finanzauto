<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Frm_Books.aspx.cs"
    MasterPageFile="~/AppWebForms/Default.master" Inherits="ApplicationWebForms.AppWebForms.Frm_Books" %>

<asp:Content ID="contentInicio" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="contentUser" ContentPlaceHolderID="contentMaster" runat="server">
    <table style="margin:auto; padding-top:2%">
        <tr>
            <td>
                <asp:Label ID="lblId" runat="server" Text="ID Libro">
                </asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtId" runat="server" Enabled="false">
                </asp:TextBox>
            </td>
            <td style="padding-left: 15px">
                <asp:Label ID="lblName" runat="server" Text="Nombre del Libro">
                </asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtName" runat="server" MaxLength="50">
                </asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblAuthor" runat="server" Text="Autor">
                </asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlAuthor" runat="server" Width="100%" Height="23px">
                </asp:DropDownList>
            </td>
            <td style="padding-left: 15px">
                <asp:Label ID="lblEditorial" runat="server" Text="Editorial">
                </asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlEditorial" runat="server" Width="100%" Height="23px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblCategory" runat="server" Text="Categoria">
                </asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlCategory" runat="server" Width="100%" Height="23px">
                </asp:DropDownList>
            </td>
            <td style="padding-left: 15px">
                <asp:Label ID="lblPublication" runat="server" Text="Año de Publicación">
                </asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtPublication" runat="server" MaxLength="4">
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
        <asp:GridView ID="gvBooks" runat="server" AllowPaging="true" PageSize="5" CellPadding="3"
            OnRowCommand="gvBooks_Row" Visible="false" AutoGenerateColumns="False" style="margin: auto" Width="50%">
            <Columns>
                <asp:TemplateField Visible="False">
                    <ItemTemplate>
                        <asp:Label ID="lblid" runat="server" Text='<%# Eval("id") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Nombre del Libro">
                    <ItemTemplate>
                        <asp:Label CausesValidation="false" ID="lblNameGv" runat="server"
                            Font-Underline="True" Font-Bold="True" Text='<%# Eval("name") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Año Publicación">
                    <ItemTemplate>
                        <asp:Label CausesValidation="false" ID="lblPublicationGv" runat="server"
                            Font-Underline="True" Font-Bold="True" Text='<%# Eval("publication_year") %>'></asp:Label>
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