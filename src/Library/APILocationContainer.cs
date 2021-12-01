using Ucu.Poo.Locations.Client;
namespace Proyect
{
    /// <summary>
    /// Contenedor de la instancia de la api de localizacion (clase singleton, solo hay una instancia).
    /// </summary>
    public sealed class APILocationContainer
    {
        private readonly static APILocationContainer _instance = new APILocationContainer();

        private LocationApiClient client;

        private APILocationContainer()
        {

            this.client = new LocationApiClient();

        }

        /// <summary>
        /// Obtiene la instancia de la instancia del contenedor de la api.
        /// </summary>
        /// <value>_instance</value>
        public static APILocationContainer Instance
        {

            get { return _instance; }

        }

        /// <summary>
        /// Obtiene la instancia de la api de localizacion.
        /// </summary>
        /// <value>this.client</value>
        public LocationApiClient APIdeLocalizacion
        {

            get { return this.client; }

        }

    }
}