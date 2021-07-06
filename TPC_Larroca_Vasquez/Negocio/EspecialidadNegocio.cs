using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Negocio
{
    public class EspecialidadNegocio
    {
        private AccesoDatos conexion = new AccesoDatos();

        public List<Especialidad> listaDeEspecialidades()
        {
            List<Especialidad> listaDeEspecialidades = new List<Especialidad>();

            try
            {
                conexion.setearConsulta("SELECT ID, DESCRIPCION FROM ESPECIALIDADES");
                conexion.ejecutarConsultaLectura();

                while (conexion.Lector.Read())
                {
                    Especialidad especialidad = new Especialidad((String)conexion.Lector["DESCRIPCION"], (int)conexion.Lector["ID"]);
                    listaDeEspecialidades.Add(especialidad);
                }

                return listaDeEspecialidades;
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

        public List<Especialidad> especialidadPorMedico(int idMedico)
        {
            List<Especialidad> EspecialidadesMedico = new List<Especialidad>();
            try
            {
                //Agrego esta forma de ejecutar un procedimiento almacenado que nos devuelve
                //datos, en caso de usarlo en algún bucle usar en Finally conexion.limpiarParametros() ya que acumula.

                //conexion.setearProcedimientoAlmacenado("pEspecialidadesPorMedico");
                //conexion.agregarParametro("@idMedico", id);
                //conexion.ejecutarProcedimientoAlmacenado(true);

                //Una forma de setear una consulta rapida, aca consulto a la vista creada.
                conexion.setearConsulta(string.Format("SELECT DESCRIPCION FROM vEspecialidadesPorMedico WHERE IDMEDICO = {0}", idMedico));
                conexion.ejecutarConsultaLectura();

                while (conexion.Lector.Read())
                {
                    Especialidad backup = new Especialidad((String)conexion.Lector["DESCRIPCION"]);
                    EspecialidadesMedico.Add(backup);
                }

                conexion.cerrarConexion();
                return EspecialidadesMedico;
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

        public bool altaDeEspecialidadPorMedico(Medico medico)
        {
            try
            {
                foreach (Especialidad especialidad in medico.Especialidades)
                {
                    conexion.setearConsulta("INSERT INTO ESPECIALIDADES_POR_MEDICOS VALUES (@idEspecialidad, @idMedico)");
                    conexion.agregarParametro("@idEspecialidad", especialidad.Id);
                    conexion.agregarParametro("@idMedico", medico.Id);
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

    }
}
