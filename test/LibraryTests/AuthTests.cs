using System;
using System.Collections;
using System.Collections.Generic;
// using NUnit.Framework;


namespace Tests
{
    /// <summary>
    /// Clase para testeo de creacion y autenticacion de las empresas y emprendedores
    /// </summary>
   // [TestFixture]
    public class AuthTests
    {/*
        private User empresa1;
        private User emprendedor1;
        private Rubro rubro1;
        private List<Qualifications> qualifications;
        private List<Qualifications> qualifications2;
        private ArrayList specializations;
        private Qualifications q1;
        private Qualifications q2;
        private Qualifications q3;
        private Qualifications s1;
        private Classification tipo;
        private ArrayList keyWords;
        private ArrayList keyWords2;
        private NonConstantOffer o;
        private ConstantOffer o2;

        /// <summary>
        /// Setup para los tests.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            keyWords = new ArrayList();
            keyWords.Add("Reciclable");
            keyWords2 = new ArrayList();
            keyWords2.Add("Toxico");

            rubro1 = new Rubro("Rubro1");

            tipo = new Classification("Reciclable");

            qualifications = new List<Qualifications>();
            qualifications2 = new List<Qualifications>();
            specializations = new ArrayList();

            q1 = new Qualifications("Vehiculo propio");
            q2 = new Qualifications("Espacio para grandes volumenes de producto");
            q3 = new Qualifications("Lugar habilitado para conservar desechos toxicos");

            qualifications.Add(q1);
            qualifications2.Add(q1);
            qualifications2.Add(q2);
            qualifications2.Add(q3);
            s1 = new Qualifications("Especializacion");
            specializations.Add(s1);

            empresa1 = new Company("WER","SECOM","Parque Battle",rubro1);
            emprendedor1 = new Emprendedor("WER","Juan","Parque Battle",rubro1,qualifications,specializations);

            o = new NonConstantOffer(tipo,20,100,"Parque Battle",qualifications,keyWords);
            o2 = new ConstantOffer(tipo,20,100,"Parque Battle",qualifications2,keyWords2);

        }
        
        /// <summary>
        /// Se testea que se registra una company, al no ser null, sé que se creo un objeto de tipo user.
        /// </summary>
        [Test]
        public void RegisterCompanyTest()
        {
            User actual = Administrator.Instance.Invite("Pepe","WER","SECOM2","Tres Cruces",rubro1);
            Assert.AreNotEqual(null,actual);
        }

        /// <summary>
        /// Se testea que si el token no es uno aprobado por el administrado, no se va a registrar a la company.
        /// </summary>
        [Test]
        public void RegisterNotValidTokenTest()
        {
            User actual = Administrator.Instance.Invite("Pep","WER","SECOM2","Tres Cruces",rubro1);
            Assert.AreEqual(null,actual);
        }

        /// <summary>
        /// Se testea si se puede crear correctamente la oferta.
        /// </summary>
        [Test]
        public void CreateOfferTest()
        {
            NonConstantOffer o = new NonConstantOffer(tipo,20,100,"Parque Battle",qualifications,keyWords);
            Assert.AreEqual("Reciclable",o.Product.Classification.Category);
            Assert.AreEqual(20,o.Product.Quantity);
            Assert.AreEqual(100,o.Product.Price);
            Assert.AreEqual("Parque Battle",o.Product.Ubication);
            Assert.AreEqual(qualifications,o.Qualifications);
            Assert.AreEqual(keyWords,o.KeyWords);
        }

        /// <summary>
        /// Se testea si se publicó correctamente una oferta.
        /// </summary>
        [Test]
        public void PublishOfferTest()
        {
            string actual = AppLogic.Instance.PublicConstantOffer((Company)empresa1,tipo,200,75,"Pocitos",qualifications,keyWords2);
            string expected = "Oferta publicada con exito";
            Assert.AreEqual(expected,actual);
        }

        /// <summary>
        /// Se testea si un emprendedor tiene las habilitaciones requeridas para aceptar una oferta.
        /// </summary>
        [Test]
        public void RequiredQualificationsTest()
        {
            string actual = AppLogic.Instance.AccepOffer((Emprendedor)emprendedor1,o);
            string expected = "Usted a aceptado la oferta con exito";
            Assert.AreEqual(expected,actual);
        }

        /// <summary>
        /// Se testea que el emprendedor no tiene las habilitaciones requeridas para aceptar la oferta.
        /// </summary>
        [Test]
        public void FailedRequiredQualificationsTest()
        {
            string actual = AppLogic.Instance.AccepOffer((Emprendedor)emprendedor1,o2);
            string expected = "Usted no dispone de las habilitaciones requeridas por la oferta";
            Assert.AreEqual(expected,actual);
        }

        /// <summary>
        /// Se testea que se pueden agregar Keywords a una oferta.
        /// </summary>
        [Test]
        public void AddKeywordsTest()
        {
            NonConstantOffer o = new NonConstantOffer(tipo,20,100,"Parque Battle",qualifications,keyWords);
            string actual = AppLogic.Instance.AddKeyWords((Company)empresa1,o,"Oportunidad");
            string expected = "Se agrego Oportunidad a la oferta de 20 de Reciclable";
            Assert.AreEqual(expected,actual);
        }

        /// <summary>
        /// Se testea que, si al intentar agregar una palabra clave que ya se encuentra en una oferta, no te va a dejar.
        /// </summary>
        [Test]
        public void KeywordAlreadyInProductTest()
        {
            NonConstantOffer o = new NonConstantOffer(tipo,20,100,"Parque Battle",qualifications,keyWords);
            string actual = AppLogic.Instance.AddKeyWords((Company)empresa1,o,"Reciclable");
            string expected = "Reciclable ya se encuntra como palabra clave en la oferta seleccionada";
            Assert.AreEqual(expected,actual);
        }

        /// <summary>
        /// Se testea que se pueda registrar una emprendedor.
        /// </summary>
        [Test]
        public void RegisterEntrepreneursTest()
        {
            string actual = AppLogic.Instance.RegisterEntrepreneurs("WER","SECOM","Parque Battle",rubro1,qualifications,specializations);
            string expected = "Usted se a registrado con exito";
            Assert.AreEqual(expected,actual);
        }

        
        /// <summary>
        /// Se testea que si no se ingresan datos, no se pueda crear al emprendedor.
        /// </summary>
        [Test]
        public void FailedRegisterEntrepreneursTest()
        {
            try
            {
                string actual = AppLogic.Instance.RegisterEntrepreneurs("WSQ","","Parque Battle",rubro1,qualifications,specializations);
            }
            catch(EmptyUserBuilderException e)
            {
                string actual = e.Message;
                string expected = "Los datos ingresados no son validos, nombre o ubicacion vacios";
                Assert.AreEqual(expected,actual);
            }
        }

        /// <summary>
        /// Se testea que se puedan encontrar ofertas por keywords.
        /// </summary>
        [Test]
        public void SearchByKeywordsTest()
        {
            AppLogic.Instance.Companies.Add((Company)empresa1);
            AppLogic.Instance.PublicConstantOffer((Company)empresa1,tipo,200,75,"Pocitos",qualifications2,keyWords2);
            string actual = (AppLogic.Instance.SearchOfferByKeywords("Toxico")[0]);
            string expected = $"200 de Reciclable\n\nCompania ofertora: SECOM\nPrecio: 75$\nUbicacion: Pocitos\nFecha de publicacion: Siempre\nHabilitaciones necesarias: |Vehiculo propio||Espacio para grandes volumenes de producto||Lugar habilitado para conservar desechos toxicos|";
            Assert.AreEqual(expected,actual);
        }

        /// <summary>
        /// Se testea que un emprendedor pueda comprar una oferta.
        /// </summary>
        [Test]
        public void PurchaseOfferTest()
        {
            string actual = AppLogic.Instance.AccepOffer((Emprendedor)emprendedor1,o);
            string expected = "Usted a aceptado la oferta con exito";
            Assert.AreEqual(expected,actual);
        }

        /// <summary>
        /// Se testea de poder ver cuantas ofertas de esta company se aceptaron por emprendedores en un periodo de tiempo.
        /// </summary>
        [Test]
        public void CompanyOffersInPeriodOfTimeTest()
        {
            AppLogic.Instance.Companies.Add((Company)empresa1);
            AppLogic.Instance.Entrepreneurs.Add((Emprendedor)emprendedor1);
            AppLogic.Instance.PublicNonConstantOffer((Company)empresa1,tipo,200,75,"Pocitos",qualifications,keyWords2);
            AppLogic.Instance.PublicNonConstantOffer((Company)empresa1,tipo,30,125,"Pocitos",qualifications,keyWords2);
            AppLogic.Instance.AccepOffer((Emprendedor)emprendedor1,AppLogic.Instance.Companies[0].OffersPublished[0]);
            AppLogic.Instance.AccepOffer((Emprendedor)emprendedor1,AppLogic.Instance.Companies[0].OffersPublished[1]);
            string expected = AppLogic.Instance.GetPeriodTimeOffersAccepted(AppLogic.Instance.Entrepreneurs[0],10);
            string actual = "200 Reciclable at a price of 75$\n30 Reciclable at a price of 125$\nUsted ah aceptado 2 ofertas en los ultimos 10 días";
            Assert.AreEqual(expected,actual);
        }

        /// <summary>
        /// Se testea de poder ver las ofertas aceptadas por este emprendedor durante un periodo de tiempo.
        /// </summary>
        [Test]
        public void EntrepreneurOffersInPeriodOfTimeTest()
        {
            AppLogic.Instance.Companies.Add((Company)empresa1);
            AppLogic.Instance.Entrepreneurs.Add((Emprendedor)emprendedor1);
            AppLogic.Instance.PublicNonConstantOffer((Company)empresa1,tipo,200,75,"Pocitos",qualifications,keyWords2);
            AppLogic.Instance.PublicNonConstantOffer((Company)empresa1,tipo,30,125,"Pocitos",qualifications,keyWords2);
            AppLogic.Instance.AccepOffer((Emprendedor)emprendedor1,AppLogic.Instance.Companies[0].OffersPublished[0]);
            string expected = AppLogic.Instance.GetPeriodTimeOffersAccepted(AppLogic.Instance.Entrepreneurs[0],3);
            string actual = "200 Reciclable at a price of 75$\nUsted ah aceptado 1 ofertas en los ultimos 3 días";
            Assert.AreEqual(expected,actual);
        }*/
    }
}
