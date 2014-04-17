using System.Net;
using AuthTestModel.Data;
using NUnit.Framework;
using ServiceStack;

namespace AuthTestIntegrationTests
{
    [TestFixture]
    public class SecuredServiceTest : TestBase
    {
        [Test]
        public void check_authenticated_get_requires_credentials()
        {
            var restClient = new JsonServiceClient(WebServerUrl);
            var error = Assert.Throws<WebServiceException>(() => restClient.Get<SecuredResponse>("/Secured/Ryan"));
            Assert.AreEqual("Unauthorized", error.Message);
            Assert.AreEqual((int)HttpStatusCode.Unauthorized, error.StatusCode);
        }

        [Test]
        public void do_authenticated_get()
        {
            var restClient = new JsonServiceClient {BaseUri = WebServerUrl, UserName = UserName, Password = UserPassword};
            var response = restClient.Get<SecuredResponse>("/Secured/SomeData1");
            Assert.IsNotNull(response);
            Assert.AreEqual("SomeData1", response.Result);
            Assert.AreEqual(UserName, response.UserName);
        }

        [Test]
        public void do_athenticated_post()
        {
            var restClient = new JsonServiceClient { BaseUri = WebServerUrl, UserName = UserName, Password = UserPassword };

            var request = new Secured { Data = "Bob" };
            var response = restClient.Post<SecuredResponse>("/Secured/", request);

            Assert.IsNotNull(response);
            Assert.AreEqual("Bob", response.Result);
            Assert.AreEqual(UserName, response.UserName);
        }
    }
}