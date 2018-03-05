// Helpers/Settings.cs
using Plugin.InAppBilling.Abstractions;
using Plugin.Settings;
using Plugin.Settings.Abstractions;
using TapFast2.Enums;

namespace TapFast2.Helpers
{
    /// <summary>
    /// This is the Settings static class that can be used in your Core solution or in any
    /// of your client applications. All settings are laid out the same exact way with getters
    /// and setters. 
    /// </summary>
    public static class Settings
    {
        private static ISettings AppSettings
        {
            get
            {
                return CrossSettings.Current;
            }
        }

        #region Setting Constants

        private const string SettingsKey = "settings_key";
        //private static readonly string SettingsDefault = string.Empty;

        const int EASY = 900;
        const int NORMAL = 800;
        const int HARD = 700;
        const int IMPOSSIBLE = 700;

        //public const int ThirtySeconds = 30;
        //public const int FifteenSeconds = 15;
        //public const int SixtySeconds = 60;

        private const string NormalGameModeKey = "NormalGameModeKey";
        private const string SoundEnabledKey = "SoundEnabledKey";
        private const string ArcadeGameModeKey = "ArcadeGameModeKey";

       private const string HighScoreEasyKey = "HighScoreEasyKey";
       private const string HighScoreNormalKey = "HighScoreNormalKey";
       private const string HighScoreHardKey = "HighScoreHardKey";
       private const string HighScoreImpossibleKey = "HighScoreImpossibleKey";

        private const string HighScore15Key = "HighScore15Key";
        private const string HighScore30Key = "HighScore30Key";
        private const string HighScore60Key = "HighScore60Key";

        private const string YourNameKey = "YourNameKey";
        private const string IsDarkThemeKey = "IsDarkThemeKey";

        private const string IsAdsRemovedKey = "IsAdsRemovedKey";
        private const string PurchaseIdKey = "PurchaseIdKey";
        private const string PurchaseTokenKey = "PurchaseTokenKey";
        private const string PurchaseStateKey = "PurchaseStateKey";


        public static bool AreAdsRemoved(PurchaseState state)
        {
            switch (state)
            {
                case PurchaseState.Restored:
                case PurchaseState.Purchasing:
                case PurchaseState.Deferred:
                case PurchaseState.Purchased:
                    return true;
                case PurchaseState.Canceled:
                case PurchaseState.Refunded:
                case PurchaseState.Failed:
                case PurchaseState.Unknown:
                default:
                    return false;
            }
        }

        #endregion

        public static int PurchaseStatus
        {
            get
            {
                return AppSettings.GetValueOrDefault<int>(PurchaseStateKey, (int)PurchaseState.Unknown);
            }
            set
            {
                AppSettings.AddOrUpdateValue<int>(PurchaseStateKey, value);
            }
        }

        public static string PurchaseToken
        {
            get
            {
                return AppSettings.GetValueOrDefault<string>(PurchaseTokenKey);
            }
            set
            {
                AppSettings.AddOrUpdateValue<string>(PurchaseTokenKey, value);
            }
        }

        public static string PurchaseId
        {
            get
            {
                return AppSettings.GetValueOrDefault<string>(PurchaseIdKey);
            }
            set
            {
                AppSettings.AddOrUpdateValue<string>(PurchaseIdKey, value);
            }
        }

        //public static bool IsAdsRemoved
        //{
        //    get
        //    {
        //        return AppSettings.GetValueOrDefault<bool>(IsAdsRemovedKey, false);
        //    }
        //    set
        //    {
        //        AppSettings.AddOrUpdateValue<bool>(IsAdsRemovedKey, value);
        //    }
        //}


        public static string YourName
        {
            get { return AppSettings.GetValueOrDefault<string>(YourNameKey); }
            set { AppSettings.AddOrUpdateValue<string>(YourNameKey, value); }
        }

        public static int Highscore15
        {
            get { return AppSettings.GetValueOrDefault<int>(HighScore15Key, 0); }
            set { AppSettings.AddOrUpdateValue<int>(HighScore15Key, value); }
        }

        public static int Highscore30
        {
            get { return AppSettings.GetValueOrDefault<int>(HighScore30Key, 0); }
            set { AppSettings.AddOrUpdateValue<int>(HighScore30Key, value); }
        }

        public static int Highscore60
        {
            get { return AppSettings.GetValueOrDefault<int>(HighScore60Key, 0); }
            set { AppSettings.AddOrUpdateValue<int>(HighScore60Key, value); }
        }


        public static int HighscoreEasy
        {
            get { return AppSettings.GetValueOrDefault<int>(HighScoreEasyKey, 0); }
            set { AppSettings.AddOrUpdateValue<int>(HighScoreEasyKey, value); }
        }

        public static int HighscoreNormal
        {
            get { return AppSettings.GetValueOrDefault<int>(HighScoreNormalKey, 0); }
            set { AppSettings.AddOrUpdateValue<int>(HighScoreNormalKey, value); }
        }
        public static int HighscoreHard
        {
            get { return AppSettings.GetValueOrDefault<int>(HighScoreHardKey, 0); }
            set { AppSettings.AddOrUpdateValue<int>(HighScoreHardKey, value); }
        }
        public static int HighscoreImpossible
        {
            get { return AppSettings.GetValueOrDefault<int>(HighScoreImpossibleKey, 0); }
            set { AppSettings.AddOrUpdateValue<int>(HighScoreImpossibleKey, value); }
        }

        public static int Difficulty
        {
            get
            {
                return AppSettings.GetValueOrDefault<int>(NormalGameModeKey, (int)GameMode.Easy);
            }
            set
            {
                AppSettings.AddOrUpdateValue<int>(NormalGameModeKey, value);
            }
        }

        public static bool SoundEnabled
        {
            get
            {
                return AppSettings.GetValueOrDefault<bool>(SoundEnabledKey, true);
            }
            set
            {
                AppSettings.AddOrUpdateValue<bool>(SoundEnabledKey, value);
            }
        }

        public static bool IsDarkTheme
        {
            get
            {
                return AppSettings.GetValueOrDefault<bool>(IsDarkThemeKey, false);
            }
            set
            {
                AppSettings.AddOrUpdateValue<bool>(IsDarkThemeKey, value);
            }
        }

        public static int ArcadeGameMode
        {
            get
            {
                return AppSettings.GetValueOrDefault<int>(ArcadeGameModeKey, (int)GameMode.FifteenSeconds);
            }
            set
            {
                AppSettings.AddOrUpdateValue<int>(ArcadeGameModeKey, value);
            }
        }

        /// <summary>
        /// color changing in miliseconds
        /// </summary>
        //public static int ColorChangingInterval
        //{
        //    get
        //    {
        //        int result;
        //        switch ((GameMode)Difficulty)
        //        {

        //            case GameMode.Normal:
        //                result = NORMAL;
        //                break;
        //            case GameMode.Hard:
        //                result = HARD;
        //                break;
        //            case GameMode.Impossible:
        //                result = IMPOSSIBLE;
        //                break;
        //            case GameMode.Easy:
        //            default:
        //                result = EASY;
        //                break;
        //        }
        //        return result;
        //    }
        //}

    }
}