using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Netflix.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NetflixNavigation : ContentView
    {

        public NetflixNavigation()
        {
            InitializeComponent();
            Content.BindingContext = this;
        }
        #region BindableProperties

        public static readonly BindableProperty PageTitleProperty = BindableProperty.Create(
            nameof(PageTitle),
            typeof(string),
            typeof(NetflixNavigation),
            string.Empty
            );

        public static readonly BindableProperty SearchCommandProperty = BindableProperty.Create(
            nameof(SearchCommand),
            typeof(ICommand),
            typeof(NetflixNavigation),
            null
            );

        public static readonly BindableProperty ProfilePicProperty = BindableProperty.Create(
            nameof(ProfilePic),
            typeof(ImageSource),
            typeof(NetflixNavigation),
            null
            );

        public static readonly BindableProperty IsProfilePicVisibleProperty = BindableProperty.Create(
           nameof(IsProfilePicVisible),
           typeof(bool),
           typeof(NetflixNavigation),
           true
           );

        public static readonly BindableProperty ProfilePicCommandProperty = BindableProperty.Create(
            nameof(ProfilePicCommand),
            typeof(ICommand),
            typeof(NetflixNavigation),
            null
            );

        public static readonly BindableProperty IsSearchVisibleProperty = BindableProperty.Create(
           nameof(IsSearchVisible),
           typeof(bool),
           typeof(NetflixNavigation),
           true
           );

        public static readonly BindableProperty SearchHorizontalProperty = BindableProperty.Create(
           nameof(SearchHorizontal),
           typeof(LayoutOptions),
           typeof(NetflixNavigation),
           LayoutOptions.EndAndExpand
           );

        public static readonly BindableProperty DPHorizontalProperty = BindableProperty.Create(
           nameof(DPHorizontal),
           typeof(LayoutOptions),
           typeof(NetflixNavigation),
           LayoutOptions.End
           );

        public static readonly BindableProperty NavigationStackProperty = BindableProperty.Create(
           nameof(NavigationStack),
           typeof(ICommand),
           typeof(NetflixNavigation),
           null
           );

        public static readonly BindableProperty IsBackArrowVisibleProperty = BindableProperty.Create(
           nameof(BackArrowProperty),
           typeof(bool),
           typeof(NetflixNavigation),
           true
           );
        #endregion



        #region Properties

        public string PageTitle
        {
            get { return GetValue(PageTitleProperty).ToString(); }
            set { SetValue(PageTitleProperty, value); }
        }

        public ICommand SearchCommand
        {
            get { return (ICommand)GetValue(SearchCommandProperty); }
            set { SetValue(SearchCommandProperty, value); }
        }

        public ImageSource ProfilePic
        {
            get { return (ImageSource)GetValue(ProfilePicProperty); }
            set { SetValue(ProfilePicProperty, value); }
        }

        public ICommand ProfilePicCommand
        {
            get { return (ICommand)GetValue(ProfilePicCommandProperty); }
            set { SetValue(ProfilePicCommandProperty, value); }
        }

        public bool IsSearchVisible
        {
            get { return (bool)GetValue(IsSearchVisibleProperty); }
            set { SetValue(IsSearchVisibleProperty, value); }
        }

        public LayoutOptions SearchHorizontal
        {
            get { return (LayoutOptions)GetValue(SearchHorizontalProperty); }
            set { SetValue(SearchHorizontalProperty, value); }
        }

        public LayoutOptions DPHorizontal
        {
            get { return (LayoutOptions)GetValue(DPHorizontalProperty); }
            set { SetValue(DPHorizontalProperty, value); }
        }

        public ICommand NavigationStack
        {
            get { return (ICommand)GetValue(NavigationStackProperty); }
            set { SetValue(NavigationStackProperty, value); }
        }

        public bool BackArrowProperty
        {
            get { return (bool)GetValue(IsBackArrowVisibleProperty); }
            set { SetValue(IsBackArrowVisibleProperty, value); }
        }

        public bool IsProfilePicVisible
        {
            get { return (bool)GetValue(IsProfilePicVisibleProperty); }
            set { SetValue(IsProfilePicVisibleProperty, value); }
        }
        #endregion
    }
}