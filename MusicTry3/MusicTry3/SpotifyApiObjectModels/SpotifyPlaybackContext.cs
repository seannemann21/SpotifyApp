using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MusicTry3.Models
{
    public class SpotifyPlaybackContext
    {
        public string progress_ms { get; set; }
        public bool is_playing { get; set; }
        public SpotifyTrack item { get; set; }
        public SpotifyContextObject context { get; set; }
    }
}