<%@ Page Title="Pacientes" Language="C#" MasterPageFile="~/Interfaz.Master" AutoEventWireup="true" CodeBehind="ListaDePacientes.aspx.cs" Inherits="TPC_Larroca_Vasquez.ListaDePacientes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <nav class="navbar navbar-light navColor">
        <div class="container-fluid container-sin-espacios">
            <i class="fas fa-star-of-life"></i>
            <p> Listado de pacientes</p>
        </div>
    </nav>
   <div class="div-tabla">
       <table class="table table-bordered">
  <thead class="table-dark">
    <tr>
      <th scope="col" class="centrar-texto">#</th>
      <th scope="col" class="centrar-texto">Nombre</th>
      <th scope="col" class="centrar-texto">Apellido</th>
      <th scope="col" class="centrar-texto">Fecha Nacimiento</th>
      <th scope="col" class="centrar-texto">Contacto</th>
      <th scope="col" class="centrar-texto">Detalles</th>
    </tr>
  </thead>
           <tbody>
               <%foreach (var paciente in listaDePacientes)
                   { %>
               <tr>
                   <th scope="row"><i class="fas fa-user "></i></th>
                   <td class="centrar-texto"><%=paciente.Nombre %></td>
                   <td class="centrar-texto"><%=paciente.Apellido %></td>
                   <td class="centrar-texto"><%=paciente.FechaNac %></td>
                   <td class="centrar-texto"><%=paciente.Mail %></td>
                   <td class="centrar-texto">
                       <a href="DetallePaciente?id=<%=paciente.Id %>" style="align-items: center;">
                           <i class="fas fa-search-plus" style="color: black;"></i>
                       </a>
                   </td>
               </tr>
               <%} %>
           </tbody>
</table>
   </div>
</asp:Content>
