using Microsoft.VisualStudio.TestTools.UnitTesting;
using SslLabsLib.Objects;
using SslLabsLib.Tests.Helpers;

namespace SslLabsLib.Tests
{
    [TestClass]
    public class InfoTests
    {
        [TestMethod]
        public void InfoTest()
        {
            SslLabsClient client = new SslLabsClient();

            Info info = client.GetInfo();
            Assert.IsNotNull(info);

            TestHelpers.EnsureAllPropertiesSet(info);
        }
    }
}
