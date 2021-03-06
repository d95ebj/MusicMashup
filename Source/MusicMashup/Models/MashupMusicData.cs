﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MusicMashup.Models
{
    public class MashupMusicData
    {
        public string Mbid { get; set; }
        public string ArtistDescription { get; set; }
        public IEnumerable<Album> Albums { get; set; }
        
    }

    public class Album
    {
        public string Title { get; set; }
        public string CoverArt { get; set; }
        public string Mbid { get; set; }
    }
}