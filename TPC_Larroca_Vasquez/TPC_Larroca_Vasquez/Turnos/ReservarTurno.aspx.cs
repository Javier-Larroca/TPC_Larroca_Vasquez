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
    public partial class ReservarTurno : System.Web.UI.Page
    {
        private EspecialidadNegocio especialidadesNegocio = new EspecialidadNegocio();
        private TurnoNegocio turnoNegocio = new TurnoNegocio();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    Especialidades.DataSource = especialidadesNegocio.listaDeEspecialidades();
                    Especialidades.DataBind();
                    HorariosDisponibles.DataSource = turnoNegocio.horariosDisponibles();
                    HorariosDisponibles.DataBind();
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }

        }
    }
}