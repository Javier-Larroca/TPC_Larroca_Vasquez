using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Negocio
{
    public class UsuarioNegocio
    {
        private AccesoDatos conexion = new AccesoDatos();

        public void listarUsuarios()
        {
            
        }

        public Usuario existeUsuario(string mail)
        {
            try
            {
                conexion.setearConsulta("SELECT ad.IDUSUARIO, ad.EMAIL, ad.FECHA_ALTA, tip.DESCRIPCION FROM ADM_USUARIOS ad " +
                                        "INNER JOIN TIPO_USUARIOS tip ON tip.ID = ad.TIPO_USUARIO " +
                                       "WHERE EMAIL = @email AND ESTADO = 1");
                conexion.agregarParametro("@email", mail);
                conexion.ejecutarConsultaLectura();

                Usuario nuevo = new Usuario();

                if (conexion.Lector.Read())
                {
                    nuevo.Id = (int)conexion.Lector["IDUSUARIO"];
                    nuevo.Mail = (string)conexion.Lector["EMAIL"];
                    nuevo.Alta = (DateTime)conexion.Lector["FECHA_ALTA"];
                    nuevo.TipoUsuario = (string)conexion.Lector["DESCRIPCION"];
                }
                return nuevo;
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
