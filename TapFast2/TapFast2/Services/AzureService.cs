using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TapFast2.Enums;
using TapFast2.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(AzureService))]
namespace TapFast2.Services
{
    public class AzureService
    {
        public MobileServiceClient Client { get; set; } = null;
        IMobileServiceSyncTable<Scores> table;

        private string _path;

        public string CurrentPath
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_path))
                {
                    _path = Path.Combine(MobileServiceClient.DefaultDatabasePath, "Scores.db");
                }
                
                return _path; }
            set { _path = value; }
        }


        public async Task Initialize()
        {
            if (Client?.SyncContext?.IsInitialized ?? false)
                return;

            var appUrl = "https://tapfast2.azurewebsites.net";

            //Create our client
            Client = new MobileServiceClient(appUrl);

            //InitialzeDatabase for path
            

           


            //setup our local sqlite store and intialize our table
            var store = new MobileServiceSQLiteStore(CurrentPath);

            //Define table
            store.DefineTable<Scores>();

            //Initialize SyncContext
            await Client.SyncContext.InitializeAsync(store, new MobileServiceSyncHandler());

            //Get our sync table that will call out to azure
            table = Client.GetSyncTable<Scores>();
        }

        public async Task<IEnumerable<Scores>> GetScores(GameType gameType)
        {
            await Initialize();
            await SyncScores();
            return await table.Where(a=> a.GameType == (int)gameType).OrderByDescending(s => s.Score).ToEnumerableAsync();
        }


        public async Task SyncScores()
        {
            try
            {
                await Client.SyncContext.PushAsync();
                await table.PullAsync("allScoreItems", table.CreateQuery());
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Unable to sync scores, that is alright as we have offline capabilities: " + ex);
            }

        }

        internal async Task<bool> SaveScore(string yourName, int score, GameType gameType, GameMode mode, int averageTime)
        {
            await Initialize();

            try
            {
                var item = await GetScoreItemByNameAsync(yourName, gameType, mode);
                if (item == null)
                {
                    item = new Scores { Name = yourName.Trim(), Score = score, GameType = (int)gameType, GameSubType = (int)mode, OSType = (int)Device.OS, AverageTime = averageTime };
                    await SaveScoreAsync(item);
                }
                else
                {
                    //set update
                    if (item.Score < score)
                    {
                        item.Score = score;
                        item.AverageTime = averageTime;
                        await SaveScoreAsync(item);
                    }
                }
                return true;
            }
            catch (MobileServiceInvalidOperationException msioe)
            {
                Debug.WriteLine(@"SaveScore sync operation: {0}", msioe.Message);
                return false;
            }
            catch (Exception e)
            {
                Debug.WriteLine(@"SaveScore error: {0}", e.Message);
                return false;
            }
        }

        public async Task SaveScoreAsync(Scores item)
        {
            if (item.Id == null)
            {
                await table.InsertAsync(item);
            }
            else
            {
                await table.UpdateAsync(item);
            }
        }

        public async Task<Scores> GetScoreItemByNameAsync(string name, GameType gameType, GameMode mode)
        {
            if (string.IsNullOrWhiteSpace(name))
                return null;

            await Initialize();

            try
            {
                //await SyncAsync();

                return (await table.Where(a => a.GameType == (int)gameType && a.GameSubType == (int)mode && a.Name.ToLower() == name.ToLower().Trim()).Take(1).ToEnumerableAsync()).FirstOrDefault();
            }
            catch (MobileServiceInvalidOperationException msioe)
            {
                Debug.WriteLine(@"GetScoreItemByNameAsync sync operation: {0}", msioe.Message);
            }
            catch (Exception e)
            {
                Debug.WriteLine(@"GetScoreItemByNameAsync error: {0}", e.Message);
            }
            return null;
        }

        public async Task<int> GetGameHighscoreAsync(GameType gameType, GameMode difficulty)
        {
            await Initialize();
            try
            {
                var result = await table.Where(a => a.GameType == (int)gameType && a.GameSubType == (int)difficulty).OrderByDescending(a => a.Score).ToListAsync();

                var scoreItem = result.FirstOrDefault();
                if (scoreItem == null)
                    return 0;

                return scoreItem.Score;
            }
            catch (MobileServiceInvalidOperationException msioe)
            {
                Debug.WriteLine(@"GetGameHighscoreAsync sync operation: {0}", msioe.Message);
            }
            catch (Exception e)
            {
                Debug.WriteLine(@"GetGameHighscoreAsync error: {0}", e.Message);
            }
            return 0;
        }
    }

}
