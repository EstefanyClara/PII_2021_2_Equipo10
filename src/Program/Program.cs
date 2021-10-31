//--------------------------------------------------------------------------------
// <copyright file="Program.cs" company="Universidad Católica del Uruguay">
//     Copyright (c) Programación II. Derechos reservados.
// </copyright>
//--------------------------------------------------------------------------------

using Proyect;
using System;
using System.Collections.Generic;
using System.Collections;
using System.Threading.Tasks;
using LocationApi;

namespace ConsoleApplication
{
    /// <summary>
    /// Programa de consola de demostración.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// Punto de entrada al programa principal.
        /// </summary>
        public static async Task Main()
        {
            AppLogic.Instance.RegisterEntrepreneurs("Matias", "Cordoba", AppLogic.Instance.Rubros[0], AppLogic.Instance.Qualifications, AppLogic.Instance.Qualifications);
            AppLogic.Instance.RegisterEntrepreneurs("Matias", "Cordoba", AppLogic.Instance.Rubros[1], new List<Qualifications>(){AppLogic.Instance.Qualifications[0],AppLogic.Instance.Qualifications[1]}, new List<Qualifications>(){AppLogic.Instance.Qualifications[0],AppLogic.Instance.Qualifications[1]});
            Company c1 = new Company("MatiasCorp", "Parque Rodó",AppLogic.Instance.Rubros[1] );
            AppLogic.Instance.PublicOffer(c1,true,AppLogic.Instance.Classifications[3], 300, 5000, "Parque Rodó", AppLogic.Instance.Qualifications, new ArrayList(){"Toxicos","Grandes volumenes"});
            await AppLogic.Instance.ObteinOfferMap(c1.OffersPublished[0]);
            Console.WriteLine(AppLogic.Instance.ValidRubrosMessage());
            Console.WriteLine(AppLogic.Instance.validQualificationsMessage());        
            }
    }
}