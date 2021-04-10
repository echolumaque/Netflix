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

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            var passedParameter = parameters.GetValue<MovieModel>("show");
            var episodePageQuery = await SearchForShow(passedParameter.Title, "casts", "infoThumbnail");

            TitleOfShow = passedParameter.Title;
            Year = passedParameter.Year;
            Synopsis = passedParameter.Synopsis;
            Thumbnail = passedParameter.Thumbnail;
            Casts = episodePageQuery.SearchForShowModel.Casts;
            InfoThumbnail = episodePageQuery.SearchForShowModel.InfoThumbnail;

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

            var myListPageQuery = await SearchForShow(TitleOfShow, "year", "synopsis", "casts");

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
