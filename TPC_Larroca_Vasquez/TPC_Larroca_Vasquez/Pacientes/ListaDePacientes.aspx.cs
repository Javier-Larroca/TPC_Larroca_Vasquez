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
    public partial class ListaDePacientes : System.Web.UI.Page
    {
        private PacienteNegocio negocio;
        public List<Paciente> listaDePacientes;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    negocio = new PacienteNegocio();
                    listaDePacientes = negocio.listarPaciente();
                    Session.Add("ListaDePacientes", listaDePacientes);
                }
                if (Request.QueryString["Email"] != null)
                {
                    string emailPacienteAEliminar = Request.QueryString["Email"];
                    negocio.eliminarPaciente(emailPacienteAEliminar);
                }
            }
            catch
            {
                Response.Redirect("Inicio");
            }

        }

    }
}