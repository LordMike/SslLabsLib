using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SslLabsLib.Objects;
using SslLabsLib.Tests.Helpers;

namespace SslLabsLib.Tests
{
    [TestClass]
    public class StatusCodeTests
    {
        [TestMethod]
        public void StatusCodeTest()
        {
            SslLabsClient client = new SslLabsClient();

            StatusCodes codes = client.GetStatusCodes();
            Assert.IsNotNull(codes);

            TestHelpers.EnsureAllPropertiesSet(codes);

            Assert.IsTrue(codes.StatusDetails.Any());
        }
    }
}