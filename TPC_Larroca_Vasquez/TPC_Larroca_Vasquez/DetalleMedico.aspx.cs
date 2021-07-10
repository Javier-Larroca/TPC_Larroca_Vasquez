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
    public partial class DetalleMedico : System.Web.UI.Page
    {
        public Medico medicoSeleccionado { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                int Id = int.Parse(Request.QueryString["Id"]);
                List<Medico> listado = (List<Medico>)Session["ListaDeMedicos"];
                medicoSeleccionado = listado.Find(x => x.Id == Id);
            }
            catch
            {
                Response.Redirect("Inicio");
            }
        }
    }
}