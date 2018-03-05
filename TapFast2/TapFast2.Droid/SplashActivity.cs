using System;
using System.Collections.Generic;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Util;


using CocosSharp;
using TapFast2;
using Xamarin.Forms.Platform.Android;
using TapFast2.Services;
using HockeyApp.Android;
using HockeyApp.Android.Metrics;
using Xamarin.Forms;
using System.Threading.Tasks;
using Android.Support.V7.App;
using Plugin.CurrentActivity;

namespace TapFast2.Droid
{
    [Activity(Theme = "@style/MyTheme.Splash", MainLauncher = true, NoHistory = true)]
    public class SplashActivity : AppCompatActivity
    {
        static readonly string TAG = "X:" + typeof(SplashActivity).Name;

        public override void OnCreate(Bundle savedInstanceState, PersistableBundle persistentState)
        {
            base.OnCreate(savedInstanceState, persistentState);

            //var mainActivityIntent = new Intent(this, typeof(MainActivity));
            //StartActivity(typeof(MainActivity));
            //Finish();

            // Log.Debug(TAG, "SplashActivity.OnCreate");

            //Task startupWork = new Task(() =>
            //{
            //    Log.Debug(TAG, "Performing some startup work that takes a bit of time.");
            //    Task.Delay(5000); // Simulate a bit of startup work.
            //    Log.Debug(TAG, "Working in the background - important stuff.");
            //});

            //startupWork.ContinueWith(t =>
            //{
            //    Log.Debug(TAG, "Work is finished - start Activity1.");
            //    StartActivity(new Intent(this, typeof(MainActivity)));
            //}, TaskScheduler.FromCurrentSynchronizationContext());

            //startupWork.Start();
        }

        protected override void OnResume()
        {
            base.OnResume();

            StartActivity(typeof(MainActivity));



        }
    }
}