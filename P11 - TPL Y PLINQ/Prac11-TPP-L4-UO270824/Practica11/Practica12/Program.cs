using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practica12
{
    class Program
    {
        static void Main(string[] args)
        {
            String texto = ProcesadorTextos.LeerFicheroTexto(@"..\..\..\clarin.txt");
            string[] palabras = ProcesadorTextos.DividirEnPalabras(texto);
            DateTime antes = DateTime.Now;
            ProcesadorTextos.PalabrasRepeticionesSecuencial(palabras);
            DateTime despues = DateTime.Now;

            

            Console.WriteLine("Secuental: Tiempo: {0:N} ms.", (despues - antes).Ticks / TimeSpan.TicksPerMillisecond);


            //antes = DateTime.Now;
            //ProcesadorTextos.PalabrasRepeticionesParalelo(palabras);
            //despues = DateTime.Now;

           
            //Console.WriteLine("Paralelo: Tiempo: {0:N} ms.", (despues - antes).Ticks / TimeSpan.TicksPerMillisecond);


            Console.ReadLine();
        }
    }
}
