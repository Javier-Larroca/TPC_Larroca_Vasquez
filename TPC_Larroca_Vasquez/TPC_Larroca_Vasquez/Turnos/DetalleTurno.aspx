<%@ Page Title="" Language="C#" MasterPageFile="~/Interfaces/Interfaz.Master" AutoEventWireup="true" CodeBehind="DetalleTurno.aspx.cs" Inherits="TPC_Larroca_Vasquez.Turnos.DetalleTurno" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<nav class="navbar navbar-light navColor">
        <div class="container-fluid container-sin-espacios">
            <i class="fas fa-star-of-life"></i>
            <p>Detalle de Turno</p>
        </div>
 </nav>
    <section>
        <div class="btns-flotantes" style="padding-left: 0px">
                <asp:Button CssClass="btn btn-secondary" Text="Volver" ID="Volver" OnClick="Volver_Click" runat="server" />
            </div>
        <div style="display: flex; margin-top: 10px; padding-left: 29px">
        <asp:Label CssClass="alert alert-success" Visible="false" role="alert" ID="SuccesTurno" runat="server" Text="Se registro correctamente el turno">
            <asp:Button CssClass="btn-close" runat="server" ID="Button1" data-bs-dismiss="alert" />
        </asp:Label>
        </div>
        <div style="display: flex; margin-top: 5px; padding-left: 29px">
        <asp:Label CssClass="alert alert-danger" Visible="false" role="alert" ID="FailTurno" runat="server" Text="No se pudo modificar los datos solicitados. Intente de nuevo">
            <asp:Button CssClass="btn-close" runat="server" ID="Button4" data-bs-dismiss="alert" />
        </asp:Label>
            </div>
        <div class="card text-center text-white bg-dark mb-3" style="margin-top: 15px; width: 30rem;">
            <div class="card-header">
                <img src="../img/turnos.png" class="card-img-top imgCard" alt="<%=TurnoSeleccionado.NumeroDeTurno %>" />
                <br />
    
            </div>
            <div class="card-body">
                <p class="card-header"><strong>Turno nro: </strong><%=TurnoSeleccionado.NumeroDeTurno %></p>
                <p class="card-text"><strong>Horario: </strong> <%=TurnoSeleccionado.Horario%></p>
                <p class="card-text"><strong>Medico:</strong> <%=TurnoSeleccionado.NombreMedico %></p>
                <p class="card-text"><strong>Paciente:</strong> <%=TurnoSeleccionado.NombrePaciente %></p>
                <%if (TurnoSeleccionado.Estado == true)
                    { %>
                <asp:Button CssClass="btn btn-primary" ID="Cancelar" OnClick="Cancelar_Click" Text="Cancelar turno" runat="server"/>
                <%} %>
            </div>
            <div class="card-footer text-muted">
                Fecha de Turno: <%=TurnoSeleccionado.FechaTurno.Date.ToString("d") %>
            </div>
            <div class="card-footer text-muted">
                Estado : <%if (TurnoSeleccionado.Estado == true) { %>
                                       Vigente
                                     <%}
                else
                { %>
                 Cancelado
                <%} %>
            </div>
        </div>
    </section>
</asp:Content>
