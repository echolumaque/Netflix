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

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            var passedParameter = parameters.GetValue<MovieModel>("show");
            var featuredMoviePopup = await SearchForShow(passedParameter.Title, "year", "synopsis", "casts");

            Year = featuredMoviePopup.SearchForShowModel.Year;
            Synopsis = featuredMoviePopup.SearchForShowModel.Synopsis;
            Thumbnail = passedParameter.Thumbnail;
            Title = passedParameter.Title;
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
