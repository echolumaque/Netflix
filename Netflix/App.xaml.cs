using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Netflix.Helpers.Database;
using Netflix.Helpers.Renderers;
using Netflix.ViewModels;
using Netflix.Views;
using Prism;
using Prism.Ioc;
using SQLite;
using Xamarin.Essentials;
using Xamarin.Essentials.Implementation;
using Xamarin.Essentials.Interfaces;
using Xamarin.Forms;


[assembly: ExportFont("MaterialIcons-Regular.ttf", Alias = "matf")]
[assembly: ExportFont("MaterialIconsOutlined-Regular.otf", Alias = "mat")]
[assembly: ExportFont("Roboto-Bold.ttf", Alias = "Bold")]
[assembly: ExportFont("Roboto-Regular.ttf", Alias = "Regular")]

namespace Netflix
{
    public partial class App
    {
        public App(IPlatformInitializer initializer)
            : base(initializer)
        {
        }

        protected override async void OnInitialized()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NDE3MDA4QDMxMzgyZTM0MmUzMFFWMG5rL1FyNFZLNnhRdEFESVdpYmJ6QjB3Y3IzRytpc3lqQXVQeGV5NDQ9");
            InitializeComponent();
            Preferences.Set("search", string.Empty);
            await NavigationService.NavigateAsync("LoginPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IAppInfo, AppInfoImplementation>();

            containerRegistry.RegisterForNavigation<CustomNavigation>();
            containerRegistry.RegisterForNavigation<MainTabPage>();

            containerRegistry.RegisterForNavigation<LoginPage, LoginPageViewModel>();
            containerRegistry.RegisterForNavigation<HomePage, HomePageViewModel>();
            containerRegistry.RegisterDialog<InfoPopupPage, InfoPopupPageViewModel>();
            containerRegistry.RegisterForNavigation<EpisodePage, EpisodePageViewModel>();
            containerRegistry.RegisterForNavigation<ComingSoonPage, ComingSoonPageViewModel>();
            containerRegistry.RegisterForNavigation<SearchPage, SearchPageViewModel>();
            containerRegistry.RegisterForNavigation<MyListPage, MyListPageViewModel>();
        }

        public static HttpClient LocalAPI = new(new HttpClientHandler {AutomaticDecompression = DecompressionMethods.Deflate} )
        {
            BaseAddress = new Uri("http://echo.somee.com/netflixapp")
        };

        public static readonly Lazy<SQLiteAsyncConnection> dbConnection = new Lazy<SQLiteAsyncConnection>(() => new SQLiteAsyncConnection(SQLiteConstants.DatabasePath, SQLiteConstants.Flags));

        public static SQLiteAsyncConnection ConnectionString = dbConnection.Value;

        public static async Task<SQLiteAsyncConnection> CreateDatabaseTable<T>()
        {
            if (!ConnectionString.TableMappings.Any(x => x.MappedType == typeof(T)))
            {
                await ConnectionString.EnableWriteAheadLoggingAsync();
                await ConnectionString.CreateTablesAsync(CreateFlags.None, typeof(T));
            }
            return ConnectionString;
        }
    }
}
