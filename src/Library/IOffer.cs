using System.Collections.Generic;
using System.Collections;
using System;

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
        List<Qualifications> Qualifications{get; set;}

        /// <summary>
        /// Obtiene la lista de palabras clave de una oferta.
        /// </summary>
        /// <value>keyWords</value>
        ArrayList KeyWords{get;set;}

        /// <summary>
        /// Obtiene la informacion de compra de la oferta.
        /// </summary>
        /// <returns>mensaje con la infromacion de compra de todas sus ofertas.</returns>
        string GetPurchesedData();

        /// <summary>
        /// Obtien todas las ofertas que le fueron aceptadas en un periodo de tiempo.
        /// </summary>
        /// <param name="periodTime"></param>
        /// <returns>mensaje con la informacion de compra de sus ofertas</returns>
        string GetPeriodTimeOffersAcceptedData(int periodTime);

        /// <summary>
        /// Obtiene la fecha en la que el imprendedor ingresado acepto la oferta.
        /// </summary>
        /// <param name="emprendedor"></param>
        DateTime GetOfferBuyerTimeData(Emprendedor emprendedor);

        /// <summary>
        /// Le asigna el emprendedor y la fecha de compra a la oferta, una vez es comprada.
        /// </summary>
        /// <param name="emprendedor"></param>
        /// <param name="timeAccepted"></param>
        void PutBuyer(Emprendedor emprendedor, DateTime timeAccepted);
    }
}