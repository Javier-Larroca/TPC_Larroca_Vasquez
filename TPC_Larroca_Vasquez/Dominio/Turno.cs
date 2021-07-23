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
        public bool Estado { get; set; }
        public int IdPaciente { get; set; }
        public string NombrePaciente { get; set; }
        public int IdMedico { get; set; }
        public string NombreMedico { get; set; }
        public string Horario{ get; set; }
        public DateTime FechaTurno{ get; set; }
        public string Observaciones{ get; set; }

    }
}
