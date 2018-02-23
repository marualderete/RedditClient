using System;

using Android.App;
using Android.Content.PM;
using Android.OS;

using ImageCircle.Forms.Plugin.Droid;

namespace MyRedditApp.Droid
{
    [Activity(Label = "MyRedditApp.Droid", Icon = "@drawable/icon", Theme = "@style/MyTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);

            //In order to use the plugin for profile photos, need to initialize it
            ImageCircleRenderer.Init();

            LoadApplication(new App());
        }
    }
}
