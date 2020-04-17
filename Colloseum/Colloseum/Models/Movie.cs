using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Colloseum.Models
{
    public class Movie
    {
        [JsonProperty("popularity")]
        public double Popularity { get; set; }
        [JsonProperty("vote_count")]
        public int VoteCount { get; set; }
        [JsonProperty("video")]
        public bool Video { get; set; }
        [JsonProperty("poster_path")]
        public string PosterPath { get; set; }
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("adult")]
        public bool Adult { get; set; }
        [JsonProperty("backdrop_path")]
        public string BackdropPath { get; set; }
        [JsonProperty("original_language")]
        public string OriginalLanguage { get; set; }
        [JsonProperty("original_title")]
        public string OriginalTitle { get; set; }
        [JsonProperty("genre_ids")]
        public List<int> GenreIds { get; set; }
        [JsonProperty("genres")]
        public List<Genre> Genres { get; set; }

        [JsonIgnore]
        public string GenresNames
        {
            get
            {
                return (Genres != null && Genres.Count() > 0) ?
                    Genres.Select(g => g.Name).Aggregate((first, second) => $"{first}, {second}") :
                    "Undefined";
            }
        }

        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("vote_average")]
        public double VoteAverage { get; set; }
        [JsonProperty("overview")]
        public string Overview { get; set; }
        [JsonProperty("release_date")]
        public DateTime ReleaseDate { get; set; }
    }
}
