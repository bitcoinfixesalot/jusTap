using System;
using System.Collections.Generic;
using System.Text;

namespace TapFast2.Enums
{
    public enum SelectedColor
    {
        Red = 1,
        Green = 2,
        Yellow = 3,
        Blue = 4
    }

    public enum PositionInGame
    {
        SignalUpLeft = 1,
        SignalUpRight = 2,
        SignalDownLeft = 3,
        SignalDownRight = 4,
        PlayUpLeft = 5,
        PlayUpRight = 6,
        PlayDownLeft = 7,
        PlayDownRight = 8
    }

    public enum LevelLivePosition
    {
        One =1,
        Two = 2,
        Three =3,
        Four =4,
        Five = 5,
        Six = 6,
        Seven = 7,
        Eight = 8,
        Nine = 9,
        Ten = 10,
        Eleven = 11,
        Twelve = 12,
        Thirteen = 13,
        Plus = 14
    }

    public enum GameMode
    {
        Easy = 1,
        Normal = 2,
        Hard = 3,
        Impossible = 4,

        FifteenSeconds = 5,
        ThirtySeconds = 6,
        SixtySeconds = 7
    }

    public enum GameType
    {
        NormalGame = 0,
        ArcadeGame = 1,
    }


    public enum Orientation
    {
        /// <summary>
        /// The none
        /// </summary>
        None = 0,
        /// <summary>
        /// The portrait
        /// </summary>
        Portrait = 1,
        /// <summary>
        /// The landscape
        /// </summary>
        Landscape = 2,
        /// <summary>
        /// The portrait up
        /// </summary>
        PortraitUp = 5,
        /// <summary>
        /// The portrait down
        /// </summary>
        PortraitDown = 9,
        /// <summary>
        /// The landscape left
        /// </summary>
        LandscapeLeft = 18,
        /// <summary>
        /// The landscape right
        /// </summary>
        LandscapeRight = 34,
    }
}
