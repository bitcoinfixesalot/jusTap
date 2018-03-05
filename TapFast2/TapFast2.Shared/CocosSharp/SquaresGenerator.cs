using System;
using System.Collections.Generic;
using System.Text;
using CocosSharp;
using TapFast2.Enums;

namespace TapFast2
{
    internal class CordinatesGenerator
    {
        internal CordinatesGenerator(CCPoint center)
        {
            _centerPoint = center;
        }

        private CCPoint _centerPoint;
        const float SPACE_BETWEEN_SQUARES = 8;

        const float HORIZONTAL_SPACE_BETWEEN_CENTER = 4;
        const float VERTICAL_SPACE_BETWEEN_CENTER = 40;

        const float squareXandY = 200;


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
            PositionAndCordinates[PositionInGame.SignalUpLeft] = new SquarePoint(squares.Find(a => a.CurrentPosition == PositionInGame.SignalUpLeft), new CCPoint(leftSquaresHorizontalPosition, firstRowSquaresVerticalPosition));
            PositionAndCordinates[PositionInGame.SignalUpRight] = new SquarePoint(squares.Find(a => a.CurrentPosition == PositionInGame.SignalUpRight), new CCPoint(rightSquaresHorizontalPosition, firstRowSquaresVerticalPosition));
            PositionAndCordinates[PositionInGame.SignalDownLeft] = new SquarePoint(squares.Find(a => a.CurrentPosition == PositionInGame.SignalDownLeft), new CCPoint(leftSquaresHorizontalPosition, secondRowSquaresVerticalPosition));
            PositionAndCordinates[PositionInGame.SignalDownRight] = new SquarePoint(squares.Find(a => a.CurrentPosition == PositionInGame.SignalDownRight), new CCPoint(rightSquaresHorizontalPosition, secondRowSquaresVerticalPosition));
            PositionAndCordinates[PositionInGame.PlayUpLeft] = new SquarePoint(squares.Find(a => a.CurrentPosition == PositionInGame.PlayUpLeft), new CCPoint(leftSquaresHorizontalPosition, thirdRowSquaresVerticalPosition));
            PositionAndCordinates[PositionInGame.PlayUpRight] = new SquarePoint(squares.Find(a => a.CurrentPosition == PositionInGame.PlayUpRight), new CCPoint(rightSquaresHorizontalPosition, thirdRowSquaresVerticalPosition));
            PositionAndCordinates[PositionInGame.PlayDownLeft] = new SquarePoint(squares.Find(a => a.CurrentPosition == PositionInGame.PlayDownLeft), new CCPoint(leftSquaresHorizontalPosition, fourthRowSquaresVerticalPosition));
            PositionAndCordinates[PositionInGame.PlayDownRight] = new SquarePoint(squares.Find(a => a.CurrentPosition == PositionInGame.PlayDownRight), new CCPoint(rightSquaresHorizontalPosition, fourthRowSquaresVerticalPosition));

            foreach (var square in squares)
            {
                square.Position = PositionAndCordinates[square.CurrentPosition].Point;
            }
        }

        public void SetLabelCordinates(CCLabel scoreLabel, CCLabel livesLabel)
        {
            scoreLabel.Position = new CCPoint(_centerPoint.X, firstRowSquaresVerticalPosition + squareXandY);

            livesLabel.Position = new CCPoint(_centerPoint.X, fourthRowSquaresVerticalPosition - squareXandY);
        }

        public Dictionary<PositionInGame, SquarePoint> PositionAndCordinates = new Dictionary<PositionInGame, SquarePoint>();


    }

    class SquarePoint
    {
        public SquarePoint(Square square, CCPoint point)
        {
            Square = square;
            Point = point;
        }

        public Square Square { get; set; }

        public CCPoint Point { get; set; }
    }

}
