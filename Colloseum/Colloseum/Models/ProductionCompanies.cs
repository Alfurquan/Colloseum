using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Colloseum.Models
{
    public class ProductionCompanies
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "origin_country")]
        public string OriginCountry { get; set; }

        [JsonProperty(PropertyName = "logo_path")]
        public string LogoPath { get; set; }
    }
}
