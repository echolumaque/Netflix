using System.Collections.ObjectModel;
using Netflix.Models;
using Prism.Commands;
using Prism.Navigation;

namespace Netflix.ViewModels
{
    public class ComingSoonPageViewModel : ViewModelBase
    {
        private INavigationService navigationService;
        public DelegateCommand SearchCommand { get; }
        public DelegateCommand ProfilePageCommand { get; }
        public ComingSoonPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            this.navigationService = navigationService;
            SearchCommand = new DelegateCommand(async () => await this.navigationService.NavigateAsync("SearchPage"));
            ProfilePageCommand = new DelegateCommand(async () => await this.navigationService.NavigateAsync("ProfilePage"));
        }

        public override async void Initialize(INavigationParameters parameters) => ComingSoon = await API.GetComingSoonShows();

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
