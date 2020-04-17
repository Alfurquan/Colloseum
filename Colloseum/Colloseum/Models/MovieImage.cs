using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Colloseum.Models
{
    public class MovieImage
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "backdrops")]
        public List<Image> Backdrops { get; set; }

        [JsonProperty(PropertyName = "posters")]
        public List<Image> Posters { get; set; }
    }
}
