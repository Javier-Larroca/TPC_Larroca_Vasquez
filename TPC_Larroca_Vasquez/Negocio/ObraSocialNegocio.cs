using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Negocio
{
    public class ObraSocialNegocio
    {
        private AccesoDatos conexion = new AccesoDatos();

        public List<ObraSocial> listaDeObrasSociales()
        {
            List<ObraSocial> listaDeObrasSociales = new List<ObraSocial>();

            try
            {
                conexion.setearConsulta("Select ID, DESCRIPCION from OBRAS_SOCIALES order by DESCRIPCION");
                conexion.ejecutarConsultaLectura();

                while (conexion.Lector.Read())
                {
                    ObraSocial obraSocial = new ObraSocial((string)conexion.Lector["DESCRIPCION"], (int)conexion.Lector["ID"]);
                    listaDeObrasSociales.Add(obraSocial);
                }

                return listaDeObrasSociales;
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
