﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MusicTry3.Models
{
    public class SpotifyPlaylist
    {
        public string id { get; set; }
        public string name { get; set; }
        public SpotifyPlaylistTrackResponse tracks { get; set; }
        public string uri { get; set; }
    }
}