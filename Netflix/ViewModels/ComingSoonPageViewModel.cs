using System.Collections.ObjectModel;
using Netflix.Helpers.API.Interfaces;
using Netflix.Models;
using Prism.Commands;
using Prism.Navigation;

namespace Netflix.ViewModels
{
    public class ComingSoonPageViewModel : ViewModelBase
    {
        private IGraphQL graphQL;
        public DelegateCommand SearchCommand { get; }
        public DelegateCommand ProfilePageCommand { get; }
        public ComingSoonPageViewModel(INavigationService navigationService, IGraphQL graphQL) : base(navigationService)
        {
            this.graphQL = graphQL;
            SearchCommand = new DelegateCommand(async () => await navigationService.NavigateAsync("SearchPage"));
            ProfilePageCommand = new DelegateCommand(async () => await navigationService.NavigateAsync("ProfilePage"));
        }

        public override async void Initialize(INavigationParameters parameters) => ComingSoon = await graphQL.MovieQuery("comingSoonShows", "infoThumbnail", "genre", "title", "synopsis");
        #region Properties

        private ObservableCollection<MovieModel> comingSoon;
        public ObservableCollection<MovieModel> ComingSoon
        {
            get { return comingSoon; }
            set { SetProperty(ref comingSoon, value); }
        }

        
        #endregion

        #region Methods
        #endregion
    }
}