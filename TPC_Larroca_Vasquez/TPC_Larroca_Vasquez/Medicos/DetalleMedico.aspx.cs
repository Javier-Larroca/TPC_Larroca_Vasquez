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
                Session.Add("MedicoSeleccionado", medicoSeleccionado);
                if (Request.QueryString["MedModif"] != null) evaluarQueryString(Request.QueryString["MedModif"]);
                else if (Request.QueryString["succesTt"] != null) evaluarQueryString(Request.QueryString["succesTt"]);
            }
            catch
            {
                Response.Redirect("Inicio");
            }
        }

        private void evaluarQueryString(string query)
        {
            if (query.ToUpper() == "TRUE") SuccessMedico.Visible = true;
            else FailMedico.Visible = true;
        }

        protected void Volver_Click(object sender, EventArgs e)
        {
            Response.Redirect("ListaDeMedicos");
        }
    }
}