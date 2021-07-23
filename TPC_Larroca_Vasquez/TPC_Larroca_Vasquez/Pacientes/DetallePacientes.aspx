<%@ Page Title="" Language="C#" MasterPageFile="../Interfaces/Interfaz.Master" AutoEventWireup="true" CodeBehind="DetallePacientes.aspx.cs" Inherits="TPC_Larroca_Vasquez.DetallePacientes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <nav class="navbar navbar-light navColor">
        <div class="container-fluid container-sin-espacios">
            <i class="fas fa-star-of-life"></i>
            <p> Detalle de Paciente</p>
        </div>
    </nav>
    <section>
        <div class="card text-center text-white bg-dark mb-3" style="margin-top: 15px; width: 30rem;">
            <div class="card-header">
                <img src="../img/imagenPacienteGeneral.jpg" class="card-img-top imgCard" alt="<%=pacienteSeleccionado.Nombre %>"/>
                <br />
                <%=pacienteSeleccionado.Nombre %> <%=pacienteSeleccionado.Apellido %>
            </div>
            <div class="card-body">                
                <p class="card-text"><strong>Fecha de Nacimiento:</strong> <%=pacienteSeleccionado.FechaNac.Date.ToString("d") %></p>
                <p class="card-text"><strong>Contacto:</strong> <%=pacienteSeleccionado.Mail %></p>
                <p class="card-text"><strong>Obra Social:</strong> <%=pacienteSeleccionado.ObraSocial.Descripcion %></p>

                <a href="ModificarPaciente?id=<%=pacienteSeleccionado.Id %>"" class="btn btn-primary">Modificar datos</a>
            </div>
            <div class="card-footer text-muted">
                Dado de Alta: <%=pacienteSeleccionado.Alta.Date.ToString("d") %>
            </div>
        </div>
    </section>
</asp:Content>
