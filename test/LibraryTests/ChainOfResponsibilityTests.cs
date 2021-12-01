using NUnit.Framework;
using Ucu.Poo.TelegramBot;
using Telegram.Bot.Types;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot;
using System.Collections.Generic;
using Proyect;

namespace Tests
{


    /// <summary>
    /// Test para probar la parte de delegacion de los Handlers.
    /// Los test funcionan correctamente de manera individual, al ejetucarlos con el comando dotnet test se encuentram fallas.
    /// </summary>
    public class ChainOfResponsibilityTests
    {
        IHandler handler;
        AutorizationHandler autorizationHandler = new AutorizationHandler(null);
        CancelHandler cancelHandler = new CancelHandler(null);
        MeHandler meHandler = new MeHandler(null);
        RegisterHandler registerHandler = new RegisterHandler(null);
        PublicOfferHandler publicOfferHandler = new PublicOfferHandler(null);
        GetConstantMaterialsHandler getConstantMaterialsHandler = new GetConstantMaterialsHandler(null);
        CompanyMyOfferHandler companyMyOfferHandler = new CompanyMyOfferHandler(null);
        SearchOfferHandler searchOfferHandler = new SearchOfferHandler(null);
        PurchasedOfferHandler purchasedOfferHandler = new PurchasedOfferHandler(null);
        AdministratorHandler administratorHandler = new AdministratorHandler(null);
        IMessage message;
        Rubro rubro;
        Qualifications qualifications;
        List<Qualifications> qualifications1;
        Company company;
        Emprendedor emprendedor;



        /// <summary>
        /// Setup de los handlers y mensaje.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            autorizationHandler.Next = cancelHandler;
            cancelHandler.Next = getConstantMaterialsHandler;
            getConstantMaterialsHandler.Next = meHandler;
            meHandler.Next = companyMyOfferHandler;
            companyMyOfferHandler.Next = searchOfferHandler;
            searchOfferHandler.Next = purchasedOfferHandler;
            purchasedOfferHandler.Next = administratorHandler;
            administratorHandler.Next = registerHandler;
            registerHandler.Next = publicOfferHandler;

            handler = autorizationHandler;

            message = new MessageTest("761714026",1234);
            rubro = new Rubro("Alimentos");
            qualifications = new Qualifications("Coche propio");
            qualifications1 = new List<Qualifications>(){qualifications};
            company = new Company("761714026","LucasCorp","Tres cruces",rubro,"lukaszury@gmail.com");
            AppLogic.Instance.Companies.Add(company);
            emprendedor = new Emprendedor("761714026","Lucas","Tres cruces",rubro,"Lukaszury@gmai.com",qualifications1,"-");
            AppLogic.Instance.Entrepreneurs.Add(emprendedor);

        }

        /// <summary>
        /// Se testea que el handler de autorización delegue la responsabilidad
        /// de registrarse al handler de registro
        /// </summary>
        [Test]
        public void TestAuthorizarionDelegatesRegister()
        {
            message.Text = autorizationHandler.Keywords[0];

            string response;

            IHandler result = handler.Handle(message,out response);
            Assert.That(result, Is.Not.Null);
            Assert.That(response, Is.EqualTo("Usted ya se encuentra registrado"));
        }

        [Test]
        public void TestCancelHandlerCancels()
        {
            message.Text = cancelHandler.Keywords[0];
            string response;

            // List<List<string>> lista = new List<List<string>>() {new List<string>(),new List<string>()};
            // DataUserContainer.Instance.UserDataHistory.Add(message.Id, lista);

            IHandler result = handler.Handle(message,out response);
            Assert.That(result,Is.Not.Null);
            Assert.That(response, Is.EqualTo("Usted no se encuentra en ningún estado especifico."));
        }

        [Test]
        public void TestPublicOfferHandlerPublishes()
        {
            message.Text = publicOfferHandler.Keywords[0];
            string response;

            List<List<string>> lista = new List<List<string>>() {new List<string>(),new List<string>()};
            // DataUserContainer.Instance.UserDataHistory.Add(message.Id, lista);
            // DataUserContainer.Instance.UserDataHistory[message.Id][0].Add("/public");

            IHandler result = handler.Handle(message,out response);
            Assert.That(result,Is.Not.Null);
            Assert.That(response, Is.EqualTo("¿La oferta que desea publicar es constante?(/si o /no)\n\nUna oferta constante son aquellas que siempre estan disponibles y las pueden aceptar varios emprendedores, mientras que las no constante solo la puede aceptar un emprendedor"));
        }

        [Test]
        public void TestMeHandlerShowInfo()
        {
            Company company = new Company("761714026","LucasCorp","Tres cruces",rubro,"lukaszury@gmail.com");
            AppLogic.Instance.Companies.Add(company);

            message.Text = meHandler.Keywords[0];
            string response;

            // List<List<string>> lista = new List<List<string>>() {new List<string>(),new List<string>()};
            // DataUserContainer.Instance.UserDataHistory.Add(message.Id, lista);

            IHandler result = handler.Handle(message,out response);
            Assert.That(result,Is.Not.Null);
            Assert.That(response, Is.EqualTo($"Nombre: {company.Name}\nRubro al que pertenece: {company.Rubro.RubroName}\nUbicación: {company.Ubication}\nContacto: {company.User_Contact}"));
        }

        [Test]
        public void TestSearchOfferHandlerSearches()
        {
            

            message.Text = searchOfferHandler.Keywords[0];
            string response;

            IHandler result = handler.Handle(message,out response);
            Assert.That(result, Is.Not.Null);
            Assert.That(response, Is.EqualTo("Indique como quiere buscar ofertas\n/1 - Palabra clave.\n/2 - Ubicacíon\n/3 - Clasificación"));
        }

        [Test]
        public void TestPurchasedOfferHandlerOffers()
        {
            

            message.Text = purchasedOfferHandler.Keywords[0];
            string response;

            IHandler result = handler.Handle(message,out response);
            Assert.That(result, Is.Not.Null);
            Assert.That(response, Is.EqualTo($"Estas son las ofertas publicadas, que fueron aceptadas por emprendedores:\n"));
        }
    }
}