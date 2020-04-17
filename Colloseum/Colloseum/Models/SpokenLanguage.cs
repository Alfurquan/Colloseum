using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Colloseum.Models
{
    public class SpokenLanguage
    {
        [JsonProperty(PropertyName = "iso_639_1")]
        public string Iso6391 { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
    }
}
