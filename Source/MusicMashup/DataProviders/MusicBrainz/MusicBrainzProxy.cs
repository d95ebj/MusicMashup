using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MusicMashup.DataProviders.MusicBrainz
{
    public interface IMusicBrainzProxy
    {
        Task<IEnumerable<ReleaseGroup>> GetReleaseGroups(string mbid);
       Task<IEnumerable<Relation>> GetUrlRelations(string mbid);
    }

    public class MusicBrainzProxy : IMusicBrainzProxy
    {
        private async Task<MusicBrainzData> GetMusicData(string mbid, string inc)
        {
            try
            {
                var url = "http://musicbrainz.org/ws/2/artist/" + mbid + "?inc=" + inc; //todo: move to config
                using (var client = new WebClient())
                {
                    client.Headers.Add("user-agent", "MusicMashup (ebj@ljusberg.se)");
                    client.Headers.Add("accept", "application/json");
                    var jSonString = await client.DownloadStringTaskAsync(url);
                    return JsonConvert.DeserializeObject<MusicBrainzData>(jSonString);
                }
            }
            catch (WebException e)
            {
                return null;
            }
        }

        public async Task<IEnumerable<ReleaseGroup>> GetReleaseGroups(string mbid)
        {
            var musicData = await GetMusicData(mbid, "release-groups");
            return musicData?.ReleaseGroups;
        }

        public async Task<IEnumerable<Relation>> GetUrlRelations(string mbid)
        {
            var musicData = await GetMusicData(mbid, "url-rels");
            return musicData?.Relations;
        }
    }

    public class MusicBrainzData
    {
        [JsonProperty(PropertyName = "name")]
        public string ArtistName { get; set; }

        [JsonProperty(PropertyName = "release-groups")]
        public List<ReleaseGroup> ReleaseGroups { get; set; }

        [JsonProperty(PropertyName = "relations")]
        public Relation[] Relations { get; set; }
    }

    public class ReleaseGroup
    {
        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "primary-type")]
        public string Type { get; set; }

    }

    public class Relation
    {
        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        [JsonProperty(PropertyName = "url")]
        public Url Url { get; set; }

    }

    public class Url
    {
        [JsonProperty(PropertyName = "resource")]
        public string Resource { get; set; }
    }
}