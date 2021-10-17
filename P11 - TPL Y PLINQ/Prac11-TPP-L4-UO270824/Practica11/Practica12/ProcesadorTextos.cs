using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practica12
{
    class ProcesadorTextos
    {

        private static void ContarPalabra(string palabra,string[] palabras)
        {
            int repeticiones = palabras.Count(p => p.Equals(palabra));
            Console.WriteLine("Palabra: " + palabra + " Repeticiones = " + repeticiones); 

        }

        public static void PalabrasRepeticionesSecuencial(string[] palabras)
        {
            //Console.WriteLine(palabras.Count()); 
            var palabrasFiltrado = palabras.Distinct();
            //Console.WriteLine(palabrasFiltrado.Count());
            foreach (var i in palabras)
            {
                ContarPalabra(i,palabras); 
            }
        }

        public static void PalabrasRepeticionesParalelo(string[] palabras)
        {
            var palabrasFiltrado = palabras.Distinct();
            Parallel.ForEach(palabrasFiltrado, p => ContarPalabra(p, palabras)); 
        }

        public static string[] DividirEnPalabras(String texto)
        {
            return texto.Split(new char[] { ' ', '\r', '\n', ',', '.', ';', ':', '-', '!', '¡', '¿', '?', '/', '«',
                                            '»', '_', '(', ')', '\"', '*', '\'', 'º', '[', ']', '#' },
                StringSplitOptions.RemoveEmptyEntries);
        }

        public static String LeerFicheroTexto(string nombreFichero)
        {
            using (StreamReader stream = File.OpenText(nombreFichero))
            {
                StringBuilder text = new StringBuilder();
                string linea;
                while ((linea = stream.ReadLine()) != null)
                {
                    text.AppendLine(linea);
                }
                return text.ToString();
            }
        }
    }
}
