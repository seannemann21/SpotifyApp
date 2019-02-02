﻿using MusicTry3.Models;
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
            IPlaylist playlist = CommonUtil.GetPlaylist(sessions, sessionId, playlistId);
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
            IPlaylist playlist = CommonUtil.GetPlaylist(sessions, sessionId, playlistId);
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
            IPlaylist playlist = CommonUtil.GetPlaylist(sessions, sessionId, playlistId);
            if (playlist != null)
            {
                playlist.Next();
            }

            return Ok();
        }


        [HttpGet]
        [Route("devices")]
        public IHttpActionResult Devices(string sessionId, string playlistId)
        {
            List<Device> restResponse = null;
            Session session = CommonUtil.GetSession(sessions, sessionId);
            if (session != null)
            {
                restResponse = GetDevices(session.spotifyCredentials.accessToken);
            }
            return restResponse != null ? (IHttpActionResult)Ok(restResponse) : NotFound();
        }

        private List<Device> GetDevices(string authorizationToken)
        {
            var client = new RestClient(Constants.Spotify.WebApiBase + "me/player");
            var request = new RestRequest("devices", Method.GET);
            request.RequestFormat = DataFormat.Json;
            request.AddHeader("Authorization", "Bearer " + authorizationToken);
            IRestResponse response = client.Execute(request);

            Devices result = JsonConvert.DeserializeObject<Devices>(response.Content);
            return result.devices;
        }

    }
}
