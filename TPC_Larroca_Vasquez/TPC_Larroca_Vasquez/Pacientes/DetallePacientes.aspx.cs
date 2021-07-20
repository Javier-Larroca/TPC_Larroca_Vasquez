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
    public partial class DetallePacientes : System.Web.UI.Page
    {
        public Paciente pacienteSeleccionado { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                int Id = int.Parse(Request.QueryString["Id"]);
                List<Paciente> listado = (List<Paciente>)Session["ListaDePacientes"];
                pacienteSeleccionado = listado.Find(x => x.Id == Id);
            }
            catch
            {
                Response.Redirect("Inicio");
            }
        }
    }
}