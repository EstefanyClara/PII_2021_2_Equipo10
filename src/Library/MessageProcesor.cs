using System; 
using System.Collections.Generic;

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
            int rubroIndex = Convert.ToInt32(Console.ReadLine());
            Rubro rubro = AppLogic.Instance.Rubros[rubroIndex-1];
            Console.WriteLine(AppLogic.Instance.validQualificationsMessage());
            Console.WriteLine("Hanilitaciones:");
            int habilitacionesIndex = Convert.ToInt32(Console.ReadLine());
            List<Qualifications> habilitaciones = new List<Qualifications>(){AppLogic.Instance.Qualifications[habilitacionesIndex-1]};
            AppLogic.Instance.RegisterEntrepreneurs(name,ubication,rubro,habilitaciones,habilitaciones);
            }
        }

    }

    
