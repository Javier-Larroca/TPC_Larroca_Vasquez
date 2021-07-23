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
        //EmailService emailService = new EmailService();

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
                    backup.ObraSocial.Id = (int)conexion.Lector["IDOBRASOCIAL"];
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

        public bool agregarPaciente(Paciente paciente)
        {

            try
            {    
                
                conexion.setearConsulta("INSERT INTO PACIENTES (IDOBRASOCIAL, NOMBRE, APELLIDO,CONTACTO,FECHA_NAC, FECHA_ALTA, ESTADO) VALUES (@idOS, @nombre, @apellido, @mail, @fechaNac, @fechaAlta, 1)");
                conexion.agregarParametro("@nombre", paciente.Nombre);
                conexion.agregarParametro("@apellido", paciente.Apellido);
                conexion.agregarParametro("@mail", paciente.Mail);
                conexion.agregarParametro("@fechaAlta", DateTime.Now);
                conexion.agregarParametro("@fechaNac", paciente.FechaNac);
                conexion.agregarParametro("@idOS", paciente.ObraSocial.Id);
                conexion.ejecutarAccion();
                conexion.limpiarParametros();
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

        public bool modificarPaciente(Paciente paciente)
        {
            try
            {
                conexion.setearConsulta("UPDATE PACIENTES SET IDOBRASOCIAL = @idOS, NOMBRE = @nombre, APELLIDO = @apellido, FECHA_NAC = @fechaNac WHERE CONTACTO = @mail");
                conexion.agregarParametro("@nombre", paciente.Nombre);
                conexion.agregarParametro("@apellido", paciente.Apellido);
                conexion.agregarParametro("@mail", paciente.Mail);
                conexion.agregarParametro("@fechaNac", paciente.FechaNac);
                conexion.agregarParametro("@idOS", paciente.ObraSocial.Id);
                conexion.ejecutarAccion();
                conexion.limpiarParametros();
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

        public bool eliminarPaciente(string email)
        {
            try
            {
                conexion.setearConsulta("UPDATE PACIENTES SET ESTADO = 0 WHERE CONTACTO = @mail");

                conexion.agregarParametro("@mail", email);

                conexion.ejecutarAccion();
                conexion.limpiarParametros();
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
    }
}
