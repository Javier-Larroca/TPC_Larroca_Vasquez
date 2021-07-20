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
    public partial class ModificarMedico : System.Web.UI.Page
    {
        private EspecialidadNegocio especialidadNegocio = new EspecialidadNegocio();
        private MedicoNegocio medicoNegocio = new MedicoNegocio();
        private List<Especialidad> listaDeEspecialidades = new List<Especialidad>();
        private Medico medicoModificado = new Medico();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                medicoModificado = (Medico)Session["MedicoSeleccionado"];
                if (!IsPostBack)
                    {
                    nombreMedico.Text = medicoModificado.Nombre;
                    apellidoMedico.Text = medicoModificado.Apellido;
                    matriculaMedico.Text = medicoModificado.Matricula.ToString();
                    emailMedico.Text = medicoModificado.Mail;

                    cargarEspecialidadesSeleccionadas(medicoModificado.Especialidades);
                    }
                else if(medicoModificado == null) throw new Exception("No se seleccionó ningún medico para modificar");
            }
            catch(Exception ex)
            {
                Response.Write(ex.Message);
            }

        }

        protected void modificarMedico_Click(object sender, EventArgs e)
        {
            try
            {
                //Si modifico mail o matricula procedemos a buscar que no se encuentren repetidos en otros medicos
                modificoDatosUnicos(medicoModificado);
                //Cargamos las listas de especialidades que guardamos en Session
                listaDeEspecialidades = especialidadNegocio.listaDeEspecialidades();
                List<Especialidad> especialidadesSeleccionadas = new List<Especialidad>();
                medicoModificado.Nombre = nombreMedico.Text;
                medicoModificado.Apellido = apellidoMedico.Text;
                medicoModificado.Matricula = int.Parse(matriculaMedico.Text);
                medicoModificado.Mail = emailMedico.Text;
                bool succes;
                bool succesLista;

                foreach (ListItem item in listaDeEspecialidadesCheckBox.Items)
                {
                    if (item.Selected == true)
                    {
                        especialidadesSeleccionadas.Add(listaDeEspecialidades.Find(Busqueda => Busqueda.Descripcion == item.Value));
                    }
                }

                medicoModificado.Especialidades = especialidadesSeleccionadas;

                if (medicoNegocio.modificarMedico(medicoModificado))
                {
                    succes = true;
                    if (medicoModificado.Especialidades == null) succesLista = false;
                    else succesLista = especialidadNegocio.altaDeEspecialidadPorMedico(medicoModificado);
                    Response.Redirect("DetalleMedico?id="+ medicoModificado.Id + "&MedModif=" + succes + "&ListMod=" + succesLista, false);
                }
                else FailMedico.Visible = true;
            }
            catch (Exception ex)
            {
                Warning.Text = ex.Message;
                Warning.Visible = true;

            }
        }

        private void cargarEspecialidadesSeleccionadas(List<Especialidad> lista)
        {
            listaDeEspecialidades = especialidadNegocio.listaDeEspecialidades();
            listaDeEspecialidadesCheckBox.DataSource = listaDeEspecialidades;
            listaDeEspecialidadesCheckBox.DataBind();

            foreach(ListItem item in listaDeEspecialidadesCheckBox.Items)
            {
                if (lista.Exists(Busqueda => Busqueda.Descripcion.ToUpper() == item.Value.ToUpper())) item.Selected = true;
            }
        }
        private void validarMail(string mail, List<Medico> medicos)
        {
            if (medicos.Exists(Busqueda => Busqueda.Mail.ToUpper().Contains(mail.ToUpper()))) throw new Exception("Ya existe un usuario dado de alta con el mail ingresado");
        }
        private void validarMatricula(int matricula, List<Medico> medicos)
        {
            if (medicos.Exists(Busqueda => Busqueda.Matricula == matricula)) throw new Exception("Ya existe un usuario dado de alta el numero de matricula ingresado");
        }
        private void modificoDatosUnicos(Medico medico)
        {
            List<Medico> medicos = (List<Medico>)Session["ListaDeMedicos"];
            if (medico.Mail.ToUpper() != emailMedico.Text.ToUpper()) validarMail(medico.Mail, medicos);
            if (medico.Matricula != int.Parse(matriculaMedico.Text)) validarMatricula(medico.Matricula, medicos);
        }
    }
}