using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Prism.Commands;

namespace Netflix.Models
{
    public class MovieModel
    {
        [JsonProperty("Thumbnail")]
        public string Thumbnail { get; set; }

        [JsonProperty("Title")]
        public string Title { get; set; }
    
        [JsonProperty("Genre")]
        public string[] Genre { get; set; }
    
        [JsonProperty("IsFeatured")]
        public bool IsFeatured { get; set; }
    
        [JsonProperty("Synopsis")]
        public string Synopsis { get; set; }

        [JsonProperty("IsPopular")]
        public bool IsPopular { get; set; }

        [JsonProperty("IsAction")]
        public bool IsAction { get; set; }

        [JsonProperty("IsComedy")]
        public bool IsComedy { get; set; }

        [JsonProperty("Casts")]
        public string Casts { get; set; }

        public DelegateCommand<MovieModel> ShowInfoCommand { get; set; }

        [JsonProperty("Year")]
        public string Year { get; set; }

        [JsonProperty("Rating")]
        public double Rating { get; set; }

        [JsonProperty("InfoThumbnail")]
        public string InfoThumbnail { get; set; }

        [JsonIgnore]
        public int EpisodeNumber { get; set; }
    }
}
