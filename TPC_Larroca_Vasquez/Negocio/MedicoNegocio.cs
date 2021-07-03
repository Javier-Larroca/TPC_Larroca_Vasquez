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
        public List<Medico> listarMedicos()
        {
            List<Medico> listaDeMedicos = new List<Medico>();
            try
            {
                conexion.setearConsulta("SELECT ID, NOMBRE, APELLIDO, CONTACTO, MATRICULA, FECHA_ALTA FROM vDetallesPorMedico");
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

                    listaDeMedicos.Add(backup);
                }
                conexion.cerrarConexion();

                foreach (Medico medico in listaDeMedicos){
                    medico.Especialidades = especialidadPorMedico(medico.Id);
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

        public List<Especialidad> especialidadPorMedico(int id)
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
                conexion.setearConsulta(string.Format("SELECT DESCRIPCION FROM vEspecialidadesPorMedico WHERE IDMEDICO = {0}", id));
                conexion.ejecutarConsultaLectura();

                while (conexion.Lector.Read())
                {
                    Especialidad backup = new Especialidad((String)conexion.Lector["DESCRIPCION"]);
                    EspecialidadesMedico.Add(backup);
                }

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

        public bool agregarMedico(Medico medico)
        {

            try
            {
                conexion.setearProcedimientoAlmacenado("pAltaDeMedico");
                conexion.agregarParametro("@mnombre", medico.Nombre);
                conexion.agregarParametro("@mApellido", medico.Apellido);
                conexion.agregarParametro("@mMail", medico.Contacto);
                conexion.agregarParametro("@mMatricula", medico.Matricula);
                conexion.ejecutarProcedimientoAlmacenado();
                conexion.limpiarParametros();
                conexion.cerrarConexion();

                if (medico.Especialidades != null)
                {
                   foreach (Especialidad especialidad in medico.Especialidades)
                    {
                        conexion.setearConsulta("INSERT INTO ESPECIALIDADES_POR_MEDICO VALUES (@idEspecialidad, @idMedico)");
                        conexion.agregarParametro("@idEspecialidad", especialidad.Id);
                        conexion.agregarParametro("@idMedico", medico.Id);
                        conexion.limpiarParametros();
                        //Agregar en caso de que falle en el for each, un DELETE que borre las especialidades para esa ID
                        //Debería ir en un metodo aparte para modificación.
                        conexion.ejecutarAccion();
                        conexion.cerrarConexion();
                    }
                }
                //Metodo para validar la cantidad de filas afectadas
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
    }
}
