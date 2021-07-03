<%@ Page Title="Medicos" Language="C#" MasterPageFile="../Interfaces/Interfaz.Master" AutoEventWireup="true" CodeBehind="ListaDeMedicos.aspx.cs" Inherits="TPC_Larroca_Vasquez.ListaDeMedicos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <nav class="navbar navbar-light navColor">
        <div class="container-fluid container-sin-espacios">
            <i class="fas fa-star-of-life"></i>
            <p> Listado de medicos</p>
        </div>
    </nav>
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
