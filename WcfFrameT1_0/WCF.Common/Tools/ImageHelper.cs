using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing.Imaging;
using System.Drawing;
using System.Web;

namespace WCF.Common.Tools
{
    public class ImageHelper
    {
        
        /// <summary>
        /// 生成缩略图  -高质量 90
        /// </summary>
        /// <param name="originalImagePath">源图路径（物理路径）带文件名</param>
        /// <param name="thumbnailPath">缩略图路径（物理路径）带文件名</param>
        /// <param name="width">缩略图宽度</param>
        /// <param name="height">缩略图高度</param>
        /// <param name="mode">生成缩略图的方式</param>
        /// <param name="streamType">1支持文件流传stream</param>
        public static void MakeThumbnail(object originalImagePath, string thumbnailPath, int width, int height, string mode, int streamType, int quality = 85)
        {
            System.Drawing.Image originalImage = null;
            if (streamType == 1)
                originalImage = System.Drawing.Image.FromStream((Stream)originalImagePath);
            else
                originalImage = System.Drawing.Image.FromFile((string)originalImagePath);

            int towidth = width;
            int toheight = height;

            int x = 0;
            int y = 0;
            int ow = originalImage.Width;
            int oh = originalImage.Height;

            switch (mode)
            {
                case "HW"://指定高宽缩放（可能变形） 
                    break;
                case "W"://指定宽，高按比例 
                    toheight = originalImage.Height * width / originalImage.Width;
                    break;
                case "H"://指定高，宽按比例
                    towidth = originalImage.Width * height / originalImage.Height;
                    break;
                case "Cut"://指定高宽裁减（不变形） 
                    if ((double)originalImage.Width / (double)originalImage.Height > (double)towidth / (double)toheight)
                    {
                        oh = originalImage.Height;
                        ow = originalImage.Height * towidth / toheight;
                        y = 0;
                        x = (originalImage.Width - ow) / 2;
                    }
                    else
                    {
                        ow = originalImage.Width;
                        oh = originalImage.Width * height / towidth;
                        x = 0;
                        y = (originalImage.Height - oh) / 2;
                    }
                    break;
                default:
                    break;
            }

            //新建一个bmp图片
            System.Drawing.Image bitmap = new System.Drawing.Bitmap(towidth, toheight);

            //新建一个画板
            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bitmap);

            g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            //设置高质量插值法
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

            //设置高质量,低速度呈现平滑程度
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;


            //清空画布并以透明背景色填充
            g.Clear(System.Drawing.Color.Transparent);

            //在指定位置并且按指定大小绘制原图片的指定部分
            g.DrawImage(originalImage, new System.Drawing.Rectangle(0, 0, towidth, toheight),
            new System.Drawing.Rectangle(x, y, ow, oh),
            System.Drawing.GraphicsUnit.Pixel);

            g.Dispose();

            //以下代码为保存图片时，设置压缩质量  
            EncoderParameters ep = new EncoderParameters();
            long[] qy = new long[1];
            qy[0] = quality;//设置压缩的比例1-100   默认80
            EncoderParameter eParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, qy);
            ep.Param[0] = eParam;

            try
            {
                ImageCodecInfo[] arrayICI = ImageCodecInfo.GetImageEncoders();
                ImageCodecInfo jpegICIinfo = null;
                for (int i = 0; i < arrayICI.Length; i++)
                {
                    if (arrayICI[i].FormatDescription.Equals("JPEG"))
                    {
                        jpegICIinfo = arrayICI[i];
                        break;
                    }
                }
                if (jpegICIinfo != null)
                {
                    bitmap.Save(thumbnailPath, jpegICIinfo, ep);
                    //ob.Save(dFile, jpegICIinfo, ep);//dFile是压缩后的新路径  
                }
                else
                {
                    //以jpg格式保存缩略图
                    bitmap.Save(thumbnailPath, System.Drawing.Imaging.ImageFormat.Jpeg);
                }
            }
            catch (System.Exception e)
            {
                throw e;
            }
            finally
            {
                originalImage.Dispose();
                bitmap.Dispose();
            }
        }

        /// <summary>
        /// 获取图片中的各帧
        /// </summary>
        /// <param name="pPath">图片路径</param>
        /// <param name="pSavePath">保存路径</param>
        public void GetFrames(string pPath, string pSavedPath)
        {
            Image gif = Image.FromFile(pPath);
            FrameDimension fd = new FrameDimension(gif.FrameDimensionsList[0]);

            //获取帧数(gif图片可能包含多帧，其它格式图片一般仅一帧)
            int count = gif.GetFrameCount(fd);

            //以Jpeg格式保存各帧
            for (int i = 0; i < count; i++)
            {
                gif.SelectActiveFrame(fd, i);
                gif.Save(pSavedPath + "\\frame_" + i + ".jpg", ImageFormat.Jpeg);
            }
        }

        /**/
        /// <summary>
        /// 获取图片缩略图
        /// </summary>
        /// <param name="pPath">图片路径</param>
        /// <param name="pSavePath">保存路径</param>
        /// <param name="pWidth">缩略图宽度</param>
        /// <param name="pHeight">缩略图高度</param>
        /// <param name="pFormat">保存格式，通常可以是jpeg</param>
        public void GetSmaller(string pPath, string pSavedPath, int pWidth, int pHeight, string ext)
        {
            using (FileStream fs = new FileStream(pPath, FileMode.Open))
            {
                MakeSmallImg(fs, pSavedPath, pWidth, pHeight, ext);
            }

        }


        //按模版比例生成缩略图（以流的方式获取源文件）  
        //生成缩略图函数  
        //顺序参数：源图文件流、缩略图存放地址、模版宽、模版高  
        //注：缩略图大小控制在模版区域内  
        public static void MakeSmallImg(System.IO.Stream fromFileStream, string fileSaveUrl, System.Double templateWidth, System.Double templateHeight, string extension)
        {
            //从文件取得图片对象，并使用流中嵌入的颜色管理信息  
            System.Drawing.Image myImage = System.Drawing.Image.FromStream(fromFileStream, true);
            string[] allowExten = new string[] { ".jpg", ".jpeg", ".gif", ".png" };
            System.Collections.ArrayList extensionArray = new System.Collections.ArrayList();
            extensionArray.AddRange(allowExten);
            if (!extensionArray.Contains(extension.ToLower()))
            {
                HttpContext.Current.Response.Write("上传的图片格式不正确");
            }
            else
            {

                //缩略图宽、高  
                System.Double newWidth = myImage.Width, newHeight = myImage.Height;
                //宽大于模版的横图  
                if (myImage.Width > myImage.Height || myImage.Width == myImage.Height)
                {
                    if (myImage.Width > templateWidth)
                    {
                        //宽按模版，高按比例缩放  
                        newWidth = templateWidth;
                        newHeight = myImage.Height * (newWidth / myImage.Width);
                    }
                }
                //高大于模版的竖图  
                else
                {
                    if (myImage.Height > templateHeight)
                    {
                        //高按模版，宽按比例缩放  
                        newHeight = templateHeight;
                        newWidth = myImage.Width * (newHeight / myImage.Height);
                    }
                }

                //取得图片大小  
                System.Drawing.Size mySize = new Size((int)newWidth, (int)newHeight);
                //新建一个bmp图片  
                System.Drawing.Image bitmap = new System.Drawing.Bitmap(mySize.Width, mySize.Height);
                //新建一个画板  
                System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bitmap);
                //设置高质量插值法  
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
                //设置高质量,低速度呈现平滑程度  
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                //清空一下画布  
                g.Clear(Color.White);
                //在指定位置画图  
                g.DrawImage(myImage, new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height),
                new System.Drawing.Rectangle(0, 0, myImage.Width, myImage.Height),
                System.Drawing.GraphicsUnit.Pixel);
                g.Dispose();

                ///文字水印  
                System.Drawing.Graphics G = System.Drawing.Graphics.FromImage(bitmap);
                System.Drawing.Font f = new Font("Lucida Grande", 6);
                System.Drawing.Brush b = new SolidBrush(Color.Gray);
                G.DrawString("", f, b, 0, 0);
                G.Dispose();

                ///图片水印  
                //System.Drawing.Image   copyImage   =   System.Drawing.Image.FromFile(System.Web.HttpContext.Current.Server.MapPath("pic/1.gif"));  
                //Graphics   a   =   Graphics.FromImage(bitmap);  
                //a.DrawImage(copyImage,   new   Rectangle(bitmap.Width-copyImage.Width,bitmap.Height-copyImage.Height,copyImage.Width,   copyImage.Height),0,0,   copyImage.Width,   copyImage.Height,   GraphicsUnit.Pixel);  

                //copyImage.Dispose();  
                //a.Dispose();  
                //copyImage.Dispose();  

                //保存缩略图  
                if (File.Exists(fileSaveUrl))
                {
                    File.SetAttributes(fileSaveUrl, FileAttributes.Normal);
                    File.Delete(fileSaveUrl);
                }

                //以下代码为保存图片时，设置压缩质量  
                EncoderParameters ep = new EncoderParameters();
                long[] qy = new long[1];
                qy[0] = 90;//设置压缩的比例1-100   默认90
                EncoderParameter eParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, qy);
                ep.Param[0] = eParam;
                try
                {
                    ImageCodecInfo[] arrayICI = ImageCodecInfo.GetImageEncoders();
                    ImageCodecInfo jpegICIinfo = null;
                    for (int x = 0; x < arrayICI.Length; x++)
                    {
                        if (arrayICI[x].FormatDescription.Equals("JPEG"))
                        {
                            jpegICIinfo = arrayICI[x];
                            break;
                        }
                    }
                    if (jpegICIinfo != null)
                    {
                        bitmap.Save(fileSaveUrl, jpegICIinfo, ep);
                        //ob.Save(dFile, jpegICIinfo, ep);//dFile是压缩后的新路径  
                    }
                    else
                    {
                        bitmap.Save(fileSaveUrl, ImageFormat.Jpeg);
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }
                finally
                {

                    myImage.Dispose();
                    bitmap.Dispose();
                }
            }
        }

        //按模版比例生成缩略图（以流的方式获取源文件）  
        //生成缩略图函数  
        //顺序参数：源图文件流、缩略图存放地址、模版宽、模版高  
        //注：缩略图大小控制在模版区域内  
        public static void MakeSmallImg(System.IO.Stream fromFileStream, string fileSaveUrl, System.Double templateWidth, System.Double templateHeight)
        {
            //从文件取得图片对象，并使用流中嵌入的颜色管理信息  
            System.Drawing.Image myImage = System.Drawing.Image.FromStream(fromFileStream, true);

            //缩略图宽、高  
            System.Double newWidth = myImage.Width, newHeight = myImage.Height;
            //宽大于模版的横图  
            if (myImage.Width > myImage.Height || myImage.Width == myImage.Height)
            {
                if (myImage.Width > templateWidth)
                {
                    if (templateHeight > myImage.Height * (templateWidth / myImage.Width))
                    {
                        //宽按模版，高按比例缩放  
                        newWidth = templateWidth;
                        newHeight = myImage.Height * (newWidth / myImage.Width);
                    }
                    else
                    {
                        //高按模版，宽按比例缩放  
                        newHeight = templateHeight;
                        newWidth = myImage.Width * (newHeight / myImage.Height);

                    }
                }

            }
            //高大于模版的竖图  
            else
            {
                if (myImage.Height > templateHeight)
                {
                    if (templateWidth < myImage.Width * (templateHeight / myImage.Height))
                    {
                        //宽按模版，高按比例缩放  
                        newWidth = templateWidth;
                        newHeight = myImage.Height * (newWidth / myImage.Width);


                    }
                    else
                    {
                        //高按模版，宽按比例缩放  
                        newHeight = templateHeight;
                        newWidth = myImage.Width * (newHeight / myImage.Height);
                    }
                }

            }
            //取得图片大小  
            System.Drawing.Size mySize = new Size((int)newWidth, (int)newHeight);
            //新建一个bmp图片  
            System.Drawing.Image bitmap = new System.Drawing.Bitmap(mySize.Width, mySize.Height);
            //新建一个画板  
            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bitmap);
            //设置高质量插值法  
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
            //设置高质量,低速度呈现平滑程度  
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            //清空一下画布  
            g.Clear(Color.White);
            //在指定位置画图  
            g.DrawImage(myImage, new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height),
            new System.Drawing.Rectangle(0, 0, myImage.Width, myImage.Height),
            System.Drawing.GraphicsUnit.Pixel);
            g.Dispose();

            ///文字水印  
            System.Drawing.Graphics G = System.Drawing.Graphics.FromImage(bitmap);
            System.Drawing.Font f = new Font("Lucida Grande", 6);
            System.Drawing.Brush b = new SolidBrush(Color.Gray);
            G.DrawString("", f, b, 0, 0);
            G.Dispose();

            ///图片水印  
            //System.Drawing.Image   copyImage   =   System.Drawing.Image.FromFile(System.Web.HttpContext.Current.Server.MapPath("pic/1.gif"));  
            //Graphics   a   =   Graphics.FromImage(bitmap);  
            //a.DrawImage(copyImage,   new   Rectangle(bitmap.Width-copyImage.Width,bitmap.Height-copyImage.Height,copyImage.Width,   copyImage.Height),0,0,   copyImage.Width,   copyImage.Height,   GraphicsUnit.Pixel);  

            //copyImage.Dispose();  
            //a.Dispose();  
            //copyImage.Dispose();  

            //保存缩略图  
            if (File.Exists(fileSaveUrl))
            {
                File.SetAttributes(fileSaveUrl, FileAttributes.Normal);
                File.Delete(fileSaveUrl);
            }

            //bitmap.Save(fileSaveUrl, System.Drawing.Imaging.ImageFormat.Jpeg);

            //以下代码为保存图片时，设置压缩质量  
            EncoderParameters ep = new EncoderParameters();
            long[] qy = new long[1];
            qy[0] = 90;//设置压缩的比例1-100   默认90
            EncoderParameter eParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, qy);
            ep.Param[0] = eParam;
            try
            {
                ImageCodecInfo[] arrayICI = ImageCodecInfo.GetImageEncoders();
                ImageCodecInfo jpegICIinfo = null;
                for (int x = 0; x < arrayICI.Length; x++)
                {
                    if (arrayICI[x].FormatDescription.Equals("JPEG"))
                    {
                        jpegICIinfo = arrayICI[x];
                        break;
                    }
                }
                if (jpegICIinfo != null)
                {
                    bitmap.Save(fileSaveUrl, jpegICIinfo, ep);
                    //ob.Save(dFile, jpegICIinfo, ep);//dFile是压缩后的新路径  
                }
                else
                {
                    bitmap.Save(fileSaveUrl, ImageFormat.Jpeg);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                myImage.Dispose();
                bitmap.Dispose();
            }
        }
        /**/
        /// <summary>
        /// 获取图片指定部分
        /// </summary>
        /// <param name="pPath">图片路径</param>
        /// <param name="pSavePath">保存路径</param>
        /// <param name="pPartStartPointX">目标图片开始绘制处的坐标X值(通常为)</param>
        /// <param name="pPartStartPointY">目标图片开始绘制处的坐标Y值(通常为)</param>
        /// <param name="pPartWidth">目标图片的宽度</param>
        /// <param name="pPartHeight">目标图片的高度</param>
        /// <param name="pOrigStartPointX">原始图片开始截取处的坐标X值</param>
        /// <param name="pOrigStartPointY">原始图片开始截取处的坐标Y值</param>
        /// <param name="pFormat">保存格式，通常可以是jpeg</param>
        public void GetPart(System.Drawing.Image pPath, string pSavedPath, int pPartStartPointX, int pPartStartPointY, int pPartWidth, int pPartHeight, int pOrigStartPointX, int pOrigStartPointY)
        {
            string ext = ".jpg";
            string[] allowExten = new string[] { ".jpg", ".jpeg", ".gif", ".png" };
            System.Collections.ArrayList extensionArray = new System.Collections.ArrayList();
            extensionArray.AddRange(allowExten);
            if (!extensionArray.Contains(ext.ToString().ToLower()))
            {
                HttpContext.Current.Response.Write("上传的图片格式不正确");
            }
            else
            {
                string normalJpgPath = pSavedPath + "3" + ext;
                using (Image originalImg = pPath)
                {
                    Bitmap partImg = new Bitmap(pPartWidth, pPartHeight);
                    Graphics graphics = Graphics.FromImage(partImg);
                    Rectangle destRect = new Rectangle(new Point(pPartStartPointX, pPartStartPointY), new Size(pPartWidth, pPartHeight));//目标位置
                    Rectangle origRect = new Rectangle(new Point(pOrigStartPointX, pOrigStartPointY), new Size(pPartWidth, pPartHeight));//原图位置（默认从原图中截取的图片大小等于目标图片的大小）


                    ///文字水印  
                    System.Drawing.Graphics G = System.Drawing.Graphics.FromImage(partImg);
                    System.Drawing.Font f = new Font("Lucida Grande", 6);
                    System.Drawing.Brush b = new SolidBrush(Color.Gray);
                    G.Clear(Color.White);
                    graphics.DrawImage(originalImg, destRect, origRect, GraphicsUnit.Pixel);
                    G.DrawString("", f, b, 0, 0);
                    G.Dispose();

                    originalImg.Dispose();
                    if (File.Exists(normalJpgPath))
                    {
                        File.SetAttributes(normalJpgPath, FileAttributes.Normal);
                        File.Delete(normalJpgPath);
                    }
                    partImg.Save(normalJpgPath);
                    GetSmaller(normalJpgPath, normalJpgPath.Replace("_3", "_2"), 74, 90, ext);
                    GetSmaller(normalJpgPath, normalJpgPath.Replace("_3", ""), 50, 56, ext);
                }
            }
        }
        /**/
        /// <summary>
        /// 获取按比例缩放的图片指定部分
        /// </summary>
        /// <param name="pPath">图片路径</param>
        /// <param name="pSavePath">保存路径</param>
        /// <param name="pPartStartPointX">目标图片开始绘制处的坐标X值(通常为)</param>
        /// <param name="pPartStartPointY">目标图片开始绘制处的坐标Y值(通常为)</param>
        /// <param name="pPartWidth">目标图片的宽度</param>
        /// <param name="pPartHeight">目标图片的高度</param>
        /// <param name="pOrigStartPointX">原始图片开始截取处的坐标X值</param>
        /// <param name="pOrigStartPointY">原始图片开始截取处的坐标Y值</param>
        /// <param name="imageWidth">缩放后的宽度</param>
        /// <param name="imageHeight">缩放后的高度</param>
        public void GetPart(Bitmap pPath, string pSavedPath, int pPartStartPointX, int pPartStartPointY, int pPartWidth, int pPartHeight, int pOrigStartPointX, int pOrigStartPointY, int imageWidth, int imageHeight)
        {
            string ext = ".jpg";
            string[] allowExten = new string[] { ".jpg", ".jpeg", ".gif", ".png" };
            System.Collections.ArrayList extensionArray = new System.Collections.ArrayList();
            extensionArray.AddRange(allowExten);
            if (!extensionArray.Contains(ext.ToString().ToLower()))
            {
                HttpContext.Current.Response.Write("上传的图片格式不正确");
            }
            else
            {
                string normalJpgPath = pSavedPath + "3" + ext;
                using (Image originalImg = pPath)
                {
                    if (originalImg.Width == imageWidth && originalImg.Height == imageHeight)
                    {
                        GetPart(pPath, pSavedPath, pPartStartPointX, pPartStartPointY, pPartWidth, pPartHeight, pOrigStartPointX, pOrigStartPointY);
                        return;
                    }

                    Image.GetThumbnailImageAbort callback = new Image.GetThumbnailImageAbort(ThumbnailCallback);
                    Image zoomImg = originalImg.GetThumbnailImage(imageWidth, imageHeight, callback, IntPtr.Zero);//缩放
                    Bitmap partImg = new Bitmap(pPath, pPartWidth, pPartHeight);

                    Graphics graphics = Graphics.FromImage(partImg);
                    Rectangle destRect = new Rectangle(new Point(pPartStartPointX, pPartStartPointY), new Size(pPartWidth, pPartHeight));//目标位置
                    //Rectangle destRect = new Rectangle(new Point(pPartStartPointX, pPartStartPointY), new Size(pPartWidth, pPartHeight));//目标位置
                    Rectangle origRect = new Rectangle(new Point(pOrigStartPointX, pOrigStartPointY), new Size(pPartWidth, pPartHeight));//原图位置（默认从原图中截取的图片大小等于目标图片的大小）
                    //Rectangle origRect = new Rectangle(new Point(pOrigStartPointX, pOrigStartPointY), new Size(pPartWidth, pPartHeight));//原图位置（默认从原图中截取的图片大小等于目标图片的大小）
                    ///文字水印  
                    System.Drawing.Graphics G = System.Drawing.Graphics.FromImage(partImg);
                    System.Drawing.Font f = new Font("Lucida Grande", 6);
                    System.Drawing.Brush b = new SolidBrush(Color.Gray);
                    G.Clear(Color.White);

                    graphics.DrawImage(pPath, destRect, origRect, GraphicsUnit.Pixel);
                    G.DrawString("", f, b, 0, 0);
                    G.Dispose();


                    if (File.Exists(normalJpgPath))
                    {
                        File.SetAttributes(normalJpgPath, FileAttributes.Normal);
                        File.Delete(normalJpgPath);
                    }
                    partImg.Save(normalJpgPath); partImg.Dispose();
                    GetSmaller(normalJpgPath, normalJpgPath.Replace("3", "2"), 74, 90, ext);
                    GetSmaller(normalJpgPath, normalJpgPath.Replace("3", "4"), 50, 56, ext);
                    originalImg.Dispose();
                }
            }
        }

        /// <summary>
        /// 获得图像高宽信息
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public ImageInformation GetImageInfo(string path)
        {
            using (Image image = Image.FromFile(path))
            {
                ImageInformation imageInfo = new ImageInformation();
                imageInfo.Width = image.Width;
                imageInfo.Height = image.Height;
                return imageInfo;
            }
        }
        public static bool ThumbnailCallback()
        {
            return false;
        }
        public struct ImageInformation
        {
            private int width;

            public int Width
            {
                get { return width; }
                set { width = value; }
            }
            private int height;

            public int Height
            {
                get { return height; }
                set { height = value; }
            }
        }
    }
}
