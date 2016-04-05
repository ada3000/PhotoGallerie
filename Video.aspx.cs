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

            //string rangeHeader = Request.Headers["Range"];

            //if (!string.IsNullOrEmpty(rangeHeader))
            //{
            //    long[] ranges = rangeHeader.Replace("bytes=","")
            //        .Split(new[] { '-' }, StringSplitOptions.RemoveEmptyEntries  )
            //        .Select(s=>long.Parse(s)).ToArray();
               
            //    using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            //    {
            //        if (ranges.Length > 0) fs.Seek(ranges[0], SeekOrigin.Begin);

            //        long maxLen = ranges.Length > 1 ? ranges[1]: fs.Length - ranges[0];
            //        long curLen = 0;

            //        int cnt = 0;
            //        while (curLen < maxLen && (cnt = fs.Read(buff, 0, buff.Length)) > 0)
            //        {
            //            curLen += cnt;
            //            if (curLen > maxLen) cnt -= (int)(curLen - maxLen);

            //            Response.OutputStream.Write(buff, 0, cnt);
            //        }
            //    }
            //}
            //else
            //{
            //    FileInfo fi = new FileInfo(filePath);
            //    Response.AppendHeader("Content-Length", fi.Length.ToString());
            //}

            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                int cnt = 0;
                while ((cnt = fs.Read(buff, 0, buff.Length)) > 0)
                    Response.OutputStream.Write(buff, 0, cnt);
            }
        }
    }
}