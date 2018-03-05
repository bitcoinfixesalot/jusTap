using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TapFast2.ViewModel;
using Xamarin.Forms;

namespace TapFast2
{
    public partial class AboutPage : ContentPage
    {
        public AboutPage()
        {
            InitializeComponent();

            //var tapGestureRecognizer = new TapGestureRecognizer();
            //tapGestureRecognizer.Tapped += (s, e) =>
            //{
            //    var viewModel = BindingContext as AboutViewModel;
            //    viewModel.ShareCommand.Execute(null);
            //};

            //shareHyperlink.GestureRecognizers.Add(tapGestureRecognizer);
        }
    }
}
