using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Subscriptions;
using Microsoft.WindowsAzure.Subscriptions.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebSiteSetupTests
{
    public class SubscriptionDemo
    {
        public async static Task<SubscriptionListOperationResponse.Subscription>
            SelectSubscription(SubscriptionCloudCredentials credentials,
            string filter)
        {
            IEnumerable<SubscriptionListOperationResponse.Subscription> ret = null;

            using (var subscriptionClient = new SubscriptionClient(credentials))
            {
                var listSubscriptionResults =
                    await subscriptionClient.Subscriptions.ListAsync();

                var subscriptions = listSubscriptionResults.Subscriptions;

                ret = subscriptions;
            }

            return ret.First(x => x.SubscriptionName.Contains(filter));
        }
    }
}
