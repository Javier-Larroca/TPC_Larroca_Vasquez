using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using Negocio;

namespace TPC_Larroca_Vasquez.Turnos
{
    public partial class TurnosAsignados : System.Web.UI.Page
    {
        private Usuario usuario = new Usuario();
        private TurnoNegocio negocio = new TurnoNegocio();
        public List<Turno> listaDeTurnos = new List<Turno>();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                usuario = (Usuario)Session["UsuarioLogueado"];
                if(usuario.Id != 0)
                {
                   listaDeTurnos = negocio.listaDeTurnosPorMedico(usuario.Id);
                }
            }
            catch
            {

            }
        }
    }
}