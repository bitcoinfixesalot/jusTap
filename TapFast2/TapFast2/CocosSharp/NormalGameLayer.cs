using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CocosSharp;
using System.Diagnostics;
using Xamarin.Forms;
using TapFast2.Enums;
using TapFast2.Helpers;
using Sounds = TapFast2.Constants.Files.Audio;
using System.Threading;
using static TapFast2.Constants.Files.Images;
using TapFast2.Resx;

namespace TapFast2
{
    public class NormalGameLayer : GameLayer
    {
       

        //CCLabel labelLives;

        CCLabel labelScore;

        CCLabel labelGameMode;

        //CCLabel labelLevel;

        uint _lives = 3;
        private int _score;
        private int _pointsPerTap = 1;

        protected float _timeBetweenColors = 1.5f;
        //private int _levelTwoPoints;
        //private int _levelThreePoints;

        private uint _currentLevel = 1;

        public bool IsGameOver
        {
            get
            {
                return _lives == 0;
            }
        }

        Dictionary<LevelLivePosition, CCSprite> levelSquares;
        Dictionary<LevelLivePosition, CCSprite> livesSquares;
        CCSprite levelLabel;
        CCSprite livesLabel;

        public NormalGameLayer(CCSizeI size, CCColor4B backcolor) : base(size, backcolor)
        {
            //labelLives = new CCLabel(string.Format("LIVES: {0}", _lives), "Fonts/MarkerFelt", 22, CCLabelFormat.SpriteFont);
            //labelLives.Color = new CCColor3B(27, 161, 226);

            //labelLevel = new CCLabel(string.Format("LEVEL: {0}", _score), "Fonts/MarkerFelt", 22, CCLabelFormat.SpriteFont);
            //labelLevel.Color = new CCColor3B(27, 161, 226);
            //AddChild(labelLives);
            //AddChild(labelLevel);

            

            labelScore = new CCLabel(string.Format("SCORE: {0}", _score), "Fonts/MarkerFelt", 22, CCLabelFormat.SpriteFont);
            labelScore.Color = new CCColor3B(27, 161, 226);
            AddChild(labelScore);

            levelLabel = new CCSprite(level);
            AddChild(levelLabel);

            livesLabel = new CCSprite(lives);
            AddChild(livesLabel);



            levelSquares = new Dictionary<LevelLivePosition, CCSprite>();
            livesSquares = new Dictionary<LevelLivePosition, CCSprite>();

            levelSquares[LevelLivePosition.One]=         new CCSprite(green40x40) { Visible = true };
            levelSquares[LevelLivePosition.Two]=         new CCSprite(green40x40) { Visible = false };
            levelSquares[LevelLivePosition.Three]=       new CCSprite(green40x40) { Visible = false };
            levelSquares[LevelLivePosition.Four]=        new CCSprite(blue40x40) { Visible = false };
            levelSquares[LevelLivePosition.Five]=        new CCSprite(blue40x40) { Visible = false };
            levelSquares[LevelLivePosition.Six]=         new CCSprite(yellow40x40) { Visible = false };
            levelSquares[LevelLivePosition.Seven]=       new CCSprite(yellow40x40) { Visible = false };
            levelSquares[LevelLivePosition.Eight]=       new CCSprite(red40x40) { Visible = false };
            levelSquares[LevelLivePosition.Nine]=        new CCSprite(red40x40) { Visible = false };
            levelSquares[LevelLivePosition.Ten]=         new CCSprite(red40x40) { Visible = false };
            levelSquares[LevelLivePosition.Eleven]=      new CCSprite(red40x40) { Visible = false };
            levelSquares[LevelLivePosition.Twelve]=      new CCSprite(red40x40) { Visible = false };
            levelSquares[LevelLivePosition.Thirteen]=    new CCSprite(red40x40) { Visible = false };
            levelSquares[LevelLivePosition.Plus] =       new CCSprite(redplus) { Visible = false };

            livesSquares[LevelLivePosition.One] = new CCSprite(red40x40) { Visible = true };
            livesSquares[LevelLivePosition.Two] = new CCSprite(red40x40) { Visible = true };
            livesSquares[LevelLivePosition.Three] = new CCSprite(red40x40) { Visible = true };
            livesSquares[LevelLivePosition.Four] = new CCSprite(yellow40x40) { Visible = false };
            livesSquares[LevelLivePosition.Five] = new CCSprite(yellow40x40) { Visible = false };
            livesSquares[LevelLivePosition.Six] = new CCSprite(blue40x40) { Visible = false };
            livesSquares[LevelLivePosition.Seven] = new CCSprite(blue40x40) { Visible = false };
            livesSquares[LevelLivePosition.Eight] = new CCSprite(green40x40) { Visible = false };
            livesSquares[LevelLivePosition.Nine] = new CCSprite(green40x40) { Visible = false };
            livesSquares[LevelLivePosition.Ten] = new CCSprite(green40x40) { Visible = false };
            livesSquares[LevelLivePosition.Eleven] = new CCSprite(green40x40) { Visible = false };
            livesSquares[LevelLivePosition.Twelve] = new CCSprite(green40x40) { Visible = false };
            livesSquares[LevelLivePosition.Thirteen] = new CCSprite(green40x40) { Visible = false };
            livesSquares[LevelLivePosition.Plus] = new CCSprite(greenplus) { Visible = false };


            foreach (var item in levelSquares.Values)
                AddChild(item);

            foreach (var item in livesSquares.Values)
                AddChild(item);

            InitNewGameComponents();


            labelGameMode =  new CCLabel(_gameModeText, "Fonts/MarkerFelt", 22, CCLabelFormat.SpriteFont);
            labelGameMode.Color = new CCColor3B(68, 14, 98);
            AddChild(labelGameMode);
            labelGameMode.Text = _gameModeText.ToUpper();
        }

        protected override void OnPausePressed(bool play)
        {
            if (!play)
            {
                UnscheduleAll();
                StopProgressLine();

                DecrementScore();
                //PlayEffect(Sounds.Paused);
            }
            else
            {
                animator.BeginSquareMovementByLevel(_currentLevel, _timeForChangingPositions, () =>
                {
                    StartOnce();
                    SchduleTime();
                });
            }
        }

        private void DecrementScore()
        {
            _score = _score - _pointsPerTap;
            SetScoreLabel();
        }

        private void IncrementScore()
        {
            _score = _score + _pointsPerTap;
            SetScoreLabel();
        }

        public override void InitNewGameComponents()
        {
            _score = 0;
            _lives = 3;
            _currentLevel = 1;
            //_levelTwoPoints = 3;
            //_levelThreePoints = 5;
            SetScoreLabel();
            SetLivesLabel();

            SetIntervalBetweenColorsAndPointsPerTap();
//#if DEBUG
//            _lives =10;
//#endif
        }

        GameMode _currentGameMode = (GameMode)Settings.Difficulty;

        string _gameModeText;

        private void SetIntervalBetweenColorsAndPointsPerTap()
        {
            switch (_currentGameMode)
            {
                case GameMode.Normal:
                    _timeBetweenColors = 1.25f;//1.5f;
                    _pointsPerTap = 2;
                    _gameModeText = AppResources.Normal;
                    break;
                case GameMode.Hard:
                    _timeBetweenColors = 1.0f;
                    _pointsPerTap = 3;
                    _gameModeText = AppResources.Hard;
                    break;
                case GameMode.Impossible:
                    _timeBetweenColors = 0.75f;
                    _pointsPerTap = 4;
                    _gameModeText = AppResources.Impossible;
                    break;
                case GameMode.Easy:
                default:
                    _timeBetweenColors = 1.5f;//2.0f;
                    _pointsPerTap = 1;
                    _gameModeText = AppResources.Easy;
                    break;
            }
        }

        private void SetLivesLabel()
        {
            //labelLives.Text = string.Format("LIVES: {0}", _lives);
        }

        private void SetScoreLabel()
        {
            labelScore.Text = string.Format("SCORE: {0}", _score);
            
        }

        private void SetLevelLabel()
        {
            //labelLevel.Text = string.Format("LEVEL: {0}", _currentLevel);
        }

        protected override void OnSquareTapped(Square obj)
        {
            obj.FadeIn();
            _pauseButton.SetPauseVisible(true);
            if (IsGameOver)
                return;

            if (!IsTapCorrect(obj))
            {

                UnscheduleAll();
                StopProgressLine();

                //if there is no lives - GAME OVER
                Failed();
                PauseListeners(this, true);
                animator.BeginLiveTakenAnimation(_lives, () =>
                {
                    //livesSquares[(LevelLivePosition)_lives + 1].Visible = false;//fix this
                    animator.HideLostLives(_lives);
                    if (IsGameOver)
                    {
                        GameOver();
                        return;
                    }
                    

                    animator.BeginSquareMovementByLevel(_currentLevel, _timeForChangingPositions, () =>
                    {
                        ResumeListeners(this, true);
                        StartOnce();
                        SchduleTime();
                    });
                });
                
               

            }
            else
            {
                Sucksess(obj);
                if (HasActiveSquares())
                {
                    return;
                }

                UnscheduleAll();
                StopProgressLine();
                PauseListeners(this, true);
                if (IsLevelNeedToChange())
                {
                    PlayEffect(Sounds.LevelRise);
                    animator.BeginLevelChangeAnimation(_currentLevel, () =>
                    {
                        ResumeListeners(this, true);

                        StartOnce();
                        SchduleTime();
                    });
                }
                else
                {
                    animator.BeginSquareMovementByLevel(_currentLevel, _timeForChangingPositions, () =>
                    {
                        ResumeListeners(this, true);

                        StartOnce();
                        SchduleTime();
                    });

                }
            }
        }

     

        private bool IsLevelNeedToChange()
        {
            int maxForLavel;
            switch (_currentLevel)
            {
                case 1:
                    maxForLavel = 10;
                    break;
                case 2:
                    maxForLavel = 20;
                    break;
                case 3:
                    maxForLavel = 30;
                    break;
                case 4:
                    maxForLavel = 40;
                    break;
                case 5:
                    maxForLavel = 50;
                    break;
                case 6:
                    maxForLavel = 60;
                    break;
                case 7:
                    maxForLavel = 70;
                    break;
                case 8:
                    maxForLavel = 90;
                    break;
                case 9:
                    maxForLavel = 100;
                    break;
                default:
                    return false;
            }
//#if DEBUG
//            if (_score > 10)
//            {
//                _currentLevel = 10;
//                SetLevelLabel();
//                return true;
//            }
//            return false;
//#endif
            if (_score >= maxForLavel)
            {
                _currentLevel++;
                SetLevelLabel();
                return true;
            }
            return false;
        }

        internal void Sucksess(Square tapped)
        {
            //TODO: if +1 set + one
            IncrementScore();
            PlayEffect(Sounds.PressSuccess);
        }

        private void SchduleTime()
        {
            //CCScheduler scheduler = new CCScheduler();
            //scheduler
            
            ScheduleOnce(
                TimeIsUp
                , _timeBetweenColors);
        }


        private void TimeIsUp(float obj)
        {
            
                //unschedule and start animation
                Debug.WriteLine("TimeIsUp {0}: {1}", DateTime.Now.Second, DateTime.Now.Millisecond);
                Failed();
                StopProgressLine();
                PauseListeners(this, true);
                animator.BeginLiveTakenAnimation(_lives, () =>
                {
                    animator.HideLostLives(_lives);
                    //livesSquares[(LevelLivePosition)_lives + 1].Visible = false;//fix this
                    if (IsGameOver)
                    {
                        GameOver();
                        return;
                    }
                    //animator.BeginSquareMovementByLevel(_currentLevel, _timeForChangingPositions, () =>
                    //{

                    ResumeListeners(this, true);

                    Device.BeginInvokeOnMainThread(() =>
                    {
                        StartOnce();
                        SchduleTime();
                    });
                    
                });

                //});  
            

        }

        private void GameOver()
        {
            
            UnscheduleAll();
            StopAllActions();

            PlayEffect(Sounds.GameOver);

            _progressTimer.Visible = false;
            WriteGameOver();
            //do game over animation
            

            ScheduleOnce(a =>
             {
                 Device.BeginInvokeOnMainThread(() =>
                 {
                     OnGameIsOver?.Invoke(_score);
                 });

             }, 1.5f);
            
        }

        private bool IsTapCorrect(Square obj)
        {
            var square = GetSignalSquare(obj.ColorType);
            if (!square.IsActive)
            {
                //Failed();
                return false;
            }
            if (square.PlusOneEnabled)
            {
                IncrementLives();
            }
            square.IsActive = false;

            return true;
        }

        private void Failed()
        {
            //if out of lives set game over

            _lives--;
            PlayEffect(Sounds.PressFail);
            SetLivesLabel();
        }

        protected override void StartProgressLine()
        {
            StopProgressLine();

            var progressTo = new CCProgressFromTo(_timeBetweenColors, 100, 0);

            _progressTimer.Percentage = 100.0f;
            _progressTimer.Repeat(_lives, progressTo);
        }

        private void IncrementLives()
        {
            _lives = _lives + 1;

            PlayEffect(Sounds.GainPoint);
            SetLivesLabel();
            animator.BeginLiveAddedAnimation(_lives);
        }

        protected override void AddedToScene()
        {
            base.AddedToScene();

            _cordinatesGenerator.SetScoreLabelCordinates(labelScore);

            _cordinatesGenerator.SetLivesLabelCordinates(livesLabel);

            _cordinatesGenerator.SetLevelsLabelCordinates(levelLabel);

            _cordinatesGenerator.SetLevelsAndLivesPosition(levelSquares, livesSquares);

            _cordinatesGenerator.SetGameModeLabelPosition(labelGameMode);
        }

        
    }
}
