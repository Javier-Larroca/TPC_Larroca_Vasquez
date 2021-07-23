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
    public partial class DetalleTurno : System.Web.UI.Page
    {
        public Turno TurnoSeleccionado { get; set; }
        private TurnoNegocio turnoNegocio = new TurnoNegocio();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                List<Turno> listado = turnoNegocio.listaDeTurnos();

                if (Request.QueryString["Id"] != null)
                {
                    int Id = int.Parse(Request.QueryString["Id"]);
                    TurnoSeleccionado = listado.Find(x => x.NumeroDeTurno == Id);
                }
                else TurnoSeleccionado = (Turno)Session["TurnoAlta"];
                    Session.Add("TurnoSeleccionado", TurnoSeleccionado);
                if (Request.QueryString["success"] != null) evaluarQueryString(Request.QueryString["success"]);
            }
            catch
            {
                Response.Redirect("Inicio");
            }
        }

        private void evaluarQueryString(string query)
        {
            if (query.ToUpper() == "TRUE") SuccesTurno.Visible = true;
            else FailTurno.Visible = true;
        }

        protected void Volver_Click(object sender, EventArgs e)
        {
            Response.Redirect("ListaDeTurnos");
        }

        protected void Cancelar_Click(object sender, EventArgs e)
        {
            TurnoSeleccionado.Estado = false;
            turnoNegocio.guardarEstado(TurnoSeleccionado.NumeroDeTurno);
            Response.Redirect("ListaDeTurnos", false);
        }

    }
}