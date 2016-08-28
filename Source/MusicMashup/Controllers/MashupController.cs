using System.Threading.Tasks;
using System.Web.Http;
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
            return await _service.GetMashupData(id);
        }
       
    }
}
