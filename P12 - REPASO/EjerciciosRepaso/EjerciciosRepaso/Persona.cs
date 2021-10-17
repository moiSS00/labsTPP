using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ListaEnlzadaLibreia; 

namespace EjerciciosRepaso
{
    class Persona
    {

        public int DNI { get; private set; }
        
        public string Nombre { get; private set; }

        public Persona(int dni , string nombre)
        {
            DNI = dni;
            Nombre = nombre; 
        }


        public override string ToString()
        {
            string cadena = "Nombre: " + Nombre + " DNI: " + DNI;
            return cadena;
        }

    }
}
