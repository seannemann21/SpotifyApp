using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MusicTry3.Models
{
    public class StartResumeRequestBody
    {
        public string context_uri { get; set; }
        public SpotifyOffset offset { get; set; }
    }
}