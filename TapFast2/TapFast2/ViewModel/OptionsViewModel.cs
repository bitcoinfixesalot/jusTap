using Plugin.InAppBilling.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TapFast2.Constants;
using TapFast2.Enums;
using TapFast2.Helpers;
using TapFast2.Resx;
using TapFast2.Services;
using Xamarin.Forms;
using static TapFast2.Helpers.Settings;

namespace TapFast2
{
    public class OptionsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName]string name = "") =>
           PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        IInAppPurchaseService _inAppPurchaseService;
        public OptionsViewModel(IInAppPurchaseService inAppPurchaseService)
        {
            _inAppPurchaseService = inAppPurchaseService;
            if (Device.OS == TargetPlatform.Android || Device.OS == TargetPlatform.iOS)//TODO: fix this
            {
                IsRemoveAdsVisible = true;
            }
            else
            {
                IsRemoveAdsVisible = false;
            }
            var adsRemoved = AreAdsRemoved((PurchaseState)Settings.PurchaseStatus);
            SetRemoveAdsText(adsRemoved);
        }

        private void SetRemoveAdsText(bool isRemoved)
        {
            if (isRemoved)
                RemoveAdsButtonText = AppResources.AdsRemoved;
            else
                RemoveAdsButtonText = AppResources.RemoveAds;
        }

        Command removeAds;
        public Command RemoveAds => removeAds ?? (removeAds = new Command(async () => await RemoveAdsAsync(), () => !IsBusy));

        private async Task RemoveAdsAsync()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;
                var productID = Options.GetInAppPurchaseProductID();
                var status = await _inAppPurchaseService.CheckAppPurchaseStatus(productID);
                if (AreAdsRemoved(status))
                {
                    PurchaseStatus = (int)status;
                    SetRemoveAdsText(true);
                    return;
                }

                var purchaseResult = await _inAppPurchaseService.MakePurchaseAsync(productID);
                if (AreAdsRemoved(purchaseResult))
                {
                    PurchaseStatus = (int)purchaseResult;
                    SetRemoveAdsText(true);
                }
                else SetRemoveAdsText(false);

            }
            catch (Exception ex)
            {
            }
            finally
            {
                IsBusy = false;
            }
        }

        public bool IsBusy
        {
            get { return busy; }
            set
            {
                busy = value;
                OnPropertyChanged();

                removeAds.ChangeCanExecute();
            }
        }

        private string removeAdsText;

        public string RemoveAdsButtonText
        {
            get { return removeAdsText; }
            set { removeAdsText = value;
                OnPropertyChanged();
            }
        }



        private bool isRemoveAdsVisible;

        public bool IsRemoveAdsVisible
        {
            get { return isRemoveAdsVisible; }
            set
            {
                isRemoveAdsVisible = value;
                OnPropertyChanged();
            }
        }


        public bool SoundEnabled
        {
            get { return Settings.SoundEnabled; }
            set
            {
                //if (Settings.SoundEnabled == value)
                //    return;

                Settings.SoundEnabled = value;
                OnPropertyChanged();
            }
        }

        public bool IsDarkTheme
        {
            get { return Settings.IsDarkTheme; }
            set
            {
                //if (Settings.SoundEnabled == value)
                //    return;
                

                Settings.IsDarkTheme = value;
                OnPropertyChanged();

                if (value)
                    Application.Current.Resources.MergedWith = typeof(Xamarin.Forms.Themes.DarkThemeResources);
                else
                    Application.Current.Resources.MergedWith = typeof(Xamarin.Forms.Themes.LightThemeResources);
            }
        }

        

        List<GameModeKeyValue> _difficultyModes;
        public List<GameModeKeyValue> DifficultyModes
        {
            get
            {
                if (_difficultyModes == null)
                {
                    _difficultyModes = new List<GameModeKeyValue>();
                    _difficultyModes.Add(new GameModeKeyValue { Mode = GameMode.Easy, ModeText = AppResources.Easy });
                    _difficultyModes.Add(new GameModeKeyValue { Mode = GameMode.Normal, ModeText = AppResources.Normal });
                    _difficultyModes.Add(new GameModeKeyValue { Mode = GameMode.Hard, ModeText = AppResources.Hard });
                    _difficultyModes.Add(new GameModeKeyValue { Mode = GameMode.Impossible, ModeText = AppResources.Impossible});
                }

                return _difficultyModes;
            }
        }

        List<GameModeKeyValue> _arcadeGameTimes;
        private bool busy;

        public List<GameModeKeyValue> ArcadeGameTimes
        {
            get
            {
                if (_arcadeGameTimes == null)
                {
                    _arcadeGameTimes = new List<GameModeKeyValue>();
                    _arcadeGameTimes.Add(new GameModeKeyValue { Mode = GameMode.FifteenSeconds, ModeText = AppResources.FifteenSeconds });
                    _arcadeGameTimes.Add(new GameModeKeyValue { Mode = GameMode.ThirtySeconds, ModeText = AppResources.ThirtySeconds });
                    _arcadeGameTimes.Add(new GameModeKeyValue { Mode = GameMode.SixtySeconds, ModeText = AppResources.SixtySeconds });
                }

                return _arcadeGameTimes;
            }
        }

        public GameModeKeyValue SelectedDifficulty
        {
            get
            {
                return DifficultyModes.Single(a => a.Mode == (GameMode)Settings.Difficulty);
            }
            set
            {
                if (Settings.Difficulty == (int)value.Mode)
                    return;

                Settings.Difficulty = (int)value.Mode;
            }
        }

        public GameModeKeyValue SelectedTime
        {
            get
            {
                return ArcadeGameTimes.Single(a => a.Mode == (GameMode)Settings.ArcadeGameMode);
            }
            set
            {
                if (Settings.ArcadeGameMode == (int)value.Mode)
                    return;

                Settings.ArcadeGameMode = (int)value.Mode;
            }
        }


    }

    public class GameModeKeyValue
    {
        public GameMode Mode { get; set; }

        public string ModeText { get; set; }
    }

    //public class ArcadeTimeKeyValue
    //{
    //    public int GameTimeInSeconds { get; set; }

    //    public string GameTimeText { get; set; }
    //}
}
