using Plugin.InAppBilling.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TapFast2.Services
{
    public interface IInAppPurchaseService
    {
        Task<PurchaseState> MakePurchaseAsync(string productID);

        Task<PurchaseState> CheckAppPurchaseStatus(string productID);


    }
}
