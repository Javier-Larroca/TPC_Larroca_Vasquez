using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Negocio
{
    public class MedicoNegocio
    {
        private AccesoDatos conexion = new AccesoDatos();
        private EspecialidadNegocio especialidades = new EspecialidadNegocio();
        private TurnoDeTrabajoNegocio turnosDeTrabajo = new TurnoDeTrabajoNegocio();
        public List<Medico> listarMedicos()
        {
            List<Medico> listaDeMedicos = new List<Medico>();
            try
            {
                conexion.setearConsulta("SELECT ID, NOMBRE, APELLIDO, CONTACTO, MATRICULA, FECHA_ALTA, FECHA_MODIFICACION FROM vDetallesPorMedico");
                conexion.ejecutarConsultaLectura();

                while (conexion.Lector.Read())
                {
                    Medico backup = new Medico();

                    //Cargamos objeto utilizando Medico backup
                    backup.Id = (int)conexion.Lector["ID"];
                    backup.Nombre = (String)conexion.Lector["NOMBRE"];
                    backup.Apellido = (String)conexion.Lector["APELLIDO"];
                    backup.Mail = (String)conexion.Lector["CONTACTO"];
                    backup.Matricula = (int)conexion.Lector["MATRICULA"];
                    backup.Alta = (DateTime)conexion.Lector["FECHA_ALTA"];
                    //If de una linea, casteo a DateTime? para indicar que puede ser nulo, y despues voy a la columna 6
                    //Osea FECHA_MODIFICACIOn y si es nulo (IsDBNUll) le asigno nulo, caso contrario traigo la fecha
                    backup.Modificacion = (DateTime?)(conexion.Lector.IsDBNull(6) ? null : conexion.Lector["FECHA_MODIFICACION"]);

                    listaDeMedicos.Add(backup);
                }

                //Ciclo foreach para que,a cada medico le cargamos sus respectivas especialidades y turnos de trabajo
                foreach (Medico medico in listaDeMedicos){
                    medico.Especialidades = especialidades.especialidadPorMedico(medico.Id);
                    medico.TurnosDeTrabajo = turnosDeTrabajo.obtenerTurnosDeTrabajo(medico.Id);
                }

                return listaDeMedicos;
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

        public bool agregarMedico(Medico medico)
        {

            try
            {
                conexion.setearProcedimientoAlmacenado("pAltaDeMedico");
                conexion.agregarParametro("@mnombre", medico.Nombre);
                conexion.agregarParametro("@mApellido", medico.Apellido);
                conexion.agregarParametro("@mMail", medico.Mail);
                conexion.agregarParametro("@mMatricula", medico.Matricula);
                conexion.ejecutarProcedimientoAlmacenado();
                conexion.limpiarParametros();
                conexion.cerrarConexion();
                
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

        //Metodo para buscar medico por Matricula
        public int buscarMedico(int numMatricula)
        {
            int busqueda;
            try
            {
                    conexion.setearConsulta(string.Format("SELECT ID FROM MEDICOS WHERE MATRICULA = {0}", numMatricula));
                    conexion.ejecutarConsultaLectura();
                    conexion.Lector.Read();
                    busqueda = (int)conexion.Lector["ID"];

                return busqueda;
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

        public bool bajaDeMedico(int idMedico)
        {
            try
            {
                conexion.setearProcedimientoAlmacenado("pBajaDeMedico");
                conexion.agregarParametro("@idMedico", idMedico);
                conexion.ejecutarProcedimientoAlmacenado();
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
        public bool modificarMedico(Medico medicoAModificar)
        {
            try
            {
                conexion.setearProcedimientoAlmacenado("pModificacionDeMedico");
                conexion.agregarParametro("@mId", medicoAModificar.Id);
                conexion.agregarParametro("@mNombre", medicoAModificar.Nombre);
                conexion.agregarParametro("@mApellido", medicoAModificar.Apellido);
                conexion.agregarParametro("@mMail", medicoAModificar.Mail);
                conexion.agregarParametro("@mMatricula", medicoAModificar.Matricula);
                conexion.ejecutarProcedimientoAlmacenado();
                conexion.limpiarParametros();

                return true;
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
