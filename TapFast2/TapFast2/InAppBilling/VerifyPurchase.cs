using Plugin.InAppBilling.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TapFast2.InAppBilling
{
    public class VerifyPurchases : IInAppBillingVerifyPurchase
    {
        const string key1 = @"MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAim/YsVOlbE1dxjnZ9mJusz2j1yJ7q4szpYXqr51zJcrMCo6KTXGAYYkDSX9IauxjdwtjcnesvKPSK5l6hZ4MgZ9iB6AOCphsLTYrm9Z84xsS6xvQLVy937RdMU89nf5wVn7/";
        const string key2 = @"5vkr7Ka4kNFDBNHVodpIO12qQxMv4K87LbSF5Zn1vQI/zJ/Jq2fNvwHzfow39+syBQItqcIyP0MCo3ucwaOz/HHQJUOui7t6pxDowVvCRPN/";
        const string key3 = @"RV5QwRYjYeV4ECbS0+F8U8mOksoiLq792hdzN4V1gmV3zX8gWEmlNNMzLzlxVE2whG3g4NRM0l9vOrJmL7X12xFuUONS8nOTred/bQIDAQAB";

        public Task<bool> VerifyPurchase(string signedData, string signature)
        {

#if __ANDROID__
            var key1Transform = Plugin.InAppBilling.InAppBillingImplementation.InAppBillingSecurity.TransformString(key1, 1);
            var key2Transform = Plugin.InAppBilling.InAppBillingImplementation.InAppBillingSecurity.TransformString(key2, 2);
            var key3Transform = Plugin.InAppBilling.InAppBillingImplementation.InAppBillingSecurity.TransformString(key3, 3);

            return Task.FromResult(Plugin.InAppBilling.InAppBillingImplementation.InAppBillingSecurity.VerifyPurchase(key1Transform + key2Transform + key3Transform, signedData, signature));
#else
            return Task.FromResult(true);
#endif
        }

    }
}
