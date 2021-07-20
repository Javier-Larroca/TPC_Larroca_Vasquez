using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class TurnoDeTrabajo
    {
        public String Dia { get; set; }
        public String HorarioEntrada { get; set; }
        public String HorarioSalida { get; set; }
        public bool DiaLibre { get; set; }

        public TurnoDeTrabajo(string dia)
        {
            Dia = dia;
            HorarioEntrada = "00:00";
            HorarioSalida = "00:00";
        }
        
    }

}
