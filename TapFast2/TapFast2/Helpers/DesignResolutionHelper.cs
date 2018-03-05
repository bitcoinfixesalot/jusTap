using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TapFast2.Helpers
{
    public class DesignResolutionHelper
    {
        public static Size CalculateDesignResolution(bool adsRemoved = false)
        {
            var device = DependencyService.Get<IDevice>(DependencyFetchTarget.GlobalInstance);

            var display = device.Display;
            double adHeight = 0;
            if(!adsRemoved)
               adHeight = GetBannerHeight(device.Display);

            double height = display.Height;
            double width = display.Width;

            //#if DEBUG //galaxy s5
            //            height = 1920;
            //            width = 1080;
            //#endif

            if (height == 0 || width == 0)
            {
                height = 1280;
                width = 750;
            }

            height = height - adHeight - 38;

            double ratio = height / width;


            height = 1280;
            //width = 750;
            width = height / ratio;

            //HockeyApp.MetricsManager.TrackEvent(string.Format("ratio: {0}", ratio));
            //HockeyApp.MetricsManager.TrackEvent(string.Format("size: {0}x{1}", width, doubleHeight));

            return new Size(width, height);
        }

        //Density-independent pixel(dp) – This is a virtual unit of measure to allow layouts to be designed independent of density.
        //    To convert dp into screen pixels the following formula is used: px= dp * dpi / 160

        private static double GetBannerHeight(IDisplay display)
        {
            double dp = 50;
            if (display.ScreenSizeInches() > 6.5)
                dp = 90;


            var px = dp * display.Ydpi / 160;

#if DEBUG //galaxy s5
            px = 50 * 430 / 160;
#endif

            //HockeyApp.MetricsManager.TrackEvent(string.Format("dp: {0}", dp));
            //HockeyApp.MetricsManager.TrackEvent(string.Format("ad px: {0}", px));
            return px;
        }
    }
}
