using PhotoGallerie.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

using Newtonsoft.Json;
using System.Web.Security;

namespace PhotoGalerie
{
    public partial class LoginPage : System.Web.UI.Page
    {
        public string ToolBarData = "[]";
        //private string Folder = @"D:\Фото\2015\(06) Июнь 10-11";
        protected void Page_Load(object sender, EventArgs e)
        {
            //var absPath = Server.MapPath("~");
            //var filenameChain = Request.FilePath.Split('/');
            //var pageFolder = absPath + "\\" + string.Join("\\", filenameChain.Skip(2).Take(filenameChain.Count() - 3));

            var pageFolder = Config.BaseFolder;
            //Response.Write(pageFolder);
            string folderParam = Request.QueryString.Get("folder") ?? "";

            Title = "";

            var folderInfo = new FolderInfo
            {
                Folders = new List<FolderInfo.Item> { new FolderInfo.Item { Id = "0", Title = "Login" } }
            };
            pageFolder = folderInfo.Path;

            Title = "Login";
            JavaScriptSerializer ser = new JavaScriptSerializer();
            ToolBarData = ser.Serialize(folderInfo.Folders);
        }

        private int UserId
        {
            get
            {
                var indentity = User.Identity as FormsIdentity;
                //var repo = new SimpleRepository<User>();
                var userInfo = JsonConvert.DeserializeObject<User>(indentity.Ticket.UserData);
                return userInfo.Id;
            }
        }

        private int UserRoleId
        {
            get
            {
                var repo = new SimpleRepository<UserRole>();
                return repo.All().First(r => r.UserId == UserId).RoleId;
            }
        }
    }
}