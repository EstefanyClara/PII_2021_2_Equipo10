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
        StartHandler startHandler = new StartHandler(null);
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
            publicOfferHandler.Next = startHandler;

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

        /// <summary>
        /// Se testea si se cancela la operacion actual.
        /// </summary>
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

        /// <summary>
        /// Se testea que se pueda acceder al mensaje para iniciar la publicacion de una oferta.
        /// </summary>
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

        /// <summary>
        /// Se testea que se muestre la lista con los datos de la compania.
        /// </summary>
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

        /// <summary>
        /// Se testea que se muestren las opciones que se verian si un usuario quiere buscar
        /// ya sea por nombre, ubicacion o clasificacion.
        /// </summary>
        [Test]
        public void TestSearchOfferHandlerSearches()
        {
            

            message.Text = searchOfferHandler.Keywords[0];
            string response;

            IHandler result = handler.Handle(message,out response);
            Assert.That(result, Is.Not.Null);
            Assert.That(response, Is.EqualTo("Indique como quiere buscar ofertas\n/1 - Palabra clave.\n/2 - Ubicacíon\n/3 - Clasificación"));
        }

        /// <summary>
        /// Se testea que se muestre las ofertas que se le aceptaron a una compania por los emprendedores.
        /// </summary>
        [Test]
        public void TestPurchasedOfferHandlerOffers()
        {
            

            message.Text = purchasedOfferHandler.Keywords[0];
            string response;

            IHandler result = handler.Handle(message,out response);
            Assert.That(result, Is.Not.Null);
            Assert.That(response, Is.EqualTo($"Estas son las ofertas publicadas, que fueron aceptadas por emprendedores:\n"));
        }

        /// <summary>
        /// Se testea que se muestre el mensaje de bienvenida a cualquier usuario.
        /// </summary>
        [Test]
        public void TestStartHandlerMessage()
        {
            message.Text = startHandler.Keywords[0];
            string response;
            IHandler result = handler.Handle(message,out response);
            Assert.That(result, Is.Not.Null);
            Assert.That(response, Is.EqualTo($"¡Hola! C4Bot es un ChatBot dedicado a conectar organizaciones o empresas que ofertan ciertos productos, con emprendedores que necesitan o hacen uso de esos productos.\n\nLa lógica de este bot esta orientada en un sistema de historia de usuario en donde a partir de un comando ingresado se puede hacer nada más que de ciertos comandos permitidos en el contexto en el que se encuentre.\n\nDentro de las funcionalidades actuales estan las de Registrarse con /Registrar donde decidirá que tipo de usuario será, dependiendo de si cumple ciertos requerimientos.\n\nFuncionalidades para las organizaciones o compañías:\nCon /Public podrá publicar una oferta, siguiendo los pasos correspondientes.\nCon /MisOfertas podrá ver todas su ofertas publicadas, y gestionar las mismas modificandolas o eliminandolas.\nCon /MisOfertasAceptadas podra ver todas las ofertas que publicó que fueron aceptadas por emprendedores.\nCon /MaterialesConstantes podra obtener información acerca de cuantas ofertas constantes tiene cierto tipo de material.\n Con /Me podra ver todos sus datos.\n\nFuncionalidades para los emprendedores:\nCon /MaterialesConstantes podra obtener información acerca de cuantas ofertas constantes tiene cierto tipo de material para asi poder regular sus insumos.\nCon /MisOfertasAceptadas podrá ver una lista de ofertas que acepto con la información de compra.\nCon /Buscar podra buscar las ofertas por palabra calve, ubicacion o nombre.\nCon /Me podra ver todos sus datos.\n\nCon /Cancel cualquier usuario podra cancelar su operacion actual, así y volver al principio."));

        }
    }
}