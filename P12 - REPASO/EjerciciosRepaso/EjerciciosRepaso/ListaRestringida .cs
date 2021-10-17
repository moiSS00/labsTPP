using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ListaEnlzadaLibreia; 

namespace EjerciciosRepaso
{
    public class ListaRestringida<T> : ListaEnlazada<T>
    {

        private Predicate<T> predicado; 

        public ListaRestringida(Predicate<T> predicado = null) : base()
        {
            if (predicado == null)
            {
                this.predicado = (e) => true;
            }
            else
            {
                this.predicado = predicado;
            }
           
        }

        public new bool Add(T elem)
        {
            if (predicado(elem))
            {
                base.Add(elem); 
                return true; 
            }
            return false; 
            
        }

        
    }
}
