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
    public partial class VideoPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.ContentType = "video/mp4";
            Response.Cache.SetExpires(DateTime.UtcNow.AddYears(1));

            bool isDownloadRequest = Request.QueryString.Get("download") == "true";

            string file = Request.QueryString.Get("file");
            string folderParam = Request.QueryString.Get("folder");

            if (file.IndexOf("..") > -1) throw new InvalidDataException();

            string pageFolder = FolderHelper.GetFolderPath(Config.BaseFolder, folderParam);
            string filePath = pageFolder + "\\" + file;

            byte[] buff = new byte[0x2000];

            //Show save as dialog
            if (isDownloadRequest)
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + file);

            string rangeHeader = Request.Headers["Range"];

            long seek = 0;
            long length = 0;
            bool isRangeMode = !string.IsNullOrEmpty(rangeHeader);

            if (isRangeMode)
            {
                Response.StatusCode = 206; //Partial content;
                long[] ranges = rangeHeader.Replace("bytes=", "")
                    .Split(new[] { '-' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(s => long.Parse(s)).ToArray();

                if (ranges.Length > 0) seek = ranges[0];
                if (ranges.Length > 1) length = ranges[1] - seek + 1;
            }

            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                fs.Seek(seek, SeekOrigin.Begin);

                long maxLen = length != 0 ? length : fs.Length - seek;
                long curLen = 0;

                Response.AppendHeader("Content-Length", maxLen.ToString());
                if (isRangeMode)
                    Response.AppendHeader("Content-Range", string.Format("bytes {0}-{1}/*", seek, seek + maxLen - 1));

                int cnt = 0;
                while (curLen < maxLen && (cnt = fs.Read(buff, 0, buff.Length)) > 0)
                {
                    curLen += cnt;
                    if (curLen > maxLen) cnt -= (int)(curLen - maxLen);

                    Response.OutputStream.Write(buff, 0, cnt);
                }
            }

            //using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            //{
            //    int cnt = 0;
            //    while ((cnt = fs.Read(buff, 0, buff.Length)) > 0)
            //        Response.OutputStream.Write(buff, 0, cnt);
            //}
        }
    }
}