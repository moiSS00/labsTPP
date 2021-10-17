using System;
using System.Threading;


namespace Lab09
{
    /// <summary>
    ///Cuenta el numero de veces que el valor del BitCoin sobrepasa o iguala cierto valor limite. 
    /// 
    /// </summary>
    public class Master
    {
        /// <summary>
        /// Vector del que se contará
        /// </summary>
        private BitcoinValueData[] vector;

        /// <summary>
        /// Número de trabajadores que se van a emplear en el cálculo.
        /// </summary>
        private int numeroHilos;

        private int limite; //Valor limite 

        public Master(BitcoinValueData[] vector, int numeroHilos, int limite) //Se le pasa tambien el valor limite 
        {
            if (numeroHilos < 1 || numeroHilos > vector.Length)
                throw new ArgumentException("El número de hilos debe ser menor o igual al tamaño del vector");
            this.vector = vector;
            this.numeroHilos = numeroHilos;
            this.limite = limite; 
        }

        /// <summary>
        /// Este método crea y coordina el cálculo
        /// </summary>
        public double Contar()
        {
            // Creamos los workers
            Worker[] workers = new Worker[this.numeroHilos];
            int numElementosPorHilo = this.vector.Length / numeroHilos;
            for (int i = 0; i < this.numeroHilos; i++)
            { 
                int indiceDesde = i * numElementosPorHilo;
                int indiceHasta = (i + 1) * numElementosPorHilo - 1;
                if (i == this.numeroHilos - 1) //el último hilo, llega hasta el final del vector.
                {
                    indiceHasta = this.vector.Length - 1;
                }
                workers[i] = new Worker(this.vector, indiceDesde, indiceHasta,this.limite);
            }
            // * Iniciamos los hilos.
            Thread[] hilos = new Thread[workers.Length];
            for (int i = 0; i < workers.Length; i++)
            {
                hilos[i] = new Thread(workers[i].Contar); // Creamos el hilo
                hilos[i].Name = "Worker número: " + (i + 1); // le damos un nombre (opcional)
                hilos[i].Priority = ThreadPriority.Normal; // prioridad (opcional)
                hilos[i].Start();   // arrancamos el hilo
            }

            //¡¡¡¡¡¡¡¡¡¡¡¡¡¡¡¡¡¡¡OJO!!!!!!!!!!!!!!!!
            //Esperamos a que acaben
            foreach (Thread thread in hilos)
                thread.Join();

            //Por último, sumamos todos los resultados parciales
            long resultado = 0;
            foreach (Worker worker in workers)
                resultado += worker.Resultado;
            return resultado; //Devolvemos el resultado. 
        }

    }
}
