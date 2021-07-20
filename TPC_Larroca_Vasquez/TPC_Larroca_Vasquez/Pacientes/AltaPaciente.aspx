<%@ Page Title="" Language="C#" MasterPageFile="../Interfaces/Interfaz.Master" AutoEventWireup="true" CodeBehind="AltaPaciente.aspx.cs" Inherits="TPC_Larroca_Vasquez.AltaDeMedico" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <nav class="navbar navbar-light navColor">
        <div class="container-fluid container-sin-espacios">
            <i class="fas fa-star-of-life"></i>
            <p> Alta de paciente</p>
        </div>
    </nav>
    <section class="section-alta-usuario">
        <div style="display: inline-block; width: 65%;">
            <strong style ="font-size: 18px;">Cargue los siguientes datos:</strong>
            <br />
        <div class="mb-3">
            <label class="form-label">Nombre</label>
            <asp:TextBox cssClass="form-control inputSize" ID="nombrePaciente" runat="server"></asp:TextBox>
        </div>
        <div class="mb-3">
            <label class="form-label">Apellido</label>
            <asp:TextBox cssClass="form-control inputSize" ID="apellidoPaciente" runat="server"></asp:TextBox>
        </div>

        <div class="mb-3">
            <label for="exampleInputEmail1" class="form-label">Email</label>
            <asp:TextBox CssClass="form-control inputSize" type="email" ID="emailPaciente" runat="server"></asp:TextBox>
            <%--<div id="emailHelp" class="form-text">We'll never share your email with anyone else.</div>--%>
        </div>
        <asp:Button cssClass="btn btn-primary" text="Crear" ID="crearPaciente" OnClick="crearPaciente_Click" runat="server"/>
            <div style="display: flex; margin-top: 10px;">
                <asp:Label CssClass="alert alert-warning" Visible="false" ID="Warning" runat="server" Text="Debe completar todos los campos obligatorios"></asp:Label></div>
            <div style="display: flex; margin-top: 10px">
                <asp:Label CssClass="alert alert-success" Visible="false" ID="SuccessPaciente" runat="server">
                    <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
                </asp:Label>
            </div>
            <div style="display: flex; margin-top: 10px">
                <asp:Label CssClass="alert alert-success" Visible="false" ID="SuccessLista" runat="server">
                    <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
                </asp:Label>
            </div>
            <div style="display: flex; margin-top: 10px">
                <asp:Label CssClass="aler alert-danger" Visible="false" ID="FailPaciente" runat="server">
                </asp:Label>
            </div>
            <div style="display: flex; margin-top: 10px">
                <asp:Label CssClass="aler alert-danger" Visible="false" ID="FailLista" runat="server">
                </asp:Label>
            </div>
            </div>

    </section>
</asp:Content>
