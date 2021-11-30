using NUnit.Framework;
using Ucu.Poo.TelegramBot;
using Telegram.Bot.Types;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot;
using Proyect;

namespace Tests
{
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
        IMessage message = new MessageTest();



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

            

        }

        [Test]
        public void TestRegisterHandlesRegister()
        {
            message.Text = registerHandler.Keywords[0];
            string response;

            IHandler result = handler.Handle(message,out response);
            Assert.That(result, Is.Not.Null);
            Assert.That(response, Is.EqualTo("Bienvenido a C4BOT\n\n¿Posee un Token?\nIngrese /si si tiene uno y asi registrarse como empresa o /no si no lo tiene y registrarse como emprendedor"));
        }

    }
}