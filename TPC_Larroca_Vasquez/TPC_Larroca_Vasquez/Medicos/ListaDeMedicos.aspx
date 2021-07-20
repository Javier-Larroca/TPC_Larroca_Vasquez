<%@ Page Title="Medicos" Language="C#" MasterPageFile="../Interfaces/Interfaz.Master" AutoEventWireup="true" CodeBehind="ListaDeMedicos.aspx.cs" Inherits="TPC_Larroca_Vasquez.ListaDeMedicos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <nav class="navbar navbar-light navColor">
        <div class="container-fluid container-sin-espacios">
            <i class="fas fa-star-of-life"></i>
            <p>Listado de medicos</p>
        </div>
    </nav>
    <div style="display: flex; margin-top: 10px; padding-left: 29px">
        <asp:Label CssClass="alert alert-success" Visible="false" role="alert" ID="SuccessMedico" runat="server" Text="Se agrego correctamente al usuario">
            <asp:Button CssClass="btn-close" runat="server" ID="Button1" data-bs-dismiss="alert" />
        </asp:Label>
    </div>
    <div style="display: flex; margin-top: -7px; padding-left: 29px">
        <asp:Label CssClass="alert alert-success" Visible="false" role="alert" ID="SuccessLista" runat="server" Text="Se cargaron correctamente las especialidades correspondientes">
            <asp:Button CssClass="btn-close" runat="server" ID="Button2" data-bs-dismiss="alert" />
        </asp:Label>
    </div>
    <div style="display: flex; margin-top: 5px; padding-left: 29px">
        <asp:Label CssClass="alert alert-danger" Visible="false" role="alert" ID="FailLista" runat="server" Text="No se cargaron especialidades. Ingresar a detalle de Medico para agregarlas">
            <asp:Button CssClass="btn-close" runat="server" ID="Button4" data-bs-dismiss="alert" />
        </asp:Label>
    </div>
    <div class="div-tabla">
        <table class="table table-bordered">
            <thead class="table-dark">
                <tr>
                    <th scope="col" class="centrar-texto">#</th>
                    <th scope="col" class="centrar-texto">Nombre</th>
                    <th scope="col" class="centrar-texto">Apellido</th>
                    <th scope="col" class="centrar-texto">Matricula</th>
                    <th scope="col" class="centrar-texto">Contacto</th>
                    <th scope="col" class="centrar-texto">Detalles</th>
                </tr>
            </thead>
            <tbody>
                <%foreach (var medico in listaDeMedicos)
                    { %>
                <tr>
                    <th scope="row"><i class="fas fa-user-md "></i></th>
                    <td class="centrar-texto"><%=medico.Nombre %></td>
                    <td class="centrar-texto"><%=medico.Apellido %></td>
                    <td class="centrar-texto"><%=medico.Matricula %></td>
                    <td class="centrar-texto"><%=medico.Mail %></td>
                    <td class="centrar-texto">
                        <a href="DetalleMedico?id=<%=medico.Id %>" style="align-items: center;">
                            <i class="fas fa-search-plus" style="color: black;"></i>
                        </a>
                    </td>
                </tr>
                <%} %>
            </tbody>
        </table>
    </div>
</asp:Content>
