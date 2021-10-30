using System; 

namespace Proyect
{
    /// <summary>
    /// Clase que procesa los mensajes de telegram (en este caso va a procesar los mensajes de consola para probra la logica)
    /// </summary>
    public class MessageProcesor
    {

        /// <summary>
        /// Metodo que 
        /// </summary>
        public void RegistEnterprenuer()
        {
            Console.WriteLine("Para registarse necesitamos algunos de sus datos, como el nombre, ubicacion actual, el rubro al cual pertenece, sus habilitaciones y especializaciones");
            Console.WriteLine("Nombre:");
            string name = Console.ReadLine();
            Console.WriteLine("Ubicacion:");
            string ubication = Console.ReadLine();
            Console.WriteLine(AppLogic.Instance.ValidRubrosMessage());
            Console.WriteLine("Rubro:");
            bool hasta = true;
            while (hasta)
            {
                string rubro = Console.ReadLine();
                if (Rubros.Contains(rubro))
                {
                    hasta = false;
                }
                else
                {
                    Console.WriteLine($"El rubro '{rubro}' es invalido, intente de nuevo");
                }
            }
        }

    }

    
}