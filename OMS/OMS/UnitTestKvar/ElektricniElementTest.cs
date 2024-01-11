using OMS;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using OMS.Services;
using OMS.Model;
using System.Globalization;
using OMS.Service;

namespace UnitTestKvar
{
    [TestClass]
    public class ElektricniElementTest
    {
        [TestMethod]
        public void KreirajElementTest()
        {
            string id = "1";
            string naziv = "uredjaj";
            string tip = "333";
            string geolokacija = "sombor";
            string naponskiNivo = "srednji nivo";

            var element = new ElektricniElement(id, naziv, tip, geolokacija, naponskiNivo);

            
            Assert.AreEqual(id, element.ID);
            Assert.AreEqual(naziv, element.Naziv);
            Assert.AreEqual(tip, element.Tip);
            Assert.AreEqual(geolokacija, element.GeoLokacija);
            Assert.AreEqual(naponskiNivo, element.NaponskiNivo);

        }

        [TestMethod]
        public void FindByIdTest()
        {
            string id = "1";
            string naziv = "uredjaj";
            string tip = "333";
            string geolokacija = "sombor";
            string naponskiNivo = "srednji nivo";

            var element = new ElektricniElement(id, naziv, tip, geolokacija, naponskiNivo);

            ElektricniElementService elektricniElementService = new ElektricniElementService();
            elektricniElementService.FindById(id);

            Assert.IsNotNull(elektricniElementService);
        }
    }
}
