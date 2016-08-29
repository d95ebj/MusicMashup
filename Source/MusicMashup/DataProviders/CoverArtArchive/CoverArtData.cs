using Newtonsoft.Json;

namespace MusicMashup.DataProviders.CoverArtArchive
{
    public class CoverArtData
    {
        [JsonProperty(PropertyName = "image")]
        public string ImageUrl { get; set; }

        [JsonProperty(PropertyName = "types")]
        public string[] Types { get; set; }
    }
}