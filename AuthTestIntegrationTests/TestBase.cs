using System.Configuration;
using NUnit.Framework;

namespace AuthTestIntegrationTests
{
    [TestFixture]
    public class TestBase
    {
        public string WebServerUrl { get; set; }
        public const string AdminName = "Administrator";
        public const string AdminPassword = "bobafet12bobafet12";
        public const string UserName = "user";
        public const string UserPassword = "p@55word";

        [TestFixtureSetUp]
        public void set_up()
        {
            WebServerUrl = ConfigurationManager.AppSettings["WebServerUrl"];
        }
    }
}
