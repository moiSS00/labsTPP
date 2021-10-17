﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPP5
{
    public class Persona
    {
        public string Nombre { get; private set; }

        public string Apellido { get; private set; }

        public string Nif { get; private set; }

        public override string ToString()
        {
            return String.Format("{0} {1} con NIF {2}", Nombre, Apellido, Nif);
        }

        public Persona(string nombre, string apellido, string nif)
        {
            this.Nombre = nombre;
            this.Apellido = apellido;
            this.Nif = nif;
        }

        public void Saluda()
        {
            Console.WriteLine("Hola, soy {0}", this.Nombre);
        }
    }
}
