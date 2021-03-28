using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Netflix.Helpers.Renderers
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomNavigation : NavigationPage
    {
        public CustomNavigation() : base()
        {
            InitializeComponent();
        }

        public CustomNavigation(Page root) : base(root)
        {
            InitializeComponent();
        }

        public bool IgnoreLayoutChange { get; set; } = false;

        protected override void OnSizeAllocated(double width, double height)
        {
            if (!IgnoreLayoutChange)
                base.OnSizeAllocated(width, height);
        }
    }
}