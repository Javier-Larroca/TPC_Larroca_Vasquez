using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Medico
    {
        public int Id { get; set; }
        public String Nombre { get; set; }
        public String Apellido { get; set; }
        public List<Especialidad> Especialidades { get; set; }
        public TurnoDeTrabajo TurnoDeTrabajo { get; set; }
        public int CantidadDeTurnosAsignados { get; set; }

        public Medico()
        {
            Id = 0;
        }
        public Medico(string nombre, string apellido)
        {
            Nombre = nombre;
            Apellido = apellido;
            CantidadDeTurnosAsignados = 0;
        }
    }
}
