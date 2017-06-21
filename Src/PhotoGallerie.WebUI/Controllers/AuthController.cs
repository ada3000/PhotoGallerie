using PhotoGalerie.Code;
using PhotoGalerie.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Security;

namespace PhotoGalerie.Controllers
{
    public class AuthController : ApiController
    {
        const string MdSolt = "PG2106AD$!_";

        [HttpPost]
        public HttpResponseMessage Login(AuthModel model)
        {

            var hash = GetMd5Hash(MdSolt + model.Pwd);
            if (hash != "xxx")
            {
                return Request.CreateResponse(
                    HttpStatusCode.OK, CommonResponse.Error
                );
            }

            var resp = Request.CreateResponse(
                    HttpStatusCode.OK, CommonResponse.Success
                );

            var authTicket = new FormsAuthenticationTicket(
                1,                             // version
                model.Login,                      // user name
                DateTime.Now,                  // created
                DateTime.Now.AddMinutes(20),   // expires
                false,                    // persistent?
                UserHelper.UserRoleName                        // can be used to store roles
                );

            string encryptedTicket = FormsAuthentication.Encrypt(authTicket);

            resp.Headers.AddCookies(new[] { new CookieHeaderValue(FormsAuthentication.FormsCookieName, encryptedTicket) });
            
            return resp;
        }
        
        [HttpGet]
        public CommonResponse Logout()
        {
            FormsAuthentication.SignOut();

            return CommonResponse.Success;
        }

        static string GetMd5Hash(string input)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

                StringBuilder sBuilder = new StringBuilder();

                for (int i = 0; i < data.Length; i++)
                    sBuilder.Append(data[i].ToString("x2"));

                return sBuilder.ToString();
            }
        }
    }
}
