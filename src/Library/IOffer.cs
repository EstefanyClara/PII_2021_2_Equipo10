using System.Collections.Generic;
using System.Collections;

namespace Proyect
{
    /// <summary>
    /// Interfaz para las ofertas de una compania, la utilizaron ambos tipos de ofertas, y si se quieren agrregar mas, tambien habran de utilizar esta interfaz.
    /// </summary>
    public interface IOffer
    {
        /// <summary>
        /// Propiedad get y set del producto de la oferta.
        /// </summary>
        /// <value>El producto.</value>
        ProductOffer Product { get; set; }

        /// <summary>
        /// Propiedad get y set de las habilitaciones de la oferta.
        /// </summary>
        /// <value>Las habiliatciones.</value>
        IList<Qualifications> Qualifications { get; set; }

        /// <summary>
        /// Propierti de la lista de palabras clave de una oferta.
        /// </summary>
        /// <value>keyWords.</value>
        ArrayList KeyWords { get; set; }

        /// <summary>
        /// Propierti de la fecha de publicacion de la oferta.
        /// </summary>
        /// <value>DateTime</value>
        string DatePublished { get; set; }

        /// <summary>
        /// La informacion de compra de la oferta.
        /// </summary>
        /// <value>La lisat con la informacion de compra de la oferta.</value>
        IList<PurchaseData> PurchesedData { get; set; }

        /// <summary>
        /// Obtien toda la informacion de compra (compardor y fecha de compra), del peridodo de tiempo indicado.
        /// Por expert le asiganmaos esta responsabiliad (Es la que tiene la lisat de informacion de compra).
        /// </summary>
        /// <param name="periodTime"></param>
        /// <returns>La lisat con la infromacion de compra.</returns>
        IList<PurchaseData> GetPeriodTimeOffersAcceptedData(int periodTime);

        /// <summary>
        /// Obtiene la infromacion de compra del emprendedor indicado, en el perido de tiempo establecido.
        /// Por expert le asiganmaos esta responsabiliad (Es la que tiene la lisat de informacion de compra).
        /// </summary>
        /// <param name="periodTime">El perido de tiempo.</param>
        /// <param name="emprendedor">El emprendedor.</param>
        /// <returns>La lista con la inforamcion de compra.</returns>
        IList<PurchaseData> GetPeriodTimeOffersAcceptedData(int periodTime, Emprendedor emprendedor);

        /// <summary>
        /// Obtiene todos los datos de compra de la oferta del emprendedor indicado.
        /// Por expert le asiganmaos esta responsabiliad (Es la que tiene la lisat de informacion de compra).
        /// </summary>
        /// <param name="emprendedor">El emprendedor.</param>
        /// <returns>La informacion de compra.</returns>
        IList<PurchaseData> GetEntrepreneursPurchaseData(Emprendedor emprendedor);

        /// <summary>
        /// Le asigna el emprendedor y la fecha de compra a la oferta, una vez es comprada (La informacion de compra).
        /// Por expert le asiganmaos esta responsabiliad (Es la que tiene la lista de informacion de compra).
        /// </summary>
        /// <param name="emprendedor">El emprendedor.</param>
        /// <returns>Mensaje de confirmacion.</returns>
        bool PutBuyer(Emprendedor emprendedor);
    }
}