using OMS;
using OMS.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using OMS.Services;
using OMS.UIHandler;

namespace UnitTestKvar
{
    [TestClass]
    public class KvarTest
    {
        [TestMethod]
        public void TestUnosKvara()
        {
            var kvar = new Kvar("1111", DateTime.Now, "Nepotvrdjen");

            KvarService kvarService = new KvarService();

            kvarService.Save(kvar);

            Assert.IsNotNull(kvar);
        }

        [TestMethod]

        public void TestFindById()
        {
            var kvar = new Kvar("2222", DateTime.Now, "Nepotvrdjen");

            KvarService kvarService = new KvarService();

            kvarService.FindById("2222");

            Assert.IsNotNull(kvarService);
        }

        [TestMethod]

        public void TestFindAll()
        {
            var kvar = new Kvar("3333", DateTime.Now, "Nepotvrdjen");
            var kvar2 = new Kvar("4444", DateTime.Now, "Nepotvrdjen");


            KvarService kvarService = new KvarService();

            kvarService.FindAll();

            Assert.IsNotNull(kvarService);
        }

        [TestMethod]

        public void TestProveraStatusa()
        {
            var kvar = new Kvar("5555", DateTime.Now, "Nepotvrdjen");

            Assert.AreEqual("Nepotvrdjen", kvar.Status);
        }
    }
}
