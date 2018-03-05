using System;
using System.Collections.Generic;
using System.Text;

using CocosSharp;
using TapFast2.Services;
using TapFast2.Enums;
using System.Diagnostics;
using System.Linq;

namespace TapFast2.Shared
{
    public class GameLayer : CCLayerColor
    {
        Square _redSignal;
        Square _blueSignal;
        Square _greenSignal;
        Square _yellowSignal;

        ISettingsService _settingsService;
        // Define a label variable
        CCLabel labelScore;
        CCLabel labelLives;

        List<Square> _squares;
        CCProgressTimer _progressTimer;

        List<Square> _signalSquares;
        //List<Square> _playSquares;

        private int _score = 0;
        private int _lives = 3;

        private int _levelTwoPoints = 3;
        private int _levelThreePoints = 5;

        CordinatesGenerator _cordinatesGenerator;

        public SelectedColor _lastActiveColor;


        //Set from settings
        float _timeBetweenColors = 1.5f;
        float _timeForChangingPositions = 0.2f;
        //Set from settings

        public GameLayer(CCSizeI size) : base(CCColor4B.Black)
        {
            

            // create and initialize a Label
            labelScore = new CCLabel(string.Format("SCORE: {0}", _score), "Fonts/MarkerFelt", 22, CCLabelFormat.SpriteFont);
            //labelScore.VerticalAlignment = CCVerticalTextAlignment.Top;

            labelLives = new CCLabel(string.Format( "LIVES: {0}", _lives), "Fonts/MarkerFelt", 22, CCLabelFormat.SpriteFont);
            //CRect rect = new CCRect(50, 50, 200, 200);

            //CCDrawNode tile = new CCDrawNode();
            //var color = new CCColor4F(255, 216, 0, 0.5f);
            //tile.DrawRect(rect, new CCColor4F(color));

            //CCRect rect2 = new CCRect(50, 50 + 200, 200, 200);
            //CCDrawNode tile2 = new CCDrawNode();
            //tile.DrawRect(rect2, CCColor4B.Red);

            // add the label as a child to this Layer
            //int i = 0;
            AddChild(labelScore);
            AddChild(labelLives);

            

            InitProgressTimer(size);
            
            InitSquares();
            
        }

        private void InitProgressTimer(CCSizeI size)
        {
            _progressTimer = new CCProgressTimer("bluePx.png");

            _progressTimer.Type = CCProgressTimerType.Bar;
            _progressTimer.ScaleX = size.Width - 20;
            _progressTimer.ScaleY = 4;
            //_progressTimer.AnchorPoint = new CCPoint(1.0f, 0.5f);
            _progressTimer.Percentage = 100.0f;
            _progressTimer.BarChangeRate = new CCPoint(1, 0);
            AddChild(_progressTimer);
        }

        private void InitSquares()
        {
            _squares = GetSquares();
            _signalSquares = _squares.FindAll(a => !a.IsTouchEnabled);
            foreach (var item in _squares)
            {
                AddChild(item);
            }

            _redSignal = _signalSquares.Find(a => a.ColorType == SelectedColor.Red);
            _blueSignal = _signalSquares.Find(a => a.ColorType == SelectedColor.Blue);
            _greenSignal = _signalSquares.Find(a => a.ColorType == SelectedColor.Green);
            _yellowSignal = _signalSquares.Find(a => a.ColorType == SelectedColor.Yellow);


            foreach (var playSquare in _squares.FindAll(a => a.IsTouchEnabled))
            {
                playSquare.OnTapped = OnSquareTapped;
            }

        }

        
        protected override void AddedToScene()
        {
            base.AddedToScene();

            // Use the bounds to layout the positioning of our drawable assets
            var bounds = VisibleBoundsWorldspace;

            // position the label on the center of the screen
            //labelScore.Position = new CCPoint(80, bounds.Size.Height - 16);

            //labelLives.Position = new CCPoint(bounds.Size.Width - 100, bounds.Size.Height - 16);

            _progressTimer.Position = bounds.Center;
            _progressTimer.Midpoint = new CCPoint(0, 0.5f);


            _cordinatesGenerator = new CordinatesGenerator(bounds.Center);

            _cordinatesGenerator.SetSquaresCordinates(_squares);
            _cordinatesGenerator.SetLabelCordinates(labelScore, labelLives);

            // Register for touch events
            //var touchListener = new CCEventListenerTouchAllAtOnce();
            //touchListener.OnTouchesEnded = OnTouchesEnded;
            //AddEventListener(touchListener, this);

            // _playHelper.Start();
            FirstRun();
        }

        //void OnTouchesEnded(List<CCTouch> touches, CCEvent touchEvent)
        //{
        //    if (touches.Count > 0)
        //    {
        //        // Perform touch handling here
        //    }
        //}

        private void OnSquareTapped(Square obj)
        {
            if (!IsTapCorrect(obj))
            {
                //if there is no lives - GAME OVER
                if (_score <= 0)
                {
                    GameOver();
                    //return;
                }
            }
            else
            {
                Sucksess(obj);
            }

            if (HasActiveSquares())
            {
                return;
            }


            Unschedule(TimeIsUp);
            StopProgressLine();


            if (_score >= _levelTwoPoints)
            {
                //SetAllSignalsInactive();

                if (_score >= _levelThreePoints)
                    ChangeSquaresPosition(5, 9);

                ChangeSquaresPosition(1, 5, ()=> 
                {
                    
                    StartOnce();
                    SchduleTime();
                });
                
            }
            else
            {
                StartOnce();
                SchduleTime();
            }
            
        }

        private bool HasActiveSquares()
        {
            return _signalSquares.Any(a => a.IsActive);
        }

        private void StartProgressLine()
        {
            StopProgressLine();

            _progressTimer.Percentage = 100.0f;
            _progressTimer.Schedule(ScheduleProgressLine, _timeBetweenColors / 100);
        }

        private void ScheduleProgressLine(float time)
        {
            _progressTimer.Percentage -= 1.0f;
        }

        private void StopProgressLine()
        {
            if (_progressTimer.IsRunning)
            {
                Debug.WriteLine("StopProgressLine");
                _progressTimer.Unschedule(ScheduleProgressLine);
            }
        }

        private void SchduleTime()
        {
            Schedule(TimeIsUp, _timeBetweenColors);
        }

        private void ChangeSquaresPosition(int firstPositionInGame, int lastPositionInGame, Action callback = null)
        {
            var firstPosition = (PositionInGame)_random.Next(firstPositionInGame, lastPositionInGame);
            var secondPosition = (PositionInGame)_random.Next(firstPositionInGame, lastPositionInGame);
            while (firstPosition == secondPosition)
                secondPosition = (PositionInGame)_random.Next(firstPositionInGame, lastPositionInGame);


            var firstSquarePosition = _cordinatesGenerator.PositionAndCordinates[firstPosition];
            var secondSquarePosition = _cordinatesGenerator.PositionAndCordinates[secondPosition];

            var firstPoint = firstSquarePosition.Point;
            var secondPoint = secondSquarePosition.Point;

            _cordinatesGenerator.PositionAndCordinates[firstPosition].Point = secondPoint;
            _cordinatesGenerator.PositionAndCordinates[secondPosition].Point = firstPoint;


            var moveToFirstPosition = new CCMoveTo(_timeForChangingPositions, firstPoint);
            var moveToSecondPosition = new CCMoveTo(_timeForChangingPositions, secondPoint);

            secondSquarePosition.Square.RunAction(moveToFirstPosition);

            if (callback != null)
            {
                var moveCompletedAction = new CCCallFunc(callback);
                CCSequence mySequence = new CCSequence(moveToSecondPosition, moveCompletedAction);

                firstSquarePosition.Square.RunAction(mySequence);
            }
            else
            {
                firstSquarePosition.Square.RunAction(moveToSecondPosition);
            }
        }

        private void TimeIsUp(float obj)
        {

            Debug.WriteLine("TimeIsUp {0}: {1}", DateTime.Now.Second, DateTime.Now.Millisecond);
            Failed();
            
            if (_lives <= 0)
            {
                //GAME OVER
                GameOver();
                //return;
            }

            StartOnce();
        }

        private void StartOnce()
        {
            SetAllSignalsInactive();

            //10% chance to get +1
            if (_random.Next(0, 1000) <= 100) // (_random.Next(1, 11) % 2 == 0)
            {
                SetColor(true);
            }

            SetColor();

            
            StartProgressLine();
        }

        private bool IsTapCorrect(Square obj)
        {
            var square = GetSignalSquare(obj.ColorType);
            if (!square.IsActive)
            {
                Failed();
                return false;
            }
            if(square.PlusOneEnabled)
            {
                IncrementLives();
            }
            square.IsActive = false;

            //if (obj.ColorType != _currentColor)
            //{
            //    Failed();
            //    return false;
            //}

            return true;
        }

        private void IncrementLives()
        {
            _lives = _lives + 1;
            labelLives.Text = string.Format(string.Format("LIVES: {0}", _lives));
        }

        private void Failed()
        {
            //if out of lives set game over
            
            _lives--;

            labelLives.Text = string.Format(string.Format("LIVES: {0}", _lives));
        }

        private void GameOver()
        {
            //IsPlaying = false;
            //Set GAME OVER
        }

        internal void Sucksess(Square tapped)
        {
            //TODO: if +1 set + one
            _score = _score + 1;

            labelScore.Text = string.Format("SCORE: {0}", _score);
        }

        public void FirstRun()
        {
            SetAllSignalsInactive();
            SetColor();
        }

        private void SetColor(bool isPlusEnabled = false)
        {
            SelectedColor randomColor = GetRandomColor();
            while (randomColor == _lastActiveColor && GetSignalSquare(randomColor).IsActive)
                randomColor = GetRandomColor();


            SetVisibleSignalColor(randomColor, isPlusEnabled);
        }
        Random _random = new Random();
        private SelectedColor GetRandomColor()
        {
            int fromOneToFour = _random.Next(1, 5);
            return (SelectedColor)fromOneToFour;
        }

        void SetAllSignalsInactive()
        {
            _greenSignal.IsActive = false;
            _redSignal.IsActive = false;
            _blueSignal.IsActive = false;
            _yellowSignal.IsActive = false;
        }

        internal void SetVisibleSignalColor(SelectedColor randomColor, bool plusOnLive = false)
        {
            _lastActiveColor = randomColor;

            var square = GetSignalSquare(randomColor);

            square.IsActive = true;
            if (plusOnLive)
                square.PlusOneEnabled = true;
        }

        private Square GetSignalSquare(SelectedColor color)
        {
            
            switch (color)
            {
                case SelectedColor.Red:
                    return _redSignal;
                case SelectedColor.Green:
                    return _greenSignal;
                case SelectedColor.Yellow:
                    return _yellowSignal;
                case SelectedColor.Blue:
                    return _blueSignal;
                default:
                    throw new ArgumentException("there is no such color");
            }
            
        }

        internal List<Square> GetSquares()
        {
            List<Square> squares = new List<Square>();

            squares.Add(CreateSquare("RedSquare.png", SelectedColor.Red, PositionInGame.SignalUpLeft, false));            // leftSquaresHorizontalPosition, firstRowSquaresVerticalPosition));
            squares.Add(CreateSquare("GreenSquare.png", SelectedColor.Green, PositionInGame.SignalUpRight, false));       // rightSquaresHorizontalPosition, firstRowSquaresVerticalPosition));
            squares.Add(CreateSquare("YellowSquare.png", SelectedColor.Yellow, PositionInGame.SignalDownLeft, false));    // leftSquaresHorizontalPosition, secondRowSquaresVerticalPosition));
            squares.Add(CreateSquare("BlueSquare.png", SelectedColor.Blue, PositionInGame.SignalDownRight, false));       //rightSquaresHorizontalPosition, secondRowSquaresVerticalPosition));

            //squares.Add( CreateSquare("RedSquare.png", leftSquaresHorizontalPosition, thirdRowSquaresVerticalPosition));
            //squares.Add( CreateSquare("GreenSquare.png", rightSquaresHorizontalPosition, thirdRowSquaresVerticalPosition));
            //squares.Add( CreateSquare("YellowSquare.png", leftSquaresHorizontalPosition, fourthRowSquaresVerticalPosition));
            //squares.Add( CreateSquare("BlueSquare.png", rightSquaresHorizontalPosition, fourthRowSquaresVerticalPosition));

            squares.Add(CreateSquare("RedSquare.png", SelectedColor.Red, PositionInGame.PlayUpLeft, true));
            squares.Add(CreateSquare("GreenSquare.png", SelectedColor.Green, PositionInGame.PlayUpRight, true));
            squares.Add(CreateSquare("YellowSquare.png", SelectedColor.Yellow, PositionInGame.PlayDownLeft, true));
            squares.Add(CreateSquare("BlueSquare.png", SelectedColor.Blue, PositionInGame.PlayDownRight, true));

            return squares;
        }

        private Square CreateSquare(string fileName, SelectedColor color, PositionInGame position, bool isTouchEnabled)
        {
            return new Square(fileName, isTouchEnabled, color, position);
        }
    }
}
