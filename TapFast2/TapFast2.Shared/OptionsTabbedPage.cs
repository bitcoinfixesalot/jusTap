using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace TapFast2
{
	public class OptionsTabbedPage : TabbedPage
	{
		public OptionsTabbedPage ()
		{
            //Content = new StackLayout {
            //	Children = {
            //		new Label { Text = "Hello Page" }
            //	}
            //};
            Children.Add(new GameOptionsPage());
            Children.Add(new ArcadeGameOptionsPage());
		}
	}
}
