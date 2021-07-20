<%@ Page Title="" Language="C#" MasterPageFile="../Interfaces/Interfaz.Master" AutoEventWireup="true" CodeBehind="BajaDeMedico.aspx.cs" Inherits="TPC_Larroca_Vasquez.BajaDeMedico" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <nav class="navbar navbar-light navColor">
        <div class="container-fluid container-sin-espacios">
            <i class="fas fa-star-of-life"></i>
            <p>Baja de Medico</p>
        </div>
    </nav>
    <asp:ScriptManager ID="ScripManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div>
                <div class="filtro-busqueda">
                    <asp:TextBox AutoPostBack="true" CssClass="form-control input-size-busqueda" ID="Filtro" OnTextChanged="Filtro_TextChanged" runat="server"></asp:TextBox>
                    <asp:DropDownList ID="opcionDeBusqueda" CssClass="dropdown-menu-dark dropdown-posicion" runat="server">
                        <asp:ListItem Selected="True" Value="Todos"></asp:ListItem>
                        <asp:ListItem Value="Nombre" runat="server"></asp:ListItem>
                        <asp:ListItem Value="Apellido" runat="server"></asp:ListItem>
                        <asp:ListItem Value="Matricula" runat="server"></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <div style="display: flex; margin-top: 5px; padding-left: 29px">
                <asp:Label CssClass="alert alert-success msj-baja-medico" Visible="false" role="alert" ID="SuccessBajaMedico" runat="server" Text="Se dio de baja correctamente al usuario">
                    <asp:Button CssClass="btn-close" runat="server" ID="Button1" data-bs-dismiss="alert" />
                </asp:Label>
            </div>
            <div style="display: flex; margin-top: 5px; padding-left: 29px">
                <asp:Label CssClass="alert alert-warning" Visible="false" role="alert" ID="Warning" runat="server" Text="Solo se admiten números para buscar por matricula">
                    <asp:Button CssClass="btn-close" runat="server" ID="boton" data-bs-dismiss="alert" />
                </asp:Label>
            </div>
            <div style="display: flex; margin-top: 5px; padding-left: 29px">
                <asp:Label CssClass="alert alert-warning" Visible="false" role="alert" ID="SinResultados" Text="No se encontraron resultados. Valide el filtro e intente nuevamente" runat="server">
                    <asp:Button CssClass="btn-close" runat="server" ID="exitSinResultados" data-bs-dismiss="alert" />
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
                            <th scope="col" class="centrar-texto">Eliminar</th>
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
                            <td class="centrar-texto">
                                <a href="BajaDeMedico?id=<%=medico.Id %>" style="align-items: center;">
                                    <i class="fas fa-user-slash" style="color: black;"></i>
                                </a>
                            </td>
                        </tr>
                        <%} %>
                    </tbody>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
