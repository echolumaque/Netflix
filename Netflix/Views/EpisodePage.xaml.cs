using Xamarin.Forms;
using Netflix.ViewModels;
using System.Threading.Tasks;
using Prism.Navigation;

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
            vm.navigationService.NavigateAsync("/MainTabPage");
            return base.OnBackButtonPressed();
        }
    }
}
