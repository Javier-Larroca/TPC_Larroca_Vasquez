<%@ Page Title="" Language="C#" MasterPageFile="~/Interfaces/Interfaz.Master" AutoEventWireup="true" CodeBehind="Observaciones.aspx.cs" Inherits="TPC_Larroca_Vasquez.Turnos.Observaciones" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <nav class="navbar navbar-light navColor">
        <div class="container-fluid container-sin-espacios">
            <i class="fas fa-star-of-life"></i>
            <p>Lista de Turnos</p>
        </div>
    </nav>
    <section>
        <h1 style="margin-top: 3%;">Observaciones</h1>
        <div style="display: flow-root; margin-top: 3%; width: 50%">
            <asp:TextBox ID="Observacion" OnTextChanged="Observacion_TextChanged" CssClass="form-control"  runat="server"></asp:TextBox>
        </div>
        <div style="margin-top: 3%; margin-left: 17%;">
            <asp:Button Text="Guardar" CssClass="btn btn-primary" ID="GuardarObservacion" OnClick="GuardarObservacion_Click" runat="server"/>
        </div>
    </section>
</asp:Content>
