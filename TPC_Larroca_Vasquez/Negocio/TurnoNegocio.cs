using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Negocio
{
    public class TurnoNegocio
    {
        private AccesoDatos conexion = new AccesoDatos();

        public List<string> horariosDisponibles()
        {
            List<string> horarios = new List<string>();
            for (int x = 0; x <= 23; x++)
            {
                if (x < 10) horarios.Add(string.Format("0{0}:00", x));
                else horarios.Add(string.Format("{0}:00", x));
            }
            return horarios;
        }

    }
}
