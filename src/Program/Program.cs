//--------------------------------------------------------------------------------
// <copyright file="Program.cs" company="Universidad Católica del Uruguay">
//     Copyright (c) Programación II. Derechos reservados.
// </copyright>
//--------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Proyect;

namespace ConsoleApplication
{
    /// <summary>
    /// Programa de consola de demostración.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// Main.
        /// </summary>
        /// <returns>Task.</returns>
        public static async Task Main()
        {
            AppLogic.Instance.RegisterEntrepreneurs("Matias", "Palacio Legislativo", AppLogic.Instance.Rubros[0], AppLogic.Instance.Qualifications, new ArrayList(){"Desechos organicos"});
            AppLogic.Instance.RegisterEntrepreneurs("Matias", "Cordoba", AppLogic.Instance.Rubros[1], new List<Qualifications>(){AppLogic.Instance.Qualifications[0], AppLogic.Instance.Qualifications[1]}, new ArrayList(){"Desechos plasticos"});
            Company c1 = new Company("MatiasCorp", "Parque Rodó", AppLogic.Instance.Rubros[1]);
            Company c2 = new Company("LucasCorp","Pocitos",AppLogic.Instance.Rubros[0]);
            AppLogic.Instance.Companies.Add(c1);
            AppLogic.Instance.Companies.Add(c2);
            AppLogic.Instance.PublicConstantOffer(c1, AppLogic.Instance.Classifications[3], 300, 5000, "Parque Rodó", AppLogic.Instance.Qualifications, new ArrayList(){"Toxicos","Grandes volumenes"});
            AppLogic.Instance.AccepOffer(AppLogic.Instance.Entrepreneurs[0], c1.OffersPublished[0]);
            await AppLogic.Instance.ObteinOfferMap(c1.OffersPublished[0]).ConfigureAwait(true);
            Console.WriteLine(await AppLogic.Instance.ObteinOfferDistance(AppLogic.Instance.Entrepreneurs[0], c1.OffersPublished[0]).ConfigureAwait(true) + " Kilometers");
            Console.WriteLine(AppLogic.Instance.ValidRubrosMessage());
            Console.WriteLine(AppLogic.Instance.validQualificationsMessage());
            Console.WriteLine(AppLogic.Instance.GetConstantMaterials());
            Console.WriteLine(AppLogic.Instance.GetOffersAccepted(c1));
            Console.WriteLine(AppLogic.Instance.GetOffersAccepted(AppLogic.Instance.Entrepreneurs[0]));
            Console.WriteLine(AppLogic.Instance.SearchOfferByType("Toxicos")[0]);
            Console.WriteLine("-----------------------------------------------------------------------");

            #region LucasTest
            
            Classification tipo = new Classification("Reciclable");
            ArrayList keyWords = new ArrayList();
            ArrayList keyWords2 = new ArrayList();
            keyWords2.Add("Reciclable");
            keyWords2.Add("Toxicos");
            List<Qualifications> qualifications = new List<Qualifications>();
            ArrayList specializations = new ArrayList();

            Qualifications q1 = new Qualifications("Calificacion1");
            qualifications.Add(q1);
            Qualifications s1 = new Qualifications("Especializacion");
            specializations.Add(s1);

            AppLogic.Instance.PublicConstantOffer(c2,tipo,20,100,"Parque Battle",qualifications,keyWords2);
            Console.WriteLine(AppLogic.Instance.SearchOfferByKeywords("Toxicos")[0]);
            
            #endregion
        }
    }
}