using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PhotoGalerie
{
    public partial class DownloadPage : System.Web.UI.Page
    {
        private const int PartialContentHttpStatus = 206;
        /// <summary>
        /// Set header: 
        /// </summary>
        private void ProcessContentType()
        {
            string type = Request.QueryString.Get("type") ?? "d";
            string contentType = "application/x-data";

            switch (type)
            {
                case "i":
                    contentType = "image/jpeg";
                    break;
                case "v":
                    contentType = "video/mp4";
                    break;
            }

            Response.ContentType = contentType;
        }

        private string GetFilePath()
        {
            string file = Request.QueryString.Get("file");
            string folderParam = Request.QueryString.Get("folder");

            if (file.IndexOf("..") > -1) throw new InvalidDataException();

            string pageFolder = FolderHelper.GetFolderPath(Config.BaseFolder, folderParam).Path;
            string filePath = pageFolder + "\\" + file;

            return filePath;
        }

        private string GetFileName()
        {
            string file = Request.QueryString.Get("file");
            return file;
        }
        /// <summary>
        /// Show save as dialog.
        /// Add header: [Content-Disposition]
        /// </summary>
        private void ProcessDownloadMode()
        {
            if(Request.QueryString.Get("download") == "true")
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + GetFileName());
        }

        private void ProcessCacheMode()
        {
            Response.Cache.SetExpires(DateTime.UtcNow.AddYears(1));
        }
        protected void Page_Load(object sender, EventArgs e)
        {

            ProcessContentType();
            ProcessCacheMode();
            ProcessDownloadMode();

            SendFile();            
        }

        private void SendFile()
        {
            byte[] buff = new byte[0x2000];
            long startPosition = 0;
            string filePath = GetFilePath();

            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                long maxLen = ProcessContentLength(ref startPosition, fs.Length);
                fs.Seek(startPosition, SeekOrigin.Begin);

                long curLen = 0;
                int cnt = 0;

                while (curLen < maxLen && (cnt = fs.Read(buff, 0, buff.Length)) > 0)
                {
                    curLen += cnt;
                    if (curLen > maxLen) cnt -= (int)(curLen - maxLen);

                    Response.OutputStream.Write(buff, 0, cnt);
                }
            }
        }
        /// <summary>
        /// Modify startPosition for Rage request. Add headers: Content-Length, [Content-Range]
        /// </summary>
        /// <param name="startPosition"></param>
        /// <param name="actualLength"></param>
        /// <returns></returns>
        private long ProcessContentLength(ref long startPosition, long actualLength)
        {
            string rangeHeader = Request.Headers["Range"];

            if (string.IsNullOrEmpty(rangeHeader))
            {
                Response.AppendHeader("Content-Length", actualLength.ToString());
                return actualLength;
            }
            
            long[] ranges = rangeHeader.Replace("bytes=", "")
                .Split(new[] { '-' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(s => long.Parse(s)).ToArray();

            long length = 0;

            if (ranges.Length > 0) startPosition = ranges[0];
            if (ranges.Length > 1) length = ranges[1] - startPosition + 1;

            long maxLen = length != 0 ? length : actualLength - startPosition;

            Response.StatusCode = PartialContentHttpStatus; //Partial content;
            Response.AppendHeader("Content-Range", string.Format("bytes {0}-{1}/*", startPosition, startPosition + maxLen - 1));
            Response.AppendHeader("Content-Length", maxLen.ToString());

            return maxLen;
        }
    }
}