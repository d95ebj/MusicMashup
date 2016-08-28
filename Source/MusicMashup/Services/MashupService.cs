using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using MusicMashup.DataProviders;
using MusicMashup.DataProviders.CoverArtArchive;
using MusicMashup.Models;

namespace MusicMashup.Services
{
    public class MashupService
    {
        private readonly IMusicInformationProvider _musicInfoProvider;
        private readonly ICoverArtInformationProvider _coverArtProvider;
        private readonly IArtistInformationProvider _artistInfoProvider;

        public MashupService(IMusicInformationProvider musicInfoProvider, ICoverArtInformationProvider coverArtProvider,
            IArtistInformationProvider artistInfoProvider)
        {
            _musicInfoProvider = musicInfoProvider;
            _coverArtProvider = coverArtProvider;
            _artistInfoProvider = artistInfoProvider;
        }
        public async Task<MashupMusicData> GetMashupData(string mbid)
        {
            var mashup = new MashupMusicData{Mbid = mbid};
            mashup.Albums = await _musicInfoProvider.GetAlbums(mbid);
        
           
           await _coverArtProvider.GetCoverArt(mashup.Albums);

            return mashup;
        }
    }
}