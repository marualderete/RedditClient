using System;
using System.Diagnostics;
using System.Threading.Tasks;
using MyRedditApp.Helpers;
using MyRedditApp.Models;
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

        IAuthenticationService _authService;

        // This is for run a particular action in background, on an interval of time that defaultTimespan defines.
        // I will define this in order to refresh the token every N minutes. It is just a hack to avoid waste time on this.
        static Stopwatch stopWatch = new Stopwatch();
        const int defaultTimespan = 40; //minutes

		#endregion

        #region App Constructor
  
        public App()
        {

            RegisterServices();

			_authService = ServiceLocator.Instance.Get<IAuthenticationService>();

            InitializeComponent();

            MainPage = new NavigationPage(new MainPostMenu());
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


        #endregion
        protected override void OnStart()
        {
            // On start runs when your application launches from a closed state, 
            if (!stopWatch.IsRunning)
            {
                stopWatch.Start();
            }

            Device.StartTimer(TimeSpan.FromMinutes(1), () =>
            {
				if (stopWatch.IsRunning && stopWatch.Elapsed.Minutes >= defaultTimespan)
				{
                    Task.Factory.StartNew( async () =>{

						//prepare to perform your data pull here as we have hit the 1 minute mark   
						// Perform your long running operations here.
						await _authService.GetRequestToken();
						
						stopWatch.Restart();

                    }).Wait();
				}
                // Always return true as to keep our device timer running.
                return true;
            });

        }

        protected override void OnSleep()
        {
            // Ensure our stopwatch is reset so the elapsed time is 0.
            stopWatch.Reset();
        }

        protected override void OnResume()
        {
            // App enters the foreground so start our stopwatch again.
            stopWatch.Start();

        }
    }
}
