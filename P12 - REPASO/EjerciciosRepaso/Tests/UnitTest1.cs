using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EjerciciosRepaso; 

namespace Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Ejercicio2()
        {

            //Ejercicio 2 

            //Sin predicado 
            ListaRestringida<int> lista = new ListaRestringida<int>();
            Assert.IsTrue(lista.isEmpty());
            Assert.IsTrue(lista.Add(4));
            Assert.IsTrue(lista.Add(3));
            Assert.IsTrue(lista.Add(1));
            Assert.IsTrue(lista.Add(3));
            Assert.IsFalse(lista.isEmpty());
            Assert.IsTrue(lista.NumberOfElements == 4);

            //Con predidcado
            lista = new ListaRestringida<int>((e) => e >= 3);
            Assert.IsTrue(lista.isEmpty());
            Assert.IsTrue(lista.Add(4));
            Assert.IsTrue(lista.Add(3));
            Assert.IsFalse(lista.Add(1));
            Assert.IsFalse(lista.Add(-3));
            Assert.IsFalse(lista.isEmpty());
            Assert.IsTrue(lista.NumberOfElements == 2);
        }
    }
}
