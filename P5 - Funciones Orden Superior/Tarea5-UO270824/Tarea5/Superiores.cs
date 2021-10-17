using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPP5
{
    public class Superiores
    {
        public static T Buscar<T>(IEnumerable<T> lista, Predicate<T> condicion)
        {
            foreach (var i in lista)
            {
                if (condicion(i))
                {
                    return i;
                }
            }
            return default(T);
        }

        public static IEnumerable<T> Filtrar<T>(IEnumerable<T> lista, Predicate<T> condicion)
        {
            IEnumerable<T> aux = new List<T>();
            foreach (var i in lista)
            {
                if (condicion(i))
                {
                    aux.Append(i);
                }
            }
            return aux;
        }



        public static U Reducir<T,U> (IEnumerable<T> lista,Func<U,T,U> funcion)
        {
            U aux = default(U); 

            foreach(var i in lista)
            {
                aux = funcion(aux,i);
            }

            return aux;
        }


    }
}

