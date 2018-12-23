using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace MusicTry3.Models
{
    public class Playlist
    {
        string spotifyBaseApi = "https://api.spotify.com/v1/";

        public SpotifyPlaylist spotifyPlaylist { get; set; }
        // make priority queue
        public List<OnBoardingSong> onBoardingSongs { get; set; }
        public Thread onBoardingSelector { get; set; }
        public bool running { get; set; }
        public SpotifyCredentials credentials { get; set; }

        public Playlist(SpotifyCredentials credentials, SpotifyPlaylist spotifyPlaylist)
        {
            onBoardingSongs = new List<OnBoardingSong> ();
            this.credentials = credentials;
            this.spotifyPlaylist = spotifyPlaylist;
            running = true;
            Thread onBoardingSelector = new Thread(() => {
                while(running)
                {
                    SpotifyPlaybackContext playbackContext;
                    if (credentials != null)
                    {
                        playbackContext = GetPlaybackContext(credentials.accessToken);
                        UpdateSpotifyPlaylist();
                        if (this.spotifyPlaylist != null && (this.spotifyPlaylist.tracks.items.Count == 0 || (playbackContext != null && (playbackContext.is_playing && playbackContext.progress_ms != null && playbackContext.item != null && AtLeastThingManySongsInPlaylistQueue(1, playbackContext, this.spotifyPlaylist)))))
                        {
                            OnBoardingSong highestVotedSong = GetHighestVotedSong(onBoardingSongs);
                            if(highestVotedSong != null)
                            {
                                AddTrackToPlaylist(spotifyPlaylist.id, credentials.accessToken, highestVotedSong.trackUri);
                            }
                        }
                    }
                    Thread.Sleep(1000);
                }
            });
            onBoardingSelector.Start();
        }

        // returns true if there are less unplayedSongs in queue than bufferSongs, false if equal to or more than buffer songs or the current song played isn't in playlist
        private bool AtLeastThingManySongsInPlaylistQueue(int bufferSongs, SpotifyPlaybackContext playbackContext, SpotifyPlaylist spotifyPlaylist)
        {
            var i = 0;
            while(i < spotifyPlaylist.tracks.items.Count && spotifyPlaylist.tracks.items[i].track.uri != playbackContext.item.uri)
            {
                i++;
            }

            var trackFound = (i < spotifyPlaylist.tracks.items.Count);
            
            return (trackFound && (spotifyPlaylist.tracks.items.Count - i - 1 < bufferSongs));
        }

        private OnBoardingSong GetHighestVotedSong(List<OnBoardingSong> onBoardingSongs)
        {
            // update currently just first
            OnBoardingSong highestVotedSong = null;
            if (onBoardingSongs != null && onBoardingSongs.Count > 0)
            {
                // should make onboardingsongs a priority queue
                highestVotedSong = onBoardingSongs.OrderByDescending(x => x.GetAverageRating()).ToList()[0];
                onBoardingSongs.Remove(highestVotedSong);
            }

            return highestVotedSong;
        }

        private void AddTrackToPlaylist(string playlistId, string authorizationToken, string trackUri)
        {
            var client = new RestClient(spotifyBaseApi + "playlists/" + playlistId + "/");
            var request = new RestRequest("tracks", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddHeader("Authorization", "Bearer " + authorizationToken);
            request.AddHeader("Content-type", "application/json");
            SpotifyTrackAdditionRequest trackAddition = new SpotifyTrackAdditionRequest();
            trackAddition.uris = new List<string>();
            trackAddition.uris.Add(trackUri);
            request.AddBody(trackAddition);
            IRestResponse response = client.Execute(request);
        }

        private SpotifyPlaybackContext GetPlaybackContext(string authorizationToken)
        {
            var client = new RestClient(spotifyBaseApi + "me/");
            var request = new RestRequest("player", Method.GET);
            request.RequestFormat = DataFormat.Json;
            request.AddHeader("Authorization", "Bearer " + authorizationToken);
            IRestResponse response = client.Execute(request);

            return JsonConvert.DeserializeObject<SpotifyPlaybackContext>(response.Content);
        }


        public void UpdateSpotifyPlaylist()
        {
            var client = new RestClient(spotifyBaseApi + "playlists/");
            var request = new RestRequest(spotifyPlaylist.id, Method.GET);
            request.RequestFormat = DataFormat.Json;
            request.AddHeader("Authorization", "Bearer " + credentials.accessToken);
            IRestResponse response = client.Execute(request);

            spotifyPlaylist = JsonConvert.DeserializeObject<SpotifyPlaylist>(response.Content, new JsonSerializerSettings
            {
                MissingMemberHandling = MissingMemberHandling.Ignore
            });
        }
    }
}