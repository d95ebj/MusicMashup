using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MusicMashup.DataProviders.MusicBrainz;
using MusicMashup.Models;

namespace MusicMashup.DataProviders
{
    public interface IMusicInformationProvider
    {
        Task<IEnumerable<Album>> GetAlbums(string mbid);
        Task<string> GetWikipediaTitle(string mbid);
    }
    public class MusicBrainzInformationProvider : IMusicInformationProvider
    {
        private readonly IMusicBrainzProxy _proxy;

        public MusicBrainzInformationProvider(IMusicBrainzProxy proxy)
        {
            _proxy = proxy;
        }

        //Gets a list of all albums connected to the mbid
        public async Task<IEnumerable<Album>> GetAlbums(string mbid)
        {
            var releaseGroups = await _proxy.GetReleaseGroups(mbid);
            return releaseGroups.Where(rg => rg.Type == "Album")
                    .Select(rg => new Album {Title = rg.Title, Mbid = rg.Id})
                    .ToList();
        }

        //Gets the string that can be used to get information from wikipedia correponding to the mbid
        public async Task<string> GetWikipediaTitle(string mbid)
        {
            var urlRelations = await _proxy.GetUrlRelations(mbid);
            var resource = urlRelations.FirstOrDefault(r => r.Type == "wikipedia")?.Url.Resource;
            
            if(resource != null)
            {
                var index = resource.LastIndexOf('/');
                return resource.Substring(index + 1);
            }
            return null;
        }

    }
  
}