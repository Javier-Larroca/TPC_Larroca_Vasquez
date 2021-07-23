<%@ Page Title="" Language="C#" MasterPageFile="~/Interfaces/Interfaz.Master" AutoEventWireup="true" CodeBehind="ReservarTurno.aspx.cs" Inherits="TPC_Larroca_Vasquez.Turnos.ReservarTurno" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <nav class="navbar navbar-light navColor">
        <div class="container-fluid container-sin-espacios">
            <i class="far fa-calendar-alt"></i>
            <p>Reservar Turno</p>
        </div>
    </nav>
    <asp:ScriptManager ID="ScripManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <section class="section-reserva-turno">
                <div>
                    <div>
                        <asp:label Cssclass="form-label" ID="PacienteSeleccionado" Text="Paciente: " runat="server"></asp:label>
                    </div>
                    <div style="width: 50%; display: inline-flex">
                        <asp:TextBox CssClass="form-control inputSize" ID="nombrePaciente" runat="server"></asp:TextBox>
                        <asp:Button CssClass="btn btn-primary" Text="Buscar" style="margin-left: 15px;" ID="Buscar" OnClick="Buscar_Click" runat="server" />
                    </div>
                </div>
                <div style="margin-top: 15px;">
                    <asp:RadioButtonList ID="Pacientes"  style="margin-top: 10px;" runat="server"></asp:RadioButtonList>
                    <asp:Button CssClass="btn btn-primary" style="margin-top: 10px;" Text="Seleccionar" ID="seleccionarPaciente" OnClick="seleccionarPaciente_Click" runat="server"  />
                </div>
                <div class="dropdown-top">
                    <asp:DropDownList ID="Especialidades" OnSelectedIndexChanged="Especialidades_SelectedIndexChanged" AutoPostBack="true" runat="server"></asp:DropDownList>
                </div>
                <div style="margin-top: 10px;">
                    <asp:Calendar ID="Calendario" OnSelectionChanged="Calendario_SelectionChanged" runat="server"></asp:Calendar>
                </div>
                <div class="dropdown-top">
                    <asp:DropDownList ID="HorariosDisponibles" Autopostback="true" OnSelectedIndexChanged="HorariosDisponibles_SelectedIndexChanged" runat="server"></asp:DropDownList>
                </div>
                <div class="dropdown-top">
                    <asp:DropDownList ID="MedicosDisponibles" runat="server"></asp:DropDownList>
                </div>
                <%--<asp:Label ID="noHayMedicos" CssClass="dropdown-top alert alert-dark" Visible="false" Text="No existen medicos disponibles" runat="server"></asp:Label>--%>
                <div style="margin-top: 15px;">
            <asp:Button ID="reservacionTurno" Text="Reservar" Enabled="false" OnClick="reservacionTurno_Click" CssClass="btn btn-primary" runat="server" />
            <asp:Button ID="Volver" Text="Volver" OnClick="Volver_Click" CssClass="btn btn-secondary" runat="server" />
            </div>
            </section>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
