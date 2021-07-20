<%@ Page Title="" Language="C#" MasterPageFile="~/Interfaces/Interfaz.Master" AutoEventWireup="true" CodeBehind="AsignacionTurnoDeTrabajos.aspx.cs" Inherits="TPC_Larroca_Vasquez.Medicos.AsignacionTurnoDeTrabajos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<nav class="navbar navbar-light navColor">
        <div class="container-fluid container-sin-espacios">
            <i class="far fa-calendar-alt"></i>
            <p>Turno de trabajo</p>
        </div>
    </nav>
    <asp:ScriptManager ID="ScripManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div class="btns-flotantes">
                <asp:Button CssClass="btn btn-primary" Text="Modificar" ID="Modificar" OnClick="Modificar_Click" runat="server" />
                <asp:Button CssClass="btn btn-secondary" Text="Volver" ID="Volver" OnClick="Volver_Click" runat="server"/>
                <asp:Button CssClass="btn btn-success" Text="Guardar" ID="Guardar" OnClick="Guardar_Click" Visible="false" runat="server" />
                <asp:Button CssClass="btn btn-danger" Text="Cancelar" ID="Cancelar" OnClick="Cancelar_Click" Visible="false" runat="server" />
            </div>
            <div class="div-tabla">
                <table class="table table-bordered">
                    <thead class="table-dark">
                        <tr>
                            <th scope="col" class="centrar-texto">Dia</th>
                            <th scope="col" class="centrar-texto">Horario de ingreso</th>
                            <th scope="col" class="centrar-texto">Horario de salida</th>
                            <th scope="col" class="centrar-texto">Dia libre</th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater runat="server" ID="tabla">
                            <ItemTemplate>
                                <tr>
                                    <td class="centrar-texto"><%#Eval("Dia")%></td>
                                    <td class="centrar-texto">
                                        <asp:DropDownList ID="horarioEntrada" runat="server"></asp:DropDownList>
                                    </td>
                                    <td class="centrar-texto">
                                        <asp:DropDownList ID="horarioSalida" runat="server"></asp:DropDownList>
                                    </td>
                                    <td class="centrar-texto">
                                        <asp:CheckBox CssClass="form-check-label" ID="esDiaLibre" Checked=<%#Eval("DiaLibre") %>  runat="server"/>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>
                </table>
            </div>
            </ContentTemplate>
    </asp:UpdatePanel>
<%--<script>
    function obtenerDia() {
        var dia = getElementById();
    }
</script>--%>
</asp:Content>
