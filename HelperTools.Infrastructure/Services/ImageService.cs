namespace HelperTools.Infrastructure.Services
{
    using Microsoft.Practices.ServiceLocation;
    using Microsoft.Practices.Unity;
    using Prism.Logging;
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Linq;
    using System.Windows.Media.Imaging;

    /// <summary>The ImageService.</summary>
    public class ImageService
    {
        #region Fields
        private readonly IUnityContainer unityContainer;
        #endregion Fields

        #region Constructor
        /// <summary>Initializes a new instance of the <see cref="ImageService" /> class.</summary>
        public ImageService()
        {
            unityContainer = ServiceLocator.Current.GetInstance<IUnityContainer>();
        }
        #endregion Constructor

        #region Methods
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
            Bitmap resizedImage = new Bitmap(b, nWidth, nHeight);
            using (Graphics g = Graphics.FromImage(resizedImage))
            {
                g.DrawImage(b, 0, 0, nWidth, nHeight);
                g.Dispose();
            }

            var logMessage = $"[{GetType().Name}] Bitmap was resized";
            unityContainer.Resolve<ILoggerFacade>().Log(logMessage, Category.Debug, Priority.None);

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

            var logMessage = $"[{GetType().Name}] Bitmap with a single border was created";
            unityContainer.Resolve<ILoggerFacade>().Log(logMessage, Category.Debug, Priority.None);

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
            
            var logMessage = $"[{GetType().Name}] Bitmap with a double border was created";
            unityContainer.Resolve<ILoggerFacade>().Log(logMessage, Category.Debug, Priority.None);

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

                var logMessage = $"[{GetType().Name}] BitmapImage with was created from a Bitmap";
                unityContainer.Resolve<ILoggerFacade>().Log(logMessage, Category.Debug, Priority.None);

                return bi;
            }
        }

        /// <summary>Method to resize, convert and save the image.</summary>
        /// <param name="b">Bitmap image.</param>
        /// <param name="filePath">file path.</param>
        /// <param name="quality">quality setting value.</param>
        /// <param name="format">The image format.</param>
        public void Save(Bitmap b, string filePath, int quality, ImageFormat format)
        {
            var newImage = new Bitmap(b);

            using (Graphics g = Graphics.FromImage(newImage))
            {
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.DrawImage(b, 0, 0, b.Width, b.Height);
            }

            newImage.Dispose();

            var imageCodecInfo = GetEncoderInfo(format);
            var encoder = Encoder.Quality;
            var encoderParameters = new EncoderParameters(1);

            var encoderParameter = new EncoderParameter(encoder, quality);
            encoderParameters.Param[0] = encoderParameter;
            newImage.Save(filePath, imageCodecInfo, encoderParameters);
        }

        /// <summary>Method to get encoder for a given image format.</summary>
        /// <param name="format">Image format</param>
        /// <returns>image codec info.</returns>
        private static ImageCodecInfo GetEncoderInfo(ImageFormat format)
        {
            return ImageCodecInfo.GetImageDecoders().SingleOrDefault(c => c.FormatID == format.Guid);
        }
        #endregion Methods
    }
}