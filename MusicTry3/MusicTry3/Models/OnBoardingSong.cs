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
        public List<Vote> votes { get; set; }

        public double GetAverageRating()
        {
            double totalRating = votes.Sum(x => x.score);
            return totalRating / votes.Count;
        }

    }
}