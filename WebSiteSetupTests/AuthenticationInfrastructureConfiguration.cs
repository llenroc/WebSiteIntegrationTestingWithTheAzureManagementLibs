
namespace WebSiteSetupTests
{
    /// <summary>
    /// Provides the basic data points needed to support AAD 
    /// authentication in an app making use of the 
    /// management libraries.
    /// </summary>
    public interface ITokenCredentialConfiguration
    {
        string GetTenantId();
        string GetClientId();
        string GetRedirectUrl();
        string GetSubscriptionId();
    }

    /// <summary>
    /// My personal configuration. :)
    /// </summary>
    public class MyPersonalConfiguration : ITokenCredentialConfiguration
    {
        public string GetTenantId()
        {
            return "YOUR AZURE AAD TENANT ID";
        }

        public string GetClientId()
        {
            return "YOUR AZURE AAD CLIENT ID";
        }

        public string GetRedirectUrl()
        {
            return "YOUR AZURE AAD REDIRECT URL";
        }

        public string GetSubscriptionId()
        {
            return "YOUR AZURE SUBSCRIPTION ID";
        }
    }
}
