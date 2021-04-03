using Prism.Commands;
using Prism.Navigation;

namespace Netflix.ViewModels
{
    public class LoginPageViewModel : ViewModelBase
    {
        public DelegateCommand GotoHomePage { get; }
        private readonly INavigationService navigationService;

        public LoginPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            this.navigationService = navigationService;
            GotoHomePage = new DelegateCommand(async () => await this.navigationService.NavigateAsync("/CustomNavigation/MainTabPage?createTab=HomePage&createTab=ComingSoonPage&createTab=DownloadPage"));

        }
    }
}
