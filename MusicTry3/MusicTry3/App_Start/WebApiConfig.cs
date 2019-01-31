using log4net.Config;
using MusicTry3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Unity;
using Unity.Lifetime;

namespace MusicTry3
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {

            XmlConfigurator.Configure();

            var container = new UnityContainer();
            container.RegisterType<ISessionRepo, SessionRepo>(new HierarchicalLifetimeManager());
            config.DependencyResolver = new UnityResolver(container);

            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { controller = "session", id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "DefaultApi2",
                routeTemplate: "api/session/{sessionId}/playlist/{id}",
                defaults: new { controller = "playlist", id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "Spotify",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { controller = "spotify", id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "Playback",
                routeTemplate: "api/session/{sesssionId}/playlist/{playlistId}/playback/{action}",
                defaults: new { controller = "playback"}
            );

        }
    }
}
