using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TapFast2.Services
{
    public static class HockeyAppHelper
    {
        public static class AppIds
        {
            public const string HockeyAppId_iOS = "f1af84638eaf47cea9470da6f80f3227";
            public const string HockeyAppId_Droid = "d34ee26ff62140c4abd2b9e8a0afa563";
        }

        public static class Events
        {
            public const string NormalGameStarted = "User Started Game";
            public const string ArcadeGameStarted = "User Started Arcade Game";
        }
    }
}
