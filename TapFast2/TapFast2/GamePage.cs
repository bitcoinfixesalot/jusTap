using System;
using System.Collections.Generic;
using System.Text;

using Xamarin.Forms;
using CocosSharp;
using TapFast2.Services;
using Sounds = TapFast2.Constants.Files.Audio;
using TapFast2.Helpers;
using static TapFast2.Helpers.Settings;

namespace TapFast2
{
    public class GamePage : ContentPage
    {
        CocosSharpView gameView;

        INavigationService _navigationService;

        //GameLayer gameLayer;

        
        public GamePage(INavigationService navigationService)
        {
            //Device.OnPlatform(iOS: () => Padding = new Thickness(0, 20, 0, 0)
            //   , Android: () => Padding = new Thickness(0, 0, 0, 0)
            //   , WinPhone: () => Padding = new Thickness(0, 0, 0, 0));

            NavigationPage.SetHasNavigationBar(this, false);
            _navigationService = navigationService;

            Grid layout = new Grid { Padding = 0 , ColumnSpacing =0, Margin =0, RowSpacing = 0};
            layout.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            layout.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });

            //addView.IsVisible = false;

            gameView = new CocosSharpView()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                // Set the game world dimensions

                DesignResolution = navigationService.DesignResolution ,
                ResolutionPolicy = CocosSharpView.ViewResolutionPolicy.ExactFit,
                // Set the method to call once the view has been initialised
                ViewCreated = LoadGame
            };

            //Random random = new Random();
            //StackLayout layout = new StackLayout();
            //layout.Padding = 10;
            //Label label = new Label();
            //label.Text = random.Next(1000, 9999).ToString();
            //layout.Children.Add(label);
            //layout.Children.Add(gameView);

            layout.Children.Add(gameView, 0, 0);

            if (!AreAdsRemoved((Plugin.InAppBilling.Abstractions.PurchaseState)PurchaseStatus))
            {
                var addView = new Controls.AdView();
                layout.Children.Add(addView, 0, 1);
            }

            Content = layout;
        }

        //private Size GetDesignResolution()
        //{
        //    var result = new Size(768, 1280 - GetAdHeight());
        //    Device.OnPlatform(iOS: () => result = new Size(750, 1334 - GetAdHeight()));

        //    return result;
        //}

        //private int GetAdHeight()
        //{
        //    var result = 150;
        //    Device.OnPlatform(iOS: () => result = 100);
        //    return result;

        //}

        protected override void OnDisappearing()
        {
            if (gameView != null)
            {
                gameView.Paused = true;
            }

            base.OnDisappearing();
            CCAudioEngine.SharedEngine.StopAllEffects();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (gameView != null)
                gameView.Paused = false;
        }

        //CCGameView nativeGameView;
        void LoadGame(object sender, EventArgs e)
        {
            var nativeGameView = sender as CCGameView;

            if (nativeGameView != null)
            {
                //var contentSearchPaths = new List<string>() { "Fonts"};
                CCSizeI viewSize = nativeGameView.ViewSize;
                CCSizeI designResolution = nativeGameView.DesignResolution;

                CCAudioEngine.SharedEngine.PreloadEffect(Sounds.PressSuccess);
                CCAudioEngine.SharedEngine.PreloadEffect(Sounds.PressFail);
                CCAudioEngine.SharedEngine.PreloadEffect(Sounds.LevelRise);
                CCAudioEngine.SharedEngine.PreloadEffect(Sounds.GameOver);
                CCAudioEngine.SharedEngine.PreloadEffect(Sounds.GainPoint);

                // Determine whether to use the high or low def versions of our images
                // Make sure the default texel to content size ratio is set correctly
                // Of course you're free to have a finer set of image resolutions e.g (ld, hd, super-hd)
                //if (designResolution.Width < viewSize.Width)
                //{
                //    contentSearchPaths.Add("Images/Hd");
                //    CCSprite.DefaultTexelToContentSizeRatio = 2.0f;
                //}
                //else
                //{
                //    contentSearchPaths.Add("Images/Ld");
                //    CCSprite.DefaultTexelToContentSizeRatio = 1.0f;
                //}

                //nativeGameView.ContentManager.SearchPaths = contentSearchPaths;

                CCScene gameScene = new CCScene(nativeGameView);

                GameLayer gameLayer = null;

                var backcolor = Settings.IsDarkTheme ? new CCColor4B(48, 48, 48) : CCColor4B.White;

                if (_navigationService.GameTypeSelected == Enums.GameType.NormalGame)
                    gameLayer = new NormalGameLayer(designResolution, backcolor);
                else
                    gameLayer = new ArcadeGameLayer(designResolution, backcolor);

                gameLayer.OnGameIsOver = GameOver;
                gameScene.AddLayer(gameLayer);
                nativeGameView.RunWithScene(gameScene);
            }
        }

        private async void GameOver(int score)
        {
            await Navigation.PushAsync(new GameOverPage(), false);
            MessagingCenter.Send(this, Constants.Messages.SCORE_MESSAGE, score);
            Navigation.RemovePage(this);
        }
    }
}
