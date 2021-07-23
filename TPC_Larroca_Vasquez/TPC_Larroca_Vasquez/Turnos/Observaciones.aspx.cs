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
    public partial class Observaciones : System.Web.UI.Page
    {
        public Turno turnoAModificar = new Turno();
        private TurnoNegocio negocio = new TurnoNegocio();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                turnoAModificar.NumeroDeTurno = int.Parse(Request.QueryString["Id"]);
                if (!IsPostBack)
                {
                    turnoAModificar = negocio.listaDeTurnos().Find(Busqueda => Busqueda.NumeroDeTurno == turnoAModificar.NumeroDeTurno);
                    Observacion.Text = turnoAModificar.Observaciones;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        protected void GuardarObservacion_Click(object sender, EventArgs e)
        {
            negocio.guardarObservacion(turnoAModificar.Observaciones, turnoAModificar.NumeroDeTurno);
            Response.Redirect("TurnosAsignados", false);
        }

        protected void Observacion_TextChanged(object sender, EventArgs e)
        {
            turnoAModificar.Observaciones = Observacion.Text;
        }
    }
}