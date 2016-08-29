using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MusicMashup.DataProviders.Wikipedia
{
    public interface IWikipediaProxy
    {
        Task<string> GetDescription(string title);
    }
    public class WikipediaProxy : IWikipediaProxy
    {
        public async Task<string> GetDescription(string title)
        {
            try
            {
                var url = "http://en.wikipedia.org/w/api.php?action=query&format=json&prop=extracts&exintro=1&titles=" + title; //todo: Move to config
                using (var client = new WebClient())
                {
                    var task = client.DownloadStringTaskAsync(url);
                    var jSonString = await task;
                    var jObject = JsonConvert.DeserializeObject<JObject>(jSonString);
                    var result =  jObject.SelectToken("query.pages.*.extract").Value<string>();
                    return result;

                }
            }
            catch (WebException)
            {
                return null;
            }
        }
    }


}