using MusicTry3.Models;
using MusicTry3.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MusicTry3.Controllers
{
    [RoutePrefix("api/session/{sessionId}/playlist/{playlistId}")]
    public class PlaybackController : ApiController
    {

        public List<Session> sessions;

        public PlaybackController(ISessionRepo sessionRepo)
        {
            sessions = sessionRepo.GetSessions();
        }

        [HttpPut]
        [Route("play")]
        public IHttpActionResult Play(string sessionId, string playlistId, string deviceId)
        {
            Playlist playlist = CommonUtil.GetPlaylist(sessions, sessionId, playlistId);
            if(playlist != null)
            {
                playlist.Play(deviceId);
            }

            return Ok();
        }

        [HttpPut]
        [Route("pause")]
        public IHttpActionResult Pause(string sessionId, string playlistId)
        {
            Playlist playlist = CommonUtil.GetPlaylist(sessions, sessionId, playlistId);
            if (playlist != null)
            {
                playlist.Pause();
            }

            return Ok();
        }

        [HttpPut]
        [Route("next")]
        public IHttpActionResult Next(string sessionId, string playlistId)
        {
            Playlist playlist = CommonUtil.GetPlaylist(sessions, sessionId, playlistId);
            if (playlist != null)
            {
                playlist.Next();
            }

            return Ok();
        }

    }
}
