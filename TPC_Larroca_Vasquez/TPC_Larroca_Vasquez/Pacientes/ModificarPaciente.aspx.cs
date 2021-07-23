using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;
using Dominio;

namespace TPC_Larroca_Vasquez.Pacientes
{
    public partial class ModificarPaciente : System.Web.UI.Page
    {
        private ObraSocialNegocio obraSocialNegocio = new ObraSocialNegocio();
        private PacienteNegocio pacienteNegocio = new PacienteNegocio();
        private List<ObraSocial> listaDeObrasSociales = new List<ObraSocial>();
        private Paciente pacienteModificado = new Paciente();
        protected void Page_Load(object sender, EventArgs e)
        {

            SuccessPaciente.Text = "Se modifico correctamente el paciente";
            FailPaciente.Text = "ATENCION: No se pudo modificar al usuario ";


            try
            {
                pacienteModificado = (Paciente)Session["PacienteSeleccionado"];
                if (!IsPostBack)
                {
                    List<int> anio = new List<int>();
                    for (int x = 1900; x < 2022; x++)
                    {
                        anio.Add(x);
                    }
                    anioNac.DataSource = anio;
                    anioNac.DataBind();

                    nombrePaciente.Text = pacienteModificado.Nombre; 
                    apellidoPaciente.Text = pacienteModificado.Apellido;
                    emailPaciente.Text = pacienteModificado.Mail;

                    idObraSocial.DataSource = obraSocialNegocio.listaDeObrasSociales();
                    idObraSocial.DataBind();
                    idObraSocial.SelectedValue = pacienteModificado.ObraSocial.Descripcion;

                    anioNac.SelectedValue = pacienteModificado.FechaNac.Year.ToString();
                    mesNac.SelectedValue = pacienteModificado.FechaNac.Month.ToString();
                    diaNac.SelectedValue = pacienteModificado.FechaNac.Day.ToString();
                }
                else if (pacienteModificado == null) throw new Exception("No se seleccionó ningún paciente para modificar");
            }
            catch (Exception ex)
            {
                Response.Redirect("../Inicio");
                Response.Write(ex.Message);
            }
        }

        protected void modificarPaciente_Click(object sender, EventArgs e)
        {

            try
            {
                List<ObraSocial> oS = obraSocialNegocio.listaDeObrasSociales();

                pacienteModificado.Nombre = nombrePaciente.Text;
                pacienteModificado.Apellido = apellidoPaciente.Text;
                pacienteModificado.Mail = emailPaciente.Text;
                string fecha = anioNac.SelectedValue+"/"+mesNac.SelectedValue+"/"+diaNac.SelectedValue;
                pacienteModificado.FechaNac = DateTime.Parse(fecha);
                pacienteModificado.ObraSocial = oS.Find(busqueda => busqueda.Descripcion.ToUpper() == idObraSocial.SelectedValue.ToUpper());


                if (pacienteNegocio.modificarPaciente(pacienteModificado))
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
            if (mes == 02)
            {
                for (int x = 1; x < 29; x++)
                {
                    dias.Add(x);
                }
            }
            else
            {
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