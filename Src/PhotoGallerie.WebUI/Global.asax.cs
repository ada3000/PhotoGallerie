using log4net;
using PhotoGalerie.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Http;
using System.Web.Security;
using System.Web.SessionState;

namespace PhotoGalerie
{
    public class Global : System.Web.HttpApplication
    {
        private static ILog Logger;

        static Global()
        {
            Logger = LogManager.GetLogger("1");
            Logger.Debug("22");
        }

        protected void Application_Start(object sender, EventArgs e)
        {
            GlobalConfiguration.Configure(RegisterWebApi);
        }

        public static void RegisterWebApi(HttpConfiguration config)
        {
            // TODO: Add any additional configuration code.

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "ApiWithActions",
                routeTemplate: "apia/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            // WebAPI when dealing with JSON & JavaScript!
            // Setup json serialization to serialize classes to camel (std. Json format)
            var formatter = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            formatter.SerializerSettings.ContractResolver =
                new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(Object sender, EventArgs e)
        {
            HttpCookie authCookie = Context.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie == null || authCookie.Value == "")
            {
                SetGuestPrincipial();
                return;
            }

            FormsAuthenticationTicket authTicket;
            try
            {
                authTicket = FormsAuthentication.Decrypt(authCookie.Value);
            }
            catch
            {
                SetGuestPrincipial();
            }
        }

        private void SetGuestPrincipial()
        {
            UserHelper.SetUser(10, "Guest", "Guest");
        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}