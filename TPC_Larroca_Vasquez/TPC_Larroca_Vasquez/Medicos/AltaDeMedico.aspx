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
        <div style="display: inline-block; width: 65%;">
            <strong style ="font-size: 18px;">Cargue los siguientes datos:</strong>
            <br />
        <div class="mb-3">
            <label class="form-label">Nombre</label>
            <asp:TextBox cssClass="form-control inputSize" ID="nombreMedico" runat="server"></asp:TextBox>
        </div>
        <div class="mb-3">
            <label class="form-label">Apellido</label>
            <asp:TextBox cssClass="form-control inputSize" ID="apellidoMedico" runat="server"></asp:TextBox>
        </div>
        <div class="mb-3">
            <label class="form-label">Matricula</label>
            <asp:TextBox cssClass="form-control inputSize" ID="matriculaMedico" runat="server"></asp:TextBox>
        </div>
        <div class="mb-3">
            <label for="exampleInputEmail1" class="form-label">Email</label>
            <asp:TextBox CssClass="form-control inputSize" type="email" ID="emailMedico" runat="server"></asp:TextBox>
            <%--<div id="emailHelp" class="form-text">We'll never share your email with anyone else.</div>--%>
        </div>
        <asp:Button cssClass="btn btn-primary" text="Crear" ID="crearMedico" OnClick="crearMedico_Click" runat="server"/>
            </div>
        <div style="display: inline-block; float:right;">
            <strong style="font-size: 18px;">Seleccione una o más especialidades:</strong>
            <br />
            <asp:CheckBoxList ID="listaDeEspecialidadesCheckBox"  runat="server"></asp:CheckBoxList>
        </div>
    </section>
</asp:Content>
