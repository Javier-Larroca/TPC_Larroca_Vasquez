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
            
            //SuccessLista.Text = "Se cargaron correctamente las especialidades correspondientes";
            //SuccessPaciente.Text = "Se agrego correctamente al usuario ";
            //FailMedico.Text = "ATENCION: No se pudo cargar al usuario ";
            //FailLista.Text = "No se pudieron cargar correctamente las especialidades correspondientes";
            try
            {
                if (!IsPostBack)
                {
                    //Llamamamos a la base y buscamos todas las especialidades disponibles para usarla en checkbox
                    //Y después buscamos en la misma lista las que el usuario seleccione
                    listaDeObrasSociales = obraSocialNegocio.listaDeObrasSociales();


                    Session.Add("ObrasSociales", listaDeObrasSociales);
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("../Inicio");
                Response.Write(ex.Message);
            }

        }

        //protected void crearPaciente_Click(object sender, EventArgs e)
        //{

        //    try
        //    {
        //        //Cargamos las listas de especialidades que guardamos en Session
        //        listaDeObrasSociales = (List<ObraSocial>)Session["ObrasSociales"];
        //        List<Especialidad> especialidadesSeleccionadas = new List<Especialidad>();
        //        //pacienteAgregado.Nombre = nombrePaciente.Text;
        //        //pacienteAgregado.Apellido = apellidoPaciente.Text;
        //        //pacienteAgregado.Mail = emailPaciente.Text;


        //        //Agregamos Medico a base y si se agrego correctamente, procedemos a cargarle sus especialidades
        //        //Procedemos a buscarlo en la base para obtener su ID
        //        if (medicoAgregado.Especialidades != null)
        //        {
        //            if (medicoNegocio.agregarMedico(medicoAgregado))
        //            {
        //                SuccessMedico.Visible = true;
        //                medicoAgregado.Id = medicoNegocio.buscarMedico(medicoAgregado.Matricula);
        //                if (!especialidadNegocio.altaDeEspecialidadPorMedico(medicoAgregado)) FailLista.Visible = true;
        //                else SuccessLista.Visible = true;
        //            }
        //            else FailMedico.Visible = true;

        //        }
        //    }
        //    catch (Exception)
        //    {
        //        Warning.Visible = true;

        //    }

        //}
    }
}