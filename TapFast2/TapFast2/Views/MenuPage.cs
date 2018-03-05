using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using TapFast2;
using TapFast2.Enums;
using TapFast2.Resx;
using TapFast2.Services;
using Xamarin.Forms;

namespace TapFast2
{
	public class MenuPage : ContentPage
	{
        INavigationService _navigationService;
        //NavigationPage _optionsPage;
        public MenuPage (INavigationService navigationService)
		{
            Device.OnPlatform(iOS: () => Padding = new Thickness(0, 20, 0, 0)
            , Android: () => Padding = new Thickness(0, 0, 0, 0)
            , WinPhone: () => Padding = new Thickness(0, 0, 0, 0));

            _navigationService = navigationService;
            //InitPages();
            var layout = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                VerticalOptions = LayoutOptions.Center
            };

            layout.Children.Add(CreateMainGrid());

            Content = layout;

            NavigationPage.SetHasNavigationBar(this, false);
        }

        //private void InitPages()
        //{
        //    _optionsPage = new NavigationPage(new OptionsTabbedPage());
        //}

        private Grid CreateMainGrid()
        {
            var grid = new Grid
            {
                Padding = new Thickness(40)
                //RowSpacing = 50, ColumnSpacing = 50
            };

            var buttonStyle = (Style)Xamarin.Forms.Application.Current.Resources[Constants.Options.MENU_BUTTON_STYLE];
            //grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(0, GridUnitType.Auto) });

            var newGameButton = new Button { HeightRequest = 200, WidthRequest = 200, Text = "New Game", Style = buttonStyle, HorizontalOptions = LayoutOptions.EndAndExpand };
            var arcadeGameButton = new Button { HeightRequest = 200, WidthRequest = 200, Text = "Arcade Game", Style = buttonStyle, HorizontalOptions = LayoutOptions.StartAndExpand };
            var leaderboardButton = new Button { HeightRequest = 200, WidthRequest = 405, Text = AppResources.Leaderboard, Style = buttonStyle, HorizontalOptions =  LayoutOptions.Center};
            var optionsButton = new Button { HeightRequest = 200, WidthRequest = 405, Text = "Options", Style = buttonStyle, HorizontalOptions = LayoutOptions.Center };
            var howToButton = new Button { HeightRequest = 200, WidthRequest = 200, Text = "How to Play", Style = buttonStyle, HorizontalOptions = LayoutOptions.EndAndExpand };
            var aboutButton = new Button { HeightRequest = 200, WidthRequest = 200, Text = "About", Style = buttonStyle, HorizontalOptions = LayoutOptions.StartAndExpand };

            newGameButton.Clicked += NewGameButton_Clicked;
            arcadeGameButton.Clicked += ArcadeGameButton_Clicked;
            optionsButton.Clicked += OptionsButton_Clicked;
            leaderboardButton.Clicked += LeaderboardButton_Clicked;
            aboutButton.Clicked += AboutButton_Clicked;
            howToButton.Clicked += HowToButton_Clicked;

            grid.Children.Add(newGameButton, 0, 0);
            grid.Children.Add(arcadeGameButton, 1, 0);
            grid.Children.Add(leaderboardButton, 0, 2, 1, 2);
            grid.Children.Add(optionsButton, 0, 2, 2, 3);
            grid.Children.Add(howToButton, 0, 1, 3, 4);
            grid.Children.Add(aboutButton, 1, 2, 3, 4);

            return grid;
        }

        private async void HowToButton_Clicked(object sender, EventArgs e)
        {
            await _navigationService.NavigateToHowTo();
        }

        private async void AboutButton_Clicked(object sender, EventArgs e)
        {
            await _navigationService.NavigateToAbout();
        }

        private async void ArcadeGameButton_Clicked(object sender, EventArgs e)
        {
            if (Device.OS == TargetPlatform.Android || Device.OS == TargetPlatform.iOS)
                HockeyApp.MetricsManager.TrackEvent(HockeyAppHelper.Events.ArcadeGameStarted);

            await _navigationService.NavigateToArcadeGame();
        }

        private async void LeaderboardButton_Clicked(object sender, EventArgs e)
        {
            await _navigationService.NavigateToLeaderboard();// Navigation.PushAsync(new LeaderboardPage());
        }

        private async void OptionsButton_Clicked(object sender, EventArgs e)
        {
            await _navigationService.NavigateToOptions();// Navigation.PushAsync(new OptionsTabbedPage());
        }

        private async void NewGameButton_Clicked(object sender, EventArgs e)
        {
            if (Device.OS == TargetPlatform.Android || Device.OS == TargetPlatform.iOS)
                HockeyApp.MetricsManager.TrackEvent(HockeyAppHelper.Events.NormalGameStarted);

            await _navigationService.NavigateToGame();//Navigation.PushAsync(new GamePage(), true);

            //var items = await ScoreItemManager.DefaultManager.GetTodoItemsAsync(true);

            //await ScoreItemManager.DefaultManager.SaveTaskAsync(
            //    new Scores { Name = "test"
            //    , OSType = (int)Device.OS
            //    , GameType = (int)Enums.GameType.NormalGame
            //    , GameSubType =(int)Enums.GameMode.Easy, Score = 19, AverageTime= 10000 });
        }
    }
}
