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
using Xamarin.Essentials.Implementation;
using Xamarin.Essentials.Interfaces;
using Xamarin.Forms;


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

            await NavigationService.NavigateAsync("CustomNavigation/LoginPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IAppInfo, AppInfoImplementation>();

            containerRegistry.RegisterForNavigation<CustomNavigation>();
            containerRegistry.RegisterForNavigation<MainTabPage>();

            containerRegistry.RegisterForNavigation<LoginPage, LoginPageViewModel>();
            containerRegistry.RegisterForNavigation<HomePage, HomePageViewModel>();
            containerRegistry.RegisterForNavigation<MyListPage, MyListPageViewModel>();
            containerRegistry.RegisterForNavigation<ShowInfo, ShowInfoViewModel>();
            containerRegistry.RegisterForNavigation<SearchPage, SearchPageViewModel>();
            containerRegistry.RegisterForNavigation<ComingSoonPage, ComingSoonPageViewModel>();
        }

        private static HttpClientHandler handler = new HttpClientHandler
        {
            AutomaticDecompression = DecompressionMethods.Deflate,
        };

        public static HttpClient LocalAPI = new HttpClient(handler)
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
