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
    public partial class ImagePage : Page
    {
        private const long ImgDefaultQuality = 80;

        private string GetFilePath()
        {
            string file = Request.QueryString.Get("file");
            string folderParam = Request.QueryString.Get("folder");

            if (file.IndexOf("..") > -1) throw new InvalidDataException();

            string pageFolder = FolderHelper.GetFolderPath(Config.BaseFolder, folderParam).Path;
            string filePath = pageFolder + "\\" + file;

            return filePath;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            int width = int.Parse(Request.QueryString.Get("w"));
            int height = int.Parse(Request.QueryString.Get("h"));

            string resizeMode = Request.QueryString.Get("mode") ?? "crop"; //fill | crop
            string filePath = GetFilePath();

            string imageKey = filePath + "_" + width + "_" + height + "_" + resizeMode;

            byte[] imgData = BinaryCache.Instance.Load(imageKey);

            if (imgData == null)
                using (Image img = Image.FromFile(filePath))
                {
                    Bitmap newImage = null;
                    if (resizeMode == "fill")
                        newImage = ImageHelper.ResizeFill(img, width, height);
                    else
                        newImage = ImageHelper.ResizeCrop(img, width, height);

                    using (newImage)
                    {
                        imgData = ImageHelper.SaveToJpeg(newImage, ImgDefaultQuality);
                        BinaryCache.Instance.Store(imageKey, imgData);
                    }
                }

            Response.ContentType = "image/jpeg";
            Response.Cache.SetExpires(DateTime.UtcNow.AddHours(24));
            Response.OutputStream.Write(imgData, 0, imgData.Length);
        }
    }
}