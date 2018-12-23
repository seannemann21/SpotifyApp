using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MusicTry3.Models
{
    public class SessionRepo : ISessionRepo
    {
        static List<Session> sessions;

        public SessionRepo()
        {
            if(sessions == null)
            {
                sessions = new List<Session>();
            }
        }

        public List<Session> GetSessions()
        {
            return sessions;
        }
    }
}