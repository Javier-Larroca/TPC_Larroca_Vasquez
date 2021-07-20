using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using Negocio;

namespace TPC_Larroca_Vasquez.Medicos
{
    public partial class AsignacionTurnoDeTrabajos : System.Web.UI.Page
    {
        private TurnoDeTrabajoNegocio turnoTrabajoNegocio = new TurnoDeTrabajoNegocio();
        public int posicionDeItemsRepeater = 0;
        public Medico medicoSeleccionado;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                medicoSeleccionado = (Medico)Session["MedicoSeleccionado"];
                if (!IsPostBack)
                {
                    tabla.DataSource = medicoSeleccionado.TurnosDeTrabajo;
                    tabla.DataBind();
                    habilitarModificación(false);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        protected void Modificar_Click(object sender, EventArgs e)
        {
            //Muestro y habilito botones, también agrego valores por defecto a los horarios que se activan
            Modificar.Enabled = false;
            Volver.Visible = false;
            Guardar.Visible = true;
            Cancelar.Visible = true;
            habilitarModificación();
        }

        protected void Guardar_Click(object sender, EventArgs e)
        {
            try
            {
                //Para cada item, busco los valores modificados por el usuario y los guardo en los items
                foreach (var item in tabla.Items)
                {
                    medicoSeleccionado.TurnosDeTrabajo[posicionDeItemsRepeater].HorarioEntrada = ((DropDownList)tabla.Items[posicionDeItemsRepeater].FindControl("horarioEntrada")).SelectedValue;
                    medicoSeleccionado.TurnosDeTrabajo[posicionDeItemsRepeater].HorarioSalida = ((DropDownList)tabla.Items[posicionDeItemsRepeater].FindControl("horarioSalida")).SelectedValue;
                    medicoSeleccionado.TurnosDeTrabajo[posicionDeItemsRepeater].DiaLibre = ((CheckBox)tabla.Items[posicionDeItemsRepeater].FindControl("esDiaLibre")).Checked;
                    posicionDeItemsRepeater++;
                }
                
                //Dependiendo el resultado que obtenga(bool) al guardar sus respectivos tunos de trabajo, vamos a DetalleMedico para mostrar su respectivo mensaje
                //Si modifica realiza un UPDATE, si los carga por primera vez, realiza un INSERT. Obtenemos verdadero o falso al buscar en la base
                //Si es que existen registros para ese medico. 
                if(turnoTrabajoNegocio.existenTurnosDeMedico(medicoSeleccionado.Id)) Response.Redirect("DetalleMedico?id=" + medicoSeleccionado.Id + "&succesTt=" + turnoTrabajoNegocio.modificarTurnosDeTrabajo(medicoSeleccionado), false);
                else Response.Redirect("DetalleMedico?id=" + medicoSeleccionado.Id + "&succesTt=" + turnoTrabajoNegocio.registrarTurnosDeTrabajo(medicoSeleccionado), false);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        protected void Cancelar_Click(object sender, EventArgs e)
        {
            Guardar.Visible = false;
            Cancelar.Visible = false;
            Modificar.Enabled = true;
            Volver.Visible = true;
            habilitarModificación(false);

        }

        private void habilitarModificación(bool habilita = true)
        {
            //Realizo un foraeach para recorrer el array de items cargados en el repeater, osea los turnos de trabajo del medico
            //Para acceder a los diferentes textbox o checkbox(controles), al estar relacionado a cada item dentro del repeater, necesitamos la posición del mismo y castearlo
            //Por el momento es la unica forma que encontre para poder recorrerla y manejarlo a gusto para las funcionalidades
            //Valor por defecto es true, en caso de desahbilitar, le pasamos false en la llamada. 
            foreach (var item in tabla.Items)
            {
                ((DropDownList)tabla.Items[posicionDeItemsRepeater].FindControl("horarioEntrada")).Enabled = habilita;
                ((DropDownList)tabla.Items[posicionDeItemsRepeater].FindControl("horarioSalida")).Enabled = habilita;
                ((CheckBox)tabla.Items[posicionDeItemsRepeater].FindControl("esDiaLibre")).Enabled = habilita;
                cargarHorariosDisponibles(posicionDeItemsRepeater);
                posicionDeItemsRepeater++;
            }
        }

        private void cargarHorariosDisponibles(int pos)
        {
            ((DropDownList)tabla.Items[pos].FindControl("horarioEntrada")).DataSource = turnoTrabajoNegocio.horariosDisponibles();
            ((DropDownList)tabla.Items[pos].FindControl("horarioEntrada")).DataBind();
            ((DropDownList)tabla.Items[pos].FindControl("horarioEntrada")).SelectedValue = medicoSeleccionado.TurnosDeTrabajo[pos].HorarioEntrada;
            ((DropDownList)tabla.Items[pos].FindControl("horarioSalida")).DataSource = turnoTrabajoNegocio.horariosDisponibles();
            ((DropDownList)tabla.Items[pos].FindControl("horarioSalida")).DataBind();
            ((DropDownList)tabla.Items[pos].FindControl("horarioSalida")).SelectedValue = medicoSeleccionado.TurnosDeTrabajo[pos].HorarioSalida;
        }

        protected void Volver_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("DetalleMedico?id=" + medicoSeleccionado.Id);
            }
            catch
            {

            }
        }
    }
}