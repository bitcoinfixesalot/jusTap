using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace TapFast2.Constants
{
    public class Options
    {
        public const string InAppPurchaseProductID = "com.vorsightech.tapfast.remove_ads";
        public static string GetInAppPurchaseProductID()
        {
            return Device.OnPlatform<string>(InAppPurchaseProductID, InAppPurchaseProductID, string.Empty);
        }

        public const string IOS_AD_MOB = "ca-app-pub-5416107198862625~7271148193";
        public const string ANDROID_AD_MOB = "ca-app-pub-5416107198862625~4368054193";

        public const string MENU_BUTTON_STYLE = "MENU_BUTTON_STYLE";

        //public const string SOUND_ENABLED = "SOUND_ENABLED";
        //public const string INTERVAL_BETWEEN_COLORS = "INTERVALS";

        //public const string YOUR_HIGHSCORE = "YOUR_HIGHSCORE";


        public const string SEC_TILE_ID = "tapFastSecondaryTileId";

        public const float FIFTEEN = 15.0f;
        public const float THIRTY = 30.0f;
        public const float SIXTY = 60.0f;

        //public const CCColor3B adsasf = Color.FromRgb(27, 161, 226)

        public const int LEVEL_TWO = 10;
        public const int LEVEL_THREE = 20;
        public const int LEVEL_FOUR = 30;
        public const int LEVEL_FIVE = 40;
        public const int LEVEL_SIX = 50;
        public const int LEVEL_SEVEN = 60;
        public const int LEVEL_EIGHT = 70;
        public const int LEVEL_NINE = 80;
        //public const int LEVEL_TEN = 90;

        static List<int> _levels;
        public static List<int> Levels
        {
            get
            {
                if (_levels == null)
                {
                    _levels = new List<int>();
                    _levels.Add(LEVEL_TWO);
                    _levels.Add(LEVEL_THREE);
                    _levels.Add(LEVEL_FOUR);
                    _levels.Add(LEVEL_FIVE);
                    _levels.Add(LEVEL_SIX);
                    _levels.Add(LEVEL_SEVEN);
                    _levels.Add(LEVEL_EIGHT);
                    _levels.Add(LEVEL_NINE);
                    //_levels.Add(LEVEL_TEN);
                }
#if DEBUG
                //_levels.Clear();
                //_levels.Add(2);
                //_levels.Add(4);
                //_levels.Add(6);
                //_levels.Add(8);
                //_levels.Add(10);
                //_levels.Add(12);
                //_levels.Add(14);
                //_levels.Add(20);
#endif

                return _levels;
            }

        }
    }
}
