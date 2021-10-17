using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace test.Vector
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void MetodosList()
        {
            IList<int> lista = new List<int>(20);
            Assert.AreEqual(0, lista.Count);
            lista.Add(0);
            lista.Add(1);
            Assert.AreEqual(2, lista.Count);
            lista.Add(2);
            Assert.AreEqual(1, lista.IndexOf(1));
            lista[0] = 1;
            Assert.AreEqual(1, lista[0]);
            Assert.IsFalse(lista.Contains(18));
            Assert.IsTrue(lista.Contains(2));
            lista.Remove(2);
            Assert.IsFalse(lista.Contains(18));

            int suma = 0;
            foreach (int i in lista)
            {
                suma += i;
            }

            Assert.AreEqual(2, suma);

        }

        [TestMethod]
        public void MetodosDic()
        {

            IDictionary<int, string> dic = new Dictionary<int, string>();

            Assert.AreEqual(0, dic.Count);
            dic.Add(1, "");
            Assert.AreEqual(1, dic.Count);
            Assert.IsFalse(dic.ContainsKey(2));
            Assert.IsTrue(dic.ContainsKey(1));
            dic.Remove(1);
            Assert.AreEqual(0, dic.Count);
            foreach (KeyValuePair<int, string> i in dic) ;
            dic.Add(1, "");
            dic.Add(2, "");
            dic[0] = "p";
            Assert.IsTrue(dic[0].Equals("p"));
        }
    }
}
