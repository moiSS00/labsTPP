using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab09
{

    internal class Worker
    {

        /// <summary>
        /// Vector del que vamos a contar 
        /// </summary>
        private BitcoinValueData[] vector;

        /// <summary>
        /// Índices que indican el rango de elementos del vector 
        /// con el que vamos a trabajar.
        /// En el rango se incluyen ambos índices.
        /// </summary>
        private int indiceDesde, indiceHasta;

        /// <summary>
        /// Resultado del conteo
        /// </summary>
        private long resultado;

        private int limite; //Barrera a parit de la cual se empieza a contar 

        internal long Resultado //Propiedad que guarda el resultado 
        {
            get { return this.resultado; }
        }

        internal Worker(BitcoinValueData[] vector, int indiceDesde, int indiceHasta, int limite) //Constructor al que se le pasa tambien el limite 
        {
            this.vector = vector;
            this.indiceDesde = indiceDesde;
            this.indiceHasta = indiceHasta;
            this.limite = limite; 
        }

        /// <summary>
        /// Método que realiza el cálculo
        /// </summary>
        internal void Contar()
        {
            this.resultado = 0;
            for (int i = this.indiceDesde; i <= this.indiceHasta; i++)
            {
                if (vector.ElementAt(i).Value >= limite)
                {
                    this.resultado += 1; 
                }
            }
               
        }

    }
}
