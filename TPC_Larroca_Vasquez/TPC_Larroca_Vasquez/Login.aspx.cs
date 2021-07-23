using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using Negocio;

namespace TPC_Larroca_Vasquez
{
    public partial class Login : System.Web.UI.Page
    {
        UsuarioNegocio negocio = new UsuarioNegocio();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Ingresar_Click(object sender, EventArgs e)
        {
            try
            {
                Usuario logueado = negocio.existeUsuario(Email.Text);

                if (logueado.Id == 0)
                {
                    ErrorLogueo.Visible = true;
                }
                else
                {
                    Session.Add("UsuarioLogueado", logueado);
                    Response.Redirect("Inicio", false);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}