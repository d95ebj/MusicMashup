using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using MusicMashup.DataProviders;
using MusicMashup.DataProviders.CoverArtArchive;
using MusicMashup.DataProviders.MusicBrainz;
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
            var mashup = new MashupMusicData {Mbid = mbid};

            //Make parallel calls to get albums with coverart and artist description  
            var albumTask = GetAlbums(mashup);
            var artistTask = GetArtistDescription(mashup);

            await Task.WhenAll(albumTask, artistTask);

            return mashup;

        }

        //Populates the MashupMusicData.Album property with a list of albums and their corresponding cover art
        private async Task GetAlbums(MashupMusicData mashUp)
        {
            mashUp.Albums = await _musicInfoProvider.GetAlbums(mashUp.Mbid);
            if (mashUp.Albums != null) 
                await _coverArtProvider.GetCoverArt(mashUp.Albums);
            
        }

        //Populates the MashupMusicData.ArtistDescription property 
        private async Task GetArtistDescription(MashupMusicData mashUp)
        {
            var wikiName = await _musicInfoProvider.GetWikipediaTitle(mashUp.Mbid);
            if (wikiName != null)
                mashUp.ArtistDescription = await _artistInfoProvider.GetArtistDescription(wikiName);
        }
    }
}