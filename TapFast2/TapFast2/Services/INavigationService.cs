using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TapFast2.Enums;
using Xamarin.Forms;

namespace TapFast2.Services
{
    public interface INavigationService
    {
        //TODO: move this
        Size DesignResolution { get; set; }

        GameType GameTypeSelected { get; set; }

        Task NavigateToMenu();

        Task NavigateToGame();

        Task NavigateToArcadeGame();

        Task NavigateToOptions();
        Task NavigateToLeaderboard();
        Task NavigateToGameOver();
        Task NavigateToAbout();
        Task NavigateToHowTo();
    }
}
