//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Reflection.Emit;
//using System.Text;
//using TapFast2.Resx;
//using Xamarin.Forms;

//namespace TapFast2
//{
//	public class __ArcadeGameOptionsPage : ContentPage
//	{
//		public __ArcadeGameOptionsPage ()
//		{
//            //Content = new StackLayout {
//            //	Children = {
//            //	   new tog
//            //	}
//            //};

//            Title = AppResources.ArcadeGameOptionsTitle;
//            var table = new TableView() { Intent = TableIntent.Settings };
//            var root = new TableRoot();
//            var section1 = new TableSection() { Title = AppResources.EnableSound };
//            var section2 = new TableSection() { Title = AppResources.GameTime };

//            //var text = new TextCell { Text = "TextCell", Detail = "TextCell Detail" };
//            //var entry = new EntryCell { Text = "EntryCell Text", Label = "Entry Label" };
//            var switchSound = new SwitchCell();//{ Text = "Sound On"  };
//            switchSound.OnChanged += SwitchSound_OnChanged;
//            //var image = new ImageCell { Text = "ImageCell Text", Detail = "ImageCell Detail", ImageSource = "XamarinLogo.png" };

//            section1.Add(switchSound);
//            //section1.Add(entry);
//            //section1.Add(switchc);
//            //section1.Add(image);
//            //section2.Add(text);
//            //section2.Add(entry);
//            //section2.Add(switchc);
//            //section2.Add(image);
//            table.Root = root;
//            root.Add(section1);
//            root.Add(section2);

//            Content = table;
//        }

//        private void SwitchSound_OnChanged(object sender, ToggledEventArgs e)
//        {
//            var switchCell = ((SwitchCell)sender);
//            if (switchCell.On)
//            {
//                switchCell.Text = AppResources.SoundOn;
//            }
//            else
//            {
//                switchCell.Text = AppResources.SoundOff;
//            }
//        }

//        protected override void OnDisappearing()
//        {
//            base.OnDisappearing();
//        }
//    }
//}
