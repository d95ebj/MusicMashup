using Microsoft.VisualStudio.TestTools.UnitTesting;
using MusicMashup.DataProviders;
using MusicMashup.DataProviders.CoverArtArchive;
using MusicMashup.DataProviders.MusicBrainz;
using MusicMashup.DataProviders.Wikipedia;

namespace MusicMashup.Tests
{
    [TestClass]
    public class MusicMashupTest
    {
        [TestInitialize]
        public void Initialize()
        {
            
        }
        [TestMethod]
        public void MusicBrainzTest()
        {
           var proxy = new MusicBrainzProxy();
            var data =  proxy.GetReleaseGroups("cc197bad-dc9c-440d-a5b5-d52ba2e14234").Result;
            Assert.IsNotNull(data);

        }

        [TestMethod]
        public void CoverArtTest()
        {
            var proxy = new CoverArtArchiveProxy();
            var data = proxy.GetCoverArt("76df3287-6cda-33eb-8e9a-044b5e15ffdd").Result;
            Assert.IsNotNull(data);
        }

        [TestMethod]
        public void WikipediaTest()
        {
            var proxy = new WikipediaProxy();
            var data = proxy.GetDescription("Coldplay").Result;
            Assert.IsNotNull(data);
        }
    }
}
