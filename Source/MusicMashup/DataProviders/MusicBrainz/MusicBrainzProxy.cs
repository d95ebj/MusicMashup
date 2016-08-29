using System;
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
                if (((HttpWebResponse) e.Response)?.StatusCode == HttpStatusCode.NotFound)
                {
                    throw new MusicBrainzException(MusicBrainzException.Error.NotFound);
                }
                if (((HttpWebResponse)e.Response)?.StatusCode == HttpStatusCode.BadRequest)
                {
                    throw new MusicBrainzException(MusicBrainzException.Error.BadRequest);
                }
                throw;
            }
        }

        public async Task<IEnumerable<ReleaseGroup>> GetReleaseGroups(string mbid)
        {
            var musicData = await GetMusicData(mbid, "release-groups");
            return musicData.ReleaseGroups;
        }

        public async Task<IEnumerable<Relation>> GetUrlRelations(string mbid)
        {
            var musicData = await GetMusicData(mbid, "url-rels");
            return musicData.Relations;
        }
    }

   
    public class MusicBrainzException : Exception
    {
        public Error Code { get; }
        public MusicBrainzException(Error code)
        {
            Code = code;
        }

        public MusicBrainzException(string message, Error code) : base(message)
        {
            Code = code;
        }

        public enum Error
        {
            NotFound,
            BadRequest
        }
    }

   

}