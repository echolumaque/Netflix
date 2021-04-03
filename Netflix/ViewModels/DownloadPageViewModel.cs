using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Navigation.TabbedPages;

namespace Netflix.ViewModels
{
    public class DownloadPageViewModel : ViewModelBase
    {
        private INavigationService navigationService;
        public DelegateCommand GotoHomePage { get; }
        public DownloadPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            this.navigationService = navigationService;
            GotoHomePage = new DelegateCommand(async () => await this.navigationService.SelectTabAsync("HomePage"));
        }
    }
}
