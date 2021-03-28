using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using Netflix.Helpers.API.Interfaces;
using Netflix.Helpers.Dependency;
using Netflix.Models;
using Prism.Commands;
using Prism.Navigation;
using Refit;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Netflix.ViewModels
{
    public class HomePageViewModel : ViewModelBase
    {
        private readonly INavigationService navigationService;
        public DelegateCommand AddToListCommand { get; }
        public DelegateCommand GotoMyListPageCommand { get; }
        public DelegateCommand ShowInfoCommand { get; }
        private IToast Toast { get; }

        public HomePageViewModel(INavigationService navigationService, IToast toast) : base(navigationService)
        {
            this.navigationService = navigationService;
            this.Toast = toast;

            AddToListCommand = new DelegateCommand(async () => await AddToList());
            GotoMyListPageCommand = new DelegateCommand(async () => await GotoMyListPage());
            ShowInfoCommand = new DelegateCommand(async () => await ShowInfoPage());
        }

        public override async void Initialize(INavigationParameters parameters)
        {
            var allShowsResponse = RestService.For<IMovie>(App.LocalAPI).GetAllShows();

            var popularResponse = RestService.For<IMovie>(App.LocalAPI).GetPopularShows();

            var actionResponse = RestService.For<IMovie>(App.LocalAPI).GetActionShows();

            var comedyResponse = RestService.For<IMovie>(App.LocalAPI).GetComedyShows();

            var comingSoonResponse = RestService.For<IMovie>(App.LocalAPI).GetComingSoonShows();

            await Task.WhenAll(allShowsResponse, popularResponse, actionResponse, comedyResponse, comingSoonResponse);

            AllShows = await allShowsResponse;
            Popular = await popularResponse;
            Action = await actionResponse;
            Comedy = await comedyResponse;
            ComingSoon = await comingSoonResponse;

            for (int i = 0; i < AllShows.Count; i++)
            {
                AllShows[i].ShowInfoCommand = new DelegateCommand<MovieModel>(async (show) => await ShowInfoPage(show));
            }
            for (int i = 0; i < Popular.Count; i++)
            {
                Popular[i].ShowInfoCommand = new DelegateCommand<MovieModel>(async (show) => await ShowInfoPage(show));
            }
            for (int i = 0; i < Action.Count; i++)
            {
                Action[i].ShowInfoCommand = new DelegateCommand<MovieModel>(async (show) => await ShowInfoPage(show));
            }
            for (int i = 0; i < Comedy.Count; i++)
            {
                Comedy[i].ShowInfoCommand = new DelegateCommand<MovieModel>(async (show) => await ShowInfoPage(show));
            }

            for (int i = 0; i < ComingSoon.Count; i++)
            {
                ComingSoon[i].ShowInfoCommand = new DelegateCommand<MovieModel>(async (show) => await ShowInfoPage(show));
            }
        }

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            var featuredMovieRequest = RestService.For<IMovie>(App.LocalAPI);
            var response = await featuredMovieRequest.GetFeaturedShow();

            Thumbnail = response.Thumbnail;
            Genre = string.Join(", ", response.Genre);
            TitleOfMovie = response.Title;
            Synopsis = response.Synopsis;
            Casts = response.Casts;
            movieModel = response;
        }


        #region Properties
        private MovieModel movieModel;
        private string Synopsis { get; set; }
        private string Casts { get; set; }
        private string thumbnail;
        public string Thumbnail
        {
            get { return thumbnail; }
            set { SetProperty(ref thumbnail, value); }
        }

        private string genre;
        public string Genre
        {
            get { return genre; }
            set { SetProperty(ref genre, value); }
        }

        private string TitleOfMovie { get; set; }

        private ObservableCollection<MovieModel> popular;
        public ObservableCollection<MovieModel> Popular
        {
            get { return popular; }
            set { SetProperty(ref popular, value); }
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

        private ObservableCollection<MovieModel> comingSoon;
        public ObservableCollection<MovieModel> ComingSoon
        {
            get { return comingSoon; }
            set { SetProperty(ref comingSoon, value); }
        }

        private ObservableCollection<MovieModel> allShows;
        public ObservableCollection<MovieModel> AllShows
        {
            get { return allShows; }
            set { SetProperty(ref allShows, value); }
        }
        #endregion

        #region Methods



        private async Task AddToList()
        {
            await App.CreateDatabaseTable<MyListModel>().ConfigureAwait(false);

            var myList = new MyListModel
            {
                Title = TitleOfMovie,
                Image = Thumbnail,
                Casts = Casts,
                Synopsis = Synopsis,
            };
            Preferences.Set("dbHasContents", true);
            await App.ConnectionString.InsertAsync(myList);

            Device.BeginInvokeOnMainThread(() =>
            {
                Toast.ShowToast("Successfully added to your list");
            });
        }

        private async Task GotoMyListPage()
        {
            if (Preferences.Get("dbHasContents", false))
            {
                await navigationService.NavigateAsync("MyListPage");
            }
            else
            {
                Toast.ShowToast("You currently don't have shows on your list");
            }
        }

        private async Task ShowInfoPage(MovieModel movieModel) //for model
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

        private async Task ShowInfoPage() //for local view model
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
