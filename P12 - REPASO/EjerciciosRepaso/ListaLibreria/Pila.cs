using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ListaEnlzadaLibreia
{
    public class Pila<T> //Se utiliza la composición con el objetivo de reutilizar la estructura de la lista enlazada. 
    {

        private ListaEnlazada<T> pila;

        public int NumeroMaxElementos { get; }

        public bool EstaVacia
        {
            get
            {
                return pila.isEmpty();
            }
        }

        public bool EstaLLena
        {
            get
            {
                return pila.NumberOfElements == this.NumeroMaxElementos;
            }
        }

        public Pila(int numeroMaxElementos)
        {
            this.NumeroMaxElementos = numeroMaxElementos;
            this.pila = new ListaEnlazada<T>();
        }

        private void Invariant()
        {
            Debug.Assert(pila.NumberOfElements <= NumeroMaxElementos && pila.NumberOfElements >= 0);
        }

        public int Size()
        {
            return pila.NumberOfElements;
        }

        public Object getElemento(int index)
        {
            return pila.GetElement(index);
        }

        public void Clear()
        {
            pila.Clear();
        }

        public void Push(T obj)
        {
            if (obj == null) //Precondiciones con excepcines 
                throw new ArgumentException("No puede añadir un objeto nulo.");
            if (this.EstaLLena)
                throw new ArgumentOutOfRangeException("No hay suficiente espacio en la pila.");

            //Las otras se deben hacer con asertos.
            int numElem = pila.NumberOfElements;
            pila.Add(obj);

            Debug.Assert(numElem + 1 == pila.NumberOfElements); //PosCondiciones e invariante 
            Debug.Assert(this.EstaVacia == false);
            Invariant();
        }

        public Object Pop()
        {
            if (this.EstaVacia)
                throw new InvalidOperationException("La lista esta vacia.");

            int numElem = pila.NumberOfElements;
            Object aux = pila.Remove(pila.NumberOfElements - 1);

            if (aux != null)
            {
                Debug.Assert(numElem - 1 == pila.NumberOfElements);
                Debug.Assert(aux != null);
            }
            else
            {
                Debug.Assert(numElem == pila.NumberOfElements);
            }
            Invariant();
            return aux;
        }
    }
}
