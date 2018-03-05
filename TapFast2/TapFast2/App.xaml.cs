using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TapFast2.Helpers;
using Xamarin.Forms;
using Microsoft.Azure.Mobile;
using Microsoft.Azure.Mobile.Analytics;
using Microsoft.Azure.Mobile.Crashes;

namespace TapFast2
{
    public partial class Application : Xamarin.Forms.Application
    {

        //// http://go.microsoft.com/fwlink/?LinkId=290986&clcid=0x409
        //public static Microsoft.WindowsAzure.MobileServices.MobileServiceClient tapfastClient = new Microsoft.WindowsAzure.MobileServices.MobileServiceClient(
        //"https://tapfast.azure-mobile.net/",
        //"nZKuZgPeFuVqMHYAzQNIcmaTDpXeEA63");

        public static string Path = "&amp; quot;MyLocalStore.db&amp;quot;";


        public Application()
        {
            // The root page of your application
            // The root page of your application
            //MainPage = new GamePage();

            //var ci = DependencyService.Get<ILocale>().GetCurrentCultureInfo();
            //L10n.SetLocale(ci);
            //AppResources.Culture = ci;
            InitializeComponent();

            MobileCenter.Start(typeof(Analytics), typeof(Crashes));

            if (Settings.IsDarkTheme)
                Application.Current.Resources.MergedWith = typeof(Xamarin.Forms.Themes.DarkThemeResources);
            else
                Application.Current.Resources.MergedWith = typeof(Xamarin.Forms.Themes.LightThemeResources);

            var buttonStyle = new Style(typeof(Button))
            {
                Setters = {
                                new Setter {
                                    Property = View.HorizontalOptionsProperty,
                                    Value = LayoutOptions.FillAndExpand
                                },
                                new Setter {
                                    Property = View.VerticalOptionsProperty,
                                    Value = LayoutOptions.FillAndExpand
                                },
                                new Setter {
                                    Property = Button.BorderColorProperty,
                                    Value = Color.White //Color.FromRgb(215,9,116)
                                },
                                new Setter {
                                    Property = Button.BorderRadiusProperty,
                                    Value = 4
                                },
                                new Setter {
                                    Property = Button.BorderWidthProperty,
                                    Value = 2
                                },
                                //new Setter {
                                //    Property = VisualElement.WidthRequestProperty,
                                //    Value = 200
                                //},
                                //new Setter {
                                //    Property = Button.TextColorProperty,
                                //    Value = Color.Teal
                                //},
                                new Setter
                                {
                                    Property = Button.BackgroundColorProperty,
                                    Value = Color.FromRgb(27,161, 226)
                                },
                                new Setter
                                {
                                    Property = Button.FontProperty,
                                    Value = GetFontOnPlatform()
                                }
                                ,
                                new Setter
                                {
                                    Property = Button.TextColorProperty,
                                    Value = Color.White
                                }
                            }
            };

            //Resources = new ResourceDictionary();
            Resources.Add(Constants.Options.MENU_BUTTON_STYLE, buttonStyle);
            //Resources.MergedWith = 
            var navService = (Resources["Locator"] as ViewModelLocator).NavigationService;

            MainPage = new NavigationPage(new MenuPage(navService));
        }

        Font GetFontOnPlatform()
        {
            Font result = Font.SystemFontOfSize(NamedSize.Large, FontAttributes.Bold);
            Xamarin.Forms.Device.OnPlatform(iOS: () => result = Font.SystemFontOfSize(NamedSize.Default, FontAttributes.Bold)
            , Android: () => result = Font.SystemFontOfSize(NamedSize.Large, FontAttributes.Bold)
            , WinPhone: () => result = Font.SystemFontOfSize(NamedSize.Medium, FontAttributes.Bold));
            return result;
        }


        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
