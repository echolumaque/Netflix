using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Netflix.Models;
using Refit;

namespace Netflix.Helpers.API.Interfaces
{
    [Headers("Accept-Encoding: gzip, deflate, br")]
    public interface IMovie
    {
        [Get("/Movies/FeaturedMovie")]
        Task<MovieModel> GetFeaturedShow();

        [Get("/Movies/TopTVShows")]
        Task<ObservableCollection<MovieModel>> GetPopularShows();

        [Get("/Movies/Action")]
        Task<ObservableCollection<MovieModel>> GetActionShows();

        [Get("/Movies/Comedy")]
        Task<ObservableCollection<MovieModel>> GetComedyShows();

        [Get("/Movies/Search?title={title}")]
        Task<ObservableCollection<MovieModel>> SearchForShows(string title);

        [Get("/movies/comingsoon")]
        Task<ObservableCollection<MovieModel>> GetComingSoonShows();

        [Get("/Movies/allshows")]
        Task<ObservableCollection<MovieModel>> GetAllShows();
    }
}
