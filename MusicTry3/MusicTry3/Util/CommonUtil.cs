using MusicTry3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MusicTry3.Util
{
    public class CommonUtil
    {
        public static Session GetSession(List<Session> sessions, string id)
        {
            return sessions.Find(x => x.id.Equals(id, StringComparison.InvariantCultureIgnoreCase));
        }

        public static Playlist GetPlaylist(List<Session> sessions, string sessionId, string playlistId)
        {
            Playlist playlist = null;
            Session session = sessions.Find(x => x.id.Equals(sessionId, StringComparison.InvariantCultureIgnoreCase));
            if(session != null)
            {
                playlist = session.playlists.Find(x => x.spotifyPlaylist.id == playlistId);
            }

            return playlist;
        }

        public static string Base64Encode(string text)
        {
            var textBytes = System.Text.Encoding.UTF8.GetBytes(text);
            return System.Convert.ToBase64String(textBytes);
        }
    }
}