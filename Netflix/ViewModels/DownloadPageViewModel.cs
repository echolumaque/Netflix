using Prism.Commands;
using Prism.Navigation;
using Prism.Navigation.TabbedPages;

namespace Netflix.ViewModels
{
    public class DownloadPageViewModel : ViewModelBase
    {
        private INavigationService navigationService;
        public DelegateCommand GotoHomePage { get; }
        public DelegateCommand SearchCommand { get; }
        public DelegateCommand ProfilePageCommand { get; }
        public DownloadPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            this.navigationService = navigationService;
            GotoHomePage = new DelegateCommand(async () => await this.navigationService.SelectTabAsync("HomePage"));
            SearchCommand = new DelegateCommand(async () => await this.navigationService.NavigateAsync("SearchPage"));
            ProfilePageCommand = new DelegateCommand(async () => await this.navigationService.NavigateAsync("ProfilePage"));
        }
    }
}
