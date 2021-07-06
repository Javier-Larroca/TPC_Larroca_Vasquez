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
        private EspecialidadNegocio especialidadNegocio = new EspecialidadNegocio();
        private MedicoNegocio medicoNegocio = new MedicoNegocio();
        private List<Especialidad> listaDeEspecialidades = new List<Especialidad>();
        private Medico medicoAgregado = new Medico();

        protected void Page_Load(object sender, EventArgs e)
        {
            SuccessLista.Text = "Se cargaron correctamente las especialidades correspondientes";
            SuccessMedico.Text = "Se agrego correctamente al usuario ";
            FailMedico.Text = "ATENCION: No se pudo cargar al usuario ";
            FailLista.Text = "No se pudieron cargar correctamente las especialidades correspondientes";
            try
            {
                if (!IsPostBack)
                {
                    //Llamamamos a la base y buscamos todas las especialidades disponibles para usarla en checkbox
                    //Y después buscamos en la misma lista las que el usuario seleccione
                    listaDeEspecialidades = especialidadNegocio.listaDeEspecialidades();
                    listaDeEspecialidadesCheckBox.DataSource = listaDeEspecialidades;
                    listaDeEspecialidadesCheckBox.DataBind();

                    Session.Add("Especialidades", listaDeEspecialidades);
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("../Inicio");
                Response.Write(ex.Message);
            }

        }

        protected void crearMedico_Click(object sender, EventArgs e)
        {

            try
            {
                //Cargamos las listas de especialidades que guardamos en Session
                listaDeEspecialidades = (List<Especialidad>)Session["Especialidades"];
                List<Especialidad> especialidadesSeleccionadas = new List<Especialidad>();
                medicoAgregado.Nombre = nombreMedico.Text;
                medicoAgregado.Apellido = apellidoMedico.Text;
                medicoAgregado.Matricula = int.Parse(matriculaMedico.Text);
                medicoAgregado.Mail = emailMedico.Text;

                foreach (ListItem item in listaDeEspecialidadesCheckBox.Items)
                {
                    if (item.Selected == true)
                    {
                        //Si el item esta seleccionado, buscamos en la lista de especialidades que cargamos al principio
                        //Agregamos a la nueva lista de especialidades seleccionadas la especialidad que nos devuelva el Find(Retorna especialidad)
                        especialidadesSeleccionadas.Add(listaDeEspecialidades.Find(Busqueda => Busqueda.Descripcion == item.Value));
                    }
                }

                medicoAgregado.Especialidades = especialidadesSeleccionadas;

                //Agregamos Medico a base y si se agrego correctamente, procedemos a cargarle sus especialidades
                //Procedemos a buscarlo en la base para obtener su ID
                if (medicoAgregado.Especialidades != null)
                {
                    if (medicoNegocio.agregarMedico(medicoAgregado))
                    {
                        SuccessMedico.Visible = true;
                        medicoAgregado.Id = medicoNegocio.buscarMedico(medicoAgregado.Matricula);
                        if (!especialidadNegocio.altaDeEspecialidadPorMedico(medicoAgregado)) FailLista.Visible = true;
                        else SuccessLista.Visible = true;
                    }
                    else
                    {
                        FailMedico.Visible = true;
                    }
                }
            }
            catch (Exception)
            {
                Warning.Visible = true;

            }

        }
    }
}