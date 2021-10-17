using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ListaEnlzadaLibreia;
using System.Linq;
using System.Threading; 

namespace EjerciciosRepaso
{
    static class Ejericicios
    {
        static void Main(string[] args)
        {

            //Prueba del ejercicio 1 
            string cadena = "Esto eS una prueba";

           
            //Metodo A 
            Pila<int> pila = cadena.Ejercicio1A();
            for (int i = 0;i<pila.NumeroMaxElementos;i++)
            {
                Console.WriteLine(pila.getElemento(i)); 
            }

            //Metodo B 
            int maxLength;
            int minLength;
            int nUppers;
            int nLowers;
            cadena.Ejercicio1B(out maxLength, out nUppers, out minLength, out nLowers);

            Console.WriteLine(maxLength + " " + nUppers + " " + minLength + " " + nLowers);


            //EJERCICIO 3 

            foreach (var i in CrearPersona().Skip(10).Take(5))
            {
                Console.WriteLine(i); 
            }


            Func<Persona> funcion = CrearPersona2(); 
            for (int i = 0;i<10;i++)
            {
                Console.WriteLine(funcion()); 
            }

            CalcularPotencias(2,3);

            //EJERCICIO 5

            Ejercicio5(CreateRandomVector(10, 1, 20)); 


            Console.ReadLine(); 
        }



        //EJERCICIO 1 

        static Pila<int> Ejercicio1A(this string cadena)
        {
            Pila<int> pila = new Pila<int>(4); 
            string[] palabras = cadena.Split(' ');

          
            int maxLength = 0;
            string maxWord = "";


            for (int i = 0;i<palabras.Length;i++)
            {
                if (palabras[i].Length > maxLength)
                {
                    maxWord = palabras[i];
                    maxLength = palabras[i].Length; 
                }
            }

            int minLength = maxLength;
            string minWord = "";

            for (int i = 0; i < palabras.Length; i++)
            {
                if (palabras[i].Length < minLength)
                {
                    minWord = palabras[i];
                    minLength = palabras[i].Length;
                }
            }

            pila.Push(maxLength);
            pila.Push(maxWord.Count(letra => Char.IsLower(letra)));
            pila.Push(minLength);
            pila.Push(minWord.Count(letra => Char.IsUpper(letra)));

            return pila; 
        }
        
        static void Ejercicio1B(this string cadena, out int maxLength, out int nUppers, out int minLength, out int nLowers)
        {
            string[] palabras = cadena.Split(' ');

            maxLength = 0;
            string maxWord = "";


            for (int i = 0; i < palabras.Length; i++)
            {
                if (palabras[i].Length > maxLength)
                {
                    maxWord = palabras[i];
                    maxLength = palabras[i].Length;
                }
            }

            minLength = maxLength;
            string minWord = "";

            for (int i = 0; i < palabras.Length; i++)
            {
                if (palabras[i].Length < minLength)
                {
                    minWord = palabras[i];
                    minLength = palabras[i].Length;
                }
            }

            nUppers = maxWord.Count(letra => Char.IsLower(letra));
            nLowers = minWord.Count(letra => Char.IsUpper(letra)); 
        }

        //EJERCICIO 3 
        public static IEnumerable<Persona> CrearPersona()
        {
            Random r = new Random();
            int DNI;
            char c1; char c2;
            string nombre;

            while (true)
            {
                DNI = r.Next(0,16);
                c1 = (char)r.Next(65,91);
                c2 = (char)r.Next(65, 91);
                nombre = "" + c1 + c2;
                yield return new Persona(DNI,nombre); 
            }
        }

        public static Func<Persona> CrearPersona2()
        {
            Random r = new Random();
            Persona p = null;
            List<int> lista = new List<int>();
            int DNI;
            char c1;char c2;
            string nombre;

            return () =>
            {
                DNI = r.Next(0,16);
                if (!lista.Contains(DNI))
                {
                    c1 = (char)r.Next(65,91);
                    c2 = (char)r.Next(65, 91);
                    nombre = "" + c1 + c2;
                    p = new Persona(DNI,nombre);
                    lista.Add(DNI);
                    return p; 
                }
                else
                {
                    return p; 
                }
            };
        }

        //EJERCICIO 4

        public static void CalcularPotencias(int numero, int n)
        { 
            ColaConcurrente<double> cola = new ColaConcurrente<double>(); //Inicializamos estructura de datos concurrente 
            Thread[] hilos = new Thread[n]; //Inicializamos el vector de hilos 
            for (int i = 0;i<n;i++) //Creamos los hilos 
            {
                hilos[i] = new Thread( () => //Se le pasa un action como tarea 
                {
                    for (int j = 1; j <= 10;j++)
                    {
                        cola.Añadir(Math.Pow(numero, j));
                    }
                    Interlocked.Increment(ref numero);
                }
                );
            }
            foreach (var hilo in hilos) //Ejecutamos los hilos 
            {
                hilo.Start();
                hilo.Join(); //Sincronizarlos. No hay mas hilos hasta que el que realice el Join() finalice. 
            }

            int nElem = cola.NumeroElementos;  //Recorremos la pila, mientras la vaciamos
            for (int i = 0; i < nElem; i++)
            {
                var aux = cola.Extraer();
                Console.WriteLine(aux); 
            }
        }

        //EJERCICIO 5 

        public static short[] CreateRandomVector(int numberOfElements, short lowest, short greatest)
        {
            short[] vector = new short[numberOfElements];
            Random random = new Random();
            for (int i = 0; i < numberOfElements; i++)
                vector[i] = (short)random.Next(lowest, greatest + 1);
            return vector;
        }


        public static double MediaVector(short[] vector)
        {
            //short media = 0;
            //for (int i = 0;i<vector.Length;i++)
            //{
            //    media += vector[i];
            //}
            //return media / vector.Length; 

            return vector.Aggregate(0.0,(semilla,n) => semilla + n) / vector.Length; 
        }

        public static double ModaVector(short[] vector)
        {
            //Dos formas 
            //return vector.GroupBy(n => n).Distinct().OrderByDescending(par => par.Count()).First().ToArray()[0];
            return vector.GroupBy(n => n).Select(par => new { Numero = par.Key, Repeticiones = par.Count()}).OrderByDescending(elem => elem.Repeticiones).First().Numero;
        }

        public static void Ejercicio5(short[] vector)
        {
            Dictionary<string, double> diccionario = new Dictionary<string, double>();
            double media = 0;
            double moda = 0;
            Parallel.Invoke(
                () => media = MediaVector(vector),
                () => moda = ModaVector(vector)
                );
            diccionario.Add("media",media);
            diccionario.Add("moda",moda);
            Console.WriteLine("La media del vector es: " + diccionario["media"]);
            Console.WriteLine("La moda del vector es: " + diccionario["moda"]);
            
            moda = vector.AsParallel().GroupBy(n => n).Distinct().OrderByDescending(par => par.Count()).First().ToArray()[0];
            Console.WriteLine("La moda con PLINQ es: " + moda); 

        }


    }

 
}
