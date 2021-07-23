using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using Negocio;

namespace TPC_Larroca_Vasquez.Turnos
{
    public partial class ReservarTurno : System.Web.UI.Page
    {
        private EspecialidadNegocio especialidadesNegocio = new EspecialidadNegocio();
        private PacienteNegocio pacienteNegocio = new PacienteNegocio();
        private MedicoNegocio medicoNegocio = new MedicoNegocio();
        private TurnoNegocio turnoNegocio = new TurnoNegocio();
        private Paciente paciente = new Paciente();
        private Turno turnoAReservar = new Turno();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    cargarDataSources();   
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }

        }

        private void cargarDataSources()
        {
            Especialidades.AppendDataBoundItems = true;
            Especialidades.Items.Add("- Seleccione una especialidad -");
            Especialidades.DataSource = especialidadesNegocio.listaDeEspecialidades();
            Especialidades.DataBind();
            ocultarRequerimientosTurno();
        }

        private void ocultarRequerimientosTurno()
        {
            Pacientes.Visible = false;
            seleccionarPaciente.Visible = false;
            MedicosDisponibles.Visible = false;
            HorariosDisponibles.Visible = false;
            Especialidades.Visible = false;
            Calendario.Visible = false;
        }

        private void habilitarRequerimientosTurno()
        {
            Especialidades.Visible = true;
            Calendario.Visible = true;
        }

        protected void seleccionarPaciente_Click(object sender, EventArgs e)
        {
            try
            {
                if(Pacientes.SelectedValue != "")
                {
                    PacienteSeleccionado.Text = "Paciente: " + Pacientes.SelectedValue;
                    Pacientes.Visible = false;
                    nombrePaciente.Visible = false;
                    Buscar.Visible = false;
                    seleccionarPaciente.Visible = false;
                    habilitarRequerimientosTurno();
                }
            }
            catch(Exception)
            {

            }

        }

        protected void Buscar_Click(object sender, EventArgs e)
        {
            try
            {
                List<Paciente> pacientesAll = pacienteNegocio.listarPaciente();
                List<Paciente> pacientesFiltrados = new List<Paciente>();
                pacientesFiltrados = pacientesAll.FindAll(Busqueda => Busqueda.Nombre.ToUpper().Contains(nombrePaciente.Text.ToUpper()) ||
                Busqueda.Apellido.ToUpper().Contains(nombrePaciente.Text.ToUpper()));
                if (pacientesFiltrados.Count > 0)
                {
                    Pacientes.DataSource = pacientesFiltrados;
                    Pacientes.DataBind();
                    Pacientes.Visible = true;
                    seleccionarPaciente.Visible = true;
                }
                else throw new Exception();
            
            }
            catch(Exception )
            {

            }
            
        }

        protected void Especialidades_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Especialidades.SelectedIndex != 0 && Calendario.SelectedDate == null)
                {

                    List<Medico> medicosAll = medicoNegocio.listarMedicos();
                    List<Medico> medicosFiltrado = new List<Medico>();

                    //Buscamos en la lista de medicos, si existe la especialidad que se selecciono.
                    //En caso de que exista, devuelve un true y agregamos al Medico a la lista filtrada
                    medicosFiltrado = medicosAll.FindAll(Busqueda =>
                    Busqueda.Especialidades.Exists(Especialidad =>
                    Especialidad.Descripcion.ToUpper() == Especialidades.SelectedValue.ToUpper()));
                    if (medicosFiltrado.Count > 0)
                    {
                        MedicosDisponibles.DataSource = medicosFiltrado;
                        MedicosDisponibles.DataBind();
                        MedicosDisponibles.Visible = true;
                    }
                    else MedicosDisponibles.Visible = false;
                }
            }
            catch
            {

            }
        }

        protected void Calendario_SelectionChanged(object sender, EventArgs e)
        {
            
            if(Especialidades.SelectedIndex != 0)
            {
                var culture = new System.Globalization.CultureInfo("es-ES");
                string diaSeleccionado = culture.DateTimeFormat.GetDayName(Calendario.SelectedDate.DayOfWeek).ToUpper();
                List<Especialidad> especialidades = especialidadesNegocio.listaDeEspecialidades();
                Especialidad especialidadSeleccionada = especialidades.Find(Busqueda => Busqueda.Descripcion.ToUpper() == Especialidades.SelectedValue.ToUpper());
                List<Medico> medicosDisponibles = medicoNegocio.medicosDisponibles(diaSeleccionado, especialidadSeleccionada.Id);


                if (medicosDisponibles.Count > 0)
                {
                    MedicosDisponibles.DataSource = medicosDisponibles;
                    MedicosDisponibles.DataBind();
                    MedicosDisponibles.Visible = true;
                    asignarHorariosDisponibles(medicosDisponibles);
                }
                else {
                    MedicosDisponibles.Visible = false;
                    HorariosDisponibles.Visible = false;
                }

            }

        }

        protected void HorariosDisponibles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (HorariosDisponibles.SelectedIndex != 0) reservacionTurno.Enabled = true;
            else reservacionTurno.Enabled = false;
        }

        private void asignarHorariosDisponibles(List<Medico> medicos)
        {
            try
            {

                Medico backup = medicos.Find(Busqueda => (Busqueda.Nombre + ' ' + Busqueda.Apellido).ToUpper() == MedicosDisponibles.SelectedValue.ToUpper());
                List<string> horariosCompletos = turnoNegocio.horariosFiltrados(backup.TurnosDeTrabajo[0].HorarioEntrada, backup.TurnosDeTrabajo[0].HorarioSalida);
                List<string> horariosNoDisponibles = turnoNegocio.horariosOcupados(backup.Id, Calendario.SelectedDate);

                foreach(string horario in horariosNoDisponibles)
                {
                    horariosCompletos.Remove(horario);
                }

                HorariosDisponibles.DataSource = horariosCompletos;
                HorariosDisponibles.DataBind();
                HorariosDisponibles.Visible = true;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        protected void reservacionTurno_Click(object sender, EventArgs e)
        {
            //Busco ID de medico asignado
            turnoAReservar.IdMedico = medicoNegocio.listarMedicos().Find(Busqueda => 
            (Busqueda.Nombre + ' ' + Busqueda.Apellido).ToUpper() == MedicosDisponibles.SelectedValue.ToUpper()).Id;
            turnoAReservar.NombreMedico = MedicosDisponibles.SelectedValue;
            //Busco ID de paciente
            turnoAReservar.IdPaciente = pacienteNegocio.listarPaciente().Find(Busqueda =>
            (Busqueda.Nombre + ' ' + Busqueda.Apellido).ToUpper() == Pacientes.SelectedValue.ToUpper()).Id;
            turnoAReservar.NombrePaciente = Pacientes.SelectedValue;
            //Asignamos fecha
            turnoAReservar.FechaTurno = Calendario.SelectedDate.Date;
            turnoAReservar.Horario = HorariosDisponibles.SelectedValue;
            turnoAReservar.Estado = true;


            if (turnoNegocio.altaDeTurno(turnoAReservar))
            {
                Session.Add("TurnoAlta", turnoAReservar);
                Response.Redirect("ListaDeTurnos?success=true", false);
            }
            else Response.Redirect("ListaDeTurnos?success=false", false);
        }

        protected void Volver_Click(object sender, EventArgs e)
        {
            Response.Redirect("ListaDeTurnos", false);
        }
    }
}