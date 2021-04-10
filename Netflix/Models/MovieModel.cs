using System;
using Newtonsoft.Json;
using Prism.Commands;
using SQLite;

namespace Netflix.Models
{
    public class MovieDatas
    {
        [JsonProperty("allShows")]
        public MovieModel[] AllMoviesModel { get; set; }

        [JsonProperty("popularShows")]
        public MovieModel[] PopularMoviesModel { get; set; }

        [JsonProperty("actionShows")]
        public MovieModel[] ActionMoviesModel { get; set; }

        [JsonProperty("comedyShows")]
        public MovieModel[] ComedyMoviesModel { get; set; }

        [JsonProperty("comingSoonShows")]
        public MovieModel[] ComingSoonMoviesModel { get; set; }

        [JsonProperty("featuredShow")]
        public MovieModel FeaturedMovieModel { get; set; }

        [JsonProperty("searchForShow")]
        public MovieModel SearchForShowModel { get; set; }

        [JsonProperty("searchForShowList")]
        public MovieModel[] SearchForShowList { get; set; }
    }

    public class MovieModel
    {
        [JsonProperty("thumbnail")]
        public string Thumbnail { get; set; }

        [JsonProperty("Title")]
        public string Title { get; set; }

        [JsonProperty("Genre")]
        [Ignore]
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
        [Ignore]
        public DelegateCommand<MovieModel> ShowInfoCommand { get; set; }

        [JsonProperty("Year")]
        public string Year { get; set; }

        [JsonProperty("Rating")]
        public double Rating { get; set; }

        [JsonProperty("InfoThumbnail")]
        public string InfoThumbnail { get; set; }

        [JsonIgnore]
        [Ignore]
        public int EpisodeNumber { get; set; }

      
    }
}
