using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TapFast2.Resx;
using Xamarin.Forms;

namespace TapFast2
{
    public partial class OptionsTestPage : ContentPage
    {
        public OptionsTestPage()
        {
            InitializeComponent();

            //BindingContext = new OptionsViewModel();
            Title = AppResources.StandardGameOptionsTitle;
        }

    }
}
