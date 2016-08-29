using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using MusicMashup.DataProviders.MusicBrainz;
using MusicMashup.Models;
using MusicMashup.Services;

namespace MusicMashup.Controllers
{
    public class MashupController : ApiController
    {
        private readonly MashupService _service;

        public MashupController(MashupService service)
        { 
            _service = service;
        }

        // GET: api/Mashup/5
        public async Task<MashupMusicData> Get(string id)
        {
            try
            {
                return await _service.GetMashupData(id);
            }
             
            catch (MusicBrainzException ex)  // todo: Maybe use filters instead
            {
                switch (ex.Code)
                {
                    case MusicBrainzException.Error.NotFound:
                        throw new HttpResponseException(HttpStatusCode.NotFound);
                    case MusicBrainzException.Error.BadRequest:
                        throw new HttpResponseException(HttpStatusCode.BadRequest);
                    default:
                        throw new HttpResponseException(HttpStatusCode.InternalServerError);
                }
            }
            catch (Exception)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        } 
    }
}
