using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TPP5;

namespace Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {

            Angulo[] angulos = Factoria.CrearAngulos();
            Persona[] personas = Factoria.CrearPersonas();

            Angulo a = Superiores.Buscar(angulos,esRecto);
            Assert.AreEqual(a.Grados,90);
            IEnumerable<Angulo> prueba = Superiores.Filtrar(angulos,esRecto);
            foreach(var i in prueba)
            {
                Assert.AreEqual(i.Grados,90);
            }
            Persona p = Superiores.Buscar(personas,esMaria);
            Assert.AreEqual(p.Nombre, "María");
            IEnumerable<Persona> prueba2 = Superiores.Filtrar(personas, esMaria);
            foreach (var i in prueba2)
            {
                Assert.AreEqual(i.Nombre,"María");
            }
            var aux = Superiores.Reducir(angulos,Angulos);
            Assert.AreEqual(aux, 64980);

        }
        Predicate<Angulo> esRecto = (Angulo a) =>
        {
            if (a.Grados == 90)
            {
                return true;
            }
            return false;
        };

        Predicate<Persona> esMaria = (Persona p) =>
        {
            if (p.Nombre.Equals("María"))
            {
                return true;
            }
            return false;
        };

        Func<double, Angulo,double> Angulos = (double aux,Angulo a) => { return aux+=a.Grados; };

    }

    
}
