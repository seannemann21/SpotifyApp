using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MusicTry3.Models
{
    public class SpotifyDevice
    {
        public string id { get; set; }
        public bool is_active { get; set; }
        public string name { get; set; }
        public string type { get; set; }
    }
}