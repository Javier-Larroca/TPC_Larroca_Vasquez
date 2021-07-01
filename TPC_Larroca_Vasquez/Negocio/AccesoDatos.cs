using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Negocio
{
    class AccesoDatos
    {
        private SqlConnection conexion;
        private SqlCommand comando;
        private SqlDataReader lector;

        public AccesoDatos()
        {
            conexion = new SqlConnection("data source=.\\SQLEXPRESS; initial catalog=LAVE; integrated security=sspi");
            comando = new SqlCommand();
        }

        //Seteamos commandType y text para ejecutar consultas o vistas
        public void setearConsulta(string query)
        {
            comando.CommandType = System.Data.CommandType.Text;
            comando.CommandText = query;
        }

        
        public void setearProcedimientoAlmacenado(string procedimiento)
        {
            comando.CommandType = System.Data.CommandType.StoredProcedure;
            comando.CommandText = procedimiento;
        }

        public void agregarParametro(string nombre, object valor)
        {
            comando.Parameters.AddWithValue(nombre, valor);
        }

        public void limpiarParametros()
        {
            comando.Parameters.Clear();
        }

        //Ejecutamos consultas a tablas o vistas
        public void ejecutarConsultaLectura()
        {
            comando.Connection = conexion;
            conexion.Open();
            lector = comando.ExecuteReader();
        }

        public void cerrarConexion()
        {
            if (lector != null)
                lector.Close();
            conexion.Close();
        }

        public SqlDataReader Lector
        {
            get { return lector; }
        }

        internal void ejecutarAccion()
        {
            comando.Connection = conexion;
            if (conexion.State == System.Data.ConnectionState.Closed) conexion.Open();
            comando.ExecuteNonQuery();
        }

        internal void ejecutarProcedimientoAlmacenado(bool esLectura = false)
        {
            comando.Connection = conexion;
            if (conexion.State == System.Data.ConnectionState.Closed) conexion.Open();

            //Si es solamente de lectura, iniciamos  lector para poder leer datos. 
            //IMPORTANTE: Si usamos este procedimiento en un bucle, es necesario al terminar de leer ejecutar limpiarParametros();
            if(!esLectura) comando.ExecuteNonQuery();
            else
            {
                lector = comando.ExecuteReader();
            }
        }


    }
}
