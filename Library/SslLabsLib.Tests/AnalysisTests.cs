using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using SslLabsLib.Enums;
using SslLabsLib.Objects;
using SslLabsLib.Tests.Helpers;

namespace SslLabsLib.Tests
{
    [TestFixture]
    public class AnalysisTests
    {
        [Test]
        public void GeneralTest()
        {
            SslLabsClient client = new SslLabsClient();
            Host analysis = client.GetAnalysisBlocking("scotthelme.co.uk", options: AnalyzeOptions.ReturnAll);

            Assert.IsNotNull(analysis);
            Assert.AreEqual(AnalysisStatus.READY, analysis.Status, "scotthelme.co.uk analysis was not ready. Wait for the analysis to complete.");

            TestHelpers.EnsureAllPropertiesSet(analysis, nameof(Host.StatusMessage));

            Assert.IsTrue(analysis.Endpoints.Any());

            Endpoint endpoint = analysis.Endpoints.First();
            TestHelpers.EnsureAllPropertiesSet(endpoint, nameof(Endpoint.StatusDetails), nameof(Endpoint.StatusDetailsMessage));

            EndpointDetails details = endpoint.Details;
            TestHelpers.EnsureAllPropertiesSet(details, nameof(EndpointDetails.StaplingRevocationErrorMessage), nameof(EndpointDetails.HttpForwarding));
        }

        [Test]
        public void HstsTest()
        {
            SslLabsClient client = new SslLabsClient();
            Host analysis = client.GetAnalysisBlocking("scotthelme.co.uk", options: AnalyzeOptions.ReturnAll);

            Assert.IsNotNull(analysis);
            Assert.AreEqual(AnalysisStatus.READY, analysis.Status, "scotthelme.co.uk analysis was not ready. Wait for the analysis to complete.");

            Endpoint endpoint = analysis.Endpoints.First();

            HstsPolicy hstsPolicy = endpoint.Details.HstsPolicy;
            Assert.IsTrue(hstsPolicy.MaxAge > 0);
            Assert.IsTrue(hstsPolicy.Preload);
            Assert.IsTrue(hstsPolicy.IncludeSubDomains);
            Assert.AreEqual(HstsStatus.Present, hstsPolicy.Status);

            List<HstsPreload> hstsPreloads = endpoint.Details.HstsPreloads;
            Assert.IsTrue(hstsPreloads.Any(s => s.Source == "Chrome"));
        }

        [Test]
        public void HpkpTest()
        {
            SslLabsClient client = new SslLabsClient();
            Host analysis = client.GetAnalysisBlocking("scotthelme.co.uk", options: AnalyzeOptions.ReturnAll);

            Assert.IsNotNull(analysis);
            Assert.AreEqual(AnalysisStatus.READY, analysis.Status, "scotthelme.co.uk analysis was not ready. Wait for the analysis to complete.");

            Endpoint endpoint = analysis.Endpoints.First();

            HpkpPolicy hpkpPolicy = endpoint.Details.HpkpPolicy;
            TestHelpers.EnsureAllPropertiesSet(hpkpPolicy, nameof(HpkpPolicy.Error));
            Assert.IsTrue(hpkpPolicy.MaxAge > 0);
            Assert.IsTrue(hpkpPolicy.IncludeSubDomains);
            Assert.AreEqual(HpkpStatus.Valid, hpkpPolicy.Status);

            Assert.IsTrue(hpkpPolicy.Pins.Any());
            Assert.IsTrue(hpkpPolicy.MatchedPins.Any());
        }
    }
}