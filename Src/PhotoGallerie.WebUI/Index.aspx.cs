using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PhotoGalerie
{
    public partial class IndexPage : System.Web.UI.Page
    {
        private const string ParentFolderName = "";
        private string imgPreviewTemplate = "image.aspx?w=217&h=217&file={0}&folder={1}&mode=crop";
        private string imgShowHdTemplate = "image.aspx?w=1230&h=768&file={0}&folder={1}&mode=fill";
        private string imgShowFullHdTemplate = "image.aspx?w=1600&h=1000&file={0}&folder={1}&mode=fill";
        private string imgFullTemplate = "Download.aspx?type=i&file={0}&folder={1}&download=true";

        private string folderImgTemplate = "FolderImage.aspx?w=217&h=217&&folder={0}";

        private string EmptyIcon = @"Images/1.gif";

        private string videoPriviewUrl = "Images/video.png";
        private string videoShowTemplate = "Download.aspx?type=v&file={0}&folder={1}";
        private string videoFullTemplate = "Download.aspx?type=v&file={0}&folder={1}&download=true";

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

            if (folderParam != "")
            {
                var folderInfo = FolderHelper.GetFolderPath(Config.BaseFolder, folderParam);
                pageFolder = folderInfo.Path;
                AddFolder(ParentFolderName, folderInfo.Folders.Count > 1 ? folderInfo.Folders[folderInfo.Folders.Count - 2].Id : "");

                Title = folderInfo.Folders.Last().Title + " - ";
                JavaScriptSerializer ser = new JavaScriptSerializer();
                ToolBarData = ser.Serialize(folderInfo.Folders);
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

                AddFolder(folderName, folderParam + (folderParam != "" ? "," : "") + i, GetFilesCount(folder));
            }

            var files = FolderHelper.GetFiles(pageFolder);

            foreach (var file in files)
                AddItem(file, folderParam);

            InitDownloadButton(files, folderParam);
        }

        private int? GetFilesCount(string folderPath)
        {
            DirectoryInfo di = new DirectoryInfo(folderPath);
            int result = di.EnumerateFiles().Count() + di.EnumerateDirectories().Count();
            return result == 0 ? null : (int?)result;
        }

        private void InitDownloadButton(List<FileDesc> files, string folderParam)
        {
            if (files.Count == 0)
            {
                DownloadFolderButton.Visible = false;
                return;
            }

            DownloadFolderButton.Attributes["data-folder"] = folderParam;
        }

        private void AddItem(FileDesc file, string folderParam)
        {
            switch (file.Type)
            {
                case FileDescType.Photo:
                    AddImage(file.Name, folderParam);
                    break;
                case FileDescType.Video:
                    AddVideo(file.Name, folderParam);
                    break;
                case FileDescType.Data:
                    AddData(file.Name, folderParam);
                    break;
            }
        }

        private void AddData(string fileName, string folderParam)
        {
            //throw new NotImplementedException();
        }

        private void AddVideo(string fileName, string folderParam)
        {
            AddItem(
                NormalizeFileName(fileName),
                fileName,
                EmptyIcon,
                string.Format(videoShowTemplate, fileName, folderParam),
                string.Format(videoFullTemplate, fileName, folderParam),
                true,
                "video js-image js-video");
        }

        private void AddImage(string fileName, string folderParam)
        {
            AddItem("",
                NormalizeFileName(fileName),
                string.Format(imgPreviewTemplate, fileName, folderParam),
                string.Format(Request.Browser.IsMobileDevice ? imgShowHdTemplate : imgShowFullHdTemplate, fileName, folderParam),
                string.Format(imgFullTemplate, fileName, folderParam),
                true,
                "image js-image");
        }

        private void AddFolder(string name, string id, int? files = null)
        {
            bool isFolderBack = name == ParentFolderName;

            string navUrl = string.IsNullOrEmpty(id) ? "/" : "?folder=" + id;
            string cssClass = isFolderBack ? "folder-back" : "folder";
            string folderIconUrl = isFolderBack ? EmptyIcon : string.Format(folderImgTemplate, id);

            AddItem(NormalizeFolderName(name), name, folderIconUrl, navUrl, "", false, cssClass, files?.ToString());
        }

        private void AddItem(string name, string title, string previewImageUrl, string navigateUrl, string downloadUrl, bool newWindow, string cssClass = "", string topInfo = "")
        {
            Panel pan = new Panel { CssClass = "img " + cssClass };
            HyperLink link = new HyperLink { Target = newWindow ? "_blank" : "", NavigateUrl = navigateUrl, ToolTip = title };
            link.Attributes.Add("download-url", downloadUrl);

            Image img = new Image { AlternateText = title, ImageUrl = previewImageUrl };

            Label lb = new Label { CssClass = "label", Text = name };

            pan.Controls.Add(link);
            link.Controls.Add(img);
            link.Controls.Add(lb);
            form1.Controls.Add(pan);

            if (!string.IsNullOrEmpty(topInfo))
            {
                Label lbTopInfo = new Label { CssClass = "top-info", Text = topInfo };
                pan.Controls.Add(lbTopInfo);
            }
        }

        private static string NormalizeFolderName(string folder)
        {
            return folder.Length > 5 && folder[0] == '(' && folder[3] == ')' ? folder.Substring(5) : folder;
        }

        private static string NormalizeFileName(string file)
        {
            int idx = file.LastIndexOf('.');
            return idx > -1 ? file.Substring(0, idx) : file;
        }
    }
}