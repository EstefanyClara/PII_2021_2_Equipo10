using System;
using System.Collections.Generic;

namespace Proyect
{
    /// <summary>
    /// Clase contenedora del historia de mensajes que un usuario manda
    /// </summary>
    public sealed class DataUserContainer
    {
        private readonly static DataUserContainer _instance = new DataUserContainer();

        private Dictionary<string,List<List<string>>> userDataHistory;

        private Dictionary<string,List<IOffer>> userOfferDataSelection;

        private DataUserContainer()
        {
            this.userDataHistory = new Dictionary<string, List<List<string>>> ();
        }

        /// <summary>
        /// Obtiene la instancia de la clase.
        /// </summary>
        /// <value>La instancia de la clase.</value>
        public static DataUserContainer Instance
        {
            get{return _instance;}
        }

        /// <summary>
        /// Obtiene el diccionario donde se guardan los mensajes de un usuario.
        /// </summary>
        /// <value>Diccionario con clave el chat ID de un usuario, y lista de la histroia de usuario.</value>
        public Dictionary<string,List<List<string>>> UserDataHistory
        {
            get{return this.userDataHistory;}
        }

        /// <summary>
        /// Obtiene el diccionario donde se guardan las ofertas que selecciono el usuario mientras usaba la aplicacion.
        /// </summary>
        /// <value></value>
        public Dictionary<string, List<IOffer>> UserOfferDataSelection
        {
            get{return this.userOfferDataSelection;}
        }

    }
}