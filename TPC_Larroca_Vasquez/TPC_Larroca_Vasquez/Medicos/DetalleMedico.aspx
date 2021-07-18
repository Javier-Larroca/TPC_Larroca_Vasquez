<%@ Page Title="" Language="C#" MasterPageFile="../Interfaces/Interfaz.Master" AutoEventWireup="true" CodeBehind="DetalleMedico.aspx.cs" Inherits="TPC_Larroca_Vasquez.DetalleMedico" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <nav class="navbar navbar-light navColor">
        <div class="container-fluid container-sin-espacios">
            <i class="fas fa-star-of-life"></i>
            <p>Detalle de Medico</p>
        </div>
    </nav>
    <section>
        <div class="btns-flotantes" style="padding-left: 0px">
                <asp:Button CssClass="btn btn-secondary" Text="Volver" ID="Volver" OnClick="Volver_Click" runat="server" />
            </div>
        <div style="display: flex; margin-top: 10px; padding-left: 29px">
        <asp:Label CssClass="alert alert-success" Visible="false" role="alert" ID="SuccessMedico" runat="server" Text="Se modifico correctamente al usuario">
            <asp:Button CssClass="btn-close" runat="server" ID="Button1" data-bs-dismiss="alert" />
        </asp:Label>
        </div>
        <div style="display: flex; margin-top: 5px; padding-left: 29px">
        <asp:Label CssClass="alert alert-danger" Visible="false" role="alert" ID="FailMedico" runat="server" Text="No se pudo modificar los datos solicitados. Intente de nuevo">
            <asp:Button CssClass="btn-close" runat="server" ID="Button4" data-bs-dismiss="alert" />
        </asp:Label>
            </div>
        <div class="card text-center text-white bg-dark mb-3" style="margin-top: 15px; width: 30rem;">
            <div class="card-header">
                <img src="../img/imagenMedicoGeneral.jpg" class="card-img-top imgCard" alt="<%=medicoSeleccionado.Nombre %>" />
                <br />
                <%=medicoSeleccionado.Nombre %> <%=medicoSeleccionado.Apellido %>
            </div>
            <div class="card-body">
                <h5 class="card-title">Especialidades: </h5>
                <%foreach (var especialidad in medicoSeleccionado.Especialidades)
                    { %>
                <p><%=especialidad.Descripcion %></p>
                <%} %>
                <p class="card-text"><strong>Contacto:</strong> <%=medicoSeleccionado.Mail %></p>
                <a href="ModificarMedico?id=<%=medicoSeleccionado.Id %>" class="btn btn-primary">Modificar datos</a>
                <a href="AsignacionTurnoDeTrabajos" class="btn btn-primary">Turno de trabajo</a>
            </div>
            <div class="card-footer text-muted">
                Dado de Alta: <%=medicoSeleccionado.Alta.Date.ToString("d") %>
            </div>
            <div class="card-footer text-muted">
                Ultima modificación: <%if (medicoSeleccionado.Modificacion == null) { %>
                                       Sin actualizaciones
                                     <%}
                else
                { %>
                <%=medicoSeleccionado.Modificacion.Value.Date.ToString("d") %>
                <%} %>
            </div>
        </div>
    </section>
</asp:Content>
