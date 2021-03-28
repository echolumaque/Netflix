using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Netflix.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;

namespace Netflix.ViewModels
{
    public class MyListPageViewModel : ViewModelBase
    {
        private readonly INavigationService navigationService;

        public MyListPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            this.navigationService = navigationService;
        }

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            try
            {
                if (await App.ConnectionString.Table<MyListModel>().CountAsync() > 0)
                {
                    List = new ObservableCollection<MyListModel>(await App.ConnectionString.Table<MyListModel>().ToListAsync());
                    for (int i = 0; i < List.Count; i++)
                    {
                        List[i].ShowInfo = new DelegateCommand<MovieModel>(async (lookForShow) => await InfoPage(lookForShow));
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        #region Properties

        private ObservableCollection<MyListModel> list;
        public ObservableCollection<MyListModel> List
        {
            get { return list; }
            set { SetProperty(ref list, value); }
        }

        #endregion

        #region Methods
        private async Task InfoPage(MovieModel movieModel)
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
