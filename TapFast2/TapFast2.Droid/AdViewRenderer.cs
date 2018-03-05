
using Android.Widget;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using Android.Gms.Ads;

[assembly: ExportRenderer(typeof(TapFast2.Controls.AdView), typeof(TapFast2.Droid.AdViewRenderer))]
namespace TapFast2.Droid
{
    public class AdViewRenderer : ViewRenderer<Controls.AdView, AdView>
    {
        string adUnitId = string.Empty;
        AdSize adSize = AdSize.SmartBanner;
        AdView adView;
        AdView CreateNativeControl()
        {
            if (adView != null)
                return adView;

            adUnitId = "ca-app-pub-5416107198862625/5844787393";//Forms.Context.Resources.GetString(5844787393);
            adView = new AdView(Forms.Context);
            adView.AdSize = adSize;
            adView.AdUnitId = adUnitId;

            var adParams = new LinearLayout.LayoutParams(LayoutParams.WrapContent, LayoutParams.WrapContent);

            adView.LayoutParameters = adParams;

            var builder = new AdRequest.Builder();

#if DEBUG
            builder.AddTestDevice(AdRequest.DeviceIdEmulator);
            builder.AddTestDevice("5DB4DF7E7BA21D9E1B65423CE5A580E5");
#else

             adView.LoadAd(builder.Build());

#endif
            return adView;
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Controls.AdView> e)
        {
            base.OnElementChanged(e);

            if (Control == null)
            {
                CreateNativeControl();
                SetNativeControl(adView);
            }
        }

        //protected override void OnElementChanged(ElementChangedEventArgs e)
        //{
        //    base.OnElementChanged(e);
        //    if (Control == null)
        //    {
        //        CreateNativeControl();
        //        SetNativeControl(adView);
        //    }
        //}
    }
}