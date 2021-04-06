using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Netflix.Models;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services.Dialogs;

namespace Netflix.ViewModels
{
    public class MyListPageViewModel : ViewModelBase
    {
        private INavigationService navigationService;
        public DelegateCommand<MovieModel> ShowPopupCommand { get; }
        public MyListPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService)
        {
            this.navigationService = navigationService;
            this.dialogService = dialogService;
            ShowPopupCommand = new DelegateCommand<MovieModel>(async (show) => await ShowPopup(show));
        }

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            ShowsList = await App.ConnectionString.Table<MovieModel>().CountAsync() > 0 ? 
                new ObservableCollection<MovieModel>(await App.ConnectionString.Table<MovieModel>().ToListAsync()) : new ObservableCollection<MovieModel>();

            for (int i = 0; i < ShowsList.Count; i++)
            {
                ShowsList[i].ShowInfoCommand = new DelegateCommand<MovieModel>(async (show) => await ShowPopup(show));
            }
        }


        #region Properties

        private ObservableCollection<MovieModel> showsList;
        public ObservableCollection<MovieModel> ShowsList
        {
            get { return showsList; }
            set { SetProperty(ref showsList, value); }
        }
        #endregion

        #region Methods

        private async Task ShowPopup(MovieModel movieModel)
        {
            var parameters = new NavigationParameters
            {
                {
                    "show",
                    new MovieModel
                    {
                        Title = movieModel.Title,
                        Year = movieModel.Year,
                        Synopsis = movieModel.Synopsis,
                        Casts = movieModel.Casts,
                        InfoThumbnail = movieModel.InfoThumbnail,
                        Thumbnail = movieModel.Thumbnail
                    }
                }
            };
            await navigationService.NavigateAsync("InfoPopupPage", parameters);
        }

        #endregion
    }
}
