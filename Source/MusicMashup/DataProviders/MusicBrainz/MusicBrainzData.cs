using System.Collections.Generic;
using Newtonsoft.Json;

namespace MusicMashup.DataProviders.MusicBrainz
{
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