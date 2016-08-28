using System.Collections.Generic;
using System.Net;
using MusicMashup.Models;
using Newtonsoft.Json;

namespace MusicMashup.DataProviders
{
    public interface IMusicBrainzProxy
    {
        MusicBrainzData GetMusicData(string mbid);
    }

    public class MusicBrainzProxy : IMusicBrainzProxy
    {
        public MusicBrainzData GetMusicData(string mbid)
        {
            var url = "http://musicbrainz.org/ws/2/artist/" + mbid + "?inc=release-groups"; //todo: move to config
            using (var client = new WebClient())
            {
                client.Headers.Add("user-agent", "MusicMashup (ebj@ljusberg.se)");
                client.Headers.Add("accept", "application/json");
                var jSonString = client.DownloadString(url);
                return JsonConvert.DeserializeObject<MusicBrainzData>(jSonString);
               
            }

        }

       
    }

    public class MusicBrainzData
    {
        [JsonProperty(PropertyName = "name")]
        public string ArtistName { get; set; }

        [JsonProperty(PropertyName = "release-groups")]
        public List<ReleaseGroup> ReleaseGroups { get; set; }


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
}