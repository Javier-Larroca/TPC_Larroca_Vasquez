using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Paciente : Usuario
    {
        public List<ObraSocial> ObraSocial { get; set; }

        public DateTime FechaNac { get; set; }

        public Paciente()
        {
            Id = 0;
        }
    }

}
