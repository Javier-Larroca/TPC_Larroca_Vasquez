using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Dominio
{
    public class Paciente : Usuario
    {
        public ObraSocial ObraSocial { get; set; }

        public DateTime FechaNac { get; set; }

        public Paciente()
        {
            Id = 0;
        }

        public Paciente(string nombre, string apellido, string mail, int contacto, int dni) : base(nombre, apellido, mail, contacto, dni)
        {

        }
    }

}
