using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class ObraSocial
    {
        public String Descripcion { get; set; }
        public int Id { get; set; }

        public ObraSocial()
        {

        }

        public ObraSocial(string descripcion)
        {
            Descripcion = descripcion;
        }
    }
}
