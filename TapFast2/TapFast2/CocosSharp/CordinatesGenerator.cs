using System;
using System.Collections.Generic;
using System.Text;
using CocosSharp;
using TapFast2.Enums;

namespace TapFast2
{
    public class CordinatesGenerator
    {
        internal CordinatesGenerator(CCRect bounds)
        {
            _centerPoint = bounds.Center;
        }

        private CCPoint _centerPoint;
        const float SPACE_BETWEEN_SQUARES = 8;

        const float HORIZONTAL_SPACE_BETWEEN_CENTER = 4;
        const float VERTICAL_SPACE_BETWEEN_CENTER = 40;

        const float squareXandY = 200;
        const float halfSquare = 100;
        const float halfSmallSqaure = 20;
        const float smallSquareYDiff = 48;


        //horizontal
        float leftSquaresHorizontalPosition;
        float rightSquaresHorizontalPosition;
        //horizontal

        //vertical
        float firstRowSquaresVerticalPosition;
        float secondRowSquaresVerticalPosition;
        float thirdRowSquaresVerticalPosition;
        float fourthRowSquaresVerticalPosition;
        //vertical

        float levelsHorizontalPosition;
        float livesHorizontalPosition;

        internal void SetSquaresCordinates(List<Square> squares)
        {
            //horizontal
            leftSquaresHorizontalPosition = _centerPoint.X - squareXandY - HORIZONTAL_SPACE_BETWEEN_CENTER + (squareXandY / 2);
            rightSquaresHorizontalPosition = _centerPoint.X + HORIZONTAL_SPACE_BETWEEN_CENTER + (squareXandY / 2);
            //horizontal

            //vertical
            firstRowSquaresVerticalPosition = _centerPoint.Y + VERTICAL_SPACE_BETWEEN_CENTER + squareXandY + SPACE_BETWEEN_SQUARES + squareXandY - (squareXandY / 2);
            secondRowSquaresVerticalPosition = _centerPoint.Y + VERTICAL_SPACE_BETWEEN_CENTER + squareXandY - (squareXandY / 2);
            thirdRowSquaresVerticalPosition = _centerPoint.Y - VERTICAL_SPACE_BETWEEN_CENTER - (squareXandY / 2);
            fourthRowSquaresVerticalPosition = _centerPoint.Y - VERTICAL_SPACE_BETWEEN_CENTER - squareXandY - SPACE_BETWEEN_SQUARES - (squareXandY / 2);
            //vertical

            //levels position
            levelsHorizontalPosition = leftSquaresHorizontalPosition - squareXandY + 20;
            //levels position

            //lives position
            livesHorizontalPosition = rightSquaresHorizontalPosition + squareXandY - 20;
            //lives position

            PositionAndCordinates[PositionInGame.SignalUpLeft]      =                     new CCPoint(leftSquaresHorizontalPosition, firstRowSquaresVerticalPosition);
            PositionAndCordinates[PositionInGame.SignalUpRight]     =                    new CCPoint(rightSquaresHorizontalPosition, firstRowSquaresVerticalPosition);
            PositionAndCordinates[PositionInGame.SignalDownLeft]    =                   new CCPoint(leftSquaresHorizontalPosition, secondRowSquaresVerticalPosition);
            PositionAndCordinates[PositionInGame.SignalDownRight]   =                  new CCPoint(rightSquaresHorizontalPosition, secondRowSquaresVerticalPosition);
            PositionAndCordinates[PositionInGame.PlayUpLeft]        =                       new CCPoint(leftSquaresHorizontalPosition, thirdRowSquaresVerticalPosition);
            PositionAndCordinates[PositionInGame.PlayUpRight]       =                      new CCPoint(rightSquaresHorizontalPosition, thirdRowSquaresVerticalPosition);
            PositionAndCordinates[PositionInGame.PlayDownLeft]      =                     new CCPoint(leftSquaresHorizontalPosition, fourthRowSquaresVerticalPosition);
            PositionAndCordinates[PositionInGame.PlayDownRight]     =                    new CCPoint(rightSquaresHorizontalPosition, fourthRowSquaresVerticalPosition);

             PositionAndSquare[PositionInGame.SignalUpLeft]      =squares.Find(a => a.CurrentPosition == PositionInGame.SignalUpLeft);   
             PositionAndSquare[PositionInGame.SignalUpRight]     =squares.Find(a => a.CurrentPosition == PositionInGame.SignalUpRight);  
             PositionAndSquare[PositionInGame.SignalDownLeft]    =squares.Find(a => a.CurrentPosition == PositionInGame.SignalDownLeft); 
             PositionAndSquare[PositionInGame.SignalDownRight]   =squares.Find(a => a.CurrentPosition == PositionInGame.SignalDownRight);
             PositionAndSquare[PositionInGame.PlayUpLeft]        =squares.Find(a => a.CurrentPosition == PositionInGame.PlayUpLeft);     
             PositionAndSquare[PositionInGame.PlayUpRight]       =squares.Find(a => a.CurrentPosition == PositionInGame.PlayUpRight);    
             PositionAndSquare[PositionInGame.PlayDownLeft]      =squares.Find(a => a.CurrentPosition == PositionInGame.PlayDownLeft);   
             PositionAndSquare[PositionInGame.PlayDownRight]     = squares.Find(a => a.CurrentPosition == PositionInGame.PlayDownRight);  

            foreach (var square in squares)
            {
                square.Position = PositionAndCordinates[square.CurrentPosition];
            }
        }


        internal void SetGameModeLabelPosition(CCLabel labelGameMode)
        {
            labelGameMode.Position = new CCPoint(_centerPoint.X, firstRowSquaresVerticalPosition + squareXandY);
        }

        public void SetScoreLabelCordinates(CCLabel scoreLabel)
        {
            scoreLabel.Position = new CCPoint(_centerPoint.X, firstRowSquaresVerticalPosition + halfSquare + 30);
        }

        public void SetLivesLabelCordinates(CCSprite livesLabel)
        {
            //livesLabel.Position = new CCPoint(_centerPoint.X, fourthRowSquaresVerticalPosition - halfSquare -30 );
            livesLabel.Position = new CCPoint(livesHorizontalPosition, fourthRowSquaresVerticalPosition -10);
        }

        internal void SetLevelsLabelCordinates(CCSprite labelLevel)
        {
            //labelLevel.Position = new CCPoint(_centerPoint.X, firstRowSquaresVerticalPosition + halfSquare+ 70);
            labelLevel.Position = new CCPoint(levelsHorizontalPosition, fourthRowSquaresVerticalPosition -5);
        }

        public Dictionary<PositionInGame, Square> PositionAndSquare = new Dictionary<PositionInGame, Square>();

        internal void SetPausePosition(Pause pauseButton)
        {
            pauseButton.Position = new CCPoint(levelsHorizontalPosition, firstRowSquaresVerticalPosition + 170);
        }

        internal void SetSoundPosition(Sound soundButton)
        {
            soundButton.Position = new CCPoint(livesHorizontalPosition, firstRowSquaresVerticalPosition + 170);
        }

        private Dictionary<PositionInGame, CCPoint> PositionAndCordinates = new Dictionary<PositionInGame, CCPoint>();

        public Dictionary<LevelLivePosition, CCSprite> Levels;

        public Dictionary<LevelLivePosition, CCSprite> Lives;


        public void SetLevelsAndLivesPosition(Dictionary<LevelLivePosition, CCSprite> levels, Dictionary<LevelLivePosition, CCSprite> lives)
        {
            Levels = levels;
            Lives = lives;
            Levels[LevelLivePosition.Five].Position         = new CCPoint(levelsHorizontalPosition, thirdRowSquaresVerticalPosition + halfSquare - halfSmallSqaure);
            levels[LevelLivePosition.Four].Position         = new CCPoint(levelsHorizontalPosition, Levels[LevelLivePosition.Five].PositionY - smallSquareYDiff);
            levels[LevelLivePosition.Three].Position        = new CCPoint(levelsHorizontalPosition, Levels[LevelLivePosition.Four].PositionY - smallSquareYDiff);
            levels[LevelLivePosition.Two].Position          = new CCPoint(levelsHorizontalPosition, Levels[LevelLivePosition.Three].PositionY - smallSquareYDiff);
            levels[LevelLivePosition.One].Position          = new CCPoint(levelsHorizontalPosition, Levels[LevelLivePosition.Two].PositionY - smallSquareYDiff);

            Levels[LevelLivePosition.Six].Position          = new CCPoint(levelsHorizontalPosition, secondRowSquaresVerticalPosition - halfSquare + halfSmallSqaure);
            Levels[LevelLivePosition.Seven].Position        = new CCPoint(levelsHorizontalPosition, Levels[LevelLivePosition.Six].PositionY + smallSquareYDiff);
            Levels[LevelLivePosition.Eight].Position        = new CCPoint(levelsHorizontalPosition, Levels[LevelLivePosition.Seven].PositionY + smallSquareYDiff);
            Levels[LevelLivePosition.Nine].Position         = new CCPoint(levelsHorizontalPosition, Levels[LevelLivePosition.Eight].PositionY + smallSquareYDiff);
            Levels[LevelLivePosition.Ten].Position          = new CCPoint(levelsHorizontalPosition, Levels[LevelLivePosition.Nine].PositionY + smallSquareYDiff);
            Levels[LevelLivePosition.Eleven].Position       = new CCPoint(levelsHorizontalPosition, Levels[LevelLivePosition.Ten].PositionY + smallSquareYDiff);
            Levels[LevelLivePosition.Twelve].Position       = new CCPoint(levelsHorizontalPosition, Levels[LevelLivePosition.Eleven].PositionY + smallSquareYDiff);
            Levels[LevelLivePosition.Thirteen].Position     = new CCPoint(levelsHorizontalPosition, Levels[LevelLivePosition.Twelve].PositionY + smallSquareYDiff);
            Levels[LevelLivePosition.Plus].Position         = new CCPoint(levelsHorizontalPosition, Levels[LevelLivePosition.Thirteen].PositionY + smallSquareYDiff);



            Lives[LevelLivePosition.Five].Position         =new CCPoint(livesHorizontalPosition,Levels[LevelLivePosition.Five].PositionY    )        ;
            Lives[LevelLivePosition.Four].Position         =new CCPoint(livesHorizontalPosition,levels[LevelLivePosition.Four].PositionY)            ;
            Lives[LevelLivePosition.Three].Position        =new CCPoint(livesHorizontalPosition,levels[LevelLivePosition.Three].PositionY)           ;
            Lives[LevelLivePosition.Two].Position          =new CCPoint(livesHorizontalPosition,levels[LevelLivePosition.Two].PositionY)             ;
            Lives[LevelLivePosition.One].Position          =new CCPoint(livesHorizontalPosition,levels[LevelLivePosition.One].PositionY)             ;

            Lives[LevelLivePosition.Six].Position          =new CCPoint(livesHorizontalPosition,Levels[LevelLivePosition.Six].PositionY)             ;
            Lives[LevelLivePosition.Seven].Position        =new CCPoint(livesHorizontalPosition,Levels[LevelLivePosition.Seven].PositionY)           ;
            Lives[LevelLivePosition.Eight].Position        =new CCPoint(livesHorizontalPosition,Levels[LevelLivePosition.Eight].PositionY)           ;
            Lives[LevelLivePosition.Nine].Position         =new CCPoint(livesHorizontalPosition,Levels[LevelLivePosition.Nine].PositionY)            ;
            Lives[LevelLivePosition.Ten].Position          =new CCPoint(livesHorizontalPosition,Levels[LevelLivePosition.Ten].PositionY)             ;
            Lives[LevelLivePosition.Eleven].Position       =new CCPoint(livesHorizontalPosition,Levels[LevelLivePosition.Eleven].PositionY)          ;
            Lives[LevelLivePosition.Twelve].Position       =new CCPoint(livesHorizontalPosition,Levels[LevelLivePosition.Twelve].PositionY)          ;
            Lives[LevelLivePosition.Thirteen].Position     =new CCPoint(livesHorizontalPosition,Levels[LevelLivePosition.Thirteen].PositionY)        ;
            Lives[LevelLivePosition.Plus].Position         =new CCPoint(livesHorizontalPosition, Levels[LevelLivePosition.Plus].PositionY);

        }

        
    }

    //public class SquarePoint
    //{
    //    public SquarePoint(Square square, CCPoint point)
    //    {
    //        Square = square;
    //        Point = point;
    //    }

    //    public Square Square { get; set; }

    //    public CCPoint Point { get; set; }
    //}

}
