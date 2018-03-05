using Plugin.InAppBilling.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TapFast2.Enums;
using TapFast2.Helpers;
using TapFast2.Resx;
using TapFast2.Services;
using Xamarin.Forms;
using static TapFast2.Helpers.Settings;

namespace TapFast2
{
    public class GameOverViewModel : INotifyPropertyChanged
    {

        ICommand _restartGameCommand;
        public ICommand RestartGameCommand => _restartGameCommand ?? (_restartGameCommand = new Command(async () => await RestartGame()));

        ICommand _menuCommand;
        public ICommand MenuCommand => _menuCommand ?? (_menuCommand = new Command(async () => await _navigationService.NavigateToMenu()));

        Command _saveScoreCommand;
        public Command SaveScoreCommand => _saveScoreCommand ?? (_saveScoreCommand = new Command(async () => await SaveScore(), () => !IsBusy));

        private async Task SaveScore()
        {
            if (IsBusy)
                return;
            //FIX THIS - set average time
            if (string.IsNullOrWhiteSpace(YourName))
                return;

            try
            {
                IsBusy = true;
                Settings.YourName = YourName;
                var result = await azureService.SaveScore(YourName, _yourHighscore, _navigationService.GameTypeSelected, CurrentGameMode, 0);
                if (result)
                    SaveScoreText = AppResources.ScoreSaved;
                else
                    SaveScoreText = AppResources.TryAgain;
            }
            catch (Exception ex)
            {
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task RestartGame()
        {
            if (_navigationService.GameTypeSelected == GameType.NormalGame)
                await _navigationService.NavigateToGame();
            else
                await _navigationService.NavigateToArcadeGame();
        }

        private INavigationService _navigationService;

        int _score = 0;
        int _yourHighscore = 0;

        AzureService azureService;
        public GameOverViewModel(INavigationService navigationService)
        {
            //SetGameOver();

            //switch (gameType)
            //{
            //    case GameType.ArcadeGame:
            //        SetArcadeGameScores();
            //        break;
            //    case GameType.NormalGame:
            //    default:
            //        SetNormalGameScores();
            //        break;
            //}
            _navigationService = navigationService;

            azureService = DependencyService.Get<AzureService>();

            MessagingCenter.Subscribe<GamePage, int>(this, Constants.Messages.SCORE_MESSAGE, (sender, args) =>
            {
                SetCurrentScores(args);
            });
            if (AreAdsRemoved((PurchaseState)PurchaseStatus))
            {
                AdViewVisible = false;
            }
            else
            {
                adViewVisible = true;
            }
#if DEBUG
            //AdViewVisible = false;
#endif
        }

        private void SetGameOver()
        {
            switch (CurrentGameMode)
            {
                case GameMode.Normal:
                    SetYourHighscore(Settings.HighscoreNormal);
                    SetGameModeText(AppResources.Normal);
                    break;
                case GameMode.Hard:
                    SetYourHighscore(Settings.HighscoreHard);
                    SetGameModeText(AppResources.Hard);
                    break;
                case GameMode.Impossible:
                    SetYourHighscore(Settings.HighscoreImpossible);
                    SetGameModeText(AppResources.Impossible);

                    break;
                case GameMode.FifteenSeconds:
                    SetYourHighscore(Settings.Highscore15);
                    SetGameModeText(AppResources.FifteenSeconds);
                    break;
                case GameMode.ThirtySeconds:
                    SetYourHighscore(Settings.Highscore30);
                    SetGameModeText(AppResources.ThirtySeconds);
                    break;
                case GameMode.SixtySeconds:
                    SetYourHighscore(Settings.Highscore60);
                    SetGameModeText(AppResources.SixtySeconds);
                    break;
                case GameMode.Easy:
                default:
                    SetYourHighscore(Settings.HighscoreEasy);
                    SetGameModeText(AppResources.Easy);
                    break;
            }

            SetGameHighscore();
        }

        private void SetCurrentScores(int score)
        {
            SaveScoreText = AppResources.SaveScore;
            YourName = Settings.YourName;
            _score = score;
            CurrentScoreText = string.Format(AppResources.CurrentScore, score);

            SetGameHighscore();

            switch (CurrentGameMode)
            {
                
                case GameMode.Normal:
                    {
                        SetGameModeText(AppResources.Normal);

                        if (Settings.HighscoreNormal < score)
                            Settings.HighscoreNormal = score;

                        SetYourHighscore(Settings.HighscoreNormal);
                    }
                    break;
                case GameMode.Hard:
                    {
                        SetGameModeText(AppResources.Hard);

                        if (Settings.HighscoreHard < score)
                            Settings.HighscoreHard = score;

                        SetYourHighscore(Settings.HighscoreHard);
                    }
                    break;
                case GameMode.Impossible:
                    {
                        SetGameModeText(AppResources.Impossible);

                        if (Settings.HighscoreImpossible < score)
                            Settings.HighscoreImpossible = score;

                        SetYourHighscore(Settings.HighscoreImpossible);
                    }
                    break;
                case GameMode.Easy:
                    {
                        SetGameModeText(AppResources.Easy);
                        if (Settings.HighscoreEasy < score)
                            Settings.HighscoreEasy = score;

                        SetYourHighscore(Settings.HighscoreEasy);
                    }
                    break;
                case GameMode.FifteenSeconds:
                    {
                        SetGameModeText(AppResources.FifteenSeconds);
                        if (Settings.Highscore15 < score)
                            Settings.Highscore15 = score;

                        SetYourHighscore(Settings.Highscore15);
                    }
                    break;
                case GameMode.ThirtySeconds:
                    {
                        SetGameModeText(AppResources.ThirtySeconds);
                        if (Settings.Highscore30 < score)
                            Settings.Highscore30 = score;

                        SetYourHighscore(Settings.Highscore30);
                    }
                    break;

                case GameMode.SixtySeconds:
                    {
                        SetGameModeText(AppResources.SixtySeconds);
                        if (Settings.Highscore60 < score)
                            Settings.Highscore60 = score;

                        SetYourHighscore(Settings.Highscore60);
                    }
                    break;
            }
        }

        private void SetGameModeText(string gameMode)
        {
            GameModeText = string.Format(AppResources.GameMode, gameMode);
        }

        private async void SetGameHighscore()
        {
            int gameHighscore = await azureService.GetGameHighscoreAsync(_navigationService.GameTypeSelected, CurrentGameMode);

            GameHighscoreText = string.Format(AppResources.GameHighscore, gameHighscore);
        }

        private void SetYourHighscore(int highscore)
        {
            _yourHighscore = highscore;
            YourHighscoreText = string.Format(AppResources.YourHighscore, highscore);
        }

        public GameMode CurrentGameMode
        {
            get
            {
                if (_navigationService.GameTypeSelected == GameType.NormalGame)
                    return (GameMode)Settings.Difficulty;
                else
                    return (GameMode)Settings.ArcadeGameMode;
            }
        }

        private bool adViewVisible;

        public bool AdViewVisible
        {
            get { return adViewVisible; }
            set { adViewVisible = value;
                OnPropertyChanged();
            }
        }


        public bool IsBusy
        {
            get { return busy; }
            set
            {
                busy = value;
                OnPropertyChanged();

                SaveScoreCommand.ChangeCanExecute();
            }
        }

        public object Logo
        {
            get { return ImageSource.FromResource("TapFast2.Images.logo240x240.png"); }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName]string name = "") =>
           PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));



        private string _gameModeText;

        public string GameModeText
        {
            get { return _gameModeText; }
            set
            {
                if (_gameModeText != value)
                {
                    _gameModeText = value;
                    OnPropertyChanged();
                }
            }
        }


        private string _currentScoreText;

        public string CurrentScoreText
        {
            get { return _currentScoreText; }
            set
            {
                if (_currentScoreText != value)
                {
                    _currentScoreText = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _saveScore;

        public string SaveScoreText
        {
            get { return _saveScore; }
            set {
                _saveScore = value;
                OnPropertyChanged();
            }
        }



        private string _yourHighscoreText;

        public string YourHighscoreText
        {
            get { return _yourHighscoreText; }
            set
            {
                if (_yourHighscoreText != value)
                {
                    _yourHighscoreText = value;
                    OnPropertyChanged();
                }
            }
        }


        private string _gameHighscoreText;

        public string GameHighscoreText
        {
            get { return _gameHighscoreText; }
            set {
                if (_gameHighscoreText != value)
                {
                    _gameHighscoreText = value;
                    OnPropertyChanged();
                }
            }
        }


        private string _yourName;
        private bool busy;

        public string YourName
        {
            get
            {
                return _yourName;
            }
            set
            {
                if (_yourName != value)
                {
                    _yourName = value;
                    OnPropertyChanged();
                }
            }
        }




    }
}
