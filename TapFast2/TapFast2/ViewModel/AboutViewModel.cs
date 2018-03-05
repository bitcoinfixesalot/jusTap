using Acr.UserDialogs;
using Plugin.AppInfo;
using Plugin.Messaging;
using Plugin.Share;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace TapFast2.ViewModel
{
    public class AboutViewModel : INotifyPropertyChanged
    {
        public AboutViewModel()
        {
            
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName]string name = "") =>
           PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));


        ICommand _shareCommand;
        public ICommand ShareCommand => _shareCommand ?? (_shareCommand = new Command(async () => await ShareGame()));

        ICommand _rateCommand;
        public ICommand RateCommand => _rateCommand ?? (_rateCommand = new Command(async () => await RateGame()));

        
             ICommand _sendFeedbackCommand;
        public ICommand SendFeedbackCommand => _sendFeedbackCommand ?? (_sendFeedbackCommand = new Command(async () => await SendFeedBack()));

        private async Task SendFeedBack()
        {
            //var promptConfig = new PromptConfig { Title = "Feedback", Text = "" };
            //promptConfig.SetPlaceholder("Enter your message");
            //promptConfig.SetOkText("Send");
            //promptConfig.SetCancelText("Cancel");

            //var result = await UserDialogs.Instance.PromptAsync(promptConfig);
            //if (result.Ok)
            //{
                var emailMessenger = CrossMessaging.Current.EmailMessenger;
                if (emailMessenger.CanSendEmail)
                {
                    emailMessenger.SendEmail("vorsightech@outlook.com", Enum.GetName(typeof(TargetPlatform), Device.OS), "<<Please enter your message!>>");
                }
            //}
        }

        private async Task ShareGame()
        {
            var message = new Plugin.Share.Abstractions.ShareMessage();
            message.Url = GetAppURL();
            message.Title = "jusTap";
            message.Text = string.Format("Check out this app - iOS: {0}, Android: {1}", "https://itunes.apple.com/bg/app/justap/id1194155031?mt=8", "http://play.google.com/store/apps/details?id=com.vorsightech.tapfast");
            await CrossShare.Current.Share(message);
        }

        private string GetAppURL()
        {
            return Device.OnPlatform<string>("https://itunes.apple.com/bg/app/justap/id1194155031?mt=8",
                "http://play.google.com/store/apps/details?id=com.vorsightech.tapfast", string.Empty);//TODO: add windows url
        }

        private Task RateGame()
        {
            throw new NotImplementedException();
        }

        public string Version
        {
            get { return string.Format("jusTap - version: {0}", CrossAppInfo.Current.Version); }
        }

        public object Logo
        {
            get { return ImageSource.FromResource("TapFast2.Images.logo240x240.png"); }
        }

        public string DevelopersText { get { return GetDevelopersText(); } }

        public string DisclaimerText { get { return GetDisclaimerText(); } }

        private string GetDisclaimerText()
        {
            var sb = new StringBuilder();

            sb.Append("Any addiction, fun, challenge, missed stops and/or sore fingers the player may experience while playing this game is pretty much intended.").Append(Environment.NewLine)
                .Append("also").Append(Environment.NewLine)
                .Append("Any resemblance of anything about this app with anything ever created by anyone else is entirely coincidental and will be vigorously denied.");

            return sb.ToString();
        }

        private string GetDevelopersText()
        {
            var sb = new StringBuilder();

            sb.Append("SA (designer, programmer, square potato)").Append(Environment.NewLine)
                .Append("Likes – Code (kidding)").Append(Environment.NewLine)
                .Append("Dislikes – Office environment (that one’s true)").Append(Environment.NewLine)
                .Append("Contribution – 99.99%").Append(Environment.NewLine)
                .Append("and").Append(Environment.NewLine)
                .Append("LK (co-designer, lazy bum, wannabe)").Append(Environment.NewLine)
                .Append("Likes – Coffee").Append(Environment.NewLine)
                .Append("Dislikes – Poor manners").Append(Environment.NewLine)
                .Append("Contribution – 0.01% (yes, really)");

            return sb.ToString();
        }
    }
}
