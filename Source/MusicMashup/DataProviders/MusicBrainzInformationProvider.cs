using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using MusicMashup.Models;
using Newtonsoft.Json;

namespace MusicMashup.DataProviders
{
    public interface IMusicInformationProvider
    {
        MashupMusicData GetMusicData(string mbid);
    }
    public class MusicBrainzInformationProvider : IMusicInformationProvider
    {
        private readonly IMusicBrainzProxy _proxy;

        public MusicBrainzInformationProvider(IMusicBrainzProxy proxy)
        {
            _proxy = proxy;
        }
        public MashupMusicData GetMusicData(string mbid)
        {
            var musicBrainzData = _proxy.GetMusicData(mbid);
            return MapToMashupMusicData(musicBrainzData);

        }

        private MashupMusicData MapToMashupMusicData(MusicBrainzData musicBrainzData)
        {
            var mashup = new MashupMusicData();

            mashup.Albums = musicBrainzData.ReleaseGroups.Where(rg => rg.Type == "Album").Select(rg => new Album { Title = rg.Title, Mbid = rg.Id }).ToList();

           return mashup;
        }
    }

   
}