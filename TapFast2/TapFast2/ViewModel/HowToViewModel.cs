using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TapFast2.ViewModel
{
    public class HowToViewModel
    {

        public string GameDescriptionText { get { return GetDescription(); } }

        private string GetDescription()
        {
//            string result = @"There are two sets of four squares on the display. 
//The top four squares are the signal pane (which you cannot press / tap), and
//  The bottom four squares which are the control pane (those you can press / tap). 
//The line dividing the both indicates remaining time between turns (colours changing).
//";
            var sb = new StringBuilder();
            sb.Append("There are two sets of four squares on the display.").Append(Environment.NewLine)
                .Append("The top four squares are the signal pane (which you cannot press / tap).").Append(Environment.NewLine)
                .Append("The bottom four squares which are the control pane (those you can press / tap).").Append(Environment.NewLine)
                .Append("The line dividing the both indicates remaining time between turns (colours changing).");

            return sb.ToString();
        }

        public string GameInstructionsText { get { return GetInstructions(); } }

        private string GetInstructions()
        {
            //            The aim: to match the signal pane’s highlighted square with the same colour square on the control pane. 
            //You need to press only one square at a time. 

            var sb = new StringBuilder();
            sb.Append("The aim: to match the signal pane’s highlighted square with the same colour square on the control pane.").Append(Environment.NewLine)
                .Append("You need to press only one square at a time.");

            return sb.ToString();
        }

        public string DifficultyModesText { get { return GetDifficultyModes(); } }

        private string GetDifficultyModes()
        {
            //            Game difficulty modes:
            //            Easy(plenty of time to react) – you earn 1 point per successful tap
            //Normal(enough time to react) – you earn 2 points per successful tap
            //Hard(hardly any time to react) – you earn 3 points per successful tap
            //Impossible(no time to react) – you earn 4 points per successful tap

            //Successful tap means that the signal and the control squares have been matched by colour within the allotted time.

            var sb = new StringBuilder();
            sb.Append("Easy(plenty of time to react) – you earn 1 point per successful tap.").Append(Environment.NewLine)
            .Append("Normal(enough time to react) – you earn 2 points per successful tap.").Append(Environment.NewLine)
            .Append("Hard(hardly any time to react) – you earn 3 points per successful tap").Append(Environment.NewLine)
                        .Append("Impossible(no time to react) – you earn 4 points per successful tap.").Append(Environment.NewLine).Append(Environment.NewLine)
                        .Append("Successful tap means that the signal and the control squares have been matched by colour within the allotted time.").Append(Environment.NewLine)
                        .Append("*Bear in mind that, if you choose to use the pause button, the same amount of points will be taken from your score.");

            return sb.ToString();

        }

        public string GameProgressText { get { return GetProgress(); } }

        private string GetProgress()
        {
            return "At first the squares are going to move in a limited fashion, but in a few seconds you will notice them behaving in a more erratic fashion. At final stage all of the squares change position at every turn.";
        }
    }
}
