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
    public partial class AltaDeMedico : System.Web.UI.Page
    {
        public Medico MedicoAgregado { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            MedicoAgregado = new Medico();

        }
    }
}