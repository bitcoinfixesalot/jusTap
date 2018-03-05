using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TapFast2.Enums;

namespace TapFast2.Helpers
{
    public static class PositionInGameHelper
    {
        static Random random = new Random();

        /// <summary>
        /// level 2
        /// </summary>
        /// <returns></returns>
        public static Tuple<PositionInGame, PositionInGame> GetHorisontalSignalCouple()
        {
            if (random.Next(0, 2) == 0)
                return new Tuple<PositionInGame, PositionInGame>(PositionInGame.SignalUpLeft, PositionInGame.SignalUpRight);
            else
                return new Tuple<PositionInGame, PositionInGame>(PositionInGame.SignalDownLeft, PositionInGame.SignalDownRight);
        }

        /// <summary>
        /// level 3
        /// </summary>
        /// <returns></returns>
        public static Tuple<PositionInGame, PositionInGame> GetVerticalSignalCouple()
        {
            if (random.Next(0, 2) == 0)
                return new Tuple<PositionInGame, PositionInGame>(PositionInGame.SignalUpLeft, PositionInGame.SignalDownLeft);
            else
                return new Tuple<PositionInGame, PositionInGame>(PositionInGame.SignalUpRight, PositionInGame.SignalDownRight);
        }

        /// <summary>
        /// level 4
        /// </summary>
        /// <returns></returns>
        public static Tuple<PositionInGame, PositionInGame> GetDiagonalSignalCouple()
        {
            if (random.Next(0, 2) == 0)
                return new Tuple<PositionInGame, PositionInGame>(PositionInGame.SignalUpLeft, PositionInGame.SignalDownRight);
            else
                return new Tuple<PositionInGame, PositionInGame>(PositionInGame.SignalUpRight, PositionInGame.SignalDownLeft);
        }

        /// <summary>
        /// level 5
        /// </summary>
        /// <returns></returns>
        public static Tuple<PositionInGame, PositionInGame> GetHorisontalPlayCouple()
        {
            if (random.Next(0, 2) == 0)
                return new Tuple<PositionInGame, PositionInGame>(PositionInGame.PlayUpLeft, PositionInGame.PlayUpRight);
            else
                return new Tuple<PositionInGame, PositionInGame>(PositionInGame.PlayDownLeft, PositionInGame.PlayDownRight);
        }

        /// <summary>
        /// level 6
        /// </summary>
        /// <returns></returns>
        public static Tuple<PositionInGame, PositionInGame> GetVerticalPlayCouple()
        {
            if (random.Next(0, 2) == 0)
                return new Tuple<PositionInGame, PositionInGame>(PositionInGame.PlayUpLeft, PositionInGame.PlayDownLeft);
            else
                return new Tuple<PositionInGame, PositionInGame>(PositionInGame.PlayUpRight, PositionInGame.PlayDownRight);
        }

        /// <summary>
        /// level 7
        /// </summary>
        /// <returns></returns>
        public static Tuple<PositionInGame, PositionInGame> GetDiagonalPlayCouple()
        {
            if (random.Next(0, 2) == 0)
                return new Tuple<PositionInGame, PositionInGame>(PositionInGame.PlayUpLeft, PositionInGame.PlayDownRight);
            else
                return new Tuple<PositionInGame, PositionInGame>(PositionInGame.PlayUpRight, PositionInGame.PlayDownLeft);
        }

        public static Tuple<PositionInGame, PositionInGame> GetRandomSignalCouple()
        {
            return GetRandomCouple(1, 5);// first four squares
        }

        public static Tuple<PositionInGame, PositionInGame> GetRandomPlayCouple()
        {
            return GetRandomCouple(5, 9);// second four squares
        }

        public static List<Tuple<PositionInGame, PositionInGame>> GetAllRandom()
        {
            var result = new List<Tuple<PositionInGame, PositionInGame>>();
            int[] arraySignal = { 1, 2, 3, 4 };
            arraySignal = ShuffleArray(arraySignal);
            result.Add(new Tuple<PositionInGame, PositionInGame>((PositionInGame)arraySignal[0], (PositionInGame)arraySignal[1]));
            result.Add(new Tuple<PositionInGame, PositionInGame>((PositionInGame)arraySignal[2], (PositionInGame)arraySignal[3]));


            int[] arrayPlay = { 5, 6, 7, 8 };
            arrayPlay = ShuffleArray(arrayPlay);

            result.Add(new Tuple<PositionInGame, PositionInGame>((PositionInGame)arrayPlay[0], (PositionInGame)arrayPlay[1]));
            result.Add(new Tuple<PositionInGame, PositionInGame>((PositionInGame)arrayPlay[2], (PositionInGame)arrayPlay[3]));

            return result;
        }

        public static List<Tuple<PositionInGame, PositionInGame>> GetAllSignalsRandom()
        {
            var result = new List<Tuple<PositionInGame, PositionInGame>>();
            int[] arraySignal = { 1, 2, 3, 4 };
            arraySignal = ShuffleArray(arraySignal);
            result.Add(new Tuple<PositionInGame, PositionInGame>((PositionInGame)arraySignal[0], (PositionInGame)arraySignal[1]));
            result.Add(new Tuple<PositionInGame, PositionInGame>((PositionInGame)arraySignal[2], (PositionInGame)arraySignal[3]));

            return result;
        }

        public static List<Tuple<PositionInGame, PositionInGame>> GetAllPlayRandom()
        {
            var result = new List<Tuple<PositionInGame, PositionInGame>>();
            int[] arrayPlay = { 5, 6, 7, 8 };
            arrayPlay = ShuffleArray(arrayPlay);

            result.Add(new Tuple<PositionInGame, PositionInGame>((PositionInGame)arrayPlay[0], (PositionInGame)arrayPlay[1]));
            result.Add(new Tuple<PositionInGame, PositionInGame>((PositionInGame)arrayPlay[2], (PositionInGame)arrayPlay[3]));

            return result;
        }

        static int[] ShuffleArray(int[] array)
        {
            Random r = new Random();
            for (int i = array.Length; i > 0; i--)
            {
                int j = r.Next(i);
                int k = array[j];
                array[j] = array[i - 1];
                array[i - 1] = k;
            }
            return array;
        }


        private static Tuple<PositionInGame, PositionInGame> GetRandomCouple(int begin, int end)
        {
            var firstPosition = (PositionInGame)random.Next(begin, end);
            var secondPosition = (PositionInGame)random.Next(begin, end);
            while (firstPosition == secondPosition)
                secondPosition = (PositionInGame)random.Next(begin, end);

            return new Tuple<PositionInGame, PositionInGame>(firstPosition, secondPosition);
        }

    }
}
