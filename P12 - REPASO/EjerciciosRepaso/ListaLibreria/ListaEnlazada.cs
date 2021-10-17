using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListaEnlzadaLibreia //El espacio de nombres es orientativo , es solo para guiarse uno mismo 
//Cada assembly(Proyecto) y la Solucion es la combinacion de todos los proyectos(el todo)   
{

    public class ListaEnlazada<T> : IEnumerable
    {
        private Node<T> head;

        public int NumberOfElements { get; private set; } //Representa el numero de elementos actual de la lista 

        private Node<T> Head //Representa el primer elemento de la lista 
        {
            get { return head; }

            set
            {
                if (value != null)
                {
                    head = value;
                }
            }
        }



        public ListaEnlazada() //Constructor 
        {
            NumberOfElements = 0;
        }

        private void AddFirst(T inicial) //Añade el primer elemento a la lista 
        {
            this.Head = new Node<T>(inicial, this.Head);
            NumberOfElements += 1;
        }

        public void Add(T n) //Primer metodo prueba, añade en la ultima posicion 
        {
            if (NumberOfElements == 0)
            {
                AddFirst(n);
            }
            else
            {
                Node<T> last = GetNode(NumberOfElements - 1);
                last.Next = new Node<T>(n, null);
                NumberOfElements += 1;
            }
        }

        public void Clear()
        {
            while (!isEmpty())
            {
                Remove(0);
            }

        }

        public bool isEmpty()
        {
            return NumberOfElements == 0;
        }

        private Node<T> GetNode(int index) //Metodo privado auxiliar para buscar el nodo de una posicion en la lista 
        {
            if (index >= 0 && index < NumberOfElements)
            {
                int posicion = 0;
                Node<T> node = this.Head;
                while (posicion < index)
                {
                    node = node.Next;
                    posicion++;
                }
                return node;
            }

            return null;
        }

        public T GetElement(int index) //Nos devuelve el valor que ocupa la posicion que le pasamos como parametro.
        {
            Node<T> aux = GetNode(index);
            if (aux != null)
            {
                return aux.Value;
            }
            return default(T); //Indicando un error, ya que no se ha encontrado.
        }

        public T Remove(int index) //Borra un elemento de la lista 
        {
            if (index >= 0 && index < NumberOfElements)
            {
                if (NumberOfElements == 0) { return default(T); } //Si esta vacia 
                T value;
                if (index == 0)
                {
                    value = head.Value;
                    this.Head = this.Head.Next;
                }
                else
                {
                    Node<T> previous = GetNode(index - 1);
                    value = previous.Next.Value;
                    previous.Next = previous.Next.Next;
                }
                NumberOfElements -= 1;
                return value;
            }
            return default(T); //En caso de parametro erroneo 
        }

        public bool Remove(T obj) //Borra un elemento de la lista 
        {
            for (int i = 0; i < NumberOfElements; i++)
            {
                if (GetElement(i).Equals(obj))
                {
                    Remove(i);
                    return true;
                }
            }

            return false;
        }


        public override string ToString() //Representa la lista por consola de manera visual 
        {

            string cadena = "";

            if (NumberOfElements == 0)
            {
                return cadena;
            }
            else
            {
                int posicion = 0;
                Node<T> node = this.Head;
                while (posicion < NumberOfElements)
                {
                    if (posicion == 0)
                    {
                        cadena = cadena + node.Value;
                    }
                    else
                    {
                        cadena = cadena + ";" + node.Value;
                    }
                    node = node.Next;
                    posicion++;
                }
                return cadena;
            }
        }


        public bool Contains(T elem)
        {
            if (NumberOfElements == 0)
            {
                return false;
            }
            else
            {
                int posicion = 0;
                Node<T> node = this.Head;
                while (posicion < NumberOfElements)
                {
                    Object aux = GetNode(posicion).Value;
                    if (aux.Equals(elem))
                    {
                        return true;
                    }
                    node = node.Next;
                    posicion++;
                }
                return false;
            }
        }

        //Le pasamos una condición y nos devulve el primer elemento de la lista que cumple la condición. 
        public T Find(Predicate<T> predicate)
        {
            foreach (T i in this)
            {
                if (predicate(i))
                {
                    return i;
                }
            }

            return default(T);
        }

        //Le pasamos una condición y nos devuelve otra lista con los elementos que cumplen la condición. 
        public ListaEnlazada<T> Filter(Predicate<T> predicate)
        {
            ListaEnlazada<T> sol = new ListaEnlazada<T>();
            foreach (T i in this)
            {
                if (predicate(i))
                {
                    sol.Add(i);
                }
            }

            return sol;
        }

        //Le pasamos una función que se aplica a los elementos de una lista para devolvernos un total.El parámetro seed (opcional) es el valor inicial, si no se pasa
        //adquiere su valor por defecto. 
        public U Reduce<U>(Func<T, U, U> funcion, U seed = default(U)) //seed es el valor inicial , izquierda a derecha 
        {
            foreach (T i in this)
            {
                seed = funcion(i, seed);
            }

            return seed;
        }



        //Da la vuelta a la lista 
        public ListaEnlazada<T> Invert()
        {
            ListaEnlazada<T> sol = new ListaEnlazada<T>();

            for (int i = NumberOfElements - 1; i >= 0; i--)
            {
                sol.Add(GetElement(i));
            }

            return sol;
        }



        //Se le pasa una función que se aplicará a todos los elementos de la lista y nos devolverñá una lista con los resultados de la función.
        public ListaEnlazada<U> Map<U>(Func<T, U> funcion)
        {
            ListaEnlazada<U> sol = new ListaEnlazada<U>();
            foreach (T i in this)
            {
                sol.Add(funcion(i));
            }

            return sol;
        }

        public void ForEach(Action<T> action)
        {
            foreach (T i in this)
            {
                action(i);
            }
        }


        public IEnumerator GetEnumerator()
        {
            return new NodeEnumerator(this.head);
        }


        internal class NodeEnumerator : IEnumerator
        {
            private Node<T> head;
            private Node<T> puntero;
            private int primero;

            public NodeEnumerator(Node<T> head)
            {
                this.head = head;
                this.puntero = head;
                this.primero = 0;
            }

            public object Current
            {
                get
                {
                    return puntero.Value;
                }
            }


            public bool MoveNext()
            {
                if (this.primero == 0)
                {
                    this.primero += 1;
                    return true;
                }
                else
                {
                    //this.puntero = this.puntero.Next;
                    //return (this.puntero.Next != null);
                    if (this.puntero.Next != null)
                    {
                        this.puntero = this.puntero.Next;
                        return true;
                    }
                    else
                    {
                        //Revisar
                        return false;
                    }
                }

            }

            public void Reset()
            {
                this.primero = 0;
                this.puntero = this.head;
            }


        }
    }




}
