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
        public static Thread onBoardingSelector { get; set; }
        public bool running { get; set; }
        public static Thread connectPlayback { get; set; }
        public bool playingThroughConnectAPI { get; set; }
        public int nextSongOffset { get; set; }
        public string deviceId { get; set; }
        public SpotifyCredentials credentials { get; set; }
        public bool isPaused { get; set; }

        public Playlist(SpotifyCredentials credentials, SpotifyPlaylist spotifyPlaylist)
        {
            onBoardingSongs = new List<OnBoardingSong> ();
            this.credentials = credentials;
            this.spotifyPlaylist = spotifyPlaylist;
            this.playingThroughConnectAPI = false;
            this.isPaused = false;
            this.nextSongOffset = 0;
            connectPlayback = new Thread(() =>
            {
                while (running)
                {
                    if(playingThroughConnectAPI)
                    {
                        SpotifyPlaybackContext playbackContext;
                        if (credentials != null)
                        {
                            playbackContext = GetPlaybackContext(credentials.accessToken);
                            if(playbackContext != null && this.isPaused)
                            {
                                this.isPaused = false;
                                ResumePlayback();
                            } else if (playbackContext == null || !playbackContext.is_playing)
                            {
                                UpdateSpotifyPlaylist();
                                // check to make sure there are enough songs
                                if(this.nextSongOffset < this.spotifyPlaylist.tracks.items.Count)
                                {
                                    PlayNextSong();
                                }
                            }
                        }
                    }
                    Thread.Sleep(2000);
                }
            });
            connectPlayback.Start();
            this.running = true;
            onBoardingSelector = new Thread(() => {
                while(running)
                {
                    SpotifyPlaybackContext playbackContext;
                    if (credentials != null)
                    {
                        playbackContext = GetPlaybackContext(credentials.accessToken);
                        UpdateSpotifyPlaylist();
                        // playbackContext.is_playing && playbackContext.progress_ms != null && playbackContext.item != null && 
                        if (this.spotifyPlaylist != null && (this.spotifyPlaylist.tracks.items.Count == 0 || (playbackContext != null && (!AtLeastThisManySongsInPlaylistQueue(1, playbackContext, this.spotifyPlaylist)))))
                        {
                            OnBoardingSong highestVotedSong = GetHighestVotedSong(onBoardingSongs);
                            if(highestVotedSong != null)
                            {
                                AddTrackToPlaylist(spotifyPlaylist.id, credentials.accessToken, highestVotedSong.trackUri);
                            }
                        }
                    }
                    Thread.Sleep(3000);
                }
            });
            onBoardingSelector.Start();
        }

        public void Next()
        {
            PausePlayback();
        }

        public void Pause()
        {
            this.playingThroughConnectAPI = false;
            this.isPaused = true;
            PausePlayback();
        }

        public void Play(string deviceId)
        {
            this.playingThroughConnectAPI = true;
            this.deviceId = deviceId;

        }

        private bool PausePlayback()
        {
            var successful = false;
            var client = new RestClient(spotifyBaseApi + "me/");
            var request = new RestRequest("player/pause", Method.PUT);
            request.RequestFormat = DataFormat.Json;
            request.AddHeader("Authorization", "Bearer " + this.credentials.accessToken);

            IRestResponse response = client.Execute(request);
            if (response.IsSuccessful)
            {
                successful = true;
            }

            return successful;
        }

        private bool ResumePlayback()
        {
            var successful = false;
            var client = new RestClient(spotifyBaseApi + "me/");
            var request = new RestRequest("player/play", Method.PUT);
            request.RequestFormat = DataFormat.Json;
            request.AddHeader("Authorization", "Bearer " + this.credentials.accessToken);

            IRestResponse response = client.Execute(request);
            if (response.IsSuccessful)
            {
                successful = true;
            }

            return successful;
        }

        private void PlayNextSong()
        {
            var client = new RestClient(spotifyBaseApi + "me/");
            var request = new RestRequest("player/play?device_id=" + deviceId, Method.PUT);
            request.RequestFormat = DataFormat.Json;
            request.AddHeader("Authorization", "Bearer " + this.credentials.accessToken);
            StartResumeRequestBody body = new StartResumeRequestBody();
            body.context_uri = this.spotifyPlaylist.uri;
            body.offset = new SpotifyOffset();
            body.offset.position = nextSongOffset;
            request.AddBody(body);

            IRestResponse response = client.Execute(request);
            if(response.IsSuccessful)
            {
                nextSongOffset++;
            }
        }

        private void Resume()
        {

        }

        // returns true if there are less unplayedSongs in queue than bufferSongs, false if equal to or more than buffer songs or the current song played isn't in playlist
        private bool AtLeastThisManySongsInPlaylistQueue(int bufferSongs, SpotifyPlaybackContext playbackContext, SpotifyPlaylist spotifyPlaylist)
        {
            var i = 0;
            while(i < spotifyPlaylist.tracks.items.Count && spotifyPlaylist.tracks.items[i].track.uri != playbackContext.item.uri)
            {
                i++;
            }

            var trackFound = (i < spotifyPlaylist.tracks.items.Count);
            
            return spotifyPlaylist.tracks.items.Count - i - 1 >= bufferSongs;
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
            if(response.IsSuccessful)
            {
                spotifyPlaylist = JsonConvert.DeserializeObject<SpotifyPlaylist>(response.Content, new JsonSerializerSettings
                {
                    MissingMemberHandling = MissingMemberHandling.Ignore
                });
            }
        }
    }
}