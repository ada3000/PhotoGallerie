<%@ Page Title="Contact" Language="C#" AutoEventWireup="true" Debug="true" %>
<%@ Import Namespace="System" %>
<%@ Import Namespace="System.Drawing" %>
<%@ Import Namespace="System.Drawing.Imaging" %>
<%@ Import Namespace="System.IO" %>

<%
    
    Response.ContentType = "image/jpeg";
    Response.Cache.SetExpires(DateTime.UtcNow.AddHours(24));
    var imgQuality = 60L;
    byte[] buff = new byte[0x2000];
    
    
    var absPath = Server.MapPath("~");
    var filenameChain = Request.FilePath.Split('/');
    var pageFolder = absPath +"\\"+ string.Join("\\", filenameChain.Skip(2).Take(filenameChain.Count() - 3));

    //Response.Write(pageFolder);

    int width = int.Parse(Request.QueryString.Get("w"));
    int height = int.Parse(Request.QueryString.Get("h"));

    string file = Request.QueryString.Get("file");
    if (file.IndexOf("..") > -1) throw new InvalidDataException();

    string filePath = pageFolder + "\\" + file;

    
    //Response.OutputStream.Write(imgBuffer, 0, imgBuffer.Length); 

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

            int newHeight =(int)( width / aspect);
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
    
    //Response.Write(filePath);
    
    //FileInfo fileInfo = new FileInfo(imgName);
    
    //DateTime imgModifyDate = fileInfo.LastWriteTime;
    //byte[] imgBuffer = null;

    //if (Session["imgModifyDate"] == null
    //    || imgModifyDate != (DateTime)Session["imgModifyDate"])
    //{
    //    try
    //    {
    //        // MemoryCache.Default
    //        using (System.Drawing.Image img = System.Drawing.Image.FromFile(imgName))
    //        {
    //            using (Bitmap resizeImg = new Bitmap(img, imgSize))
    //            {
    //                using (MemoryStream ms = new MemoryStream(resizeImg.Width * resizeImg.Height * 4 + 1000))
    //                {
    //                    using (Graphics gr = Graphics.FromImage(resizeImg))
    //                    {
    //                        var dateString = imgModifyDate.ToString(dateFormat);
    //                        var w2 = resizeImg.Width/2;
							
    //                        for (int x = -1; x <= 1; x++)
    //                            for (int y = -1; y <= 1; y++)
    //                                gr.DrawString(dateString, font, new SolidBrush(fontBackColor), x, y);
							
    //                        gr.DrawString(dateString, font, new SolidBrush(fontForeColor), 0, 0);
							
    //                        for (int x = -1; x <= 1; x++)
    //                            for (int y = -1; y <= 1; y++)
    //                                gr.DrawString(title, font, new SolidBrush(fontBackColor), x+w2, y);
							
    //                        gr.DrawString(title, font, new SolidBrush(fontForeColor), w2, 0);
    //                    }

    //                    #region SaveImage

    //                    ImageCodecInfo jgpEncoder = null;

    //                    ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();

    //                    foreach (ImageCodecInfo codec in codecs)
    //                    {
    //                        if (codec.FormatID == ImageFormat.Jpeg.Guid)
    //                        {
    //                            jgpEncoder = codec;
    //                            break;
    //                        }
    //                    }

    //                    System.Drawing.Imaging.Encoder myEncoder = System.Drawing.Imaging.Encoder.Quality;
    //                    EncoderParameters myEncoderParameters = new EncoderParameters(1);

    //                    EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder, imgQuality);
    //                    myEncoderParameters.Param[0] = myEncoderParameter;

    //                    resizeImg.Save(ms, jgpEncoder, myEncoderParameters);

    //                    ms.Position = 0;
    //                    imgBuffer = new byte[ms.Length];
    //                    ms.Read(imgBuffer, 0, imgBuffer.Length);

    //                    #endregion
    //                }
    //            }
    //        }

    //        using (FileStream fs = new FileStream(imgNamePrev, FileMode.Create, FileAccess.Write))
    //            fs.Write(imgBuffer, 0, imgBuffer.Length);
    //    }
    //    catch (Exception ex)
    //    {
    //        if (File.Exists(imgNamePrev))
    //        {
    //            imgBuffer = File.ReadAllBytes(imgNamePrev);

    //            Response.Cache.SetLastModified(imgModifyDate);
    //        }
    //        else
    //            throw;
    //    }
    //}
    //else
    //{
    //    if (File.Exists(imgNamePrev))
    //    {
    //        imgBuffer = File.ReadAllBytes(imgNamePrev);
            
    //        Response.Cache.SetLastModified(imgModifyDate);
    //    }
    //}

    
    
    //Response.Write("<br/>");
    //Response.Write("imgBuffer.Length=" + imgBuffer.Length);
       %>