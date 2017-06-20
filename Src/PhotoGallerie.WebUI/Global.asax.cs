using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
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
            FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
                1, "Guest", DateTime.Now, 
                DateTime.Now.AddMinutes(20), false, "{\"Id\":10}");

            FormsIdentity id = new FormsIdentity(authTicket);
            Context.User = new GenericPrincipal(id, new[] { "Guest" });
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