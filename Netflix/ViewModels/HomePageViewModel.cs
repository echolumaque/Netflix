using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Netflix.Helpers.Dependency;
using Netflix.Models;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services.Dialogs;
using Xamarin.Forms;

namespace Netflix.ViewModels
{
    public class HomePageViewModel : ViewModelBase
    {
        private INavigationService navigationService;
        private IDialogService dialogService;
        private IToast toast;
        public DelegateCommand ShowInfo { get; }
        public DelegateCommand GotoSearchPageCommand { get; }
        public DelegateCommand GotoMyListPage { get; }
        public DelegateCommand AddtoListCommand { get; }
        public DelegateCommand GotoProfilePage { get; }

        public HomePageViewModel(INavigationService navigationService, IDialogService dialogService, IToast toast) : base(navigationService)
        {
            this.navigationService = navigationService;
            this.dialogService = dialogService;
            this.toast = toast;
            ShowInfo = new DelegateCommand(async () => await ShowPopup());
            GotoSearchPageCommand = new DelegateCommand(async  () => await this.navigationService.NavigateAsync("SearchPage"));
            GotoMyListPage = new DelegateCommand(async () => await this.navigationService.NavigateAsync("MyListPage"));
            GotoProfilePage = new DelegateCommand(async () => await this.navigationService.NavigateAsync("ProfilePage"));
        }

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            var featuredShow = await API.GetFeaturedShow();

            Thumbnail = featuredShow.Thumbnail;
            Genres = string.Join(" • ", featuredShow.Genre);
            Synopsis = featuredShow.Synopsis;
            Year = featuredShow.Year;
            TitleOfShow = featuredShow.Title;
            Casts = featuredShow.Casts;
            infoThumbnail = featuredShow.InfoThumbnail;

            var popular =  API.GetPopularShows();
            var action =  API.GetActionShows();
            var comedy =  API.GetComedyShows();
            var comingSoon =  API.GetComingSoonShows();
            var allShows =  API.GetAllShows();

            await Task.WhenAll(popular, action, comedy, comingSoon, allShows);

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
        private string infoThumbnail;

        private string casts;
        public string Casts
        {
            get { return casts; }
            set { SetProperty(ref casts, value); }
        }

        private string title;
        public string TitleOfShow
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        private string year;
        public string Year
        {
            get { return year; }
            set { SetProperty(ref year, value); }
        }

        private string synopsis;
        public string Synopsis
        {
            get { return synopsis; }
            set { SetProperty(ref synopsis, value); }
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
            var parameters = new DialogParameters
            {
                {
                    "show",
                    new MovieModel
                    {
                        Title = TitleOfShow,
                        Year = Year,
                        Synopsis = Synopsis,
                        Casts = Casts,
                        InfoThumbnail = infoThumbnail,
                        Thumbnail = Thumbnail
                    }
                }
            };

            await dialogService.ShowDialogAsync("InfoPopupPage", parameters);
        }

        private async Task ShowPopup(MovieModel movieModel)
        {
            var parameters = new DialogParameters
            {
                {
                    "show",
                    new MovieModel
                    {
                        Title = movieModel.Title,
                        Year = movieModel.Year,
                        Synopsis = movieModel.Synopsis,
                        Casts = movieModel.Casts,
                        InfoThumbnail = movieModel.InfoThumbnail,
                        Thumbnail = movieModel.Thumbnail
                    }
                }
            };

            await dialogService.ShowDialogAsync("InfoPopupPage", parameters);
        }

        private async Task AddtoList()
        {
            await App.CreateDatabaseTable<MovieModel>().ConfigureAwait(false);

            var listItem = new MovieModel
            {
                Title = TitleOfShow,
                Year = Year,
                Synopsis = Synopsis,
                Casts = Casts,
                InfoThumbnail = infoThumbnail,
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
