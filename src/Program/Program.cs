//--------------------------------------------------------------------------------
// <copyright file="Program.cs" company="Universidad Católica del Uruguay">
//     Copyright (c) Programación II. Derechos reservados.
// </copyright>
//--------------------------------------------------------------------------------

using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot;
using Telegram.Bot.Types;                                                                                                       
using Telegram.Bot.Types.Enums;
using Proyect;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System.Collections.Generic;
namespace Ucu.Poo.TelegramBot
{
    /// <summary>
    /// Un programa que implementa un bot de Telegram.
    /// </summary>
    public static class Program
    {
        // La instancia del bot.
        private static TelegramBotClient Bot;

        // El token provisto por Telegram al crear el bot.
        //
        // *Importante*:
        // Para probar este ejemplo, crea un bot nuevo y eeemplaza este token por el de tu bot.
        private static string Token = "2101777088:AAFz3DmdRIvOU2omgOF17kozROw2kIY5WJY";

        private static IHandler firstHandler;

        /// <summary>
        /// Punto de entrada al programa.
        /// </summary>
        public static void Main()
        {
            if (!System.IO.File.Exists(@"../Library/Persistencia/Rubros.json"))
            {
                string json = AppLogic.Instance.ConvertToJson(AppLogic.Instance.Rubros);
                System.IO.File.WriteAllText(@"../Library/Persistencia/Rubros.json", json);
            }
            if (!System.IO.File.Exists(@"../Library/Persistencia/Habilitaciones.json"))
            {
                string json = AppLogic.Instance.ConvertToJson(AppLogic.Instance.Qualifications);
                System.IO.File.WriteAllText(@"../Library/Persistencia/Habilitaciones.json", json);
            }
            if (!System.IO.File.Exists(@"../Library/Persistencia/ClasificacionesProductos.json"))
            {
                string json = AppLogic.Instance.ConvertToJson(AppLogic.Instance.Classifications);
                System.IO.File.WriteAllText(@"../Library/Persistencia/ClasificacionesProductos.json", json);
            }
            if (!System.IO.File.Exists(@"../Library/Persistencia/Emprendedores.json"))
            {
                string json = AppLogic.Instance.ConvertToJson(AppLogic.Instance.Entrepreneurs);
                System.IO.File.WriteAllText(@"../Library/Persistencia/Emprendedores.json", json);
            }else
            {
                AppLogic.Instance.Entrepreneurs = AppLogic.Instance.DeserializeEntrenprenuers();
            }
            if (!System.IO.File.Exists(@"../Library/Persistencia/Companias.json"))
            {
                string json = AppLogic.Instance.ConvertToJson(AppLogic.Instance.Companies);
                System.IO.File.WriteAllText(@"../Library/Persistencia/Companias.json", json);
            }else
            {
                AppLogic.Instance.Companies = AppLogic.Instance.DeserializeCompanies();
            }

            Bot = new TelegramBotClient(Token);

            firstHandler = new StartHandler(
                            new AutorizationHandler(
                            new CancelHandler(
                            new RegisterHandler(
                            new PublicOfferHandler(
                            new GetConstantMaterialsHandler(
                            new CompanyMyOfferHandler(
                            new SearchOfferHandler(
                            new PurchasedOfferHandler(null)))))))));

            var cts = new CancellationTokenSource();

            // Comenzamos a escuchar mensajes. Esto se hace en otro hilo (en background). El primer método
            // HandleUpdateAsync es invocado por el bot cuando se recibe un mensaje. El segundo método HandleErrorAsync
            // es invocado cuando ocurre un error.
            Bot.StartReceiving(
                new DefaultUpdateHandler(HandleUpdateAsync, HandleErrorAsync),
                cts.Token
            );

            Console.WriteLine($"Bot is up!");

            // Esperamos a que el usuario aprete Enter en la consola para terminar el bot.
            Console.ReadLine();

            // Terminamos el bot.
            cts.Cancel();

            System.IO.File.WriteAllText(@"../Library/Persistencia/Habilitaciones.json", AppLogic.Instance.ConvertToJson(AppLogic.Instance.Qualifications));
            System.IO.File.WriteAllText(@"../Library/Persistencia/ClasificacionesProductos.json", AppLogic.Instance.ConvertToJson(AppLogic.Instance.Classifications));
            System.IO.File.WriteAllText(@"../Library/Persistencia/Rubros.json", AppLogic.Instance.ConvertToJson(AppLogic.Instance.Rubros));
            System.IO.File.WriteAllText(@"../Library/Persistencia/Emprendedores.json", AppLogic.Instance.ConvertToJson(AppLogic.Instance.Entrepreneurs));
            System.IO.File.WriteAllText(@"../Library/Persistencia/Companias.json", AppLogic.Instance.ConvertToJson(AppLogic.Instance.Companies));
        }

        /// <summary>
        /// Maneja las actualizaciones del bot (todo lo que llega), incluyendo mensajes, ediciones de mensajes,
        /// respuestas a botones, etc. En este ejemplo sólo manejamos mensajes de texto.
        /// </summary>
        public static async Task HandleUpdateAsync(Update update, CancellationToken cancellationToken)
        {
            try
            {
                // Sólo respondemos a mensajes de texto
                if (update.Type == UpdateType.Message)
                {
                    await HandleMessageReceived(new TelegramAdapter(update.Message));
                }
            }
            catch(Exception e)
            {
                await HandleErrorAsync(e, cancellationToken);
            }
        }

        /// <summary>
        /// Maneja los mensajes que se envían al bot.
        /// Lo único que hacemos por ahora es escuchar 3 tipos de mensajes:
        /// - "hola": responde con texto
        /// - "chau": responde con texto
        /// - "foto": responde con una foto
        /// </summary>
        /// <param name="message">El mensaje recibido</param>
        /// <returns></returns>
        private static async Task HandleMessageReceived(IMessage message)
        {
            Console.WriteLine($"Received a message from {message.Id} saying: {message.Text}");

            string response = string.Empty;

            firstHandler.Handle(message, out response);

            if (!string.IsNullOrEmpty(response))
            {
                await Bot.SendTextMessageAsync(message.MsgId, response);
            }
        }

        /// <summary>
        /// Manejo de excepciones. Por ahora simplemente la imprimimos en la consola.
        /// </summary>
        public static Task HandleErrorAsync(Exception exception, CancellationToken cancellationToken)
        {
            Console.WriteLine(exception.Message);
            return Task.CompletedTask;
        }
    }
}