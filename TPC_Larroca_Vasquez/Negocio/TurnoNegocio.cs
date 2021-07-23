using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Negocio
{
    public class TurnoNegocio
    {
        private AccesoDatos conexion = new AccesoDatos();

        public List<string> horariosDisponibles()
        {
            List<string> horarios = new List<string>();
            for (int x = 0; x <= 23; x++)
            {
                if (x < 10) horarios.Add(string.Format("0{0}:00", x));
                else horarios.Add(string.Format("{0}:00", x));
            }
            return horarios;
        }

        public List<string> horariosFiltrados(string entrada, string salida)
        {
            List<string> horarios = new List<String>();

            horarios.Add("- Seleccione un horario -");
            int hEntrada = int.Parse(entrada.Split(':')[0]);
            int hSalida = int.Parse(salida.Split(':')[0]);

            for (int x = hEntrada; x < hSalida; x++)
            {
                if (x < 10) horarios.Add(string.Format("0{0}:00", x));
                else horarios.Add(string.Format("{0}:00",x));
            }
            return horarios;
        }

        public bool altaDeTurno(Turno turno)
        {
            try
            {
                conexion.setearProcedimientoAlmacenado("pAltaDeTurno");
                conexion.agregarParametro("@idPaciente", turno.IdPaciente);
                conexion.agregarParametro("@idMedico", turno.IdMedico);
                conexion.agregarParametro("@fecha", turno.FechaTurno);
                conexion.agregarParametro("@horario", turno.Horario);
                conexion.ejecutarProcedimientoAlmacenado();
                return true;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conexion.cerrarConexion();
            }
        }

        public List<Turno> listaDeTurnos()
        {
            try
            {
                List<Turno> turnos = new List<Turno>();
                conexion.setearConsulta("SELECT * FROM turnosCompletos");
                conexion.ejecutarConsultaLectura();

                while (conexion.Lector.Read())
                {
                    Turno backup = new Turno();

                    backup.NumeroDeTurno = (int)conexion.Lector["ID"];
                    backup.IdPaciente = (int)conexion.Lector["IDPACIENTE"];
                    backup.IdMedico = (int)conexion.Lector["IDMEDICO"];
                    backup.FechaTurno = (DateTime)conexion.Lector["FECHA_TURNO"];
                    backup.Horario = (string)conexion.Lector["HORARIO"];
                    backup.Observaciones = (string)(conexion.Lector["OBSERVACIONES"] == DBNull.Value ? "Sin Observaciones" : conexion.Lector["OBSERVACIONES"]);
                    backup.Estado = (bool)conexion.Lector["ESTADO"];
                    backup.NombrePaciente = (string)conexion.Lector["PACIENTE"];
                    backup.NombreMedico = (string)conexion.Lector["MEDICO"];

                    if (System.DateTime.Now > backup.FechaTurno) backup.Estado = false;

                    turnos.Add(backup);
                }

                return turnos;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                conexion.cerrarConexion();
            }
        }

        public List<string> horariosOcupados(int id, DateTime fecha)
        {
            try
            {
                List<string> horariosOcupados = new List<string>();
                conexion.setearConsulta("SELECT HORARIO FROM turnosCompletos WHERE IDMEDICO = @idMedico AND FECHA_TURNO = @fecha");
                conexion.agregarParametro("@idMedico", id);
                conexion.agregarParametro("@fecha", fecha);
                conexion.ejecutarConsultaLectura();

                while (conexion.Lector.Read())
                {
                    string horario;
                    horario = (string)conexion.Lector["HORARIO"];

                    horariosOcupados.Add(horario);
                }
                return horariosOcupados;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                conexion.cerrarConexion();
            }
        }

        public void guardarEstado(int id)
        {
            try
            {
                conexion.setearConsulta("UPDATE TURNOS SET ESTADO = 0 WHERE ID = @Id");
                conexion.agregarParametro("@Id", id);
                conexion.ejecutarConsultaLectura();
                conexion.limpiarParametros();
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                conexion.cerrarConexion();
            }
        }

        public List<Turno> listaDeTurnosPorMedico(int idMedico)
        {
            try
            {
                List<Turno> turnosPorMedico = new List<Turno>();
                conexion.setearConsulta("SELECT * FROM turnosCompletos WHERE IDMEDICO= @idMedico");
                conexion.agregarParametro("@idMedico", idMedico);
                conexion.ejecutarConsultaLectura();
                conexion.limpiarParametros();

                while (conexion.Lector.Read())
                {
                    Turno backup = new Turno();
                    backup.NumeroDeTurno = (int)conexion.Lector["ID"];
                    backup.IdPaciente = (int)conexion.Lector["IDPACIENTE"];
                    backup.IdMedico = (int)conexion.Lector["IDMEDICO"];
                    backup.FechaTurno = (DateTime)conexion.Lector["FECHA_TURNO"];
                    backup.Horario = (string)conexion.Lector["HORARIO"];
                    backup.Observaciones = (string)(conexion.Lector["OBSERVACIONES"] == DBNull.Value ? "Sin Observaciones" : conexion.Lector["OBSERVACIONES"]);
                    backup.Estado = (bool)conexion.Lector["ESTADO"];
                    backup.NombrePaciente = (string)conexion.Lector["PACIENTE"];
                    backup.NombreMedico = (string)conexion.Lector["MEDICO"];

                    if (System.DateTime.Now > backup.FechaTurno) backup.Estado = false;

                    turnosPorMedico.Add(backup);
                }
                return turnosPorMedico;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                conexion.cerrarConexion();
            }
        }

        public void guardarObservacion(string observacion, int id)
        {
            try
            {
                conexion.setearConsulta("UPDATE TURNOS SET OBSERVACIONES = @observacion WHERE ID = @id");
                conexion.agregarParametro("@observacion", observacion);
                conexion.agregarParametro("@id", id);
                conexion.ejecutarAccion();
                conexion.limpiarParametros();
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                conexion.cerrarConexion();
            }
        }

    }
}
