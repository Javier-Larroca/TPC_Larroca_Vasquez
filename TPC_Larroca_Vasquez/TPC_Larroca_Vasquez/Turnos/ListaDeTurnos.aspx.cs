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
    public partial class ListaDeTurnos : System.Web.UI.Page
    {
        private TurnoNegocio negocio = new TurnoNegocio();
        public List<Turno> listaDeTurnos;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                if (Request.QueryString["success"] != null)
                {

                    evaluarQueryString(Request.QueryString["success"]);
                }

                listaDeTurnos = negocio.listaDeTurnos();
                Session.Add("ListaDeMedicos", listaDeTurnos);
            }
            catch
            {
                Response.Redirect("Inicio");
            }
        }

        protected void opcionDeBusqueda_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (opcionDeBusqueda.SelectedIndex == 1)
            {
                listaDeTurnos = listaDeTurnos.FindAll(Busqueda => Busqueda.Estado == true);
            }
            else if (opcionDeBusqueda.SelectedIndex == 2) listaDeTurnos = listaDeTurnos.FindAll(Busqueda => Busqueda.Estado == false);
            
        }

        private void evaluarQueryString(string query)
        {
            if (query.ToUpper() == "TRUE") SuccesTurno.Visible = true;
            else FailTurno.Visible = true;
        }
    }
}