using System.Linq;
using NUnit.Framework;
using SslLabsLib.Objects;
using SslLabsLib.Tests.Helpers;

namespace SslLabsLib.Tests
{
    [TestFixture]
    public class StatusCodeTests
    {
        [Test]
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