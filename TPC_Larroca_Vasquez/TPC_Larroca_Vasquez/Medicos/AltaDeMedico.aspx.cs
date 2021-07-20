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
            try
            {
                //Llamamamos a la base y buscamos todas las especialidades disponibles para usarla en checkbox
                //Y después buscamos en la misma lista las que el usuario seleccione
                if(!IsPostBack){
                    listaDeEspecialidades = especialidadNegocio.listaDeEspecialidades();
                    listaDeEspecialidadesCheckBox.DataSource = listaDeEspecialidades;
                    listaDeEspecialidadesCheckBox.DataBind();

                    Session.Add("Especialidades", listaDeEspecialidades);
                }
            }
            catch (Exception ex)
            {
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
                bool succes;
                bool succesLista;

                validarDatos(medicoAgregado.Matricula, medicoAgregado.Mail);

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
                //Linea 72, en caso de algún error no arrrojamos una excepción ya que si entro al IF es porque pudo agregar
                //al medico. En ese caso, pasamos succesLista como false para informar en ListaDeMedicos que no se agregaron especialdiades.
                //Mismo si se generó algún error al agregarlos, lo pasamos como false para informar lo mismo

                if (medicoNegocio.agregarMedico(medicoAgregado))
                {
                    succes = true;
                    medicoAgregado.Id = medicoNegocio.buscarMedico(medicoAgregado.Matricula);
                    if (medicoAgregado.Especialidades == null) succesLista = false;
                       else succesLista = especialidadNegocio.altaDeEspecialidadPorMedico(medicoAgregado);
                    Response.Redirect("ListaDeMedicos?Med=" + succes + "&List=" + succesLista, false);
                }
                else FailMedico.Visible = true;
            }
            catch (Exception ex)
            {
                Warning.Text = ex.Message;
                Warning.Visible = true;

            }

        }
        private void validarDatos(int matricula, string mail)
        {
            List<Medico> medicos = medicoNegocio.listarMedicos();
            if (medicos.Exists(Busqueda => Busqueda.Matricula == matricula)) throw new Exception("Ya existe un usuario dado de alta el numero de matricula ingresado");
            if (medicos.Exists(Busqueda => Busqueda.Mail.ToUpper().Contains(mail.ToUpper()))) throw new Exception("Ya existe un usuario dado de alta con el mail ingresado");
        }

    }
}