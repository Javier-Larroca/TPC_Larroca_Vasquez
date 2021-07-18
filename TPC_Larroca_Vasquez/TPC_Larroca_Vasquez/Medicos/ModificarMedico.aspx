<%@ Page Title="" Language="C#" MasterPageFile="../Interfaces/Interfaz.Master" AutoEventWireup="true" CodeBehind="ModificarMedico.aspx.cs" Inherits="TPC_Larroca_Vasquez.ModificarMedico" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <nav class="navbar navbar-light navColor">
        <div class="container-fluid container-sin-espacios">
            <i class="fas fa-star-of-life"></i>
            <p> Modificar datos de Medico</p>
        </div>
    </nav>
    <section class="section-alta-usuario">
        <div style="display: inline-block; width: 65%;">
            <strong style="font-size: 18px;">Cargue los siguientes datos:</strong>
            <br />
            <div class="mb-3">
                <label class="form-label">Nombre</label>
                <asp:TextBox CssClass="form-control inputSize" ID="nombreMedico" runat="server"></asp:TextBox>
            </div>
            <div class="mb-3">
                <label class="form-label">Apellido</label>
                <asp:TextBox CssClass="form-control inputSize" ID="apellidoMedico" runat="server"></asp:TextBox>
            </div>
            <div class="mb-3">
                <label class="form-label">Matricula</label>
                <asp:TextBox CssClass="form-control inputSize" ID="matriculaMedico" runat="server"></asp:TextBox>
            </div>
            <div class="mb-3">
                <label for="exampleInputEmail1" class="form-label">Email</label>
                <asp:TextBox CssClass="form-control inputSize" type="email" ID="emailMedico" runat="server"></asp:TextBox>
            </div>
            <asp:Button CssClass="btn btn-primary" Text="Modificar" ID="modificarMedico" OnClick="modificarMedico_Click" AutoPostback="true" runat="server" />
            <div style="display: flex; margin-top: 10px;">
                <asp:Label CssClass="alert alert-warning" Visible="false" role="alert" ID="Warning" runat="server" Text="Debe completar todos los campos obligatorios">
                    <asp:Button CssClass="btn-close" runat="server" ID="boton" data-bs-dismiss="alert" />
                </asp:Label>
            </div>
            <div style="display: flex; margin-top: 10px">
                <asp:Label CssClass="aler alert-danger" Visible="false" role="alert" ID="FailMedico" runat="server" Text="ATENCION: No se pudo cargar al usuario ">
                    <asp:Button CssClass="btn-close" runat="server" ID="Button3" data-bs-dismiss="alert" />
                </asp:Label>
            </div>
        </div>
        <div style="display: inline-block; float: right;">
            <strong style="font-size: 18px;">Modifique una o más especialidades:</strong>
            <br />
            <asp:CheckBoxList ID="listaDeEspecialidadesCheckBox" runat="server"></asp:CheckBoxList>
        </div>
    </section>
</asp:Content>
