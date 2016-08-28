using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MusicMashup.DataProviders
{
    public interface IArtistInformationProvider
    {
        string GetArtistDescription(string artist);
    }
    public class WikipediaArtistInformationProvider : IArtistInformationProvider
    {
        public string GetArtistDescription(string artist)
        {
            throw new NotImplementedException();
        }
    }
}