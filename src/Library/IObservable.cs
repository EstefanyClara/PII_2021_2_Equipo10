using System.Collections.Generic;

namespace Proyect
{
    /// <summary>
    /// Interfaz observable para las notificaciones a cada emprendedor (Patron observer).
    /// </summary>
    public interface IObservable
    {
        /// <summary>
        /// Obtiene la lista de usuarios emprendedores registrados.
        /// </summary>
        /// <value></value>
        List<IObserver> emprendedoresRegistrados{get;}

        /// <summary>
        /// Agrega un emprendedor a lalista de emprendedores a notificar.
        /// </summary>
        /// <param name="emprendedor"></param>
        /// <returns></returns>
        string AddUser(IObserver emprendedor);

        /// <summary>
        /// Remueve un emprendedor de la lista de emprendedores a notificar.
        /// </summary>
        /// <param name="emprendedor"></param>
        /// <returns></returns>
        string RemoveUser(IObserver emprendedor);
    }
}
