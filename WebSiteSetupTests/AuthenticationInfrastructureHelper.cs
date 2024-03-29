﻿using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.WindowsAzure;
using System;
using System.Threading;

namespace WebSiteSetupTests
{
    public class TokenCredentialHelper<T>
        where T : ITokenCredentialConfiguration, new()
    {
        static T configuration = new T();

        private static string GetAuthorizationHeader()
        {
            AuthenticationResult result = null;

            var context = new AuthenticationContext(
                string.Format("https://login.windows.net/{0}",
                    configuration.GetTenantId()));

            var thread = new Thread(() =>
            {
                result = context.AcquireToken(
                    clientId: configuration.GetClientId(),
                    redirectUri: new Uri(configuration.GetRedirectUrl()),
                    resource: "https://management.core.windows.net/",
                    promptBehavior: PromptBehavior.Auto);
            });

            thread.SetApartmentState(ApartmentState.STA);
            thread.Name = "AquireTokenThread";
            thread.Start();
            thread.Join();
            return result.CreateAuthorizationHeader().Substring("Bearer ".Length);
        }

        /// <summary>
        /// Hands back the credential. 
        /// 
        /// Credentials don't need to belong to a specific subscription
        /// a subscription needs to be accessed. In that case, the 
        /// AAD tenant & app need to be "blessed," or the app needs 
        /// to be accessing assets in the same subscription. 
        /// 
        /// Calling code can create a general-purpose credential, 
        /// mainly to be used to get a list of subscriptions.
        /// 
        /// Once the desired subscription is found, the token can be 
        /// re-used in conjunction with a subscription ID, to provide
        /// direct management access via the Azure API to manage
        /// assets that are under that same subscription.
        /// </summary>
        /// <param name="subscriptionId"></param>
        /// <returns></returns>
        public static TokenCloudCredentials GetCredentials(string subscriptionId = null)
        {
            var token = GetAuthorizationHeader();

            if (subscriptionId == null)
            {
                subscriptionId = configuration.GetSubscriptionId();

                if(!string.IsNullOrEmpty(subscriptionId))
                {
                    return new TokenCloudCredentials(subscriptionId, token);
                }

                return new TokenCloudCredentials(token);
            }
            else
                return new TokenCloudCredentials(subscriptionId, token);
        }
    }
}
