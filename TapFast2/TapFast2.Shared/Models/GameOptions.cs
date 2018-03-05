using System;
using System.Collections.Generic;
using System.Text;

namespace TapFast2.Models
{
    public class GameOptions
    {
        /// <summary>
        /// color changing in miliseconds
        /// </summary>
        public int ColorChangingInterval { get; set; }

        //TODO: Add sounds
        public bool SoundEnabled { get; set; }
    }
}
