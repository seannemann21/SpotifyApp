using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MusicTry3.Models
{
    public class Session
    {

        private static HashSet<int> existingSessionIds = new HashSet<int>();
        private static Random random = new Random();

        private static readonly int charsInId = 3;
        public static readonly int maxId = 17575;

        public String id { get; set; }
        public SpotifyCredentials spotifyCredentials { get; set; }
        public List<IPlaylist> playlists { get; set; }
        public List<User> users { get; set; }
        public SpotifyUser spotifyUser { get; set; }
        public DateTime lastContactWithMaster { get; set; }
        public String keepAliveToken { get; set; }
        public SpotifyTokenRefresher tokenRefresher {get; set;}

        public Session(SpotifyCredentials credentials, SpotifyUser spotifyUser)
        {
            this.spotifyCredentials = credentials;
            this.id = GenerateId(existingSessionIds, random);
            this.playlists = new List<IPlaylist>();
            this.users = new List<User>();
            this.spotifyUser = spotifyUser;
            this.lastContactWithMaster = DateTime.UtcNow;
            this.keepAliveToken = Guid.NewGuid().ToString();
            this.tokenRefresher = new SpotifyTokenRefresher(credentials);
            this.tokenRefresher.Start();
        }

        public static string GenerateId(HashSet<int> existingIds, Random rand)
        {
            if (existingIds.Count == (maxId + 1))
            {
                return "error";
            }
            int idAsNumber = rand.Next(maxId);
            while (existingIds.Contains(idAsNumber))
            {
                idAsNumber = (idAsNumber + 1) % maxId;
            }
            existingIds.Add(idAsNumber);
            string id = "";
            for(int i = 0; i < charsInId; i++)
            {
                var nextLetter = (char)((idAsNumber % 26) + 'A');
                id += nextLetter;
                idAsNumber = idAsNumber / 26;
            }

            return id;
        }

        private static int Power(int a, int b)
        {
            int result = 1;
            for(int i = 0; i < b; i++)
            {
                result *= a;
            }

            return result;
        }
    }
}