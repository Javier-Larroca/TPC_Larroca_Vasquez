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
                conexion.setearConsulta("SELECT P.ID, P.NOMBRE, P.APELLIDO, P.CONTACTO, P.FECHA_NAC, P.FECHA_ALTA, P.IDOBRASOCIAL, O.DESCRIPCION  FROM PACIENTES as P INNER JOIN OBRAS_SOCIALES as O ON P.IDOBRASOCIAL = O.ID");
                conexion.ejecutarConsultaLectura();

                while (conexion.Lector.Read())
                {
                    Paciente backup = new Paciente();

                    //Cargamos objeto utilizando Medico backup
                    backup.Id = (int)conexion.Lector["ID"];
                    backup.Nombre = (string)conexion.Lector["NOMBRE"];
                    backup.Apellido = (string)conexion.Lector["APELLIDO"];
                    backup.Mail = (string)conexion.Lector["CONTACTO"];
                    backup.FechaNac = (DateTime)conexion.Lector["FECHA_NAC"];
                    backup.Alta = (DateTime)conexion.Lector["FECHA_ALTA"];
                    backup.ObraSocial.Descripcion = (string)conexion.Lector["DESCRIPCION"];

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
