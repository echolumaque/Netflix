using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Netflix.Helpers.API.Interfaces;
using Netflix.Helpers.Dependency;
using Netflix.Models;
using Prism.Commands;
using Prism.Navigation;
using Xamarin.Forms;

namespace Netflix.ViewModels
{
    public class HomePageViewModel : ViewModelBase
    {
        private INavigationService navigationService;
        private IToast toast;
        private IGraphQL graphQL;
        public DelegateCommand ShowInfo { get; }
        public DelegateCommand GotoSearchPageCommand { get; }
        public DelegateCommand GotoMyListPage { get; }
        public DelegateCommand AddtoListCommand { get; }
        public DelegateCommand GotoProfilePage { get; }

        public HomePageViewModel(INavigationService navigationService, IToast toast, IGraphQL graphQL) : base(navigationService)
        {
            this.navigationService = navigationService;
            this.toast = toast;
            this.graphQL = graphQL;
            ShowInfo = new DelegateCommand(async () => await ShowPopup());
            GotoSearchPageCommand = new DelegateCommand(async  () => await this.navigationService.NavigateAsync("SearchPage"));
            GotoMyListPage = new DelegateCommand(async () => await this.navigationService.NavigateAsync("MyListPage"));
            GotoProfilePage = new DelegateCommand(async () => await this.navigationService.NavigateAsync("ProfilePage"));
            AddtoListCommand = new DelegateCommand(async () => await AddtoList());
        }

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            var popular = graphQL.MovieQuery("popularShows", "thumbnail", "title");
            var action = graphQL.MovieQuery("actionShows", "thumbnail", "title");
            var comedy = graphQL.MovieQuery("comedyShows", "thumbnail", "title");
            var comingSoon = graphQL.MovieQuery("comingSoonShows", "thumbnail", "title");
            var allShows = graphQL.MovieQuery("allShows", "thumbnail", "title");

            await Task.WhenAll(popular, action, comedy, comingSoon, allShows);

            var graphQLQuery = await graphQL.FeaturedMovieQuery("thumbnail", "genre", "title");

            Thumbnail = graphQLQuery.FeaturedMovieModel.Thumbnail;
            Genres = string.Join(" • ", graphQLQuery.FeaturedMovieModel.Genre);
            TitleOfShow = graphQLQuery.FeaturedMovieModel.Title;

            Popular = await popular;
            Action = await action;
            Comedy = await comedy;
            ComingSoon = await comingSoon;
            AllShows = await allShows;

            for (int i = 0; i < Popular.Count; i++)
            {
                Popular[i].ShowInfoCommand = new DelegateCommand<MovieModel>(async (show) => await ShowPopup(show));
            }
            for (int i = 0; i < Action.Count; i++)
            {
                Action[i].ShowInfoCommand = new DelegateCommand<MovieModel>(async (show) => await ShowPopup(show));
            }
            for (int i = 0; i < Comedy.Count; i++)
            {
                Comedy[i].ShowInfoCommand = new DelegateCommand<MovieModel>(async (show) => await ShowPopup(show));
            }
            for (int i = 0; i < ComingSoon.Count; i++)
            {
                ComingSoon[i].ShowInfoCommand = new DelegateCommand<MovieModel>(async (show) => await ShowPopup(show));
            }
            for (int i = 0; i < AllShows.Count; i++)
            {
                AllShows[i].ShowInfoCommand = new DelegateCommand<MovieModel>(async (show) => await ShowPopup(show));
            }
        }

        #region Properties
        private string title;
        public string TitleOfShow
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        private ObservableCollection<MovieModel> allShows;
        public ObservableCollection<MovieModel> AllShows
        {
            get { return allShows; }
            set { SetProperty(ref allShows, value); }
        }

        private ObservableCollection<MovieModel> comingSoon;
        public ObservableCollection<MovieModel> ComingSoon
        {
            get { return comingSoon; }
            set { SetProperty(ref comingSoon, value); }
        }

        private ObservableCollection<MovieModel> comedy;
        public ObservableCollection<MovieModel> Comedy
        {
            get { return comedy; }
            set { SetProperty(ref comedy, value); }
        }

        private ObservableCollection<MovieModel> action;
        public ObservableCollection<MovieModel> Action
        {
            get { return action; }
            set { SetProperty(ref action, value); }
        }

        private ObservableCollection<MovieModel> popular;
        public ObservableCollection<MovieModel> Popular
        {
            get { return popular; }
            set { SetProperty(ref popular, value); }
        }

        private string thumbail;
        public string Thumbnail
        {
            get { return thumbail; }
            set { SetProperty(ref thumbail, value); }
        }

        private string genres;
        public string Genres
        {
            get { return genres; }
            set { SetProperty(ref genres, value); }
        }
        #endregion

        #region Methods

        private async Task ShowPopup()
        {
            var parameters = new NavigationParameters
            {
                {
                    "show",
                    new MovieModel
                    {
                        Title = TitleOfShow,
                        Thumbnail = Thumbnail
                    }
                }
            };
            await navigationService.NavigateAsync("InfoPopupPage", parameters);
        }

        private async Task ShowPopup(MovieModel movieModel)
        {
            var parameters = new NavigationParameters
            {
                {
                    "show",
                    new MovieModel
                    {
                        Title = movieModel.Title,
                        Thumbnail = movieModel.Thumbnail
                    }
                }
            };
            await navigationService.NavigateAsync("InfoPopupPage", parameters);
        }

        private async Task AddtoList()
        {
            await App.CreateDatabaseTable<MovieModel>().ConfigureAwait(false);

            var myListPageQuery = await graphQL.SearchForShow(TitleOfShow, "year", "synopsis", "casts");

            var listItem = new MovieModel
            {
                Title = TitleOfShow,
                Year = myListPageQuery.SearchForShowModel.Year,
                Synopsis = myListPageQuery.SearchForShowModel.Synopsis,
                Casts = myListPageQuery.SearchForShowModel.Casts,
                Thumbnail = Thumbnail
            };
            Device.BeginInvokeOnMainThread(() =>
            {
                toast.ShowToast("Succesfully added to your list");
            });
            await App.ConnectionString.InsertAsync(listItem).ConfigureAwait(false);
        }

        #endregion
    }
}
