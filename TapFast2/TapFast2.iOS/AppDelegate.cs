using Foundation;
using UIKit;

using TapFast2;
using System;
using HockeyApp.iOS;
using TapFast2.Services;
using System.IO;
using Xamarin.Forms;
using Google.MobileAds;
using TapFast2.iOS.Device;

namespace TapFast2.iOS
{
	[Register ("AppDelegate")]
	public partial class AppDelegate : 
	global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate 
	{
		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
            InitializeHockeyApp();

            // New Xlabs             
            //var container = new SimpleContainer();
            //container.Register<IDevice>(t => AppleDevice.CurrentDevice);
            //Resolver.SetResolver(container.GetResolver()); // End new Xlabs
            Forms.Init ();

            Microsoft.WindowsAzure.MobileServices.CurrentPlatform.Init();
            SQLitePCL.Batteries.Init();

            var azureService = DependencyService.Get<AzureService>();

            if (!File.Exists(azureService.CurrentPath))
            {
                File.Create(azureService.CurrentPath).Dispose();
            }
            RegisterDevice();

            LoadApplication(new TapFast2.Application ());

            var x = typeof(Xamarin.Forms.Themes.DarkThemeResources);
            x = typeof(Xamarin.Forms.Themes.LightThemeResources);
            x = typeof(Xamarin.Forms.Themes.iOS.UnderlineEffect);

            MobileAds.Configure(Constants.Options.IOS_AD_MOB);
            return base.FinishedLaunching (app, options);
		}

        private void RegisterDevice()
        {
            var deviceInfo = AppleDevice.GetDeviceInfo();

            switch (deviceInfo.Type)
            {
                case DeviceType.Phone:
                    DependencyService.Register<IDevice, Phone>();
                    break;
                case DeviceType.Pad:
                    DependencyService.Register<IDevice, Pad>();

                    break;
                case DeviceType.Pod:
                    DependencyService.Register<IDevice, Pod>();
                    break;
                default:
                    DependencyService.Register<IDevice, Simulator>();
                    break;

            }

        }

        private void InitializeHockeyApp()
        {
            var manager = BITHockeyManager.SharedHockeyManager;
            manager.Configure(HockeyAppHelper.AppIds.HockeyAppId_iOS);
            manager.StartManager();
            manager.Authenticator.AuthenticateInstallation();
        }
    }
}