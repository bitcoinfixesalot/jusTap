///*
// * To add Offline Sync Support:
// *  1) Add the NuGet package Microsoft.Azure.Mobile.Client.SQLiteStore (and dependencies) to all client projects
// *  2) Uncomment the #define OFFLINE_SYNC_ENABLED
// *
// * For more information, see: http://go.microsoft.com/fwlink/?LinkId=620342
// */
//#define OFFLINE_SYNC_ENABLED

//using System;
//using System.Collections.Generic;
//using System.Collections.ObjectModel;
//using System.Diagnostics;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Microsoft.WindowsAzure.MobileServices;
//using Xamarin.Forms;
//using System.IO;

//#if OFFLINE_SYNC_ENABLED
//using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
//using Microsoft.WindowsAzure.MobileServices.Sync;
//using TapFast2.Enums;
//#endif

//namespace TapFast2
//{
//    public partial class ScoreItemManager
//    {
//        static ScoreItemManager defaultInstance = new ScoreItemManager();
//        MobileServiceClient client;

//#if OFFLINE_SYNC_ENABLED
//        IMobileServiceSyncTable<Scores> todoTable;

     
//#else
//        IMobileServiceTable<ScoreItem> todoTable;
//#endif

//        const string offlineDbPath = @"Scores.db";

////        private ScoreItemManager()
////        {
////            this.client = new MobileServiceClient("http://tapfast2.azurewebsites.net");

////#if OFFLINE_SYNC_ENABLED
////            var path = Path.Combine(MobileServiceClient.DefaultDatabasePath, offlineDbPath);

////            var store = new MobileServiceSQLiteStore(path);
////            store.DefineTable<ScoreItem>();

////            //Initializes the SyncContext using the default IMobileServiceSyncHandler.
////            this.client.SyncContext.InitializeAsync(store, new MobileServiceSyncHandler());

////            this.todoTable = client.GetSyncTable<ScoreItem>();
////#else
////            this.todoTable = client.GetTable<ScoreItem>();
////#endif
////        }

//        private async Task Initialize()
//        {
//            if (client?.SyncContext?.IsInitialized ?? false)
//                return;

//            this.client = new MobileServiceClient("https://tapfast2.azurewebsites.net/");

//            var path = Path.Combine(MobileServiceClient.DefaultDatabasePath, offlineDbPath);

//            var store = new MobileServiceSQLiteStore(path);
//            store.DefineTable<Scores>();

//            //Initializes the SyncContext using the default IMobileServiceSyncHandler.
//            try
//            {
//                await this.client.SyncContext.InitializeAsync(store, new MobileServiceSyncHandler());

//                this.todoTable = client.GetSyncTable<Scores>();
//            }
//            catch (Exception ex)
//            {
//                Debug.WriteLine("Initialize(); " + ex.Message);
//            }
//        }

        
//        public static ScoreItemManager DefaultManager
//        {
//            get
//            {
//                return defaultInstance;
//            }
//            private set
//            {
//                defaultInstance = value;
//            }
//        }

//        public MobileServiceClient CurrentClient
//        {
//            get { return client; }
//        }

//        public bool IsOfflineEnabled
//        {
//            get { return todoTable is Microsoft.WindowsAzure.MobileServices.Sync.IMobileServiceSyncTable<Scores>; }
//        }



//        public async Task<int> GetGameHighscoreAsync(GameType gameType, GameMode difficulty)
//        {
//            await Initialize();
//            try
//            {
//                var result = await todoTable.Where(a => a.GameType == (int)gameType && a.GameSubType == (int)difficulty).OrderByDescending(a => a.Score).ToListAsync();

//                var scoreItem = result.FirstOrDefault();
//                if (scoreItem == null)
//                    return 0;

//                return scoreItem.Score;
//            }
//            catch (MobileServiceInvalidOperationException msioe)
//            {
//                Debug.WriteLine(@"GetGameHighscoreAsync sync operation: {0}", msioe.Message);
//            }
//            catch (Exception e)
//            {
//                Debug.WriteLine(@"GetGameHighscoreAsync error: {0}", e.Message);
//            }
//            return 0;
//        }

//        public async Task<Scores> GetScoreItemByNameAsync(string name, GameType gameType, GameMode mode)
//        {
//            if (string.IsNullOrWhiteSpace(name))
//                return null;

//            await Initialize();

//            try
//            {
//                await SyncAsync();

//                return (await todoTable.Where(a => a.GameType == (int)gameType && a.GameSubType == (int)mode && a.Name.ToLower() == name.Trim()).Take(1).ToEnumerableAsync()).FirstOrDefault();
//            }
//            catch (MobileServiceInvalidOperationException msioe)
//            {
//                Debug.WriteLine(@"GetScoreItemByNameAsync sync operation: {0}", msioe.Message);
//            }
//            catch (Exception e)
//            {
//                Debug.WriteLine(@"GetScoreItemByNameAsync error: {0}", e.Message);
//            }
//            return null;
//        }

//        public async Task<ObservableCollection<Scores>> GetTodoItemsAsync(bool syncItems = false)
//        {
//            await Initialize();
//            try
//            {
//#if OFFLINE_SYNC_ENABLED
//                if (syncItems)
//                {
//                    await this.SyncAsync();
//                }
//#endif
//                IEnumerable<Scores> items = await todoTable
//                    //.Where(scoreItem => !scoreItem.)
//                    .ToEnumerableAsync();

//                return new ObservableCollection<Scores>(items);
//            }
//            catch (MobileServiceInvalidOperationException msioe)
//            {
//                Debug.WriteLine(@"Invalid sync operation: {0}", msioe.Message);
//            }
//            catch (Exception e)
//            {
//                Debug.WriteLine(@"Sync error: {0}", e.Message);
//            }
//            return null;
//        }

//        internal async Task SaveScore(string yourName, int score, GameType gameType, GameMode mode, int averageTime)
//        {
//            await Initialize();

//            try
//            {
//                var item = await GetScoreItemByNameAsync(yourName, gameType, mode);
//                if (item == null)
//                {
//                    item = new Scores { Name = yourName, Score = score, GameType = (int)gameType, GameSubType = (int)mode, OSType = (int)Device.OS, AverageTime = averageTime };
//                    await SaveTaskAsync(item);
//                }
//                else
//                {
//                    //set update
//                    if (item.Score < score)
//                    {
//                        item.Score = score;
//                        item.AverageTime = averageTime;
//                        await SaveTaskAsync(item);
//                    }
//                }
//            }
//            catch (MobileServiceInvalidOperationException msioe)
//            {
//                Debug.WriteLine(@"SaveScore sync operation: {0}", msioe.Message);
//            }
//            catch (Exception e)
//            {
//                Debug.WriteLine(@"SaveScore error: {0}", e.Message);
//            }
//        }

//        public async Task SaveTaskAsync(Scores item)
//        {
//            if (item.Id == null)
//            {
//                await todoTable.InsertAsync(item);
//            }
//            else
//            {
//                await todoTable.UpdateAsync(item);
//            }
//        }

//#if OFFLINE_SYNC_ENABLED
//        public async Task SyncAsync()
//        {
//            await Initialize();

//           // ReadOnlyCollection<MobileServiceTableOperationError> syncErrors = null;

//            try
//            {
//                await this.client.SyncContext.PushAsync();

//                await this.todoTable.PullAsync(
//                    //The first parameter is a query name that is used internally by the client SDK to implement incremental sync.
//                    //Use a different query name for each unique query in your program
//                    "allScoreItems",
//                    this.todoTable.CreateQuery());
//            }
//            catch (Exception exc)
//            {
//                Debug.WriteLine("SyncAsync();" + exc.Message);
//                //if (exc.PushResult != null)
//                //{
//                //    syncErrors = exc.PushResult.Errors;
//                //}
//            }

//            // Simple error/conflict handling. A real application would handle the various errors like network conditions,
//            // server conflicts and others via the IMobileServiceSyncHandler.
//            //if (syncErrors != null)
//            //{
//            //    foreach (var error in syncErrors)
//            //    {
//            //        if (error.OperationKind == MobileServiceTableOperationKind.Update && error.Result != null)
//            //        {
//            //            //Update failed, reverting to server's copy.
//            //            await error.CancelAndUpdateItemAsync(error.Result);
//            //        }
//            //        else
//            //        {
//            //            // Discard local change.
//            //            await error.CancelAndDiscardItemAsync();
//            //        }

//            //        Debug.WriteLine(@"Error executing sync operation. Item: {0} ({1}). Operation discarded.", error.TableName, error.Item["id"]);
//            //    }
//            //}
//        }
//#endif
//    }
//}
