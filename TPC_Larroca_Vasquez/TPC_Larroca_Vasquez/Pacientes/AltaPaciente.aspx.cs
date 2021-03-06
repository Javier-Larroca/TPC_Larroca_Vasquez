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
    public partial class AltaPaciente : System.Web.UI.Page
    {
        private ObraSocialNegocio obraSocialNegocio = new ObraSocialNegocio();
        private PacienteNegocio pacienteNegocio = new PacienteNegocio();
        private List<ObraSocial> listaDeObrasSociales = new List<ObraSocial>();
        private Paciente pacienteAgregado = new Paciente();

        protected void Page_Load(object sender, EventArgs e)
        {
            
            SuccessPaciente.Text = "Se agrego correctamente el paciente";
            FailPaciente.Text = "ATENCION: No se pudo cargar el paciente ";

            try
            {
                if (!IsPostBack)
                {
                    idObraSocial.DataSource = obraSocialNegocio.listaDeObrasSociales();
                    idObraSocial.DataBind();

                    List<int> anio = new List<int>();
                    for (int x = 1900; x < 2022; x++)
                    {
                        anio.Add(x);
                    }
                    anioNac.DataSource = anio;
                    anioNac.DataBind();
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("../Inicio");
                Response.Write(ex.Message);
            }

        }

        protected void crearPaciente_Click(object sender, EventArgs e)
        {

            try
            {
                List<ObraSocial> oS = obraSocialNegocio.listaDeObrasSociales();
                pacienteAgregado.Nombre = nombrePaciente.Text;
                pacienteAgregado.Apellido = apellidoPaciente.Text;
                pacienteAgregado.Mail = emailPaciente.Text;
                string fecha = anioNac.SelectedValue+"/"+mesNac.SelectedValue+"/"+diaNac.SelectedValue;
                pacienteAgregado.FechaNac = DateTime.Parse(fecha);
                pacienteAgregado.ObraSocial = oS.Find(busqueda => busqueda.Descripcion.ToUpper() == idObraSocial.SelectedValue.ToUpper());

                    if (pacienteNegocio.agregarPaciente(pacienteAgregado))
                    {
                        SuccessPaciente.Visible = true;
                    }
                    else FailPaciente.Visible = true;
                
            }
            catch (Exception)
            {
                Warning.Visible = true;
            }

        }


        protected void mesNac_SelectedIndexChanged(object sender, EventArgs e)
        {
            int mes = int.Parse(mesNac.SelectedItem.Value);
            List<int> dias = new List<int>();
            if (mes == 02) {
                for (int x = 1; x < 29; x++)
                {
                    dias.Add(x);
                }
            }
            else { 
                if (mes == 01 || mes == 03 || mes == 05 || mes == 07 || mes == 08 || mes == 10 || mes == 12)
                {
                    for (int x = 1; x < 32; x++)
                    {
                        dias.Add(x);
                    }
                }
                else
                {
                    for (int x = 1; x < 31; x++)
                    {
                        dias.Add(x);
                    }
                }
            }
            diaNac.DataSource = dias;
            diaNac.DataBind();
        }

    }
}