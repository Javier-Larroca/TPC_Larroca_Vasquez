using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;
using Dominio;
namespace TPC_Larroca_Vasquez
{
    public partial class ListaDeMedicos : System.Web.UI.Page
    {
        private MedicoNegocio negocio;
        public List<Medico> listaDeMedicos;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString["Med"] != null) evaluarQueryString(Request.QueryString["Med"], Request.QueryString["List"]);
                if (!IsPostBack)
                {
                    negocio = new MedicoNegocio();
                    listaDeMedicos = negocio.listarMedicos();
                    Session.Add("ListaDeMedicos", listaDeMedicos);
                }
            }
            catch
            {
                Response.Redirect("Inicio");
            }
            
        }

        private void evaluarQueryString(string estadoMedico, string estadolista)
        {
            if (estadoMedico.ToUpper() == "TRUE") SuccessMedico.Visible = true;
            if (estadolista.ToUpper() == "TRUE") SuccessLista.Visible = true;
            else FailLista.Visible = true;
        }

    }
}