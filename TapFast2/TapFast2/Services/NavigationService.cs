using Plugin.InAppBilling.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TapFast2.Enums;
using TapFast2.Helpers;
using Xamarin.Forms;
using static TapFast2.Helpers.Settings;

namespace TapFast2.Services
{
    public class NavigationService : INavigationService
    {
        public GameType GameTypeSelected
        {
            get;
            set;
        }

        public Size DesignResolution { get; set; }


        public NavigationService()
        {
        }


        public async Task NavigateToArcadeGame()
        {
            CalculateResolution();
            GameTypeSelected = GameType.ArcadeGame;
            await Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new GamePage(this), false);
        }

        private void CalculateResolution()
        {
#if DEBUG
            DesignResolution = DesignResolutionHelper.CalculateDesignResolution(true);
#else

            DesignResolution = DesignResolutionHelper.CalculateDesignResolution(AreAdsRemoved((PurchaseState)PurchaseStatus));
#endif
        }

        public async Task NavigateToGame()
        {
            CalculateResolution();
            GameTypeSelected = GameType.NormalGame;
            await Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new GamePage(this), false);
        }

        public async Task NavigateToGameOver()
        {
            GC.Collect();
            await Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new GameOverPage(), false);
        }

        public async Task NavigateToLeaderboard()
        {
            await Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new LeaderboardTabbedPage(), false);
        }

        public async Task NavigateToMenu()
        {
            //await Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new MenuPage(this), false);
            await Xamarin.Forms.Application.Current.MainPage.Navigation.PopAsync(false);
        }

        public async Task NavigateToOptions()
        {
            await Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new OptionsTabbedPage(), false);
        }

        public async Task NavigateToAbout()
        {
            await Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new AboutPage(), false);
        }

        public async Task NavigateToHowTo()
        {
            await Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new HowToPage(), false);
        }
    }
}
