<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="TPC_Larroca_Vasquez.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent1" runat="server">
    <div style="margin-left: 33%; margin-top: 14px;">
        <img src="img/LAVE.png" style="width: 30%" />
    </div>
    <div>
        <h1 style="margin-left: 39%; margin-top: 21px;">LAVE </h1>
    </div>
    <div style="margin-left: 26%; margin-top: 2%; margin-bottom: 2%;">
        Ingrese mail para poder ingresar al sistema
    </div>
    <div class="form-floating mb-3" style="width: 30%; margin-left: 29%;">
        <asp:TextBox CssClass="form-control" ID="Email" runat="server"></asp:TextBox>
        <label for="Email">Email</label>
    </div>
    <div style="margin-left: 40%; margin-top: 2%; margin-bottom: 2%;">
        <asp:Button ID="Ingresar" CssClass="btn btn-primary" OnClick="Ingresar_Click" Text="Ingresar" runat="server" />
    </div>
    <div style="width: 30%; margin-left: 33%; margin-top: 3%;">
        <asp:Label CssClass="alert alert-danger" ID="ErrorLogueo" Visible="false" Text ="Usuario ingresado no válido" runat="server"></asp:Label>
    </div>
</asp:Content>

