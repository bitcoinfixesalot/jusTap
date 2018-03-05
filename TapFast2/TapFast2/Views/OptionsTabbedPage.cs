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
            Device.OnPlatform(iOS: () => Padding = new Thickness(0, 20, 0, 0)
                    , Android: () => Padding = new Thickness(0, 0, 0, 0)
                    , WinPhone: () => Padding = new Thickness(0, 0, 0, 0));

            //Content = new StackLayout {
            //	Children = {
            //		new Label { Text = "Hello Page" }
            //	}
            //};
            //  var optionsViewModel = new OptionsViewModel();
            Children.Add(new OptionsTestPage ());
            Children.Add(new ArcadeGameOptionsPage ());
		}
	}
}
