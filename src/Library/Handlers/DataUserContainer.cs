

namespace Proyect
{
    /// <summary>
    /// Clase contenedora del historia de mensajes que un usuario manda
    /// </summary>
    public sealed class DataUserContainer
    {
        private readonly static DataUserContainer _instance = new DataUserContainer();

        private DataUserContainer client;

        private DataUserContainer()
        {
            this.client = new DataUserContainer();
            
        }

    }
}