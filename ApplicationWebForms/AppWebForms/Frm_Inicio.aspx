<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Frm_Inicio.aspx.cs"
    MasterPageFile="~/AppWebForms/Default.master" Inherits="ApplicationWebForms.AppWebForms.Frm_Inicio" %>

<asp:Content ID="contentInicio" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="contentUser" ContentPlaceHolderID="contentMaster" runat="server">
    <table style="margin: auto; padding-top: 10px">
        <tr>
            <td>
                <asp:Label ID="lblTitle" runat="server" Text="Seleccione alguna de las ventanas...">
                </asp:Label>
            </td>
        </tr>
    </table>
</asp:Content>