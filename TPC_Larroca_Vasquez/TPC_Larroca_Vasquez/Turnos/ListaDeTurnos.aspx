<%@ Page Title="" Language="C#" MasterPageFile="~/Interfaces/Interfaz.Master" AutoEventWireup="true" CodeBehind="ListaDeTurnos.aspx.cs" Inherits="TPC_Larroca_Vasquez.Turnos.ListaDeTurnos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <nav class="navbar navbar-light navColor">
        <div class="container-fluid container-sin-espacios">
            <i class="fas fa-star-of-life"></i>
            <p>Lista de Turnos</p>
        </div>
    </nav>
    <asp:ScriptManager ID="ScripManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div style="display: flex; margin-top: 10px; padding-left: 29px">
        <asp:Label CssClass="alert alert-success" Visible="false" role="alert" ID="SuccesTurno" runat="server" Text="Se guardo correctamente el turno">
            <asp:Button CssClass="btn-close" runat="server" ID="Button2" data-bs-dismiss="alert" />
        </asp:Label>
    </div>
            <div style="display: flex; margin-top: 5px; padding-left: 29px">
        <asp:Label CssClass="alert alert-danger" Visible="false" role="alert" ID="FailTurno" runat="server" Text="No se cargaron especialidades. Ingresar a detalle de Medico para agregarlas">
            <asp:Button CssClass="btn-close" runat="server" ID="Button4" data-bs-dismiss="alert" />
        </asp:Label>
    </div>
            <div>
                <div class="filtro-busqueda" style="padding-left: 28%;">
                    <asp:DropDownList ID="opcionDeBusqueda" AutoPostBack="true" OnSelectedIndexChanged="opcionDeBusqueda_SelectedIndexChanged" CssClass="dropdown-menu-dark dropdown-posicion" runat="server">
                        <asp:ListItem Selected="True" Value="Todos"></asp:ListItem>
                        <asp:ListItem Value="Vigentes" runat="server"></asp:ListItem>
                        <asp:ListItem Value="Vencidos" runat="server"></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <div style="display: flex; margin-top: 5px; padding-left: 29px">
                <asp:Label CssClass="alert alert-success msj-baja-medico" Visible="false" role="alert" ID="SuccessBajaMedico" runat="server" Text="Se dio de baja correctamente al usuario">
                    <asp:Button CssClass="btn-close" runat="server" ID="Button1" data-bs-dismiss="alert" />
                </asp:Label>
                <div class="div-tabla">
                    <table class="table table-bordered">
                        <thead class="table-dark">
                            <tr>
                                <th scope="col" class="centrar-texto">#</th>
                                <th scope="col" class="centrar-texto">Numero</th>
                                <th scope="col" class="centrar-texto">Paciente</th>
                                <th scope="col" class="centrar-texto">Fecha</th>
                                <th scope="col" class="centrar-texto">Horario</th>
                                <th scope="col" class="centrar-texto">Vigente</th>
                                <th scope="col" class="centrar-texto">Detalles</th>
                            </tr>
                        </thead>
                        <tbody>
                            <%foreach (var turno in listaDeTurnos)
                                { %>
                            <tr>
                                <th scope="row"><i class="fas fa-user-md "></i></th>
                                <td class="centrar-texto"><%=turno.NumeroDeTurno %></td>
                                <td class="centrar-texto"><%=turno.NombrePaciente %></td>
                                <td class="centrar-texto"><%=turno.NombreMedico %></td>
                                <td class="centrar-texto"><%=turno.FechaTurno.Date.ToString("d") %></td>
                                <td class="centrar-texto"><%=turno.Horario %></td>
                                <td class="centrar-texto">
                                    <%if (turno.Estado == true)
                                        { %>
                                Vigente
                                <%}
                                    else
                                    { %>
                                Cancelado
                                <%} %>
                                </td>
                                <td class="centrar-texto">
                                    <a href="DetalleTurno?id=<%=turno.NumeroDeTurno %>" style="align-items: center;">
                                        <i class="fas fa-search-plus" style="color: black;"></i>
                                    </a>
                                </td>
                            </tr>
                            <%} %>
                        </tbody>
                    </table>
                </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
