using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TapFast2.Enums;
using TapFast2.Resx;
using TapFast2.ViewModel;
using Xamarin.Forms;

namespace TapFast2
{
    public partial class LeaderboardPage : ContentPage
    {
        public LeaderboardPage(LeaderboardViewModel viewModel, GameType gameType)
        {
            BindingContext = viewModel;
            InitializeComponent();
            if (gameType == GameType.NormalGame)
                Title = AppResources.Leaderboard;
            else
                Title = AppResources.ArcadeLeaderboard;

            viewModel.CurrentGameType = gameType;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            var viewModel = BindingContext as LeaderboardViewModel;

            if (viewModel != null&& viewModel.ScoreItems.Count ==0)
            {
                viewModel.GetLeaderboardCommand.Execute(null);
            }
        }
    }
}
