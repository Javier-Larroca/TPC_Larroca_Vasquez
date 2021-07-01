<%@ Page Title="" Language="C#" MasterPageFile="~/Interfaz.Master" AutoEventWireup="true" CodeBehind="DetalleMedico.aspx.cs" Inherits="TPC_Larroca_Vasquez.DetalleMedico" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <nav class="navbar navbar-light navColor">
        <div class="container-fluid container-sin-espacios">
            <i class="fas fa-star-of-life"></i>
            <p> Detalle de Medico</p>
        </div>
    </nav>
    <section>
        <div class="card text-center">
            <div class="card-header">
                <%=medicoSeleccionado.Nombre %> <%=medicoSeleccionado.Apellido %>
            </div>
            <div class="card-body">
                <h5 class="card-title">Especialidades: </h5>
                <%foreach (var especialidad in medicoSeleccionado.Especialidades)
                  { %>
                    <p><%=especialidad.Descripcion %></p>
                <%} %>
                <p class="card-text"><strong>Contacto:</strong> <%=medicoSeleccionado.Mail %></p>
                <a href="#" class="btn btn-primary">Modificar datos</a>
            </div>
            <div class="card-footer text-muted">
                Dado de Alta: <%=medicoSeleccionado.Alta.Date.ToString("d") %>
            </div>
        </div>
    </section>
</asp:Content>
