using Acr.UserDialogs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TapFast2.Helpers;
using Xamarin.Forms;
using TapFast2.InAppBilling;
using Plugin.InAppBilling.Abstractions;
using Plugin.InAppBilling;

namespace TapFast2.Services
{
    public class InAppPurchaseService : IInAppPurchaseService
    {
        public InAppPurchaseService()
        {

        }

        public async Task<PurchaseState> CheckAppPurchaseStatus(string productId)
        {
            var result = PurchaseState.Unknown;
            try
            {
                var connected = await CrossInAppBilling.Current.ConnectAsync();

                if (!connected)
                {
                    //Couldn't connect
                    //result = false;
                    await UserDialogs.Instance.AlertAsync("Please check your connection", "Can't connect");
                }

                //check purchases

                var purchases = await CrossInAppBilling.Current.GetPurchasesAsync(ItemType.InAppPurchase, new VerifyPurchases());

                if (purchases?.Any(p => p.ProductId == productId) ?? false)
                {
                    //Purchase restored
                    var purchase = purchases.Where(p => p.ProductId == productId).First();
                    result = purchase.State;
                    //Settings.PurchaseId = purchase.Id;
                    //Settings.PurchaseToken = purchase.PurchaseToken;
                }
                else
                {
                    //no purchases found
                   
                }
            }
            catch (Exception ex)
            {
                //Something has gone wrong
            }
            finally
            {
                await CrossInAppBilling.Current.DisconnectAsync();
            }
            return result;
        }

        public async Task<PurchaseState> MakePurchaseAsync(string productId)
        {
            var result = PurchaseState.Unknown;
            try
            {
                var connected = await CrossInAppBilling.Current.ConnectAsync();
                if (!connected)
                {
                    //Couldn't connect to billing
                    //result = false;
                    await UserDialogs.Instance.AlertAsync("Please check your connection", "Can't connect");
                }

                //try to purchase item
                var purchase = await CrossInAppBilling.Current.PurchaseAsync(productId, ItemType.InAppPurchase, "apppayload", new VerifyPurchases());
                if (purchase == null)
                {
                    //Not purchased
                    //result = false;
                    
                }
                else
                {
                    //Purchased!
                    result = purchase.State;
                    Settings.PurchaseStatus = (int)result;
                    Settings.PurchaseId = purchase.Id;
                    Settings.PurchaseToken = purchase.PurchaseToken;

                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("MakePurchaseAsync: " + ex.Message);
            }
            finally
            {
                //Disconnect, it is okay if we never connected, this will never throw an exception
                await CrossInAppBilling.Current.DisconnectAsync();
            }
            return result;
        }
    }
}
