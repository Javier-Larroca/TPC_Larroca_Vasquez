using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Negocio
{
    public class TurnoDeTrabajoNegocio
    {
        private AccesoDatos conexion = new AccesoDatos();
        //Asignación automatica para generar una lista de Turnos de trabajo con Dias semanales
        public List<TurnoDeTrabajo> asignarDiasSemanales()
        {
            List<TurnoDeTrabajo> turnosConDias = new List<TurnoDeTrabajo> {
                new TurnoDeTrabajo("Lunes"),
                new TurnoDeTrabajo("Martes"),
                new TurnoDeTrabajo("Miércoles"),
                new TurnoDeTrabajo("Jueves"),
                new TurnoDeTrabajo("Viernes"),
                new TurnoDeTrabajo("Sábado"),
                new TurnoDeTrabajo("Domingo")
            };
            return turnosConDias;
        }
        public List<string> horariosDisponibles()
        {
            List<string> horarios = new List<string>();
            for (int x = 0; x <= 24; x++) 
            {
                if (x < 10) horarios.Add(string.Format("0{0}:00", x));
                else horarios.Add(string.Format("{0}:00",x));
            }
            return horarios;
        }
        //Sin implementar por el momento
        public void asignarHorarios(string[] hEntrada, string[] hSalida, ref List<TurnoDeTrabajo> turnos)
        {
            for (int x = 0; x < 7; x++)
            {
                turnos[x].HorarioEntrada = hEntrada[x];
                turnos[x].HorarioSalida = hSalida[x];
            }
        }

        public bool registrarTurnosDeTrabajo(Medico medico)
        {
            try
            {
                foreach (var turnoDeTrabajo in medico.TurnosDeTrabajo)
                {
                    conexion.setearConsulta("INSERT INTO TURNOS_DE_TRABAJO VALUES (@idMedico, @dia,@hIngreso, @hSalida,@dLibre)");
                    conexion.agregarParametro("@idMedico", medico.Id);
                    conexion.agregarParametro("@dia", turnoDeTrabajo.Dia.ToUpper());
                    conexion.agregarParametro("@hIngreso", turnoDeTrabajo.HorarioEntrada);
                    conexion.agregarParametro("@hSalida", turnoDeTrabajo.HorarioSalida);
                    conexion.agregarParametro("@dLibre",turnoDeTrabajo.DiaLibre);
                    conexion.ejecutarAccion();
                    conexion.limpiarParametros();
                }
                return true;
            }
            catch(Exception ex)
            {
                return false;
                throw ex;
            }
            finally
            {
                conexion.cerrarConexion();
            }
        }

        public bool modificarTurnosDeTrabajo(Medico medico)
        {
            try
            {
                foreach (var turnoDeTrabajo in medico.TurnosDeTrabajo)
                {
                    conexion.setearProcedimientoAlmacenado("pModificarTurnosDeTrabajo");
                    conexion.agregarParametro("@idMedico", medico.Id);
                    conexion.agregarParametro("@dia", turnoDeTrabajo.Dia.ToUpper());
                    conexion.agregarParametro("@hIngreso", turnoDeTrabajo.HorarioEntrada);
                    conexion.agregarParametro("@hSalida", turnoDeTrabajo.HorarioSalida);
                    conexion.agregarParametro("@diaLibre", turnoDeTrabajo.DiaLibre);
                    conexion.ejecutarProcedimientoAlmacenado();
                    conexion.limpiarParametros();
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw ex;
            }
            finally
            {
                conexion.cerrarConexion();
            }

        }

        public List<TurnoDeTrabajo> obtenerTurnosDeTrabajo(int id)
        {
            try
            {
                //Usamos una lista de backup donde asignamos dias semanales
                List<TurnoDeTrabajo> backup = asignarDiasSemanales();
                conexion.setearConsulta(string.Format("SELECT DIA, HORARIO_INGRESO, HORARIO_SALIDA, DIA_LIBRE FROM TURNOS_DE_TRABAJO WHERE IDMEDICO = {0}", id));
                conexion.ejecutarConsultaLectura();
                
                //Mientras encontramos resultados, vamos a buscar por dia de turno, los días que tenemos registrados en la base
                //Si encoontramos coincidencia en la primer lectura, entre día de la base y día del turno de trabajo, asignamos valores
                //Uso esta forma porque no lo pude ordenar de lunes a domingo en SQL
                while (conexion.Lector.Read())
                {
                   int pos = backup.FindIndex(Busqueda => Busqueda.Dia.ToUpper() == (String)conexion.Lector["DIA"]);
                    if(pos != -1)
                    {
                        backup[pos].HorarioEntrada = (String)conexion.Lector["HORARIO_INGRESO"];
                        backup[pos].HorarioSalida = (String)conexion.Lector["HORARIO_SALIDA"];
                        backup[pos].DiaLibre = (bool)conexion.Lector["DIA_LIBRE"];
                    }
                }
                return backup;
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

        public bool existenTurnosDeMedico(int id)
        {
            try
            {
                conexion.setearConsulta("SELECT COUNT(*) AS CANTIDAD FROM TURNOS_DE_TRABAJO WHERE IDMEDICO = @id");
                conexion.agregarParametro("@id", id);
                conexion.ejecutarConsultaLectura();
                conexion.limpiarParametros();

                conexion.Lector.Read();

                if ((int)conexion.Lector["CANTIDAD"] ==  0) return false;
                else return true;
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
