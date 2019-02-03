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
using Spotify.ApiObjectModels;

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
            return session != null ? (IHttpActionResult) Ok(session.player.GetAllPlaylists()) : NotFound();
        }

        [HttpGet]
        public IHttpActionResult Get(string sessionId, string id)
        {
            IPlaylist playlist = null;
            Session session = CommonUtil.GetSession(sessions, sessionId);
            if (session != null)
            {
                playlist = session.player.GetPlaylistById(id);
            }

            return playlist != null ? (IHttpActionResult) Ok(playlist) : NotFound();
        }

        [HttpPost]
        public IHttpActionResult Post(string sessionId, [FromBody] string name)
        {
            IPlaylist playlist = null;
            IPlayer player = null;
            Session currentSession = CommonUtil.GetSession(sessions, sessionId);
            if(currentSession != null)
            {
                //SpotifyPlaylist spotifyPlaylist = CreateSpotifyPlaylist(currentSession.spotifyCredentials.accessToken, currentSession.spotifyUser.id, name);
                player = currentSession.player;
                playlist = player.CreateNewPlaylist(name);
            }
            
            return playlist != null ? (IHttpActionResult) Ok(playlist) : NotFound();
        }

        [HttpPut]
        public IHttpActionResult Put(string sessionId, string playlistId, string trackUri, string name, string artist, string submitter)
        {
            // add track to onboarding track list
            bool trackAdded = false;
            Session currentSession = CommonUtil.GetSession(sessions, sessionId);
            if(currentSession != null)
            {
                User currentUser = currentSession.GetUser(submitter);
                if(currentUser != null)
                {
                    trackAdded = currentSession.AddTrackToOnboardingList(currentUser, playlistId, trackUri, name, artist);
                }
            }
            
            return trackAdded ? (IHttpActionResult) Ok() : NotFound();
        }

        [HttpPut]
        public IHttpActionResult Put(string sessionId, string playlistId, string trackUri, int rating, string username)
        {
            // update track vote in onboarding track list
            bool trackUpdated = false;
            Session currentSession = CommonUtil.GetSession(sessions, sessionId);
            if (currentSession != null)
            {
                User user = currentSession.GetUser(username);
                trackUpdated = currentSession.UpdateTrackVote(user, playlistId, trackUri, rating);

            }

            return trackUpdated ? (IHttpActionResult) Ok() : NotFound();
        }


    }
}
