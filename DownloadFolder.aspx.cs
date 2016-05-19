using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;
using System.Text;

namespace PhotoGalerie
{
    public partial class DownloadFolderPage : System.Web.UI.Page
    {
        private ILog Logger = LogManager.GetLogger("PhotoGalerie.DownloadFolderPage");

        /// <summary>
        /// Set header: 
        /// </summary>
        private void ProcessContentType()
        {
            string contentType = "application/zip";
            Response.ContentType = contentType;
        }

        private FolderInfo GetFolderInfo()
        {
            string folderParam = Request.QueryString.Get("folder");

            return FolderHelper.GetFolderPath(Config.BaseFolder, folderParam);
        }
        /// <summary>
        /// Show save as dialog.
        /// Add header: [Content-Disposition]
        /// </summary>
        private void ProcessDownloadMode(string fileName)
        {
            fileName = fileName.Replace(' ', '_');
            Response.AppendHeader("Content-Disposition", "attachment; filename*=UTF-8''" + HttpUtility.UrlEncode(fileName));
        }

        private void ProcessCacheMode()
        {
            Response.Cache.SetExpires(DateTime.UtcNow.AddYears(1));
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            LogRequest();

            var foldeInfo = GetFolderInfo();
            string fileName = foldeInfo.Folders.Count>0? foldeInfo.Folders.Last().Title + ".zip" : "main.zip";

            ProcessContentType();
            ProcessCacheMode();
            ProcessDownloadMode(fileName);

            SendFiles(foldeInfo.Path);
        }

        private void LogRequest()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Url: " + Request.Url);
            Logger.Debug(sb);
        }

        private void SendFiles(string folder)
        {
            List<FileDesc> files = FolderHelper.GetFiles(folder);
            
            using (ZipArchive arc = new ZipArchive(new PositionWrapperStream(Response.OutputStream), ZipArchiveMode.Create, true))
                foreach (var file in files)
                    arc.CreateEntryFromFile(file.Path, file.Name, CompressionLevel.NoCompression);
        }
    }
}