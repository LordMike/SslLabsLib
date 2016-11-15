using NUnit.Framework;
using SslLabsLib.Objects;
using SslLabsLib.Tests.Helpers;

namespace SslLabsLib.Tests
{
    [TestFixture]
    public class InfoTests
    {
        [Test]
        public void InfoTest()
        {
            SslLabsClient client = new SslLabsClient();

            Info info = client.GetInfo();
            Assert.IsNotNull(info);

            TestHelpers.EnsureAllPropertiesSet(info);
        }
    }
}
