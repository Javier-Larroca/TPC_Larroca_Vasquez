using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public enum TipoUsuario
    {
        SOPORTE = 1,
        RECEPCIONISTA = 2,
        MEDICO = 3,
        PACIENTE = 4
    }
    public class Usuario
    {
        public int Id { get; set; }
        public int Dni { get; set; }
        public String Nombre { get; set; }
        public String Apellido { get; set; }
        public String Mail { get; set; }
        public int Contacto { get; set; }
        public bool Activo{ get; set; }
        public string Contraseña{ get; set; }
        public DateTime Alta{ get; set; }
        public DateTime? Modificacion { get; set; }
        public string TipoUsuario{ get; set; }
        //Le agrego '?' al DatTime para indicar que puede ser nulleable por definicion de BBDD
        public DateTime? Baja{ get; set; }

        public Usuario()
        {
            Id = 0;
        }

        public Usuario(string nombre, string apellido, string mail, int contacto, int dni)
        {
            Nombre = nombre;
            Apellido = apellido;
            Mail = mail;
            Contacto = contacto;
            Dni = dni;
        }

    }
}
