using System;
using System.Text;
using System.Linq;

namespace Proyect
{
    /// <summary>
    /// Clase base para implementar el patrón Chain of Responsibility.
    /// </summary>
    public class MeHandler : BaseHandler
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="AutorizationHandler"/>. Esta clase procesa si la perosna que envio el mensaje esta registrada.
        /// </summary>
        /// <param name="next">El próximo "handler".</param>
        public MeHandler(BaseHandler next) : base(next)
        {
            this.Keywords = new string[] { "/me" };
        }

        /// <summary>
        /// Procesa el mensaje "/registrar" y retorna true; retorna false en caso contrario.
        /// </summary>
        /// <param name="message">El mensaje a procesar.</param>
        /// <param name="response">La respuesta al mensaje procesado.</param>
        /// <returns>true si el mensaje fue procesado; false en caso contrario.</returns>
        protected override bool InternalHandle(IMessage message, out string response)
        {
            if (this.Keywords.Contains(message.Text.ToLower().Replace(" ", "")))
            {
                Company company = AppLogic.Instance.GetCompany(message.Id);
                if (company != null)
                {
                    response = $"Nombre: {company.Name}\nRubro al que pertenece: {company.Rubro.RubroName}\nUbicacíon: {company.Ubication}\nContacto: {company.User_Contact}";
                    return true;
                }
                else
                {
                    Emprendedor emprendedor = AppLogic.Instance.GetEmprendedor(message.Id);
                    StringBuilder mensaje = new StringBuilder();
                    foreach (Qualifications item in emprendedor.Qualifications)
                    {
                        mensaje.Append($"\n-{item.QualificationName}");
                    }
                    response = $"Nombre: {emprendedor.Name}\nRubro al que pertenece: {emprendedor.Rubro}\nUbicacíon actual: {emprendedor.Ubication}\nContacto: {emprendedor.User_Contact}\nHabilitaciones: {mensaje}";
                    return true;
                }
            }
            response = string.Empty;
            return false;
        }
    }
}