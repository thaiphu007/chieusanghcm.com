using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

namespace Utilities.Helper
{
    public static class ImageHelper
    {
        public static byte[] CropImage(byte[] content, int x, int y, int width, int height,double scale,int rotate)
        {
            using (MemoryStream stream = new MemoryStream(content))
            {
                return CropImage(stream, x, y, width, height,scale,rotate);
            }
        }
        public static Bitmap FixOrientation(Bitmap image, int rotate)
        {
            switch (rotate)
            {
                // up is pointing to the right
                case 270:
                    image.RotateFlip(RotateFlipType.Rotate270FlipNone);
                    break;
                // up is pointing to the bottom (image is upside-down)
                case 180:
                    image.RotateFlip(RotateFlipType.Rotate180FlipNone);
                    break;
                // up is pointing to the left
                case 90:
                    image.RotateFlip(RotateFlipType.Rotate90FlipNone);
                    break;
                // up is pointing up (correct orientation)
                default:
                    break;
            }
            return image;
        }
        public static byte[] CropImage(Stream content, int x, int y, int width, int height, double scale, int rotate)
        {
            
            //Parsing stream to bitmap
            using (Bitmap sourceBitmap = new Bitmap(content))
            {
                switch (rotate)
                {
                    // up is pointing to the right
                    case 270:
                        sourceBitmap.RotateFlip(RotateFlipType.Rotate270FlipNone);
                        break;
                    // up is pointing to the bottom (image is upside-down)
                    case 180:
                        sourceBitmap.RotateFlip(RotateFlipType.Rotate180FlipNone);
                        break;
                    // up is pointing to the left
                    case 90:
                        sourceBitmap.RotateFlip(RotateFlipType.Rotate90FlipNone);
                        break;
                    // up is pointing up (correct orientation)
                    default:
                        break;
                }
                //Get new dimensions      
                x = (int)(x/scale);
                y = (int)(y/scale);
                width = (int)(Math.Abs(x - (x + width)) / scale);
                height = (int)(Math.Abs(y - (y + height)) / scale);
                Rectangle source_rect = new Rectangle(x, y, width, height);
                Rectangle dest_rect = new Rectangle(0, 0, width, height);

                // Copy that part of the image to a new bitmap.
                Bitmap new_image = new Bitmap(width, height);

               
                using (Graphics gr = Graphics.FromImage(new_image))
                {
                    gr.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    gr.SmoothingMode = SmoothingMode.HighQuality;
                    gr.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    gr.CompositingQuality = CompositingQuality.HighQuality;
                    gr.DrawImage(sourceBitmap, dest_rect, source_rect,
                        GraphicsUnit.Pixel);
                    int wid = (int)(scale * (new_image.Width));
                    int hgt = (int)(scale * (new_image.Height));
                    Bitmap ScaledImage = new Bitmap(wid, hgt);
                    using (Graphics gr1 = Graphics.FromImage(ScaledImage))
                    {
                        Rectangle src_rect = new Rectangle(0, 0,new_image.Width, new_image.Height);
                        Rectangle dest_rect1 = new Rectangle(0, 0, wid, hgt);
                        gr1.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        gr1.SmoothingMode = SmoothingMode.HighQuality;
                        gr1.PixelOffsetMode = PixelOffsetMode.HighQuality;
                        gr1.CompositingQuality = CompositingQuality.HighQuality;
                        gr1.DrawImage(new_image, dest_rect1, src_rect,
                            GraphicsUnit.Pixel);
                        return GetBitmapBytes(ScaledImage);
                    }
                }
              
            }
        }

        public static byte[] GetBitmapBytes(Bitmap source)
        {
            //Settings to increase quality of the image
            ImageCodecInfo codec = ImageCodecInfo.GetImageEncoders()[4];
            EncoderParameters parameters = new EncoderParameters(1);
            parameters.Param[0] = new EncoderParameter(Encoder.Quality, 100L);

            //Temporary stream to save the bitmap
            using (MemoryStream tmpStream = new MemoryStream())
            {
                source.Save(tmpStream, codec, parameters);

                //Get image bytes from temporary stream
                byte[] result = new byte[tmpStream.Length];
                tmpStream.Seek(0, SeekOrigin.Begin);
                tmpStream.Read(result, 0, (int)tmpStream.Length);

                return result;
            }
        }
    }
}