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
    public class SpotifyController : ApiController
    {

        public List<Session> sessions;

        public SpotifyController(ISessionRepo sessionRepo)
        {
            sessions = sessionRepo.GetSessions();
        }
        
        [HttpGet]
        public IHttpActionResult Search(string query, string sessionId)
        {
            List<Track> restResponse = null;
            Session session = CommonUtil.GetSession(sessions, sessionId);
            if(session != null)
            {
                restResponse = SearchSpotify(query, session.spotifyCredentials.accessToken);
            }
            return restResponse != null ? (IHttpActionResult) Ok(restResponse) : NotFound();
        }

        private List<Track> SearchSpotify(string query, string accessToken)
        {
            var client = new RestClient(Constants.Spotify.WebApiBase);
            var request = new RestRequest("search", Method.GET);
            request.AddHeader("Authorization", "Bearer " + accessToken);
            request.AddParameter("q", query);
            request.AddParameter("type", "track");
            IRestResponse response = client.Execute(request);
            TrackResponse trackResponse = new TrackResponse();
            if (response.IsSuccessful)
            {
                Dictionary<String, TrackResponse> searchResponse = JsonConvert.DeserializeObject<Dictionary<String, TrackResponse>>(response.Content);
                if (searchResponse.ContainsKey("tracks"))
                {
                    trackResponse = searchResponse["tracks"];
                }
            }
            if(trackResponse.items == null)
            {
                trackResponse.items = new List<Track>();
            }
            return trackResponse.items;
        }
    }
}
