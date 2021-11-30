using System.Collections.Generic;
using System.Collections;
using System;
using System.Text.Json;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace Proyect
{
    /// <summary>
    /// Interfaz para las ofertas de una compania.
    /// </summary>
    public interface IOffer
    {
        /// <summary>
        /// Obtiene el producto de la oferta.
        /// </summary>
        /// <value></value>
        ProductOffer Product{get;}

        /// <summary>
        /// Obtiene las habilitaciones de la oferta.
        /// </summary>
        /// <value>qualifications</value>
        IList<Qualifications> Qualifications{get; set;}

        /// <summary>
        /// Obtiene la lista de palabras clave de una oferta.
        /// </summary>
        /// <value>keyWords</value>
        ArrayList KeyWords{get;set;}

        /// <summary>
        /// Obtiene la fecha de publicacion de la oferta.
        /// </summary>
        /// <value>DateTime</value>
        string DatePublished{get;}

        /// <summary>
        /// La informacion de compra de la oferta.
        /// </summary>
        /// <value></value>
        IList<PurchaseData> PurchesedData{get;}

        /// <summary>
        /// Obtien todas las ofertas que le fueron aceptadas en un periodo de tiempo.
        /// </summary>
        /// <param name="periodTime"></param>
        /// <returns>mensaje con la informacion de compra de sus ofertas</returns>
        IList<PurchaseData> GetPeriodTimeOffersAcceptedData(int periodTime);

        /// <summary>
        /// Obtiene la infromacion de compra del emprendedor indicado, en el tiempo establecido.
        /// </summary>
        /// <param name="periodTime"></param>
        /// <param name="emprendedor"></param>
        /// <returns></returns>
        IList<PurchaseData> GetPeriodTimeOffersAcceptedData(int periodTime, Emprendedor emprendedor);

        /// <summary>
        /// Obtiene todos los datos de compra de la oferta, del emprendedor indicado.
        /// </summary>
        /// <param name="emprendedor"></param>
        /// <returns></returns>
        IList<PurchaseData> GetEntrepreneursPurchaseData(Emprendedor emprendedor);
        /// <summary>
        /// Le asigna el emprendedor y la fecha de compra a la oferta, una vez es comprada.
        /// </summary>
        /// <param name="emprendedor"></param>
        bool PutBuyer(Emprendedor emprendedor);
    }
}