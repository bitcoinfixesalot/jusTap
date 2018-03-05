using CocosSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TapFast2.Enums;
using TapFast2.Helpers;

namespace TapFast2
{
    public class Animator
    {

        Square playUpLeft ;
        Square playUpRight;
        Square playDownLeft;
        Square playDownRight;

        CordinatesGenerator _cordinatesGenerator;

        public Animator(CordinatesGenerator cordinates)
        {
            _cordinatesGenerator = cordinates;
        }

        public void BeginSquareMovementByLevel(uint level, float timeForMovement, Action callback)
        {
            Queue<Tuple<PositionInGame, PositionInGame>> positionsToMove = GetPositionToMoveByLevel(level);
            if (positionsToMove.Count == 0)
            {
                callback.Invoke();
                return;
            }
            while (positionsToMove.Count != 0)
            {
                var positionsTuple = positionsToMove.Dequeue();
                if (positionsToMove.Count == 0)
                    ChangeSquaresPosition(timeForMovement, positionsTuple.Item1, positionsTuple.Item2, callback);
                else
                    ChangeSquaresPosition(timeForMovement, positionsTuple.Item1, positionsTuple.Item2);
            }
        }

        private Queue<Tuple<PositionInGame, PositionInGame>> GetPositionToMoveByLevel(uint level)
        {
            Queue<Tuple<PositionInGame, PositionInGame>> result = new Queue<Tuple<PositionInGame, PositionInGame>>();
            switch (level)
            {

                case 1:
                    result.Enqueue(PositionInGameHelper.GetHorisontalSignalCouple());
                    break;
                case 2:
                    result.Enqueue(PositionInGameHelper.GetVerticalSignalCouple());
                    break;
                case 3:
                    result.Enqueue(PositionInGameHelper.GetDiagonalSignalCouple());
                    break;
                case 4:
                    result.Enqueue(PositionInGameHelper.GetHorisontalSignalCouple());
                    result.Enqueue(PositionInGameHelper.GetHorisontalPlayCouple());
                    break;
                case 5:
                    result.Enqueue(PositionInGameHelper.GetVerticalSignalCouple());
                    result.Enqueue(PositionInGameHelper.GetVerticalPlayCouple());
                    break;
                case 6:
                    result.Enqueue(PositionInGameHelper.GetDiagonalSignalCouple());
                    result.Enqueue(PositionInGameHelper.GetDiagonalPlayCouple());
                    break;
                case 7:
                    result.Enqueue(PositionInGameHelper.GetRandomSignalCouple());
                    result.Enqueue(PositionInGameHelper.GetRandomPlayCouple());
                    break;
                case 8:
                default:
                    var couples = PositionInGameHelper.GetAllRandom();
                    foreach (var item in couples)
                        result.Enqueue(item);
                    break;
                //case 10:
                //    foreach (var item in PositionInGameHelper.GetAllSignalsRandom())
                //        result.Enqueue(item);
                //    break;
            }

            return result;
        }

        CCBlink blinkOnce = new CCBlink(0.5f, 1);
        CCBlink blinkLives = new CCBlink(0.5f, 2);


        internal void BeginLiveTakenAnimation(uint lives, Action callback)
        {
            if (lives == 0)
            {
                callback.Invoke();
                return;
            }
            var completedAction = new CCCallFunc(callback);

            //CCBlink blinkLives = new CCBlink(0.5f, 2);
            CCSequence mySequence = new CCSequence(blinkLives, completedAction);
            uint liveToTake = lives + 1;
            if (liveToTake > 14)
            {
                _cordinatesGenerator.Lives[LevelLivePosition.Plus].RunAction(mySequence);
            }
            else
            {
                var currentLevelSquare = _cordinatesGenerator.Lives[(LevelLivePosition)liveToTake];
                currentLevelSquare.RunAction(mySequence);
            }
        }

        internal void BeginLiveAddedAnimation(uint lives)
        {
            if (_cordinatesGenerator.Lives.ContainsKey((LevelLivePosition)lives))
            {
                var livesSquare = _cordinatesGenerator.Lives[(LevelLivePosition)lives];
                livesSquare.Visible = true;
                livesSquare.RunAction(blinkLives);
                ShowEarnedLives(lives);
            }
            else
            {
                _cordinatesGenerator.Lives[LevelLivePosition.Plus].RunAction(blinkLives);
            }
        }

        private void ShowEarnedLives(uint lives)
        {
            var livePosition = lives;
            while (livePosition >= 1)
            {
                if (_cordinatesGenerator.Lives.ContainsKey((LevelLivePosition)livePosition))
                {
                    var livesSquare = _cordinatesGenerator.Lives[(LevelLivePosition)livePosition];
                    livesSquare.Visible = true;
                }
                livePosition--;
            }
        }

        internal void HideLostLives(uint lives)
        {
            var livePosition = lives + 1;
            while (livePosition <= 14)
            {
                if (_cordinatesGenerator.Lives.ContainsKey((LevelLivePosition)livePosition))
                {
                    var livesSquare = _cordinatesGenerator.Lives[(LevelLivePosition)livePosition];
                    livesSquare.Visible = false;
                }
                livePosition++;
            }
        }


        public void BeginLevelChangeAnimation(uint level, Action callback)
        {

            //foreach (PositionInGame positionType in Enum.GetValues(typeof(PositionInGame)))
            //{
            //    var square = _cordinatesGenerator.PositionAndSquare[positionType];


            //    square.RunAction(blink);
            //}

            int levelIndex = 1;
            while (levelIndex < level)
            {
                var smallSquare = _cordinatesGenerator.Levels[(LevelLivePosition)levelIndex];
                smallSquare.RunAction(blinkOnce);

                levelIndex++;
            }

            var completedAction = new CCCallFunc(callback);

            CCBlink blinkLevel = new CCBlink(1.0f, 3);
            CCSequence mySequence = new CCSequence(blinkLevel, completedAction);
            var currentLevelSquare = _cordinatesGenerator.Levels[(LevelLivePosition)level];
            currentLevelSquare.Visible = true;
            currentLevelSquare.RunAction(mySequence);
            //RunAction(mySequence);

            //if (level >= 10)
            //{
            //    if (rotating == false)
            //    {
            //        rotating = true;
            //        //DiagonalSquares(() => StarRotating(true));
            //        StarRotating(true);
            //    }
            //}
        }


        bool _isDiagonal = false;

        private async Task DiagonalSquares(CCFiniteTimeAction rotate, Action callback = null)
        {
            if (_isDiagonal)
                return;

            _isDiagonal = true;
            playUpLeft.RunAction(rotate);


            playDownRight.RunAction(rotate);
            playDownLeft.RunAction(rotate);
            if (callback != null)
            {
                var movecompletedaction = new CCCallFunc(callback);
                CCSequence mysequence = new CCSequence(rotate, movecompletedaction);
                playUpRight.RunAction(mysequence);
            }
            else
                await playUpRight.RunActionAsync(rotate);
        }

        bool rotating = false;

        public readonly CancellationToken Cancel = new CancellationToken();

        private void StarRotating(bool clockwise)
        {
            if (Cancel.IsCancellationRequested)
                return;
            bool clockWise = true;
            var task = RotatePlayButtons(clockWise, () =>
            {
                clockWise = !clockWise;
                StarRotating(clockWise);
            });

            Task.WhenAny(task);
        }

        private async Task RotatePlayButtons(bool clockwise, Action callback)
        {
            float timePerMove = 1.5f;


            //var signalUpLeft = _cordinatesGenerator.PositionAndSquare[PositionInGame.SignalUpLeft];
            //var signalUpRight = _cordinatesGenerator.PositionAndSquare[PositionInGame.SignalUpRight];
            //var signalDownLeft = _cordinatesGenerator.PositionAndSquare[PositionInGame.SignalDownLeft];
            //var signalDownRight = _cordinatesGenerator.PositionAndSquare[PositionInGame.SignalDownRight];
            playUpLeft = _cordinatesGenerator.PositionAndSquare[PositionInGame.PlayUpLeft];
            playUpRight = _cordinatesGenerator.PositionAndSquare[PositionInGame.PlayUpRight];
            playDownLeft = _cordinatesGenerator.PositionAndSquare[PositionInGame.PlayDownLeft];
            playDownRight = _cordinatesGenerator.PositionAndSquare[PositionInGame.PlayDownRight];


          
            var moveToUpLeft = new CCMoveTo(timePerMove, new CCPoint(playUpLeft.Position.X, playUpLeft.Position.Y));

            var moveToUpRight = new CCMoveTo(timePerMove, new CCPoint(playUpRight.Position.X, playUpRight.Position.Y));




            var moveToDownRight = new CCMoveTo(timePerMove, new CCPoint(playDownRight.Position.X, playDownRight.Position.Y));


            var moveToDownLeft = new CCMoveTo(timePerMove, new CCPoint(playDownLeft.Position.X, playDownLeft.Position.Y));

            var moveCompletedAction = new CCCallFunc(callback);
            CCSequence mySequence = new CCSequence(moveToDownRight, moveCompletedAction);



            CCRotateBy rotate = new CCRotateBy(0.5f, 45);
            var rotateBack = rotate.Reverse();
            await DiagonalSquares(rotate);

            await MoveHalfWay(timePerMove);


            //var halfMoveCompletedAction = new CCCallFunc(() =>
            //{
            _cordinatesGenerator.PositionAndSquare[PositionInGame.PlayUpLeft] = playDownLeft;
            _cordinatesGenerator.PositionAndSquare[PositionInGame.PlayUpRight] = playUpLeft;
            _cordinatesGenerator.PositionAndSquare[PositionInGame.PlayDownLeft] = playDownRight;
            _cordinatesGenerator.PositionAndSquare[PositionInGame.PlayDownRight] = playUpRight;

            playUpLeft.RunAction(moveToUpRight);
                playDownRight.RunAction(moveToDownLeft);
                playDownLeft.RunAction(moveToUpLeft);
                await playUpRight.RunActionAsync(mySequence);

            //await DiagonalSquares(rotateBack, callback);
            //});
            

            
            //}
            //else
            //{
            //    playUpLeft.RunAction(moveToDownLeft);
            //    playUpRight.RunAction(moveToUpLeft);
            //    playDownRight.RunAction(moveToUpRight);
            //    playDownLeft.RunAction(mySequence);

            //    _cordinatesGenerator.PositionAndSquare[PositionInGame.PlayUpLeft] = playUpRight;
            //    _cordinatesGenerator.PositionAndSquare[PositionInGame.PlayUpRight] = playDownRight;
            //    _cordinatesGenerator.PositionAndSquare[PositionInGame.PlayDownLeft] = playUpLeft;
            //    _cordinatesGenerator.PositionAndSquare[PositionInGame.PlayDownRight] = playDownLeft;
            //}
        }


        private async Task MoveHalfWay(float timePerMove)
        {
            float diffXY = 70.0f;
            float halfWay = 104.0f;

            var moveToUpLeftHalfWay = new CCMoveTo(timePerMove, new CCPoint(playUpLeft.Position.X - diffXY, playUpLeft.Position.Y - halfWay));
            var moveToUpRightHalfWay = new CCMoveTo(timePerMove, new CCPoint(playUpRight.Position.X - halfWay, playUpRight.Position.Y + diffXY));
            var moveToDownRightHalfWay = new CCMoveTo(timePerMove, new CCPoint(playDownRight.Position.X + diffXY, playDownRight.Position.Y + halfWay));  //- halfWay, playDownRight.Position.Y - diffXY
            var moveToDownLeftHalfWay = new CCMoveTo(timePerMove, new CCPoint(playDownLeft.Position.X + halfWay, playDownLeft.Position.Y - diffXY));  //+ diffXY, playDownLeft.Position.Y - halfWay

            //CCSequence halfWaySequence = new CCSequence(moveToDownRightHalfWay, halfMoveCompletedAction);

            //if (clockwise)
            //{

            playUpLeft.RunAction(moveToUpRightHalfWay);


            playDownRight.RunAction(moveToDownLeftHalfWay);
            playDownLeft.RunAction(moveToUpLeftHalfWay);
            await playUpRight.RunActionAsync(moveToDownRightHalfWay);
        }

        public void ChangeSquaresPosition(float timeForChanging, PositionInGame firstPosition, PositionInGame secondPosition, Action callback = null)
        {
            //var firstPosition = (PositionInGame)_random.Next(firstPositionInGame, lastPositionInGame);
            //var secondPosition = (PositionInGame)_random.Next(firstPositionInGame, lastPositionInGame);
            //while (firstPosition == secondPosition)
            //    secondPosition = (PositionInGame)_random.Next(firstPositionInGame, lastPositionInGame);


            var firstSquarePosition = _cordinatesGenerator.PositionAndSquare[firstPosition];
            var secondSquarePosition = _cordinatesGenerator.PositionAndSquare[secondPosition];

            var firstSquare = firstSquarePosition;
            var secondSquare = secondSquarePosition;

            _cordinatesGenerator.PositionAndSquare[firstPosition] = secondSquare;
            _cordinatesGenerator.PositionAndSquare[secondPosition] = firstSquare;


            var moveToFirstPosition = new CCMoveTo(timeForChanging, new CCPoint(firstSquare.Position.X, firstSquare.Position.Y));
            var moveToSecondPosition = new CCMoveTo(timeForChanging, new CCPoint(secondSquare.Position.X, secondSquare.Position.Y));

            secondSquare.RunAction(moveToFirstPosition);

            if (callback != null)
            {
                var moveCompletedAction = new CCCallFunc(callback);
                CCSequence mySequence = new CCSequence(moveToSecondPosition, moveCompletedAction);

                firstSquare.RunAction(mySequence);
            }
            else
            {
                firstSquare.RunAction(moveToSecondPosition);
            }
        }

        
    }
}
