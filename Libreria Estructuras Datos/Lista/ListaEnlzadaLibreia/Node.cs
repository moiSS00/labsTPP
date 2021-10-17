using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListaEnlzadaLibreia
{
    public class Node<T>//Solo se puede ver en el mismo assembly 
    {

        //Las propiedades nos permiten disfrutar de los beneficios de la encapsulacion (controles principalmente) y 
        // nos ofrecen una sintaxis mas sencilla (como si fueran publicos).

        private Node<T> next; //Similar a atributo Java (encapsulado).Si queremos redefinir es necesario indicar el campo privado.
        public T Value { get; set; } //Propiedad que representa el valor del nodo
       
        public Node<T> Next  //Propiedad que representa al nodo siguiente
        {
            get { return next; }

            set
            {
                if(value != null) //La cte value almacena el valor que se le pasa al set.
                {
                    next = value; 
                }
            }
        }

        public Node(T value,Node<T> next) //Constructor utilizando propiedades (ventaja controles) 
        {
            this.Value = value;
            this.Next = next;
        }

        
    }
}
