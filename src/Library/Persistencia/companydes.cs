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
    public class CompanyDes : IJsonConvertible
    {
        [JsonInclude]
        public IList<Company> companias {get;set;}

        [JsonConstructor]
        public CompanyDes()
        {
        }

        public string ConvertToJson()
        {
            JsonSerializerOptions options = new()
            {
                ReferenceHandler = MyReferenceHandler.Preserve,
                WriteIndented = true
            };
            foreach(Company value in this.companias)
            {
                foreach(IOffer item in value.OffersPublished)
                    {
                            if(item.GetType().Equals(typeof(ConstantOffer)))
                            {
                                    value.OfertasConstantes.Add(item as ConstantOffer);
                            }else
                            {
                                    value.OfertasNoConstantes.Add(item as NonConstantOffer);
                        }
                    }
                    value.OffersPublished.Clear();
            }
            return System.Text.Json.JsonSerializer.Serialize(this, options);
        }
    }
}