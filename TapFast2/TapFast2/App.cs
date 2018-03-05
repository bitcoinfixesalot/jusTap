//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using TapFast2.Resx;
//using Xamarin.Forms;

//namespace TapFast2
//{
//	public class App : Application
//	{

//        //// http://go.microsoft.com/fwlink/?LinkId=290986&clcid=0x409
//        //public static Microsoft.WindowsAzure.MobileServices.MobileServiceClient tapfastClient = new Microsoft.WindowsAzure.MobileServices.MobileServiceClient(
//        //"https://tapfast.azure-mobile.net/",
//        //"nZKuZgPeFuVqMHYAzQNIcmaTDpXeEA63");

//        public App ()
//		{
//        // The root page of your application
//        // The root page of your application
//        //MainPage = new GamePage();

//        //var ci = DependencyService.Get<ILocale>().GetCurrentCultureInfo();
//        //L10n.SetLocale(ci);
//        //AppResources.Culture = ci;

//        var buttonStyle = new Style(typeof(Button))
//            {
//                Setters = {
//                                new Setter {
//                                    Property = View.HorizontalOptionsProperty,
//                                    Value = LayoutOptions.Center
//                                },
//                                new Setter {
//                                    Property = View.VerticalOptionsProperty,
//                                    Value = LayoutOptions.CenterAndExpand
//                                },
//                                new Setter {
//                                    Property = Button.BorderColorProperty,
//                                    Value = Color.FromRgb(215,9,116)
//                                },
//                                new Setter {
//                                    Property = Button.BorderRadiusProperty,
//                                    Value = 4
//                                },
//                                new Setter {
//                                    Property = Button.BorderWidthProperty,
//                                    Value = 4
//                                },
//                                //new Setter {
//                                //    Property = VisualElement.WidthRequestProperty,
//                                //    Value = 200
//                                //},
//                                //new Setter {
//                                //    Property = Button.TextColorProperty,
//                                //    Value = Color.Teal
//                                //},
//                                new Setter
//                                {
//                                    Property = Button.BackgroundColorProperty,
//                                    Value = Color.FromRgb(27,161, 226)
//                                },
//                                new Setter
//                                {
//                                    Property = Button.FontProperty,
//                                    Value = Font.SystemFontOfSize(NamedSize.Large, FontAttributes.Bold)
//                                }
//                                ,
//                                new Setter
//                                {
//                                    Property = Button.TextColorProperty,
//                                    Value = Color.White
//                                }
//                            }
//            };

//            Resources = new ResourceDictionary();
//            Resources.Add(Constants.Settings.MENU_BUTTON_STYLE, buttonStyle);

//            MainPage = new NavigationPage(new MenuPage());
//        }

//        protected override void OnStart ()
//		{
//			// Handle when your app starts
//		}

//		protected override void OnSleep ()
//		{
//			// Handle when your app sleeps
//		}

//		protected override void OnResume ()
//		{
//			// Handle when your app resumes
//		}
//	}
//}
