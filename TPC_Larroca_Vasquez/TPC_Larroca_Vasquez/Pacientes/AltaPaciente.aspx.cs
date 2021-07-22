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
            
            SuccessLista.Text = "Se cargaron correctamente las especialidades correspondientes";
            SuccessPaciente.Text = "Se agrego correctamente al usuario ";
            FailPaciente.Text = "ATENCION: No se pudo cargar al usuario ";
            FailLista.Text = "No se pudieron cargar correctamente las especialidades correspondientes";
            try
            {
                if (!IsPostBack)
                {
                    idObraSocial.DataSource = obraSocialNegocio.listaDeObrasSociales();
                    idObraSocial.DataBind();

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
                //Cargamos las listas de especialidades que guardamos en Session
                //listaDeObrasSociales = (List<ObraSocial>)Session["ObrasSociales"];
                //List<Especialidad> especialidadesSeleccionadas = new List<Especialidad>();
                pacienteAgregado.Nombre = nombrePaciente.Text;
                pacienteAgregado.Apellido = apellidoPaciente.Text;
                pacienteAgregado.Mail = emailPaciente.Text;
                pacienteAgregado.ObraSocial.Id = idObraSocial.SelectedIndex;


                    if (pacienteNegocio.agregarPaciente(pacienteAgregado))
                    {
                        SuccessPaciente.Visible = true;
                        //pacienteAgregado.Id = pacienteNegocio.buscarPaciente(pacienteAgregado.Id);
                        //if (!especialidadNegocio.altaDeEspecialidadPorMedico(pacienteAgregado)) FailLista.Visible = true;
                        SuccessLista.Visible = true;
                    }
                    else FailPaciente.Visible = true;

                
            }
            catch (Exception)
            {
                Warning.Visible = true;

            }

        }
    }
}