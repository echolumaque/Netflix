using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Netflix.Helpers.API.Interfaces;
using Netflix.Models;
using Prism;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Refit;

namespace Netflix.ViewModels
{
    public class ComingSoonPageViewModel : ViewModelBase
    {
        private readonly INavigationService navigationService;
        public ComingSoonPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            this.navigationService = navigationService;
        }

        public override async void Initialize(INavigationParameters parameters)
        {
            ComingSoon = await RestService.For<IMovie>(App.LocalAPI).GetComingSoonShows();

            for (int i = 0; i < ComingSoon.Count; i++)
            {
                ComingSoon[i].ShowInfoCommand = new DelegateCommand<MovieModel>(async (show) => await ShowInfoPage(show));
            }
        }

        #region Properties

        private ObservableCollection<MovieModel> comingSoon;
        public ObservableCollection<MovieModel> ComingSoon
        {
            get { return comingSoon; }
            set { SetProperty(ref comingSoon, value); }
        }
        #endregion

        #region Methods

        private async Task ShowInfoPage(MovieModel movieModel) //for model
        {
            var parameters = new NavigationParameters
            {
                {
                    "show",
                    new MovieModel
                    {
                        Casts = movieModel.Casts,
                        Genre = movieModel.Genre,
                        Synopsis = movieModel.Synopsis,
                        Title = movieModel.Title,
                        Year = movieModel.Year,
                        Rating = movieModel.Rating,
                        InfoThumbnail = movieModel.InfoThumbnail,
                        Thumbnail = movieModel.Thumbnail
                    }
                }
            };
            await navigationService.NavigateAsync("ShowInfo", parameters);
        }
        #endregion
    }
}
