using log4net;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using Spotify.ApiObjectModels;

namespace MusicTry3.Models
{
    public class Playlist : IPlaylist
    {

        private static readonly ILog logger = LogManager.GetLogger("Playlist");
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
        public Credentials credentials { get; set; }
        public bool isPaused { get; set; }

        public Playlist(Credentials credentials, SpotifyPlaylist spotifyPlaylist)
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
                        PlaybackContext playbackContext;
                        if (credentials != null)
                        {
                            playbackContext = GetPlaybackContext(credentials.accessToken);
                            if(playbackContext != null && this.isPaused)
                            {
                                logger.Info("Resuming playback since user unpaused");
                                this.isPaused = false;
                                ResumePlayback();
                            } else if (playbackContext == null || !playbackContext.is_playing)
                            {
                                logger.Info("Starting playback or last song finished and playing next");
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
                    PlaybackContext playbackContext;
                    if (credentials != null)
                    {
                        playbackContext = GetPlaybackContext(credentials.accessToken);
                        UpdateSpotifyPlaylist();
                        // playbackContext.is_playing && playbackContext.progress_ms != null && playbackContext.item != null && 
                        if (this.spotifyPlaylist != null && !AtLeastThisManySongsInPlaylistQueue(1, playbackContext, this.spotifyPlaylist))
                        {
                            OnBoardingSong nextSong = SelectNextSong(onBoardingSongs);
                            if(nextSong != null)
                            {
                                AddTrackToPlaylist(spotifyPlaylist.id, credentials.accessToken, nextSong.trackUri);
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
            UpdateSpotifyPlaylist();
            if(nextSongOffset < spotifyPlaylist.tracks.items.Count)
            {
                logger.Info("Attempting to play song " + nextSongOffset);
                var uriToPlay = spotifyPlaylist.tracks.items[nextSongOffset].track.uri;
                var client = new RestClient(spotifyBaseApi + "me/");
                var request = new RestRequest("player/play?device_id=" + deviceId, Method.PUT);
                //var request = new RestRequest("player/play?device_id=" + deviceId, Method.PUT);
                request.RequestFormat = DataFormat.Json;
                request.AddHeader("Authorization", "Bearer " + this.credentials.accessToken);
                StartResumeRequestBody body = new StartResumeRequestBody();
                body.uris = new List<string>();
                body.uris.Add(uriToPlay);
                request.AddBody(body);

                IRestResponse response = client.Execute(request);
                if (response.IsSuccessful)
                {
                    logger.Info("Playing next song request succeeded for: " + nextSongOffset);
                    nextSongOffset++;
                    logger.Info("Next song offset incremented to " + nextSongOffset);
                }
            }
        }

        private void Resume()
        {

        }

        // returns true if there are more unplayedSongs in queue than bufferSongs, false if equal to or less than buffer songs or the current song played isn't in playlist
        private bool AtLeastThisManySongsInPlaylistQueue(int bufferSongs, PlaybackContext playbackContext, SpotifyPlaylist spotifyPlaylist)
        {
            return spotifyPlaylist.tracks.items.Count > (this.nextSongOffset + bufferSongs);
        }

        private OnBoardingSong SelectNextSong(List<OnBoardingSong> onBoardingSongs)
        {
            OnBoardingSong nextSong = onBoardingSongs.Find(x => x.priority == true);
            if(nextSong == null)
            {
                nextSong = GetHighestVotedSong(onBoardingSongs);
                if(nextSong != null)
                {
                    nextSong.submitter.SongSelected();
                }
            }

            if(nextSong != null)
            {
                onBoardingSongs.Remove(nextSong);
            }

            return nextSong;
        }

        private OnBoardingSong GetHighestVotedSong(List<OnBoardingSong> onBoardingSongs)
        {
            // update currently just first
            OnBoardingSong highestVotedSong = null;
            if (onBoardingSongs != null && onBoardingSongs.Count > 0)
            {
                // should make onboardingsongs a priority queue
                highestVotedSong = onBoardingSongs.OrderByDescending(x => x.GetAverageRating()).ToList()[0];
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
            TrackAdditionRequest trackAddition = new TrackAdditionRequest();
            trackAddition.uris = new List<string>();
            trackAddition.uris.Add(trackUri);
            request.AddBody(trackAddition);
            IRestResponse response = client.Execute(request);
        }

        private PlaybackContext GetPlaybackContext(string authorizationToken)
        {
            var client = new RestClient(spotifyBaseApi + "me/");
            var request = new RestRequest("player", Method.GET);
            request.RequestFormat = DataFormat.Json;
            request.AddHeader("Authorization", "Bearer " + authorizationToken);
            IRestResponse response = client.Execute(request);

            return JsonConvert.DeserializeObject<PlaybackContext>(response.Content);
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