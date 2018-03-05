using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using TapFast2.Shared;
using Xamarin.Forms;

namespace TapFast2
{
	public class MenuPage : ContentPage
	{
        NavigationPage _optionsPage;
		public MenuPage ()
		{
            InitPages();
            var layout = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                Padding = 20
            };

            layout.Children.Add(CreateMainGrid());

            Content = layout;
        }

        private void InitPages()
        {
            _optionsPage = new NavigationPage(new OptionsTabbedPage());
        }

        private Grid CreateMainGrid()
        {
            var grid = new Grid
            {
                 Padding = new Thickness(40)
                //RowSpacing = 50, ColumnSpacing = 50
            };

            var buttonStyle = (Style)Application.Current.Resources[Constants.Settings.MENU_BUTTON_STYLE];
            //grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(0, GridUnitType.Auto) });

           var newGameButton        =  new Button { HeightRequest = 200, WidthRequest = 200, Text = "New Game", Style = buttonStyle }                    ;
           var arcadeGameButton         = new Button { HeightRequest = 200, WidthRequest = 200, Text = "Arcade Game", Style = buttonStyle }               ;
           var leaderboardButton        = new Button { HeightRequest = 200, WidthRequest = 400, Text = "Leaderboard", Style = buttonStyle }              ;
           var optionsButton        = new Button { HeightRequest = 200, WidthRequest = 400, Text = "Options", Style = buttonStyle }                      ;
           var howToButton          = new Button { HeightRequest = 200, WidthRequest = 200, Text = "How to Play", Style = buttonStyle }                    ;
           var aboutButton          = new Button { HeightRequest = 200, WidthRequest = 200, Text = "About", Style = buttonStyle };

            newGameButton.Clicked += NewGameButton_Clicked;
            optionsButton.Clicked += OptionsButton_Clicked;

            grid.Children.Add(newGameButton    , 0, 0);
            grid.Children.Add(arcadeGameButton , 1, 0);
            grid.Children.Add(leaderboardButton, 0, 2, 1, 2);
            grid.Children.Add(optionsButton    , 0, 2, 2, 3);
            grid.Children.Add(howToButton      , 0, 1, 3, 4);
            grid.Children.Add(aboutButton, 1, 2, 3, 4);



            return grid;
        }

        private async void OptionsButton_Clicked(object sender, EventArgs e)
        {
            await  Navigation.PushAsync(new OptionsTabbedPage());
        }

        private async void NewGameButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new GamePage(), true);
        }
    }
}
