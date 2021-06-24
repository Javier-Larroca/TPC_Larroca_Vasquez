using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Especialidad
    {
        public String Descripcion { get; set; }
        public int Id { get; set; }

        public Especialidad(string descripcion)
        {
            Descripcion = descripcion;
        }
    }

}
