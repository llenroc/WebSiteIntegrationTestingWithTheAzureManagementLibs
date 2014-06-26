using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;

namespace WebSiteSetupTests
{
    [TestClass]
    public class SiteCreationTests
    {
        private string _welcomeHtmlTemplate = "<h2 class=\"welcomeMessage\">{0}</h2>";
        private string _descriptionHtmlTemplate = "<p class=\"description\">{0}</p>";
        private SiteCreator _siteCreator;

        [TestInitialize]
        public void Setup()
        {
            var webSitePath = @"YOUR SITE PATH HERE";

            _siteCreator = new SiteCreator();
            _siteCreator.Setup(webSitePath);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _siteCreator.Dispose();
        }

        [TestMethod]
        public void WebSiteDisplaysTheCorrectMessages()
        {
            var url = string.Format("http://{0}.azurewebsites.net",
                _siteCreator.RandomWebSiteName);

            var httpClient = new HttpClient();
            var html = httpClient.GetAsync(url).Result.Content.ReadAsStringAsync().Result;

            var welcomeMessage = string.Format(_welcomeHtmlTemplate,
                _siteCreator.WelcomeMessage);

            var description = string.Format(_descriptionHtmlTemplate,
                _siteCreator.Description);

            Assert.IsTrue(html.Contains(welcomeMessage));
            Assert.IsTrue(html.Contains(description));
        }
    }
}
