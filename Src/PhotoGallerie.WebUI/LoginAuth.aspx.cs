using PhotoGalerie.Code;
using PhotoGalerie.Helpers;
using PhotoGalerie.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PhotoGalerie
{
    public partial class LoginAuth : System.Web.UI.Page
    {
        public string Result = "{}";
        const string MdSolt = "PG2106AD$!_";
                
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.ContentType = "application/json";

            string action = Request.QueryString.Get("action") ?? "";

            if (action == "login")
                DoLogin();
            if (action == "logout")
                DoLogout();
        }

        private void DoLogin()
        {
            string login = Request.QueryString.Get("login") ?? "";
            string pwd = Request.QueryString.Get("pwd") ?? "";

            var hash = (MdSolt + pwd).ToMD5();
            var userId = UserHelper.GetUserId(login);
            var expectedHash = UserHelper.GetUserPwd(UserHelper.GetUserId(login));

            if (hash != expectedHash)
            {
                Result = CommonResponse.Error.ToJsonString();
                return;
            }

            var ticket = UserHelper.SetUser(userId, login, UserHelper.UserRoleName(UserHelper.UserRoleId(userId)));

            string encryptedTicket = FormsAuthentication.Encrypt(ticket);
            var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
            Response.Cookies.Add(authCookie);

            Result = CommonResponse.Success.ToJsonString();
        }

        private void DoLogout()
        {
            FormsAuthentication.SignOut();
            Result = CommonResponse.Success.ToJsonString();
        }
    }
}