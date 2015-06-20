using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PhotoGalerie
{
    public partial class IndexPage : System.Web.UI.Page
    {
        private string imgPreviewTemplate = "image.aspx?w=160&h=120&file={0}&folder={1}";
        private string imgShowTemplate = "image.aspx?w=1024&h=768&file={0}&folder={1}";
        private string imgFullTemplate = "image.aspx?w=0&h=0&file={0}&folder={1}";

        //private string Folder = @"D:\Фото\2015\(06) Июнь 10-11";
        protected void Page_Load(object sender, EventArgs e)
        {
            //var absPath = Server.MapPath("~");
            //var filenameChain = Request.FilePath.Split('/');
            //var pageFolder = absPath + "\\" + string.Join("\\", filenameChain.Skip(2).Take(filenameChain.Count() - 3));

            var pageFolder = Config.BaseFolder;
            //Response.Write(pageFolder);
            string folderParam = Request.QueryString.Get("folder") ?? "";

            if (folderParam != "")
            {
                pageFolder = FolderHelper.GetFolderPath(Config.BaseFolder, folderParam);
                var folderParams = folderParam.Split(',');

                AddFolder("..", string.Join(",", folderParams.Take(folderParams.Length - 1)));
            }

            string[] displayFoldersRaw = Directory.GetDirectories(pageFolder);
            List<string> displayFolders = new List<string>(displayFoldersRaw);
            displayFolders.Sort();

            for (int i = 0; i < displayFolders.Count; i++)
            {
                string folder = displayFolders[i];
                string folderName = folder.Substring(folder.LastIndexOf("\\") + 1);
                //skip .sync folder
                if (folderName.IndexOf(".") == 0) continue;

                AddFolder(folderName, folderParam + (folderParam != "" ? "," : "") + i);
            }

            string[] displayImagesRaw = Directory.GetFiles(pageFolder);

            List<string> displayImages = new List<string>(displayImagesRaw);
            displayImages.Sort();

            foreach (string s in displayImages)
            {
                string fileName = s.Substring(s.LastIndexOf("\\") + 1);
                string fileNameL = fileName.ToLower();
                if (fileNameL.IndexOf(".jpg") == -1 && fileNameL.IndexOf(".png") == -1) continue;

                AddImage(fileName, folderParam);
            }
        }

        private void AddImage(string fileName, string folderParam)
        {
            AddItem("", 
                fileName, 
                string.Format(imgPreviewTemplate, fileName, folderParam), 
                string.Format(imgShowTemplate, fileName, folderParam), 
                true,
                "image js-image");
        }

        private void AddFolder(string name, string id)
        {
            AddItem(name, name, @"Images/1.gif", "?folder=" + id, false, "folder");
        }

        private void AddItem(string name, string title, string previewImageUrl, string navigateUrl, bool newWindow, string cssClass = "")
        {
            Panel pan = new Panel { CssClass = "img " + cssClass };
            HyperLink link = new HyperLink { Target = newWindow ? "_blank" : "", NavigateUrl = navigateUrl, ToolTip = title };
            Image img = new Image { AlternateText = title, ImageUrl = previewImageUrl };
            Label lb = new Label { CssClass = "label", Text = name };

            pan.Controls.Add(link);
            link.Controls.Add(img);
            link.Controls.Add(lb);

            form1.Controls.Add(pan);
        }
    }
}