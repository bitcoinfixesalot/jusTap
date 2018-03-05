using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TapFast2.Enums;
using TapFast2.Services;
using Xamarin.Forms;

namespace TapFast2.ViewModel
{
    public class LeaderboardViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName]string name = "") =>
           PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        AzureService azureService;

        public LeaderboardViewModel()
        {
            azureService = DependencyService.Get<AzureService>();

            ScoreItems = new ObservableCollection<Scores>();
            //Task.Run(async () =>
            //{
            //    var items =  await  azureService.GetScores();

            //    Device.BeginInvokeOnMainThread(() =>
            //    {
            //        //ScoreItems.ReplaceRange(items);
            //        foreach (var item in items)
            //        {
            //            ScoreItems.Add(item);
            //        }
            //    });
                
            //});
        }

        public ObservableCollection<Scores> ScoreItems { get; set; } 


        ICommand getLeaderboard;
        public ICommand GetLeaderboardCommand =>
                getLeaderboard ??
                (getLeaderboard = new Command(async () => await ExecuteGetLeaderboardCommand()));

        bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set { isBusy = value; OnPropertyChanged(); }
        }

        GameType gameType;
        public GameType CurrentGameType
        {
            get { return gameType; }
            set
            {
                gameType = value;
                
                //IsBusy = true;
                //try
                //{
                //    Task.Run(async () =>
                //    {
                //        var items = await azureService.GetScores(gameType);

                    //        Device.BeginInvokeOnMainThread(() =>
                    //        {
                    //            //ScoreItems.ReplaceRange(items);
                    //            ScoreItems.Clear();
                    //            foreach (var item in items)
                    //            {
                    //                ScoreItems.Add(item);
                    //            }
                    //        });

                    //    });
                    //}
                    //finally
                    //{
                    //    IsBusy = false;
                    //}
            }
        }

        private async Task ExecuteGetLeaderboardCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                var items = await azureService.GetScores(CurrentGameType);
                ScoreItems.Clear();
                int i = 1;
                foreach (var item in items)
                {
                    item.Number = i;
                    ScoreItems.Add(item);
                    i++;
                }

            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
