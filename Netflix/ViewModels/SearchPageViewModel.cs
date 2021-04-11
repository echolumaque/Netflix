using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Netflix.Helpers.API.Interfaces;
using Netflix.Models;
using Prism.Commands;
using Prism.Navigation;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Netflix.ViewModels
{
    public class SearchPageViewModel : ViewModelBase
    {
        private INavigationService navigationService;
        private IGraphQL graphQL;
        public DelegateCommand ProfilePageCommand { get; }
        public DelegateCommand<string> SendRequest { get; }
        public SearchPageViewModel(INavigationService navigationService, IGraphQL graphQL) : base(navigationService)
        {
            this.navigationService = navigationService;
            this.graphQL = graphQL;
            SendRequest = new DelegateCommand<string>(async (show) => await SearchForShows(show));
            ProfilePageCommand = new DelegateCommand(async () => await this.navigationService.NavigateAsync("ProfilePage"));
        }
        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            if (string.IsNullOrWhiteSpace(Preferences.Get("search", string.Empty)))
            {
                AllShows = await graphQL.MovieQuery("allShows", "thumbnail", "title");
                ColViewLayout = new LinearItemsLayout(ItemsLayoutOrientation.Vertical);
            }

            for (int i = 0; i < AllShows.Count; i++)
            {
                AllShows[i].ShowInfoCommand = new DelegateCommand<MovieModel>(async (show) => await ShowPopup(show));
            }
            IsSearchVisible = false;
        }

        public override void OnNavigatedFrom(INavigationParameters parameters) => Preferences.Set("search", string.Empty);

        #region Properties

        private bool isSearchVisible;
        public bool IsSearchVisible
        {
            get { return isSearchVisible; }
            set { SetProperty(ref isSearchVisible, value); }
        }
        private ObservableCollection<MovieModel> allShows;
        public ObservableCollection<MovieModel> AllShows
        {
            get { return allShows; }
            set { SetProperty(ref allShows, value); }
        }

        private string searchQuery;
        public string SearchQuery
        {
            get { return searchQuery; }
            set
            { SetProperty(ref searchQuery, value); }   
        }

        private ItemsLayout colViewLayout;
        public ItemsLayout ColViewLayout
        {
            get { return colViewLayout; }
            set { SetProperty(ref colViewLayout, value); }
        }
        #endregion

        #region Methods

        private async Task SearchForShows(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                Preferences.Set("search", title);
                AllShows = await graphQL.MovieQuery("allShows", "thumbnail", "title");
                ColViewLayout = new LinearItemsLayout(ItemsLayoutOrientation.Vertical);
                for (int i = 0; i < AllShows.Count; i++)
                {
                    AllShows[i].ShowInfoCommand = new DelegateCommand<MovieModel>(async (show) => await ShowPopup(show));
                }
            }

            else
            {
                Preferences.Set("search", title);
                AllShows = await graphQL.SearchForShowsList(title, "thumbnail", "title");
                ColViewLayout = new GridItemsLayout(3, ItemsLayoutOrientation.Vertical);
                for (int i = 0; i < AllShows.Count; i++)
                {
                    AllShows[i].ShowInfoCommand = new DelegateCommand<MovieModel>(async (show) => await ShowPopup(show));
                }
            }
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
        #endregion
    }
}
