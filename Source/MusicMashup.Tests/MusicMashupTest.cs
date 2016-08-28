using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MusicMashup.Controllers;
using MusicMashup.DataProviders;
using MusicMashup.DataProviders.CoverArtArchive;
using MusicMashup.Models;
using MusicMashup.Services;
using NSubstitute;

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
           var provider = new MusicBrainzInformationProvider(new MusicBrainzProxy());
            var data = provider.GetMusicData("cc197bad-dc9c-440d-a5b5-d52ba2e14234");
            Assert.IsNotNull(data);

        }

        [TestMethod]
        public void CoverArtTest()
        {
            var proxy = new CoverArtArchiveProxy();
            var data = proxy.GetCoverArt("76df3287-6cda-33eb-8e9a-044b5e15ffdd");
            Assert.IsNotNull(data);

        }
    }
}
