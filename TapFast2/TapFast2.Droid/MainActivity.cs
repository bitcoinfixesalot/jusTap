
using Android.App;
using Android.Content.PM;
using Android.OS;
using Xamarin.Forms.Platform.Android;
using TapFast2.Services;
using HockeyApp.Android;
using HockeyApp.Android.Metrics;
using Xamarin.Forms;
using System.IO;
using Android.Gms.Ads;
using Acr.UserDialogs;
using TapFast2.Droid.Device;
using Microsoft.Azure.Mobile;
using Android.Content;

using Plugin.InAppBilling;

[assembly: MetaData("net.hockeyapp.android.appIdentifier", Value = HockeyAppHelper.AppIds.HockeyAppId_Droid)]
namespace TapFast2.Droid
{
    [Activity (Label = "TapFast2.Droid", Icon = "@drawable/icon", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Portrait)]
	public class MainActivity : FormsAppCompatActivity  //global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
    {
		protected override void OnCreate (Bundle bundle)
		{
            ToolbarResource = Resource.Layout.toolbar;
            TabLayoutResource = Resource.Layout.tabs;

            base.OnCreate (bundle);
            InitializeHockeyApp();



            // New Xlabs             
            //var container = new SimpleContainer();
            //container.Register<IDevice> (t => AndroidDevice.CurrentDevice);
            //Resolver.SetResolver(container.GetResolver()); // Resolving the services  
            DependencyService.Register<IDevice, AndroidDevice>();
            // End new Xlabs 

            global::Xamarin.Forms.Forms.Init (this, bundle);

            Microsoft.WindowsAzure.MobileServices.CurrentPlatform.Init();
            SQLitePCL.Batteries.Init();

            var azureService = DependencyService.Get<AzureService>();



            if (!File.Exists(azureService.CurrentPath))
            {
                File.Create(azureService.CurrentPath).Dispose();
            }

            MobileAds.Initialize(ApplicationContext, Constants.Options.ANDROID_AD_MOB);
            UserDialogs.Init(this);

            MobileCenter.Configure("26ae2a0a-a386-4d86-910c-57540b745470");
            LoadApplication(new Application ());
            var x = typeof(Xamarin.Forms.Themes.DarkThemeResources);
            x = typeof(Xamarin.Forms.Themes.LightThemeResources);
            x = typeof(Xamarin.Forms.Themes.Android.UnderlineEffect);

            
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            InAppBillingImplementation.HandleActivityResult(requestCode, resultCode, data);
        }

        private void InitializeHockeyApp()
        {
            CrashManager.Register(this);
            MetricsManager.Register(Application);
        }

        
    }
}

