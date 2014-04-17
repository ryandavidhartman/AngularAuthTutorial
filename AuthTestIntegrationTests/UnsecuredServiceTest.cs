using AuthTestModel.Data;
using NUnit.Framework;
using ServiceStack;

namespace AuthTestIntegrationTests
{
    [TestFixture]
    public class UnsecuredServiceTest : TestBase
    {
        [Test]
        public void do_unsecured_get()
        {
            var restClient = new JsonServiceClient(WebServerUrl);
            var response = restClient.Get<UnsecuredResponse>("/Unsecured/SomeData1");
            Assert.IsNotNull(response);
            Assert.AreEqual("SomeData1", response.Result);
        }

        [Test]
        public void do_unsecured_post()
        {
            var restClient = new JsonServiceClient(WebServerUrl);

            var request = new Unsecured { Data = "Bob" };
            var response = restClient.Post<UnsecuredResponse>("/Unsecured/", request);

            Assert.IsNotNull(response);
            Assert.AreEqual("Bob", response.Result);
        }

        /*
        [Test]
        public void do_un*/
    }
}
