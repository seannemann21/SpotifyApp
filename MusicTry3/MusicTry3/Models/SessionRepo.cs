using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace MusicTry3.Models
{
    public class SessionRepo : ISessionRepo
    {
        static List<Session> sessions;
        static bool running;

        public SessionRepo()
        {
            if(sessions == null)
            {
                sessions = new List<Session>();
                running = true;
                Thread deadSessionRemover = new Thread(() => {
                    while (running)
                    {
                        DateTime now = DateTime.UtcNow;
                        sessions.RemoveAll(x => x.lastContactWithMaster.AddMinutes(1).Ticks < now.Ticks);
                        // 20 minutes
                        Thread.Sleep(20*60*1000);
                    }
                });
                deadSessionRemover.Start();
            }
        }

        public List<Session> GetSessions()
        {
            return sessions;
        }
    }
}