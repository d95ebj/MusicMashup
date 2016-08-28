using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using MusicMashup.DataProviders.Wikipedia;

namespace MusicMashup.DataProviders
{
    public interface IArtistInformationProvider
    {
        Task<string> GetArtistDescription(string artist);
    }
    public class WikipediaArtistInformationProvider : IArtistInformationProvider
    {
        private readonly IWikipediaProxy _proxy;

        public WikipediaArtistInformationProvider(IWikipediaProxy proxy)
        {
            _proxy = proxy;
        }
        public async Task<string> GetArtistDescription(string artist)
        {
            return await _proxy.GetDescription(artist);
        }
    }
}