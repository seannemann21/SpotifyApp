using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MusicTry3.Models
{
    public class OnBoardingSong
    {
        public string artist { get; set; }
        public string name { get; set; }
        public string trackUri { get; set; }
        public bool priority { get; set; }
        public User submitter { get; set; }
        public List<Vote> votes { get; set; }

        public OnBoardingSong(string artist, string name, string trackUri, bool priority, User submitter)
        {
            this.artist = artist;
            this.name = name;
            this.trackUri = trackUri;
            this.priority = priority;
            this.votes = new List<Vote>();
            this.submitter = submitter;
        }

        public double GetAverageRating()
        {
            double totalRating = votes.Sum(x => x.score);
            return totalRating / votes.Count;
        }

    }
}