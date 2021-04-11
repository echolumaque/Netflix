using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Netflix.Models;

namespace Netflix.Helpers.API.Interfaces
{
    public interface IGraphQL
    {
        Task<ObservableCollection<MovieModel>> MovieQuery(string queryType, params string[] graphQuery);
        Task<MovieDatas> FeaturedMovieQuery(params string[] graphQuery);
        Task<MovieDatas> SearchForShow(string title, params string[] graphQuery);
        Task<ObservableCollection<MovieModel>> SearchForShowsList(string title, params string[] graphQuery);
    }
}
