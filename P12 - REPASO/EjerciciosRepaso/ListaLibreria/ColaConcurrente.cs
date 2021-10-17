using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ListaEnlzadaLibreia
{
    public class ColaConcurrente<T>
    {

        public int NumeroElementos
        {
            get
            {
                return cola.NumberOfElements;
            }
        }

        private ListaEnlazada<T> cola;

        public ColaConcurrente()
        {
            this.cola = new ListaEnlazada<T>();
        }

        public bool EstaVacia()
        {
            return NumeroElementos == 0;
        }

        public void Añadir(T elem)
        {
            lock (cola)
            {
                cola.Add(elem);
            }
        }

        public T Extraer()
        {
            lock (cola)
            {
                T aux = cola.GetElement(0);
                cola.Remove(aux);
                return aux;
            }

        }

        public T PrimerElemento()
        {
            lock (cola)
            {
                return cola.GetElement(0);
            }
        }
    }
}