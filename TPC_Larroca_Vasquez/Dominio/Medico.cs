using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Medico : Persona
    {
        public List<Especialidad> Especialidades { get; set; }
        public TurnoDeTrabajo TurnoDeTrabajo { get; set; }
        public int CantidadDeTurnosAsignados { get; set; }

        public Medico()
        {
            Id = 0;
        }
        public Medico(string nombre, string apellido, string mail, int dni)
        {
            Nombre = nombre;
            Apellido = apellido;
            Mail = mail;
            Dni = dni;
            CantidadDeTurnosAsignados = 0;
        }
    }
}
