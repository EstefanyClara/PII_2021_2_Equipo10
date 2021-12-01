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
    /// </summary>
    public class ChainOfResponsibilityTests
    {
        IHandler handler;
        AutorizationHandler autorizationHandler = new AutorizationHandler(null);
        CancelHandler cancelHandler = new CancelHandler(null);
        RegisterHandler registerHandler = new RegisterHandler(null);
        PublicOfferHandler publicOfferHandler = new PublicOfferHandler(null);
        GetConstantMaterialsHandler getConstantMaterialsHandler = new GetConstantMaterialsHandler(null);
        CompanyMyOfferHandler companyMyOfferHandler = new CompanyMyOfferHandler(null);
        SearchOfferHandler searchOfferHandler = new SearchOfferHandler(null);
        PurchasedOfferHandler purchasedOfferHandler = new PurchasedOfferHandler(null);
        AdministratorHandler administratorHandler = new AdministratorHandler(null);
        IMessage message;
        Rubro rubro;


        /// <summary>
        /// Setup de los handlers y mensaje.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            autorizationHandler.Next = cancelHandler;
            cancelHandler.Next = registerHandler;
            registerHandler.Next = publicOfferHandler;
            publicOfferHandler.Next = getConstantMaterialsHandler;
            getConstantMaterialsHandler.Next = companyMyOfferHandler;
            companyMyOfferHandler.Next = searchOfferHandler;
            searchOfferHandler.Next = purchasedOfferHandler;
            purchasedOfferHandler.Next = administratorHandler;

            handler = autorizationHandler;

            message = new MessageTest("761714026",1234);
            rubro = new Rubro("Alimentos");
            

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
            Assert.That(response, Is.EqualTo("Bienvenido a C4BOT\n\n¿Posee un Token?\nIngrese /si si tiene uno y asi registrarse como empresa o /no si no lo tiene y registrarse como emprendedor"));
        }

        [Test]
        public void TestCancelHandlerCancels()
        {
            message.Text = cancelHandler.Keywords[0];
            string response;

            List<List<string>> lista = new List<List<string>>() {new List<string>(),new List<string>()};
            DataUserContainer.Instance.UserDataHistory.Add(message.Id, lista);

            IHandler result = handler.Handle(message,out response);
            Assert.That(result,Is.Not.Null);
            Assert.That(response, Is.EqualTo("Regresando al estado inicial..."));
        }

        [Test]
        public void TestPublicOfferHandlerPublishes()
        {
            Company c = new Company("761714026","LucasCorp","Tres cruces",rubro,"lukaszury@gmail.com");
            AppLogic.Instance.Companies.Add(c);

            message.Text = publicOfferHandler.Keywords[0];
            string response;

            List<List<string>> lista = new List<List<string>>() {new List<string>(),new List<string>()};
            DataUserContainer.Instance.UserDataHistory.Add(message.Id, lista);
            DataUserContainer.Instance.UserDataHistory[message.Id][0][0] = "/buscar";

            IHandler result = handler.Handle(message,out response);
            Assert.That(result,Is.Not.Null);
            Assert.That(response, Is.EqualTo("¿La oferta que desea publicar es constante?(/si o /no)\n\nUna oferta constante son aquellas que siempre estan disponibles y las pueden aceptar varios emprendedores, mientras que las no constante solo la puede aceptar un emprendedor"));
        }

    }
}