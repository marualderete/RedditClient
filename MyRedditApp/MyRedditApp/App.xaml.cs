using MyRedditApp.Helpers;
using MyRedditApp.Pages;
using MyRedditApp.PlatformServices;
using MyRedditApp.Services;
using MyRedditApp.Services.Interfaces;
using SQLite;
using Xamarin.Forms;

namespace MyRedditApp
{
    public partial class App : Application
    {
        #region Private properties

        static string _database;
        static SQLiteAsyncConnection _databaseConnection;

		#endregion

        #region App Constructor
  
        public App()
        {
            _database = DependencyService.Get<IAppConnectionService>().GetLocalFilePath("RedditClientDB.db3");
            _databaseConnection = new SQLiteAsyncConnection(Database);
            //CreateDatabaseTables();
            RegisterServices();

            InitializeComponent();

            MainPage = new MainPostMenu();
        }

        #endregion


        #region Public methods

        public static SQLiteAsyncConnection DatabaseConnection
        {
            get { return _databaseConnection; }
        }

        public static string Database
        {
            get { return _database; }
        }

		#endregion

        #region Private  methods

        /// <summary>
        /// Generate an instance of each service in the app, in order to make them usable on it.
        /// </summary>
        void RegisterServices()
        {
            ServiceLocator.Instance.Register<IAuthenticationService, AuthenticationService>();
            ServiceLocator.Instance.Register<IPostService, PostService>();
        }

        void CreateDatabaseTables()
        {
           // DatabaseConnection.CreateTableAsync<UserCredentialResponseModel>().Wait();
           // DatabaseConnection.CreateTableAsync<PeatonUserModel>().Wait();
           
        }

        #endregion
        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
