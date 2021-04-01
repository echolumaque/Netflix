using System.Collections.ObjectModel;
using Netflix.Models;
using Prism.Navigation;
using Prism.AppModel;
using System.Threading.Tasks;

namespace Netflix.ViewModels
{
    public class EpisodePageViewModel : ViewModelBase, IInitialize
    {
        public INavigationService navigationService;
        public EpisodePageViewModel(INavigationService navigationService) : base(navigationService)
        {
            this.navigationService = navigationService;
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            TitleOfShow = parameters.GetValue<MovieModel>("show").Title;
            Year = parameters.GetValue<MovieModel>("show").Year;
            Synopsis = parameters.GetValue<MovieModel>("show").Synopsis;
            Casts = parameters.GetValue<MovieModel>("show").Casts;
            InfoThumbnail = parameters.GetValue<MovieModel>("show").InfoThumbnail;

            var episodes = new ObservableCollection<MovieModel>();
            for (int i = 1; i < 11; i++)
            {
                var episodesToAdd = new MovieModel
                {
                    InfoThumbnail = InfoThumbnail,
                    EpisodeNumber = i,
                };
                episodes.Add(episodesToAdd);
            }
            Episodes = new ObservableCollection<MovieModel>(episodes);
        }
        #region Properties

        private ObservableCollection<MovieModel> episodes;
        public ObservableCollection<MovieModel> Episodes
        {
            get { return episodes; }
            set { SetProperty(ref episodes, value); }
        }

        private string infoThumbnail;
        public string InfoThumbnail
        {
            get { return infoThumbnail; }
            set { SetProperty(ref infoThumbnail, value); }
        }

        private string titleofShow;
        public string TitleOfShow
        {
            get { return titleofShow; }
            set { SetProperty(ref titleofShow, value); }
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
        #endregion
    }
}
