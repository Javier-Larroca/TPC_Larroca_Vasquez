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
            <section>
                <div>
                    <label class="form-label">Paciente :</label>
                    <asp:TextBox CssClass="form-control inputSize" ID="nombrePaciente" runat="server"></asp:TextBox>
                </div>
                <div>
                    <asp:Calendar ID="Calendario" runat="server"></asp:Calendar>
                </div>
                <div>
                    <asp:DropDownList ID="Especialidades" runat="server"></asp:DropDownList>
                </div>
                <div>
                    <asp:DropDownList ID="HorariosDisponibles" runat="server"></asp:DropDownList>
                </div>
                <div>
                    <asp:DropDownList ID="MedicosDisponibles" runat="server"></asp:DropDownList>
                </div>
                <div>
            </section>
            <asp:Button ID="reservacionTurno" Text="Reservar" Enabled="false" CssClass="btn btn-primary" runat="server" />
            <asp:Button ID="Volver" Text="Volver" CssClass="btn btn-secondary" runat="server" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
