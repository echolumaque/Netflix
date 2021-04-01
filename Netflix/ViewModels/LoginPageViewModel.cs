using System.Threading.Tasks;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services.Dialogs;

namespace Netflix.ViewModels
{
    public class LoginPageViewModel : ViewModelBase
    {
        public DelegateCommand GotoHomePage { get; }
        private readonly INavigationService navigationService;

        public LoginPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            this.navigationService = navigationService;
            GotoHomePage = new DelegateCommand(async () => await HomePage());

        }

        private async Task HomePage() => await navigationService.NavigateAsync("/MainTabPage");
    }
}
