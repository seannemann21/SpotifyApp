using MusicTry3.Constants;
using MusicTry3.Models;
using MusicTry3.Util;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MusicTry3.Controllers
{
    [RoutePrefix("api/session/{sessionId}/playlist/{action}")]
    public class PlaylistController : ApiController
    {

        public List<Session> sessions;

        public PlaylistController(ISessionRepo sessionRepo)
        {
            sessions = sessionRepo.GetSessions();
        }

        [HttpGet]
        public IHttpActionResult Get(string sessionId)
        {
            Session session = sessions.Find(x => x.id.Equals(sessionId, StringComparison.InvariantCultureIgnoreCase));
            return session != null ? (IHttpActionResult) Ok(session.playlists) : NotFound();
        }

        [HttpGet]
        public IHttpActionResult Get(string sessionId, string id)
        {
            IPlaylist playlist = null;
            Session session = CommonUtil.GetSession(sessions, sessionId);
            if (session != null)
            {
                playlist = session.playlists.Find(x => x.spotifyPlaylist.id == id);
                if(playlist != null)
                {
                    playlist.UpdateSpotifyPlaylist();
                }
            }

            return session != null ? (IHttpActionResult) Ok(playlist) : NotFound();
        }

        [HttpPost]
        public IHttpActionResult Post(string sessionId, [FromBody] string name)
        {
            IPlaylist playlist = null;
            Session currentSession = CommonUtil.GetSession(sessions, sessionId);
            if(currentSession != null)
            {
                SpotifyPlaylist spotifyPlaylist = CreateSpotifyPlaylist(currentSession.spotifyCredentials.accessToken, currentSession.spotifyUser.id, name);
                if(spotifyPlaylist != null)
                {
                    playlist = new Playlist(currentSession.spotifyCredentials, spotifyPlaylist);
                    currentSession.playlists.Add(playlist);
                }
            }
            
            return playlist != null ? (IHttpActionResult) Ok(playlist.spotifyPlaylist) : NotFound();
        }

        [HttpPut]
        public IHttpActionResult Put(string sessionId, string playlistId, string trackUri, string name, string artist, string submitter)
        {
            // add track to onboarding track list
            bool trackAdded = false;
            IPlaylist currentPlaylist = CommonUtil.GetPlaylist(sessions, sessionId, playlistId);
            Session currentSession = CommonUtil.GetSession(sessions, sessionId);
            if(currentSession != null)
            {
                if(currentSession.users != null)
                {
                    User currentUser = currentSession.users.Find(x => x.name == submitter);
                    if(currentUser != null)
                    {
                        if (currentPlaylist != null)
                        {
                            bool priority = currentUser.readyForFreePick;
                            currentPlaylist.onBoardingSongs.Add(new OnBoardingSong(artist, name, trackUri, priority, currentUser));
                            currentUser.readyForFreePick = false;
                            trackAdded = true;
                        }
                    }
                }
            }
            
            return trackAdded ? (IHttpActionResult) Ok() : NotFound();
        }

        [HttpPut]
        public IHttpActionResult Put(string sessionId, string playlistId, string trackUri, int rating, string username)
        {
            // update track vote in onboarding track list
            bool trackUpdated = false;
            IPlaylist currentPlaylist = CommonUtil.GetPlaylist(sessions, sessionId, playlistId);
            Session currentSession = CommonUtil.GetSession(sessions, sessionId);
            if (currentPlaylist != null)
            {
                OnBoardingSong track = currentPlaylist.onBoardingSongs.Find(x => x.trackUri == trackUri);
                if(track != null)
                {
                    // username is unique id
                    Vote vote = track.votes.Find(x => x.user.name == username);
                    if (vote == null)
                    {
                        vote = new Vote();
                        User currentUser = currentSession.users.Find(x => x.name == username);
                        if (currentUser != null)
                        {
                            vote.user = currentUser;
                        }
                        track.votes.Add(vote);
                    }
                    vote.score = rating;
                    trackUpdated = true;
                }
            }

            return trackUpdated ? (IHttpActionResult) Ok() : NotFound();
        }

        private SpotifyPlaylist CreateSpotifyPlaylist(string authorizationToken, string userId, string name)
        {
            var client = new RestClient(Spotify.WebApiBase + "users/" + userId + "/");
            var request = new RestRequest("playlists", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddHeader("Authorization", "Bearer " + authorizationToken);
            request.AddHeader("Content-type", "application/json");
            request.AddBody(new SpotifyPlaylistRequestBody { name = name});
            IRestResponse response = client.Execute(request);
            SpotifyPlaylist playlistResponse = null;
            if(response.IsSuccessful)
            {
                playlistResponse = JsonConvert.DeserializeObject<SpotifyPlaylist>(response.Content, new JsonSerializerSettings
                {
                    MissingMemberHandling = MissingMemberHandling.Ignore
                });
            }
            return playlistResponse;
        }

    }
}
