using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace TapFast2
{
    public partial class GameOverPage : ContentPage
    {
        public GameOverPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

            //var gameOverViewModel = BindingContext as GameOverViewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            Navigation.RemovePage(this);
        }
    }
}
