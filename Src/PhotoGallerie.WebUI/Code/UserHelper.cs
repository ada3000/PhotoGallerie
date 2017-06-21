using Newtonsoft.Json;
using PhotoGallerie.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Http;
using System.Web.Security;

namespace PhotoGalerie.Code
{
    public static class UserHelper
    {
        public static int GetUserId()
        {
            var indentity = HttpContext.Current.User.Identity as FormsIdentity;
            //var repo = new SimpleRepository<User>();
            var userInfo = JsonConvert.DeserializeObject<User>(indentity.Ticket.UserData);
            return userInfo.Id;
        }

        public static void SetUser(int id, string name, string role)
        {
            FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
                1, name, DateTime.Now,
                DateTime.Now.AddMinutes(20), false, "{\"Id\":" + id + "}");

            FormsIdentity fid = new FormsIdentity(authTicket);
            HttpContext.Current.User = new GenericPrincipal(fid, new[] { role });
        }

        public static int UserRoleId
        {
            get
            {
                var repo = new SimpleRepository<UserRole>();
                var userId = GetUserId();
                return repo.All().First(r => r.UserId == userId).RoleId;
            }
        }

        public static string UserRoleName
        {
            get
            {
                var repo = new SimpleRepository<Role>();
                var roleId = UserRoleId;
                return repo.Get(UserRoleId).Name;
            }
        }
    }
}