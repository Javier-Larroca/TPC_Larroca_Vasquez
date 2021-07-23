<%@ Page Title="" Language="C#" MasterPageFile="../Interfaces/Interfaz.Master" AutoEventWireup="true" CodeBehind="ModificarPaciente.aspx.cs" Inherits="TPC_Larroca_Vasquez.Pacientes.ModificarPaciente" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <nav class="navbar navbar-light navColor">
        <div class="container-fluid container-sin-espacios">
            <i class="fas fa-star-of-life"></i>
            <p> Modificar paciente</p>
        </div>
    </nav>

        <asp:ScriptManager ID="ScripManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel runat="server">
        <ContentTemplate>

    <section class="section-alta-usuario">
        <div style="display: inline-block; width: 65%;">
            <strong style ="font-size: 18px;">Modifique los siguientes datos:</strong>
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
            
        <div class="mb-3">
            <label class="form-label">Obra Social</label>
            <asp:DropDownList runat="server" ID="idObraSocial" cssClass="form-control inputSize dropdown-toggle">
            </asp:DropDownList>
        </div>


                   <div class="mb-3">
           <label class="form-label">Fecha de nacimiento</label>

            <br>Año
           <asp:DropDownList runat="server" ID="anioNac" cssClass="form-control inputSize dropdown-toggle">

           </asp:DropDownList>
               
            <br>Mes
           <asp:DropDownList runat="server" ID="mesNac" cssClass="form-control inputSize dropdown-toggle"
               AutoPostBack="true" OnSelectedIndexChanged="mesNac_SelectedIndexChanged">
                   <asp:ListItem Text="01" />
                   <asp:ListItem Text="02" />
                   <asp:ListItem Text="03" />
                   <asp:ListItem Text="04" />
                   <asp:ListItem Text="05" />
                   <asp:ListItem Text="06" />
                   <asp:ListItem Text="07" />
                   <asp:ListItem Text="08" />
                   <asp:ListItem Text="09" />
                   <asp:ListItem Text="10" />
                   <asp:ListItem Text="11" />
                   <asp:ListItem Text="12" />
               </asp:DropDownList>

            <br>Dia
           <asp:DropDownList runat="server" ID="diaNac" cssClass="form-control inputSize dropdown-toggle">
           </asp:DropDownList>

         <br>
        <asp:Button cssClass="btn btn-primary" text="Modificar" ID="modificarPaciente" OnClick="modificarPaciente_Click" runat="server"/>
            
                       
            <div style="display: flex; margin-top: 10px;">
                <asp:Label CssClass="alert alert-warning" Visible="false" ID="Warning" runat="server" Text="Debe completar todos los campos obligatorios">
            </asp:Label></div>
            
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
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
