using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MusicTry3.Models
{
    public class MockedSessionRepo : ISessionRepo
    {
        static List<Session> mockedSessions = new List<Session>();

        public List<Session> GetSessions()
        {
            /*
            Session session = new Session(new SpotifyCredentials("accessToken", "refreshToken", new List<string>()));
            session.id = "AAAA";
            Playlist playlist = new Playlist();
            playlist.id = "bbbb";
            playlist.name = "Sean's Bangers";
            playlist.songs = new List<Song>();
            playlist.onBoardingSongs = new List<OnBoardingSong>();
            playlist.songs.Add(new Song { name = "Temperature", artist = "Sean Paul" });
            playlist.songs.Add(new Song { name = "Before I Forget", artist = "Slipknot" });
            List<Vote> unbelieversVote = new List<Vote>();
            unbelieversVote.Add(new Vote { user = new User { name = "Sean" }, score = 5 });
            playlist.onBoardingSongs.Add(new OnBoardingSong { song = "Unbelievers", artist = "Vampire Weekend",  votes = unbelieversVote });
            session.playlists.Add(playlist);
            mockedSessions.Add(session);
            */
            return mockedSessions;
        }

    }
}