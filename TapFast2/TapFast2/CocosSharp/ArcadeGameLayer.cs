using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CocosSharp;
using TapFast2.Helpers;
using TapFast2.Enums;
using Xamarin.Forms;
using Sounds = TapFast2.Constants.Files.Audio;
using TapFast2.Resx;

namespace TapFast2
{
    public class ArcadeGameLayer : GameLayer
    {
        CCLabel labelScore;

        private int _score;

        private float _gameTime;

        public bool IsGameOver { get; private set; }

        bool _isTimerStarted;
        private CCLabel labelGameMode;
        private string _gameModeText;

        public ArcadeGameLayer(CCSizeI size, CCColor4B backcolor) : base(size, backcolor)
        {
            labelScore = new CCLabel(string.Format("SCORE: {0}", _score), "Fonts/MarkerFelt", 22, CCLabelFormat.SpriteFont);
            labelScore.Color = new CCColor3B(27, 161, 226);
            InitNewGameComponents();

            AddChild(labelScore);

            labelGameMode = new CCLabel(_gameModeText, "Fonts/MarkerFelt", 22, CCLabelFormat.SpriteFont);
            labelGameMode.Color = new CCColor3B(68, 14, 98);
            AddChild(labelGameMode);
            labelGameMode.Text = _gameModeText.ToUpper();
        }

        public override void InitNewGameComponents()
        {
            _score = 0;
            IsGameOver = false;
            _isTimerStarted = false;
            switch ((GameMode)Settings.ArcadeGameMode)
            {
                case GameMode.ThirtySeconds:
                    _gameTime = Constants.Options.THIRTY;
                    _gameModeText = AppResources.ThirtySeconds;
                    break;
                case GameMode.SixtySeconds:
                    _gameTime = Constants.Options.SIXTY;
                    _gameModeText = AppResources.SixtySeconds;
                    break;
                default:
                case GameMode.FifteenSeconds:
                    _gameTime = Constants.Options.FIFTEEN;
                    _gameModeText = AppResources.FifteenSeconds;
                    break;
            }
            _timeForChangingPositions = 0.1f;
            SetScoreLabel();
        }

        private void SetScoreLabel()
        {
            labelScore.Text = string.Format("SCORE: {0}", _score);
        }

        protected override void OnSquareTapped(Square obj)
        {
            if (IsGameOver)
                return;

            obj.FadeIn();
            OnPausePressed(true);
            if (!IsTapCorrect(obj))
            {
                Failed();
            }
            else
            {
                Sucksess();
            }

            if (HasActiveSquares())
            {
                return;
            }

            //BeginMoveRandomSignals(() => StartOnce());

            StartOnce();


            //if (_score >= _levelTwoPoints)
            //{
            //    //SetAllSignalsInactive();

            //    if (_score >= _levelThreePoints)
            //        ChangeSquaresPosition(5, 9);

            //    ChangeSquaresPosition(1, 5, () =>
            //    {

            //        StartOnce();
            //        SchduleTime();
            //    });

            //}
            //else
            //{
            //    StartOnce();
            //    SchduleTime();
            //}
        }

        private void BeginMoveRandomSignals(Action callback)
        {
            var signalCouple = PositionInGameHelper.GetRandomSignalCouple();

            animator.ChangeSquaresPosition(_timeForChangingPositions, signalCouple.Item1, signalCouple.Item2, callback);
        }

        private void Sucksess()
        {
            _score = _score + 1;
            PlayEffect(Sounds.PressSuccess);
            SetScoreLabel();
        }

        private void Failed()
        {
            _score = _score - 1;
            PlayEffect(Sounds.PressFail);
            SetScoreLabel();
        }

        private bool IsTapCorrect(Square obj)
        {
            var square = GetSignalSquare(obj.ColorType);
            if (!square.IsActive)
            {
                return false;
            }
            if (square.PlusOneEnabled)
            {
                Sucksess();
            }
            square.IsActive = false;

            return true;
        }

       

        protected override void StartProgressLine()
        {
            if (_isTimerStarted)
                return;
            _isTimerStarted = true;

            StopProgressLine();

            var progressTo = new CCProgressTo(_gameTime, 100);

            var moveCompletedAction = new CCCallFunc(GameFinished);
            CCSequence mySequence = new CCSequence(progressTo, moveCompletedAction);

            _progressTimer.Percentage = .0f;
            _progressTimer.Repeat(1, mySequence);
        }

        private void GameFinished()
        {
            IsGameOver = true;
            UnscheduleAll();
            _progressTimer.StopAllActions();

            //FIX THIS change sound
            PlayEffect(Sounds.GameOver);


            Device.BeginInvokeOnMainThread(() =>
            {
                OnGameIsOver?.Invoke(_score);
            });
            //this.Dispose();
        }

        protected override void AddedToScene()
        {
            base.AddedToScene();

            _cordinatesGenerator.SetScoreLabelCordinates(labelScore);
            _cordinatesGenerator.SetGameModeLabelPosition(labelGameMode);
        }

        protected override void OnPausePressed(bool play)
        {
            if (!play)
            {
                _progressTimer.Pause();
                _score = _score - 1;
                //PlayEffect(Sounds.Paused);
                SetScoreLabel();
            }
            else
            {
                _pauseButton.SetPauseVisible(true);
                _progressTimer.Resume();
            }
        }
    }
}
