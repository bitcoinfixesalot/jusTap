using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace TapFast2
{
	public class GameOptionsPage : ContentPage
	{
		public GameOptionsPage()
		{
            Title = "Standard Game Options";

            var table = new TableView() { Intent = TableIntent.Settings };
            var root = new TableRoot();
            var section1 = new TableSection() { Title = "Enable Sound" };
            var section2 = new TableSection() { Title = "Game Time" };

            var switchSound = new SwitchCell { Text = "Sound On" };
            //switchSound.OnChanged += SwitchSound_OnChanged;

            section1.Add(switchSound);
            table.Root = root;
            root.Add(section1);
            root.Add(section2);

            Content = table;

        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }
    }
}
