using Prism.Commands;
using Prism.Navigation;

namespace Netflix.ViewModels
{
    public class ProfilePageViewModel : ViewModelBase
    {
        public DelegateCommand PopStack { get; }
        public ProfilePageViewModel(INavigationService navigationService) : base(navigationService)
        {
            PopStack = new DelegateCommand(async () => await navigationService.GoBackAsync());
        }
    }
}
