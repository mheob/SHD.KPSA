namespace HelperTools.Infrastructure.Services
{
    using System;
    using System.Drawing;

    /// <summary>The ImageService.</summary>
    public class ImageService
    {
        /// <summary>Resizes the bitmap.</summary>
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

        /// <summary>Resizes the bitmap.</summary>
        /// <param name="b">The bitmap.</param>
        /// <param name="maxWidth">The maximum width.</param>
        /// <param name="maxHeight">The maximum height.</param>
        /// <param name="allowLargerImageCreation">if set to <c>true</c> allow larger image creation.</param>
        /// <param name="keepRatio">if set to <c>true</c> keep the ratio.</param>
        /// <returns>The resized bitmap.</returns>
        public Bitmap ResizeBitmap(Bitmap b, int maxWidth, int maxHeight, bool allowLargerImageCreation, bool keepRatio = true)
        {
            if (!allowLargerImageCreation && maxWidth >= b.Width && maxHeight >= b.Height)
                return ResizeBitmap(b, b.Width, b.Height);

            if (!keepRatio)
                return ResizeBitmap(b, maxWidth, maxHeight);

            double ratioWidth = (double) maxWidth / b.Width;
            double ratioHeight = (double) maxHeight / b.Height;

            bool scaleToMaxWidth = ratioWidth < ratioHeight;
            double ratio = scaleToMaxWidth ? ratioWidth : ratioHeight;

            var newWidth = Convert.ToInt32(b.Width * ratio);
            var newHeight = Convert.ToInt32(b.Height * ratio);

            Bitmap resizedImage = ResizeBitmap(b, newWidth, newHeight);

            return resizedImage;
        }

        /// <summary>Resizes the bitmap.</summary>
        /// <param name="b">The bitmap.</param>
        /// <param name="nWidth">New width of the bitmap.</param>
        /// <param name="nHeight">New height of the bitmap.</param>
        /// <returns>The resized bitmap.</returns>
        private static Bitmap ResizeBitmap(Image b, int nWidth, int nHeight)
        {
            Bitmap resizedImage = new Bitmap(nWidth, nHeight);
            using (Graphics g = Graphics.FromImage(resizedImage))
            {
                g.DrawImage(b, 0, 0, nWidth, nHeight);
            }

            b.Dispose();

            return resizedImage;
        }
    }
}