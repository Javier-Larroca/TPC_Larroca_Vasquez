<%@ Page Title="" Language="C#" MasterPageFile="~/Interfaces/Interfaz.Master" AutoEventWireup="true" CodeBehind="ListaDeUsuarios.aspx.cs" Inherits="TPC_Larroca_Vasquez.Usuarios.ListaDeUsuarios" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <nav class="navbar navbar-light navColor">
        <div class="container-fluid container-sin-espacios">
            <i class="fas fa-users"></i>
            <p>Lista de usuarios</p>
        </div>
    </nav>
    <asp:ScriptManager ID="ScripManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div>
                <div class="filtro-busqueda" style="padding-left: 28%;">
                    <asp:DropDownList ID="opcionDeBusqueda" AutoPostBack="true" OnSelectedIndexChanged="opcionDeBusqueda_SelectedIndexChanged" CssClass="dropdown-menu-dark dropdown-posicion" runat="server">
                        <asp:ListItem Selected="True" Value="Todos"></asp:ListItem>
                        <asp:ListItem Value="Pacientes" runat="server"></asp:ListItem>
                        <asp:ListItem Value="Medicos" runat="server"></asp:ListItem>
                        <asp:ListItem Value="Soportes" runat="server"></asp:ListItem>
                        <asp:ListItem Value="Recepcionistas" runat="server"></asp:ListItem> 
                    </asp:DropDownList>
                    <asp:DropDownList ID="opcionEstado" AutoPostBack="true" OnSelectedIndexChanged="opcionEstado_SelectedIndexChanged" CssClass="dropdown-menu-dark dropdown-posicion" runat="server">
                        <asp:ListItem Selected="True" Value="Todos"></asp:ListItem>
                        <asp:ListItem Value="Activos"></asp:ListItem>
                        <asp:ListItem Value="Eliminados"></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
              </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
