using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TapFast2.Enums;
using Xamarin.Forms;

namespace TapFast2
{
    public class LeaderboardTabbedPage : TabbedPage
    {
        public LeaderboardTabbedPage()
        {
            Device.OnPlatform(iOS: () => Padding = new Thickness(0, 20, 0, 0)
                  , Android: () => Padding = new Thickness(0, 0, 0, 0)
                  , WinPhone: () => Padding = new Thickness(0, 0, 0, 0));

            var locator = (Xamarin.Forms.Application.Current.Resources["Locator"] as ViewModelLocator);

            var leaderboardViewModel = locator.NormalLeaderboardViewModel;
            var arcadeLeaderboardViewModel = locator.ArcadeLeaderboardViewModel;


            Children.Add(new LeaderboardPage(leaderboardViewModel,GameType.NormalGame));
            Children.Add(new LeaderboardPage(arcadeLeaderboardViewModel, GameType.ArcadeGame));
        }

    }
}
