using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MusicTry3.Models
{
    public class PlaybackContext
    {
        public string progress_ms { get; set; }
        public bool is_playing { get; set; }
        public Track item { get; set; }
        public Context context { get; set; }
    }
}