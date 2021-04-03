using Netflix.ViewModels;
using Xamarin.Forms;

namespace Netflix.Views
{
    public partial class EpisodePage : ContentPage
    {
        public EpisodePage()
        {
            InitializeComponent();
        }

        protected override bool OnBackButtonPressed()
        {
            var vm = (EpisodePageViewModel)BindingContext;
            vm.navigationService.NavigateAsync("/CustomNavigation/MainTabPage?createTab=HomePage&createTab=ComingSoonPage&createTab=DownloadPage");
            return base.OnBackButtonPressed();
        }
    }
}