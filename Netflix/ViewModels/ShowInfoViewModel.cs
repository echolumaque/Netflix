using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Netflix.Models;
using Prism.Commands;
using Prism.Navigation;

namespace Netflix.ViewModels
{
    public class ShowInfoViewModel : ViewModelBase
    {
        private readonly INavigationService navigationService;
        public DelegateCommand AddToListCommand { get; set; }
        public ShowInfoViewModel(INavigationService navigationService) : base(navigationService)
        {
            this.navigationService = navigationService;
            AddToListCommand = new DelegateCommand(async () => await AddToList());
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            var objectRequest = parameters["show"] as MovieModel;
            TitleOfShow = objectRequest.Title;
            Synopsis = objectRequest.Synopsis;
            Rating = objectRequest.Rating;
            Year = objectRequest.Year;
            Casts = objectRequest.Casts;
            Thumbnail = objectRequest.InfoThumbnail;
            ShowThumbnail = objectRequest.Thumbnail;

            var episodes = new ObservableCollection<MovieModel>();
            for (int i = 1; i < 11; i++)
            {
                var episodeToBeAdd = new MovieModel
                {
                    EpisodeNumber = i,
                    InfoThumbnail = Thumbnail,
                    Synopsis = Synopsis
                    
                };
                episodes.Add(episodeToBeAdd);
            }
            Episodes = new ObservableCollection<MovieModel>(episodes);
        }

        #region Properties
     
        private string ShowThumbnail { get; set; }

        private string thumbnail;
        public string Thumbnail
        {
            get { return thumbnail; }
            set { SetProperty(ref thumbnail, value); }
        }

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

        private string synopsis;
        public string Synopsis
        {
            get { return synopsis; }
            set { SetProperty(ref synopsis, value); }
        }

        private string year;
        public string Year
        {
            get { return year; }
            set { SetProperty(ref year, value); }
        }

        private double rating;
        public double Rating
        {
            get { return rating; }
            set { SetProperty(ref rating, value); }
        }

        private ObservableCollection<MovieModel> episodes;
        public ObservableCollection<MovieModel> Episodes
        {
            get { return episodes; }
            set { SetProperty(ref episodes, value); }
        }
        #endregion

        #region Methods

        private async Task AddToList()
        {
            await App.CreateDatabaseTable<MyListModel>().ConfigureAwait(false);

            var myList = new MyListModel
            {
                Title = TitleOfShow,
                Image = ShowThumbnail,
            };

            await App.ConnectionString.InsertAsync(myList);
        }
        #endregion
    }
}
