namespace HelperTools.Infrastructure.Services
{
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Windows.Media.Imaging;

    /// <summary>The ImageService.</summary>
    public class ImageService
    {
        /// <summary>Resizes the bitmap in case of a scale value.</summary>
        /// <param name="b">The bitmap.</param>
        /// <param name="scale">The scale.</param>
        /// <returns>The resized bitmap.</returns>
        public Bitmap ResizeBitmap(Bitmap b, float scale)
        {
            int nWidth = (int) Math.Ceiling(b.Width * scale);
            int nHeight = (int) Math.Ceiling(b.Height * scale);

            Bitmap resizedImage = ResizeBitmap(b, nWidth, nHeight);

            return resizedImage;
        }

        /// <summary>Resizes the bitmap in case of maximal dimension values.</summary>
        /// <param name="b">The bitmap.</param>
        /// <param name="maxWidth">The maximum width.</param>
        /// <param name="maxHeight">The maximum height.</param>
        /// <param name="allowLargerImageCreation">if set to <c>true</c> allow larger image creation.</param>
        /// <param name="keepRatio">if set to <c>true</c> keep the ratio.</param>
        /// <returns>The resized bitmap.</returns>
        public Bitmap ResizeBitmap(Bitmap b, int maxWidth, int maxHeight, bool allowLargerImageCreation, bool keepRatio = true)
        {
            if (!allowLargerImageCreation && maxWidth >= b.Width && maxHeight >= b.Height) return ResizeBitmap(b, b.Width, b.Height);

            if (!keepRatio) return ResizeBitmap(b, maxWidth, maxHeight);

            double ratioWidth = (double) maxWidth / b.Width;
            double ratioHeight = (double) maxHeight / b.Height;

            bool scaleToMaxWidth = ratioWidth < ratioHeight;
            double ratio = scaleToMaxWidth ? ratioWidth : ratioHeight;

            var newWidth = Convert.ToInt32(b.Width * ratio);
            var newHeight = Convert.ToInt32(b.Height * ratio);

            Bitmap resizedImage = ResizeBitmap(b, newWidth, newHeight);

            return resizedImage;
        }

        /// <summary>Resizes the bitmap in case of new dimension values.</summary>
        /// <param name="b">The bitmap.</param>
        /// <param name="nWidth">The new width.</param>
        /// <param name="nHeight">The new height.</param>
        /// <returns>The resized bitmap.</returns>
        public Bitmap ResizeBitmap(Image b, int nWidth, int nHeight)
        {
            Bitmap resizedImage = new Bitmap(nWidth, nHeight);
            using (Graphics g = Graphics.FromImage(resizedImage))
            {
                g.DrawImage(b, 0, 0, nWidth, nHeight);
            }

            b.Dispose();

            return resizedImage;
        }

        /// <summary>Creates a single framed image.</summary>
        /// <param name="b">The bitmap.</param>
        /// <param name="outline">The outline color.</param>
        /// <param name="border">The border thickness.</param>
        /// <returns>The framed image.</returns>
        public Bitmap CreateSingleBorder(Bitmap b, SolidBrush outline, int border)
        {
            Bitmap nImage = new Bitmap(b);
            using (Graphics g = Graphics.FromImage(nImage))
            {
                g.FillRectangle(outline, 0, 0, b.Width, b.Height);
                g.DrawImage(b, border, border, b.Width - border * 2, b.Height - border * 2);
            }

            return nImage;
        }

        /// <summary>Creates a single framed image.</summary>
        /// <param name="b">The bitmap.</param>
        /// <param name="outline">The outline color.</param>
        /// <param name="inline">The inline color.</param>
        /// <param name="borderOut">The outline border thickness.</param>
        /// <param name="borderIn">The inline border thickness.</param>
        /// <returns>The framed image.</returns>
        public Bitmap CreateDoubleBorder(Bitmap b, SolidBrush outline, SolidBrush inline, int borderOut, int borderIn)
        {
            int border = borderIn + borderOut;

            Bitmap nImage = new Bitmap(b);
            using (Graphics g = Graphics.FromImage(nImage))
            {
                g.FillRectangle(outline, 0, 0, b.Width, b.Height);
                g.FillRectangle(inline, borderOut, borderOut, b.Width - borderOut * 2, b.Height - borderOut * 2);
                g.DrawImage(b, border, border, b.Width - border * 2, b.Height - border * 2);
            }

            return nImage;
        }

        /// <summary>Prepares a bitmap for display in an image element.</summary>
        /// <param name="bitmap">The bitmap that is to be converted.</param>
        /// <param name="imgFormat">The format in which the image is to be cached.</param>
        /// <returns>The image, rendered for viewing in a Imagebox.</returns>
        public BitmapImage BitmapToImageSource(Bitmap bitmap, ImageFormat imgFormat)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                bitmap.Save(memory, imgFormat);
                memory.Position = 0;

                BitmapImage bi = new BitmapImage();

                bi.BeginInit();
                bi.StreamSource = memory;
                bi.CacheOption = BitmapCacheOption.OnLoad;
                bi.EndInit();

                return bi;
            }
        }
    }
}