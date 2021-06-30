﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Medico : Usuario
    {
        public List<Especialidad> Especialidades { get; set; }
        public TurnoDeTrabajo TurnoDeTrabajo { get; set; }
        public int CantidadDeTurnosAsignados { get; set; }
        public int Matricula { get; set; }

        public Medico()
        {
            Id = 0;
        }

        //Implemento constructor de Clase Padre "Usuario"
        public Medico(string nombre, string apellido, string mail,int contacto, int dni, int matricula):base(nombre, apellido, mail,contacto, dni)
        { 
            CantidadDeTurnosAsignados = 0;
            Matricula = matricula;
        }
    }
}
