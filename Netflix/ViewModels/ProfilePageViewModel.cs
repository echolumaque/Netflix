using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;

namespace Netflix.ViewModels
{
    public class ProfilePageViewModel : ViewModelBase
    {
        private INavigationService navigationService;
        public ProfilePageViewModel(INavigationService navigationService) : base(navigationService)
        {
            this.navigationService = navigationService;
        }
    }
}
