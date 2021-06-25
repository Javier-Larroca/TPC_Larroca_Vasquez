using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Turno
    {
        public int NumeroDeTurno{ get; set; }
        public int Estado { get; set; }
        public DateTime FechaTurno{ get; set; }
        public String Observaciones{ get; set; }

    }
}
