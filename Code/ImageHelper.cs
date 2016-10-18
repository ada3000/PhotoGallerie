using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;

namespace PhotoGalerie
{
    public static class ImageHelper
    {
        public static Bitmap ResizeCrop(string file, int width, int height)
        {
            using (Image img = Image.FromFile(file))
                return ResizeCrop(img, width, height);
        }
        public static Bitmap ResizeCrop(Image img, int width, int height)
        {
            double aspect = 1.0 * img.Width / img.Height;

            int tmpW = width;
            int tmpH = height;

            int drawX = 0;
            int drawY = 0;

            if (aspect > 1)
            {
                tmpW = (int)(aspect * tmpH);
                drawX = (width - tmpW) / 2;
            }
            else
            {
                tmpH = (int)(tmpW / aspect);
                drawY = (height - tmpH) / 2;
            }

            Bitmap result = new Bitmap(width, height);

            using (Bitmap tmpImage = new Bitmap(img, tmpW, tmpH))
            using (Graphics gr = Graphics.FromImage(result))
                gr.DrawImage(tmpImage, drawX, drawY);

            return result;
        }

        public static Bitmap ResizeFill(Image img, int width, int height)
        {
            double aspect = 1.0 * img.Width / img.Height;

            int newHeight = (int)(width / aspect);
            int newWidth = (int)(height * aspect);

            if (newHeight > height)
                width = newWidth;
            else
                height = newHeight;

            var imgSize = new Size(width, height);

            return new Bitmap(img, imgSize);
        }

        public static byte[] SaveToJpeg(Bitmap image, long quality)
        {
            using (MemoryStream ms = new MemoryStream(image.Width * image.Height * 4 + 1000))
            {
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

                Encoder myEncoder = Encoder.Quality;
                EncoderParameters myEncoderParameters = new EncoderParameters(1);

                EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder, quality);
                myEncoderParameters.Param[0] = myEncoderParameter;

                image.Save(ms, jgpEncoder, myEncoderParameters);

                ms.Position = 0;
                byte[] imgBuffer = new byte[ms.Length];
                ms.Read(imgBuffer, 0, imgBuffer.Length);

                return imgBuffer;
            }
        }

        public static byte[] SaveToPng(Bitmap image)
        {
            using (MemoryStream ms = new MemoryStream(image.Width * image.Height * 4 + 1000))
            {
                image.Save(ms, ImageFormat.Png);

                ms.Position = 0;
                byte[] imgBuffer = new byte[ms.Length];
                ms.Read(imgBuffer, 0, imgBuffer.Length);

                return imgBuffer;
            }
        }
    }
}