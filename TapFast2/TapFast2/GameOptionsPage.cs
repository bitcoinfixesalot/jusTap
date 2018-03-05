//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Reflection.Emit;
//using System.Text;
//using TapFast2.Resx;
//using Xamarin.Forms;

//namespace TapFast2
//{
//	public class GameOptionsPage : ContentPage
//	{
//		public GameOptionsPage()
//		{
            
//            //Device.OnPlatform(null, null, SetResourceManager, null);
//            Title = AppResources.StandardGameOptionsTitle;
//            //var table = new TableView() { Intent = TableIntent.Form };
//            //var root = new TableRoot();
//            //var section1 = new TableSection() { Title = AppResources.EnableSound };
//            //var section2 = new TableSection() { Title = AppResources.GameDifficulty };

//            //var switchSound = new SwitchCell(); //{ Text = "Sound On" };
//            //switchSound.OnChanged += SwitchSound_OnChanged;


//            //var picker = new Picker { Title = "Difficulty", HorizontalOptions = LayoutOptions.CenterAndExpand, InputTransparent = true };//, VerticalOptions = LayoutOptions.CenterAndExpand , HorizontalOptions = LayoutOptions.CenterAndExpand , HeightRequest =100 };

//            //picker.Items.Add(AppResources.Easy);
//            //picker.Items.Add(AppResources.Normal);
//            //picker.Items.Add(AppResources.Hard);
//            //picker.Items.Add(AppResources.Impossible);

//            //var diff = new PickerCell()
//            //{
//            //    //Label = "Select Difficulty",
//            //    Picker = picker,
//            //};

//            //section2.Add(diff);
//            //section1.Add(switchSound);

//            //table.Root = root;
//            //root.Add(section1);
//            //root.Add(section2);

//            //Content = table;

//            var panel = new StackLayout { Orientation = StackOrientation.Vertical, Padding = new Thickness(20) };

//            var picker = new Picker { Title = "Difficulty" , Margin = new Thickness(20)};
//            picker.Items.Add(AppResources.Easy);
//            picker.Items.Add(AppResources.Normal);
//            picker.Items.Add(AppResources.Hard);
//            picker.Items.Add(AppResources.Impossible);
//            panel.Children.Add(picker);

//            Content = panel;
//            //var switchSound = new Switch {}

//        }

//        private void SetResourceManager()
//        {
            
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
