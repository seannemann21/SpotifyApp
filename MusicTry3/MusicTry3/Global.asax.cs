using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace MusicTry3
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
           
            RouteTable.Routes.MapPageRoute(
                "Pages",
                "home",
                "~/Front/landing.aspx"
            );

            RouteTable.Routes.MapPageRoute(
                "CreateUsrePage",
                "createuser",
                "~/Front/createUser.aspx"
            );

            RouteTable.Routes.MapPageRoute(
                "SessionPage",
                "session",
                "~/Front/session.aspx"
            );

            RouteTable.Routes.MapPageRoute(
                "PlaylistPage",
                "session/playlist",
                "~/Front/playlist.aspx"
            );
        }
    }
}
