using System.Threading.Tasks;
using Netflix.Models;
using Prism.Commands;
using Prism.Navigation;

namespace Netflix.ViewModels
{
    public class InfoPopupPageViewModel : ViewModelBase
    {
        private INavigationService navigationService;
        public DelegateCommand CloseCommand { get; }
        public DelegateCommand EpisodePageCommand { get; }

        public InfoPopupPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            this.navigationService = navigationService;
            CloseCommand = new DelegateCommand(async () => await this.navigationService.ClearPopupStackAsync());
            EpisodePageCommand = new DelegateCommand(async () => await GotoEpisodePage());
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            InfoThumbnail = parameters.GetValue<MovieModel>("show").InfoThumbnail;
            Year = parameters.GetValue<MovieModel>("show").Year;
            Synopsis = parameters.GetValue<MovieModel>("show").Synopsis;
            Casts = parameters.GetValue<MovieModel>("show").Casts;
            Thumbnail = parameters.GetValue<MovieModel>("show").Thumbnail;
            Title = parameters.GetValue<MovieModel>("show").Title;
        }

        #region Properties

        private string title;
        public new string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        private string thumbnail;
        public string Thumbnail
        {
            get { return thumbnail; }
            set { SetProperty(ref thumbnail, value); }
        }

        private string infoThumnail;
        public string InfoThumbnail
        {
            get { return infoThumnail; }
            set { SetProperty(ref infoThumnail, value); }
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

        private string casts;
        public string Casts
        {
            get { return casts; }
            set { SetProperty(ref casts, value); }
        }

        #endregion

        #region Methods

        private async Task GotoEpisodePage()
        {
            var parameters = new NavigationParameters
            {
                {
                    "show",
                    new MovieModel
                    {
                        Title = Title,
                        Year = Year,
                        Synopsis = Synopsis,
                        Casts = Casts,
                        InfoThumbnail = InfoThumbnail,
                        Thumbnail = Thumbnail
                    }
                }
            };
            await navigationService.ClearPopupStackAsync();
            await navigationService.NavigateAsync("EpisodePage", parameters);
        }
        #endregion
    }
}
