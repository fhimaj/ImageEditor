using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace EditorTools
{
    public static class ImageEditor
    {
        public const int BRIGHTNESS_INCREASE_RATE = 10;
        public const int BRIGHTNESS_DECREASE_RATE = -10;
        public const string IMAGE_EXTENSIONS_PATTERN = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";

        public static Image initialImage = null;
        public static Image loadedImage = null;
        public static float widthMultiplier;
        public static float heightMultiplier;

        public static PointF lastPoint;
        public static bool isDrawing = false, isMouseDown = false;
        public static int brushThickness = 0;
        public static Color brushColor = Color.Black;
        public static int ResizeWidth = 200;
        public static int ResizeHeight = 200;

        public static Bitmap AdjustBrightness(Bitmap image, int threshold)
        {

            Bitmap tempBitmap = new Bitmap(image);
            float finalValue = threshold / 255.0f;

            float[][] colorMatrixElements = {
            new float[] { 1, 0, 0, 0, 0 },
            new float[] { 0, 1, 0, 0, 0 },
            new float[] { 0, 0, 1, 0, 0 },
            new float[] { 0, 0, 0, 1, 0 },
            new float[] { finalValue, finalValue, finalValue, 0, 1 }
            };

            ColorMatrix colorMatrix = new ColorMatrix(colorMatrixElements);
            ImageAttributes attributes = new ImageAttributes();
            attributes.SetColorMatrix(colorMatrix);

            Bitmap newBitmap = new Bitmap(tempBitmap.Width, tempBitmap.Height);
            using (Graphics gfx = Graphics.FromImage(newBitmap))
            {
                gfx.DrawImage(tempBitmap, new Rectangle(0, 0, tempBitmap.Width, tempBitmap.Height),
                              0, 0, tempBitmap.Width, tempBitmap.Height, GraphicsUnit.Pixel, attributes);
            }

            return newBitmap;
        }

        public static Bitmap AdjustContrast(Bitmap sourceBitmap, int threshold)
        {
            BitmapData sourceData = sourceBitmap.LockBits(new Rectangle(0, 0, sourceBitmap.Width, sourceBitmap.Height),
                                                          ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

            byte[] pixelBuffer = new byte[sourceData.Stride * sourceData.Height];
            Marshal.Copy(sourceData.Scan0, pixelBuffer, 0, pixelBuffer.Length);
            sourceBitmap.UnlockBits(sourceData);

            double contrastLevel = Math.Pow((100.0 + threshold) / 100.0, 2);
            double blue, green, red;

            for (int k = 0; k + 4 < pixelBuffer.Length; k += 4)
            {
                blue = (((pixelBuffer[k] / 255.0) - 0.5) * contrastLevel + 0.5) * 255.0;
                green = (((pixelBuffer[k + 1] / 255.0) - 0.5) * contrastLevel + 0.5) * 255.0;
                red = (((pixelBuffer[k + 2] / 255.0) - 0.5) * contrastLevel + 0.5) * 255.0;

                blue = Math.Min(255, Math.Max(0, blue));
                green = Math.Min(255, Math.Max(0, green));
                red = Math.Min(255, Math.Max(0, red));

                pixelBuffer[k] = (byte)blue;
                pixelBuffer[k + 1] = (byte)green;
                pixelBuffer[k + 2] = (byte)red;
            }

            Bitmap resultBitmap = new Bitmap(sourceBitmap.Width, sourceBitmap.Height);
            BitmapData resultData = resultBitmap.LockBits(new Rectangle(0, 0, resultBitmap.Width, resultBitmap.Height),
                                                          ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
            Marshal.Copy(pixelBuffer, 0, resultData.Scan0, pixelBuffer.Length);
            resultBitmap.UnlockBits(resultData);

            return resultBitmap;
        }


        public static Image Rotate90(Image img)
        {
            var bmp = new Bitmap(img);

            using (Graphics gfx = Graphics.FromImage(bmp))
            {
                gfx.Clear(Color.Transparent);
                gfx.DrawImage(img, 0, 0, img.Width, img.Height);
            }

            bmp.RotateFlip(RotateFlipType.Rotate270FlipNone);
            return bmp;
        }

        public static Image ResizeImage(Image img, int width, int height)
        {
            var resizedBmp = new Bitmap(width, height);

            if (width > 2000)
                width = 2000;

            if (height > 2000)
                height = 2000;

            if (width < 50)
                width = 50;

            if (height < 50)
                height = 50;

            using (Graphics gfx = Graphics.FromImage(resizedBmp))
            {
                gfx.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                gfx.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                gfx.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

                gfx.DrawImage(img, 0, 0, width, height);
            }

            return resizedBmp;
        }

        public static Bitmap GenerateHistogram(Image img)
        {
            var image = new Bitmap(img);
            int[] redHistogram = new int[256];
            int[] greenHistogram = new int[256];
            int[] blueHistogram = new int[256];

            for (int x = 0; x < image.Width; x++)
            {
                for (int y = 0; y < image.Height; y++)
                {
                    Color pixelColor = image.GetPixel(x, y);
                    redHistogram[pixelColor.R]++;
                    greenHistogram[pixelColor.G]++;
                    blueHistogram[pixelColor.B]++;
                }
            }

            var width = 514;
            var height = 256;
            var margin = 10;
            Bitmap histogramBitmap = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(histogramBitmap))
            {
                g.Clear(Color.White);

                int preX = margin;

                for (int i = 0; i < 256; i++)
                {
                    g.DrawLine(Pens.Red,preX, height+margin, preX+2, height+margin - redHistogram[i] / 10);
                    preX = preX + 2;
                }

                preX = margin;
                for (int i = 0; i < 256; i++)
                {
                    g.DrawLine(Pens.Green, preX, height+margin, preX + 2, height+margin - greenHistogram[i] / 10);
                    preX = preX + 2;
                }

                preX = margin;
                for (int i = 0; i < 256; i++)
                {
                    g.DrawLine(Pens.Blue,preX, height+margin, preX + 2, height+margin - greenHistogram[i] / 10);
                    preX = preX + 2;
                }
            }

            return histogramBitmap;
        }
    }
}
