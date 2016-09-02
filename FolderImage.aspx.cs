using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace PhotoGalerie
{
    public partial class FolderImagePage : Page
    {
        private const long ImgDefaultQuality = 80;

        private string GetFolderPath()
        {
            string folderParam = Request.QueryString.Get("folder");
            string pageFolder = FolderHelper.GetFolderPath(Config.BaseFolder, folderParam).Path;

            return pageFolder;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            int width = int.Parse(Request.QueryString.Get("w"));
            int height = int.Parse(Request.QueryString.Get("h"));
            int itemPadding = 4;

            int itemWidth = (width - 3 * itemPadding) / 2;
            int itemHeight = (height - 3 * itemPadding) / 2;

            string folder = GetFolderPath();
            string[] imagesPath = GetImages(folder, 4);

            Bitmap[] images = imagesPath.Select(i => ImageHelper.ResizeCrop(i, itemWidth, itemHeight)).ToArray();

            using (Bitmap result = new Bitmap(width, height, PixelFormat.Format32bppArgb))
            using (Graphics gr = Graphics.FromImage(result))
            {
                //gr.Clear(Color.White);
                //result.MakeTransparent(Color.White);

                if (images.Length > 0) gr.DrawImage(images[0], itemPadding, itemPadding);
                if (images.Length > 1) gr.DrawImage(images[1], itemWidth + 2 * itemPadding, itemPadding);
                if (images.Length > 2) gr.DrawImage(images[2], itemPadding, itemHeight + 2 * itemPadding);
                if (images.Length > 3) gr.DrawImage(images[3], itemWidth + 2 * itemPadding, itemHeight + 2 * itemPadding);

                foreach (var img in images)
                    img.Dispose();

                byte[] imgData = ImageHelper.SaveToPng(result);

                Response.ContentType = "image/png";
                Response.Cache.SetExpires(DateTime.UtcNow.AddHours(24));
                Response.OutputStream.Write(imgData, 0, imgData.Length);
            }
        }

        private string[] GetImages(string folder, int limit)
        {
            DirectoryInfo di = new DirectoryInfo(folder);

            return di.EnumerateFiles()
                .Where(f => Config.PhotoExtensions.Contains(f.Extension.Substring(1).ToLower()))
                .Select(f => f.FullName)
                .Take(limit).ToArray();
        }
    }
}