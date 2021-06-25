using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Persona
    {
        public int Id { get; set; }
        public int Dni { get; set; }
        public String Nombre { get; set; }
        public String Apellido { get; set; }
        public String Mail { get; set; }

        /**
         * Constructor de clase padre, confirmar si se puede implementar en clases hijas usando base
        public Persona()
        {
            Id = 0;
        }
        
        public Persona(string nombre, string apellido, string mail, int dni)
        {
            Nombre = nombre;
            Apellido = apellido;
            Mail = mail;
            Dni = dni;
        }
        */
    }
}
