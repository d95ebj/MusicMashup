using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;

namespace MusicMashup.DataProviders.CoverArtArchive
{ 
    public interface ICoverArtArchiveProxy
    {
        Task<string> GetCoverArt(string mbid);
    }

    public class CoverArtArchiveProxy : ICoverArtArchiveProxy
    {

        public async Task<string> GetCoverArt(string mbid)
        {
            try
            {
                var url = "http://coverartarchive.org/release-group/" + mbid; //todo: move to config
                var images = new List<CoverArtData>(); 
                using (var client = new WebClient())
                {
                    var getStringTask = client.DownloadStringTaskAsync(url);
                    var jSonString = await getStringTask;
                    JsonConvert.PopulateObject(jSonString, new {images});
                }

                if (!images.Any())
                    return null;

                //return the url to the first image marked as Front or if no image is marked as Front the url to the first image.
                return (images.FirstOrDefault(i => i.Types.Contains("Front")) ?? images.First()).ImageUrl;
            }
            catch (WebException)
            {
                return null;
            }
        }
    }
}