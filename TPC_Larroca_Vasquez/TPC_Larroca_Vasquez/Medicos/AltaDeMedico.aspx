<%@ Page Title="" Language="C#" MasterPageFile="../Interfaces/Interfaz.Master" AutoEventWireup="true" CodeBehind="AltaDeMedico.aspx.cs" Inherits="TPC_Larroca_Vasquez.AltaDeMedico" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <nav class="navbar navbar-light navColor">
        <div class="container-fluid container-sin-espacios">
            <i class="fas fa-star-of-life"></i>
            <p> Alta de medico</p>
        </div>
    </nav>
    <section class="section-alta-usuario">
        <div class="mb-3">
            <label class="form-label">Nombre</label>
            <input class="form-control inputSize" id="nombreMedico" aria-describedby="emailHelp">
        </div>
        <div class="mb-3">
            <label class="form-label">Apellido</label>
            <input class="form-control inputSize" id="apellidoMedico" aria-describedby="emailHelp">
        </div>
        <div class="mb-3">
            <label class="form-label">Matricula</label>
            <input class="form-control inputSize" id="matriculaMedico" aria-describedby="emailHelp">
        </div>
        <div class="mb-3">
            <label class="form-label">Apellido</label>
            <input class="form-control inputSize" id="" aria-describedby="emailHelp">
        </div>
        <div class="mb-3">
            <label for="exampleInputEmail1" class="form-label">Email</label>
            <input type="email" class="form-control inputSize" id="exampleInputEmail1" aria-describedby="emailHelp">
            <%--<div id="emailHelp" class="form-text">We'll never share your email with anyone else.</div>--%>
        </div>
        <button type="submit" class="btn btn-primary">Crear</button>
    </section>
</asp:Content>
