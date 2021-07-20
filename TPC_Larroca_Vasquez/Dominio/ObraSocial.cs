using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class ObraSocial
    {
        public string Descripcion { get; set; }
        public int Id { get; set; }

        public ObraSocial()
        {

        }

        public ObraSocial(string descripcion)
        {
            Descripcion = descripcion;
        }

        public ObraSocial(string descripcion, int id = 0)
        {
            Descripcion = descripcion;
            Id = id;
        }

        public override string ToString()
        {
            return Descripcion;
        }
    }
}
