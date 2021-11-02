using System;
using System.Collections;
using System.Collections.Generic;
using Proyect;
using NUnit.Framework;


namespace Tests
{
    /// <summary>
    /// Clase para testeo de creacion y autenticacion de las empresas y emprendedores
    /// </summary>
    [TestFixture]
    public class AuthTests
    {
        private User empresa1;
        private User emprendedor1;
        private Rubro rubro1;
        private List<Qualifications> qualifications;
        private List<Qualifications> specializations;
        private Qualifications q1;
        private Qualifications s1;
        private Classification tipo;
        private ArrayList keyWords;

        /// <summary>
        /// Setup para los tests.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            keyWords = new ArrayList();
            keyWords.Add("Reciclable");

            rubro1 = new Rubro("Rubro1");

            tipo = new Classification("Reciclable");

            qualifications = new List<Qualifications>();
            specializations = new List<Qualifications>();

            q1 = new Qualifications("Calificacion1");
            qualifications.Add(q1);
            s1 = new Qualifications("Especializacion");
            specializations.Add(s1);

            this.empresa1 = new Company("SECOM","Parque Battle",rubro1);
            this.emprendedor1 = new Emprendedor("Juan","Parque Battle",rubro1,qualifications,specializations);
        }

        /// <summary>
        /// Se testea si se puede crear correctamente la oferta.
        /// </summary>
        [Test]
        public void CreateOffer()
        {
            Offer o = new Offer(false,tipo,20,100,"Parque Battle",qualifications,keyWords);
            Assert.AreEqual(false,o.Constant);
            Assert.AreEqual("Reciclable",o.Product.Classification.Category);
            Assert.AreEqual(20,o.Product.Quantity);
            Assert.AreEqual(100,o.Product.Price);
            Assert.AreEqual("Parque Battle",o.Product.Ubication);
            Assert.AreEqual(qualifications,o.Qualifications);
            Assert.AreEqual(keyWords,o.KeyWords);
        }

        [Test]
        public void PurchaseOffer()
        {
            Offer o = new Offer(false,tipo,20,100,"Parque Battle",qualifications,keyWords);
            string actual = AppLogic.Instance.AccepOffer((Emprendedor)emprendedor1,o);
            string expected = "Usted no dispone de las habilitaciones requeridas por la oferta";
            Assert.AreEqual(expected,actual);
        }

    }
}
