using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spotify.ApiObjectModels;

namespace MusicTry3.Models
{
    public interface IPlaylist
    {
        SpotifyPlaylist spotifyPlaylist { get; set; }
        List<OnBoardingSong> onBoardingSongs { get; set; }
        bool running { get; set; }
        bool playingThroughConnectAPI { get; set; }
        string deviceId { get; set; }
        bool isPaused { get; set; }
        void Next();
        void Pause();
        void Play(String deviceId);
        void UpdateSpotifyPlaylist();
    }
}
