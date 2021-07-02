using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Negocio
{
    public class PacienteNegocio
    {
        private AccesoDatos conexion = new AccesoDatos();

        public List<Paciente> listarPaciente()
        {
            List<Paciente> listaDePacientes = new List<Paciente>();
            try
            {
                conexion.setearQuery("SELECT ID, NOMBRE, APELLIDO, CONTACTO, FECHA_NAC, FECHA_ALTA FROM PACIENTES");
                conexion.ejecutarQueryLectura();

                while (conexion.Lector.Read())
                {
                    Paciente backup = new Paciente();

                    //Cargamos objeto utilizando Medico backup
                    backup.Id = (int)conexion.Lector["ID"];
                    backup.Nombre = (String)conexion.Lector["NOMBRE"];
                    backup.Apellido = (String)conexion.Lector["APELLIDO"];
                    backup.Mail = (String)conexion.Lector["CONTACTO"];
                    backup.FechaNac = (DateTime)conexion.Lector["FECHA_NAC"];
                    backup.Alta = (DateTime)conexion.Lector["FECHA_ALTA"];
                    //backup.Especialidades = especialidadPorMedico(backup.Id);

                    listaDePacientes.Add(backup);
                }

                return listaDePacientes;
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
