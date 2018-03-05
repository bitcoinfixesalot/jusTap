using CocosSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TapFast2.Enums;

namespace TapFast2
{
    public class Player
    {
        private int _score = 0;
        private uint _lives = 3;

        List<Square> _squares;
        CCProgressTimer _progressTimer;

        List<Square> _signalSquares;

        Square _redSignal;
        Square _blueSignal;
        Square _greenSignal;
        Square _yellowSignal;

        public SelectedColor _lastActiveColor;

        public bool IsGameOver { get; set; }


        public int Score { get { return _score; } }


        public Player()
        {
        }

        public CCProgressTimer InitProgressTimer(CCSize viewSize)
        {
            _progressTimer = new CCProgressTimer("bluePx.png");

            _progressTimer.Type = CCProgressTimerType.Bar;
            _progressTimer.ScaleX = viewSize.Width - 8;
            _progressTimer.ScaleY = 4;
            _progressTimer.Percentage = 100.0f;
            _progressTimer.BarChangeRate = new CCPoint(1, 0);

            return _progressTimer;
        }

        public List<Square> InitSquares()
        {
            _squares = GetSquares();
            _signalSquares = _squares.FindAll(a => !a.IsTouchEnabled);
            //foreach (var item in _squares)
            //{
            //    AddChild(item);
            //}

            _redSignal = _signalSquares.Find(a => a.ColorType == SelectedColor.Red);
            _blueSignal = _signalSquares.Find(a => a.ColorType == SelectedColor.Blue);
            _greenSignal = _signalSquares.Find(a => a.ColorType == SelectedColor.Green);
            _yellowSignal = _signalSquares.Find(a => a.ColorType == SelectedColor.Yellow);


            foreach (var playSquare in _squares.FindAll(a => a.IsTouchEnabled))
            {
                playSquare.OnTapped = OnSquareTapped;
            }

            return _squares;
        }

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

        public void FirstRun()
        {
            IsGameOver = false;
            SetAllSignalsInactive();
            SetColor();
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

        public bool IsTapCorrect(Square obj)
        {
            var square = GetSignalSquare(obj.ColorType);
            if (!square.IsActive)
            {
                Failed();
                return false;
            }
            if (square.PlusOneEnabled)
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

        public void TimeIsUp(float obj)
        {

            Debug.WriteLine("TimeIsUp {0}: {1}", DateTime.Now.Second, DateTime.Now.Millisecond);
            Failed();

            if (_lives <= 0)
            {
                //GAME OVER
                GameOver();
                return;
            }

            StartOnce();
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

        public Action<int> OnGameIsOver;

        

        internal void Sucksess(Square tapped)
        {
            //TODO: if +1 set + one
            _score = _score + 1;

            labelScore.Text = string.Format("SCORE: {0}", _score);
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


        private void SetColor(bool isPlusEnabled = false)
        {
            SelectedColor randomColor = GetRandomColor();
            while (randomColor == _lastActiveColor || GetSignalSquare(randomColor).IsActive)
                randomColor = GetRandomColor();


            SetVisibleSignalColor(randomColor, isPlusEnabled);
        }
        Random _random = new Random();

        public bool HasActiveSquares()
        {
            return _signalSquares.Any(a => a.IsActive);
        }


        protected virtual void OnSquareTapped(Square obj)
        {
        }

        protected virtual void StartProgressLine()
        {
        }


        public void StopProgressLine()
        {
            _progressTimer.StopAllActions();
        }






        private List<Square> GetSquares()
        {
            List<Square> squares = new List<Square>();

            squares.Add(CreateSquare("RedSquare.png", SelectedColor.Red, PositionInGame.SignalUpLeft, false));          
            squares.Add(CreateSquare("GreenSquare.png", SelectedColor.Green, PositionInGame.SignalUpRight, false));     
            squares.Add(CreateSquare("YellowSquare.png", SelectedColor.Yellow, PositionInGame.SignalDownLeft, false));  
            squares.Add(CreateSquare("BlueSquare.png", SelectedColor.Blue, PositionInGame.SignalDownRight, false));          


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
