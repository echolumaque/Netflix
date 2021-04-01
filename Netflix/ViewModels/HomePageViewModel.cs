using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Netflix.Helpers.API.Interfaces;
using Netflix.Models;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services.Dialogs;
using Refit;

namespace Netflix.ViewModels
{
    public class HomePageViewModel : ViewModelBase
    {
        private INavigationService navigationService;
        private IDialogService dialogService;
        public DelegateCommand ShowInfo { get; }
        public HomePageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService)
        {
            this.navigationService = navigationService;
            this.dialogService = dialogService;
            ShowInfo = new DelegateCommand(async () => await ShowPopup());
        }

        public override async void Initialize(INavigationParameters parameters)
        {
            var featuredShow = await RestService.For<IMovie>(App.LocalAPI).GetFeaturedShow();

            Thumbnail = featuredShow.Thumbnail;
            Genres = string.Join(" • ", featuredShow.Genre);
            Synopsis = featuredShow.Synopsis;
            Year = featuredShow.Year;
            TitleOfShow = featuredShow.Title;
            Casts = featuredShow.Casts;
            infoThumbnail = featuredShow.InfoThumbnail;

            var popular =  RestService.For<IMovie>(App.LocalAPI).GetPopularShows();
            var action =  RestService.For<IMovie>(App.LocalAPI).GetActionShows();
            var comedy =  RestService.For<IMovie>(App.LocalAPI).GetComedyShows();
            var comingSoon =  RestService.For<IMovie>(App.LocalAPI).GetComingSoonShows();
            var allShows =  RestService.For<IMovie>(App.LocalAPI).GetAllShows();

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
        #endregion
    }
}
