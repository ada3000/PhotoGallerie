using PhotoGalerie.Code;
using PhotoGalerie.Helpers;
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
        [HttpGet]
        public CommonResponse Logout()
        {
            FormsAuthentication.SignOut();

            return CommonResponse.Success;
        }        
    }
}
