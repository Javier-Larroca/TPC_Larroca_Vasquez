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
                conexion.setearQuery("SELECT ID, NOMBRE, APELLIDO, CONTACTO, MATRICULA, FECHA_ALTA FROM MEDICOS");
                conexion.ejecutarQueryLectura();

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
                    //backup.Especialidades = especialidadPorMedico(backup.Id);

                    listaDeMedicos.Add(backup);
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
                conexion.setearQuery("EXECUTE pEspecialidadesPorMedico " + id);
                conexion.ejecutarQueryLectura();

                while (conexion.Lector.Read())
                {
                    Especialidad backup = new Especialidad((String)conexion.Lector["ESPECIALIDAD"]);
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
    }
}
