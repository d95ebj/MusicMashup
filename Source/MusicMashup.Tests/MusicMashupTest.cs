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
        
        [TestMethod]
        public void MusicBrainzAlbumProxyTest()
        {
           var proxy = new MusicBrainzProxy();
            var data =  proxy.GetReleaseGroups("cc197bad-dc9c-440d-a5b5-d52ba2e14234").Result;
            Assert.IsNotNull(data);

        }

        [TestMethod]
        public void MusicBrainzUrlRelationsProxyTest()
        {
            var proxy = new MusicBrainzProxy();
            var data = proxy.GetUrlRelations("cc197bad-dc9c-440d-a5b5-d52ba2e14234").Result;
            Assert.IsNotNull(data);
        }

        [TestMethod]
        public void CoverArtProxyTest()
        {
            var proxy = new CoverArtArchiveProxy();
            var data = proxy.GetCoverArt("19e53fc4-38a3-4b43-a8c3-5086817d2739").Result;
            Assert.IsNotNull(data);
        }

        [TestMethod]
        public void WikipediaProxyTest()
        {
            var proxy = new WikipediaProxy();
            var data = proxy.GetDescription("Coldplay").Result;
            Assert.IsNotNull(data);
        }

        [TestMethod]
        public void GetWIkipediaTitleTest()
        {
            var provider = new MusicBrainzInformationProvider(new MusicBrainzProxy());
            var data = provider.GetWikipediaTitle("cc197bad-dc9c-440d-a5b5-d52ba2e14234").Result;
           
            Assert.IsNotNull(data == "http://en.wikipedia.org/wiki/Coldplay");
        }
    }
}
