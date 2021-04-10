using System.Collections.ObjectModel;
using System.Threading.Tasks;
using GraphQL;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using Netflix.Models;
using Newtonsoft.Json;
using Prism.Mvvm;
using Prism.Navigation;

namespace Netflix.ViewModels
{
    public class ViewModelBase : BindableBase, IInitialize, INavigationAware, IDestructible
    {
        protected INavigationService NavigationService { get; private set; }

        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private static GraphQLHttpClient client = new GraphQLHttpClient("http://echo.somee.com/netflixgraph/graphql", new NewtonsoftJsonSerializer());
       
        public async Task<ObservableCollection<MovieModel>> MovieQuery(string queryType, params string[] graphQuery)
        {
            if (queryType == "allShows")
            {
                var concatenatedQuery = string.Join(" ", graphQuery);
                var query = new GraphQLRequest
                {
                    Query = $"{{allShows {{{ concatenatedQuery }}} }}",
                };
                var request = await client.SendQueryAsync<object>(query);

                var json = JsonConvert.DeserializeObject<MovieDatas>(request.Data.ToString());

                var rawData = new ObservableCollection<MovieModel>();

                for (int i = 0; i < json.AllMoviesModel.Length; i++)
                {
                    rawData.Add(json.AllMoviesModel[i]);
                }
                return rawData;
            }
            else if (queryType == "actionShows")
            {
                var concatenatedQuery = string.Join(" ", graphQuery);
                var query = new GraphQLRequest
                {
                    Query = $"{{actionShows {{{ concatenatedQuery }}} }}",
                };
                var request = await client.SendQueryAsync<object>(query);

                var json = JsonConvert.DeserializeObject<MovieDatas>(request.Data.ToString());

                var rawData = new ObservableCollection<MovieModel>();

                for (int i = 0; i < json.ActionMoviesModel.Length; i++)
                {
                    rawData.Add(json.ActionMoviesModel[i]);
                }
                return rawData;
            }
            else if (queryType == "comedyShows")
            {
                var concatenatedQuery = string.Join(" ", graphQuery);
                var query = new GraphQLRequest
                {
                    Query = $"{{comedyShows {{{ concatenatedQuery }}} }}",
                };
                var request = await client.SendQueryAsync<object>(query);

                var json = JsonConvert.DeserializeObject<MovieDatas>(request.Data.ToString());

                var rawData = new ObservableCollection<MovieModel>();

                for (int i = 0; i < json.ComedyMoviesModel.Length; i++)
                {
                    rawData.Add(json.ComedyMoviesModel[i]);
                }
                return rawData;
            }
            else if (queryType == "comingSoonShows")
            {
                var concatenatedQuery = string.Join(" ", graphQuery);
                var query = new GraphQLRequest
                {
                    Query = $"{{comingSoonShows {{{ concatenatedQuery }}} }}",
                };
                var request = await client.SendQueryAsync<object>(query);

                var json = JsonConvert.DeserializeObject<MovieDatas>(request.Data.ToString());

                var rawData = new ObservableCollection<MovieModel>();

                for (int i = 0; i < json.ComingSoonMoviesModel.Length; i++)
                {
                    rawData.Add(json.ComingSoonMoviesModel[i]);
                }
                return rawData;
            }
            else if (queryType == "popularShows")
            {
                var concatenatedQuery = string.Join(" ", graphQuery);
                var query = new GraphQLRequest
                {
                    Query = $"{{popularShows {{{ concatenatedQuery }}} }}",
                };
                var request = await client.SendQueryAsync<object>(query);

                var json = JsonConvert.DeserializeObject<MovieDatas>(request.Data.ToString());

                var rawData = new ObservableCollection<MovieModel>();

                for (int i = 0; i < json.PopularMoviesModel.Length; i++)
                {
                    rawData.Add(json.PopularMoviesModel[i]);
                }
                return rawData;
            }
            else
                return new ObservableCollection<MovieModel>();

        }

        public async Task<MovieDatas> FeaturedMovieQuery(params string[] graphQuery)
        {
            var concatenatedQuery = string.Join(" ", graphQuery);
            var query = new GraphQLRequest
            {
                Query = $"{{featuredShow {{{ concatenatedQuery }}} }}",
            };
            var request = await client.SendQueryAsync<object>(query);
            return JsonConvert.DeserializeObject<MovieDatas>(request.Data.ToString());
        }

        public async Task<MovieDatas> SearchForShow(string title, params string[] graphQuery)
        {
            var concatenateQuery = string.Join(" ", graphQuery);
            var query = new GraphQLRequest
            {
                Query = $"query Searches($title: String!) {{ searchForShow(title: $title) {{ {concatenateQuery} }}}}",
                Variables = new { title },
            };

            var request = await client.SendQueryAsync<object>(query);
            return JsonConvert.DeserializeObject<MovieDatas>(request.Data.ToString());
        }

        public async Task<ObservableCollection<MovieModel>> SearchForShowsList(string title, params string[] graphQuery)
        {
            var concatenatedQuery = string.Join(" ", graphQuery);

            var query = new GraphQLRequest
            {
                Query = $"query Search($title : String!) {{ searchForShowList(title: $title) {{ {concatenatedQuery} }} }}",
                Variables = new { title }
            };

            var request = await client.SendQueryAsync<object>(query);

            var json = JsonConvert.DeserializeObject<MovieDatas>(request.Data.ToString());

            var rawData = new ObservableCollection<MovieModel>();

            for (int i = 0; i < json.SearchForShowList.Length; i++)
            {
                rawData.Add(json.SearchForShowList[i]);
            }
            return rawData;
        }

        public ViewModelBase(INavigationService navigationService)
        {
            NavigationService = navigationService;
        }

        public virtual void Initialize(INavigationParameters parameters)
        {

        }


        public virtual void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public virtual void OnNavigatedTo(INavigationParameters parameters)
        {

        }

        public virtual void Destroy()
        {

        }

    }
}
