using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Proyect
{
    /// <summary>
    /// Esta clase administrador invita a los usuarios a registarse.
    /// Clase singleton, solo una instancia de administrador. 
    /// </summary>
    public class EmprendedoresDes : IJsonConvertible
    {
        [JsonInclude]
        public IList<Emprendedor> Emprendedores {get; set;}

        [JsonConstructor]
        public EmprendedoresDes()
        {
        }

        public string ConvertToJson()
        {
            JsonSerializerOptions options = new()
            {
                ReferenceHandler = MyReferenceHandler.Preserve,
                WriteIndented = true
            };
            foreach(Emprendedor value in this.Emprendedores)
            {
                foreach(IOffer item in value.PurchasedOffers)
                    {
                            if(item.GetType().Equals(typeof(ConstantOffer)))
                            {
                                    value.OfertasConstantes.Add(item as ConstantOffer);
                            }else
                            {
                                    value.OfertasNoConstantes.Add(item as NonConstantOffer);
                        }
                    }
                    value.PurchasedOffers.Clear();
            }
            return System.Text.Json.JsonSerializer.Serialize(this, options);
        }
    }
}