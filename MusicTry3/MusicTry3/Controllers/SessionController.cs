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
    [RoutePrefix("api/{controller}/{action}")]
    public class SessionController : ApiController
    {
        public List<Session> sessions;
        string grantType = "authorization_code";

        public SessionController(ISessionRepo sessionRepo)
        {
            sessions = sessionRepo.GetSessions();
        }

        public IHttpActionResult GetAll()
        {
            return Ok(sessions);
        }

        [HttpGet]
        public IHttpActionResult Get(string id)
        {
            Session session = sessions.Find(x => x.id.Equals(id, StringComparison.InvariantCultureIgnoreCase));
            return session != null ? (IHttpActionResult) Ok(session) : NotFound();
        }

        [HttpPost]
        public IHttpActionResult Create(string code)
        {
            Session session = null;
            var client = new RestClient(Spotify.AccountsBaseApi);
            var request = new RestRequest("token", Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("application/x-www-form-urlencoded", $"client_id={Spotify.ClientId}&client_secret={Spotify.ClientSecret}&grant_type={grantType}&code={code}&redirect_uri={Spotify.RedirectUri}", ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);
            if(response.IsSuccessful)
            {
                SpotifyTokenResponse responseBody = JsonConvert.DeserializeObject<SpotifyTokenResponse>(response.Content);
                if (responseBody.scope != null && responseBody.access_token != null)
                {
                    SpotifyCredentials credentials = new SpotifyCredentials(responseBody.access_token, responseBody.refresh_token, new List<string>(responseBody.scope.Split(' ')));
                    SpotifyUser spotifyUser = GetCurrentSpotifyUser(responseBody.access_token);
                    session = new Session(credentials, spotifyUser);
                    sessions.Add(session);
                }
            }
            
            return session != null ? (IHttpActionResult) Ok(session) : NotFound();
        }

        [HttpPut]
        public IHttpActionResult keepalive(string sessionId, string keepAlive)
        {
            bool keepAliveUpdated = false;
            Session session = CommonUtil.GetSession(sessions, sessionId);
            if(session != null)
            {
                if(session.keepAliveToken == keepAlive)
                {
                    session.lastContactWithMaster = DateTime.UtcNow;
                    keepAliveUpdated = true;
                }
            }
            return keepAliveUpdated ? (IHttpActionResult) Ok() : NotFound();
        }

        private SpotifyUser GetCurrentSpotifyUser(string authorizationToken)
        {
            var client = new RestClient(Spotify.WebApiBase);
            var request = new RestRequest("me");
            request.AddHeader("Authorization", "Bearer " + authorizationToken);
            IRestResponse response = client.Execute(request);
            SpotifyUser currentUser = null;
            if(response.IsSuccessful)
            {
                string content = response.Content;
                currentUser = JsonConvert.DeserializeObject<SpotifyUser>(content);
            }
            return currentUser;
        }

        [HttpPut]
        public void Createuser(string username, string sessionId)
        {
            // implement
        }
    }
}
