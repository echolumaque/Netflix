using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Netflix.Helpers.API.Interfaces;
using Netflix.Models;
using Prism.Commands;
using Prism.Navigation;
using Refit;

namespace Netflix.ViewModels
{
    public class SearchPageViewModel : ViewModelBase
    {
        private readonly INavigationService navigationService;
        public DelegateCommand<string> SendRequest { get; }
        public SearchPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            this.navigationService = navigationService;
            SendRequest = new DelegateCommand<string>(async (show) => await SearchForShows(show));
        }

        #region Properties

        private ObservableCollection<MovieModel> shows;
        public ObservableCollection<MovieModel> Shows
        {
            get { return shows; }
            set { SetProperty(ref shows, value); }
        }

        private string searchQuery;
        public string SearchQuery
        {
            get { return searchQuery; }
            set
            { 
                SetProperty(ref searchQuery, value); 
                SearchForShows(SearchQuery); 
            }
        }
        #endregion

        #region Methods

        private async Task SearchForShows(string title)
        {
            Shows = await RestService.For<IMovie>(App.LocalAPI).SearchForShows(title);

            for(int i = 0; i < shows.Count; i++)
            {
                Shows[i].ShowInfoCommand = new DelegateCommand<MovieModel>(async (show) => await InfoPage(show));
            }
        }

        private async Task InfoPage(MovieModel movieModel)
        {
            var parameters = new NavigationParameters
            {
                {
                    "show",
                    new MovieModel
                    {
                        Casts = movieModel.Casts,
                        Genre = movieModel.Genre,
                        Synopsis = movieModel.Synopsis,
                        Title = movieModel.Title,
                        Year = movieModel.Year,
                        Rating = movieModel.Rating,
                        InfoThumbnail = movieModel.InfoThumbnail,
                        Thumbnail = movieModel.Thumbnail
                    }
                }
            };
            await navigationService.NavigateAsync("ShowInfo", parameters);
        }
        #endregion
    }
}
