using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Netflix.Helpers.Dependency;
using Netflix.Models;
using Prism.Commands;
using Prism.Navigation;
using Xamarin.Forms;

namespace Netflix.ViewModels
{
    public class EpisodePageViewModel : ViewModelBase, IInitialize
    {
        public INavigationService navigationService;
        private IToast toast;
        public DelegateCommand AddToListCommand { get; }
        public DelegateCommand SearchPageCommand { get; }
        public DelegateCommand ProfilePageCommand { get; }


        public EpisodePageViewModel(INavigationService navigationService, IToast toast) : base(navigationService)
        {
            this.navigationService = navigationService;
            this.toast = toast;
            AddToListCommand = new DelegateCommand(async () => await AddtoList());
            SearchPageCommand = new DelegateCommand(async () => await this.navigationService.NavigateAsync("SearchPage"));
            ProfilePageCommand = new DelegateCommand(async () => await this.navigationService.NavigateAsync("ProfilePage"));
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            TitleOfShow = parameters.GetValue<MovieModel>("show").Title;
            Year = parameters.GetValue<MovieModel>("show").Year;
            Synopsis = parameters.GetValue<MovieModel>("show").Synopsis;
            Casts = parameters.GetValue<MovieModel>("show").Casts;
            InfoThumbnail = parameters.GetValue<MovieModel>("show").InfoThumbnail;
            Thumbnail = parameters.GetValue<MovieModel>("show").Thumbnail;
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
        private string Thumbnail;

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

        private async Task AddtoList()
        {
            await App.CreateDatabaseTable<MovieModel>().ConfigureAwait(false);

            var listItem = new MovieModel
            {
                Title = TitleOfShow,
                Year = Year,
                Synopsis = Synopsis,
                Casts = Casts,
                InfoThumbnail = InfoThumbnail,
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
