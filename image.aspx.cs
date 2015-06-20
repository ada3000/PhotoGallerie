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
    public partial class ImagePage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.ContentType = "image/jpeg";
            Response.Cache.SetExpires(DateTime.UtcNow.AddHours(24));
            var imgQuality = 60L;
            byte[] buff = new byte[0x2000];

            int width = int.Parse(Request.QueryString.Get("w"));
            int height = int.Parse(Request.QueryString.Get("h"));

            string file = Request.QueryString.Get("file");
            string folderParam = Request.QueryString.Get("folder");

            if (file.IndexOf("..") > -1) throw new InvalidDataException();

            string pageFolder = FolderHelper.GetFolderPath(Config.BaseFolder, folderParam);
            string filePath = pageFolder + "\\" + file;

            if (width == 0 && height == 0)
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    int cnt = 0;
                    while ((cnt = fs.Read(buff, 0, buff.Length)) > 0)
                        Response.OutputStream.Write(buff, 0, cnt);
                }
            }
            else
            {
                using (System.Drawing.Image img = System.Drawing.Image.FromFile(filePath))
                {
                    double aspect = 1.0 * img.Width / img.Height;

                    int newHeight = (int)(width / aspect);
                    int newWidth = (int)(height * aspect);

                    if (newHeight > height)
                        width = newWidth;
                    else
                        height = newHeight;

                    var imgSize = new Size(width, height);

                    using (Bitmap resizeImg = new Bitmap(img, imgSize))
                    {
                        using (MemoryStream ms = new MemoryStream(resizeImg.Width * resizeImg.Height * 4 + 1000))
                        {
                            #region SaveImage

                            ImageCodecInfo jgpEncoder = null;

                            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();

                            foreach (ImageCodecInfo codec in codecs)
                            {
                                if (codec.FormatID == ImageFormat.Jpeg.Guid)
                                {
                                    jgpEncoder = codec;
                                    break;
                                }
                            }

                            System.Drawing.Imaging.Encoder myEncoder = System.Drawing.Imaging.Encoder.Quality;
                            EncoderParameters myEncoderParameters = new EncoderParameters(1);

                            EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder, imgQuality);
                            myEncoderParameters.Param[0] = myEncoderParameter;

                            resizeImg.Save(ms, jgpEncoder, myEncoderParameters);

                            ms.Position = 0;
                            byte[] imgBuffer = new byte[ms.Length];
                            ms.Read(imgBuffer, 0, imgBuffer.Length);

                            #endregion

                            Response.OutputStream.Write(imgBuffer, 0, imgBuffer.Length);
                        }
                    }
                }
            }
        }
    }
}