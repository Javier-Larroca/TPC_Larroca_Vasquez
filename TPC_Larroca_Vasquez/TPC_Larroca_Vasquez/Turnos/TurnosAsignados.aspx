<%@ Page Title="" Language="C#" MasterPageFile="~/Interfaces/Interfaz.Master" AutoEventWireup="true" CodeBehind="TurnosAsignados.aspx.cs" Inherits="TPC_Larroca_Vasquez.Turnos.TurnosAsignados" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <nav class="navbar navbar-light navColor">
        <div class="container-fluid container-sin-espacios">
            <i class="fas fa-star-of-life"></i>
            <p>Turnos asignados</p>
        </div>
    </nav>
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
                                <th scope="col" class="centrar-texto">Observaciones</th>
                            </tr>
                        </thead>
                        <tbody>
                            <%foreach (var turno in listaDeTurnos)
                                { %>
                            <tr>
                                <th scope="row"><i class="fas fa-user-md "></i></th>
                                <td class="centrar-texto"><%=turno.NumeroDeTurno %></td>
                                <td class="centrar-texto"><%=turno.NombrePaciente %></td>
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
                                    <a href="Observaciones?id=<%=turno.NumeroDeTurno %>" style="align-items: center;">
                                        <i class="fas fa-pencil-alt" style="color: black;"></i>
                                    </a>
                                </td>
                            </tr>
                            <%} %>
                        </tbody>
                    </table>
                </div>
</asp:Content>
