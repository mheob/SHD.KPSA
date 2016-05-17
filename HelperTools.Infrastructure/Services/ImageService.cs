namespace HelperTools.Infrastructure.Services
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Windows.Media.Imaging;
    using Microsoft.Practices.ServiceLocation;
    using Microsoft.Practices.Unity;
    using Prism.Logging;

    /// <summary>The ImageService.</summary>
    public class ImageService
    {
        #region Enum
        /// <summary>The Anchor positions.</summary>
        public enum AnchorPosition
        {
            /// <summary>Position center.</summary>
            Center,

            /// <summary>Position top.</summary>
            Top,

            /// <summary>Position bottom.</summary>
            Bottom,

            /// <summary>Position left.</summary>
            Left,

            /// <summary>Position right.</summary>
            Right,
        }
        #endregion Enum

        #region Fields
        private readonly IUnityContainer unityContainer;
        #endregion Fields

        #region Constructor
        /// <summary>Initializes a new instance of the <see cref="ImageService" /> class.</summary>
        public ImageService()
        {
            unityContainer = ServiceLocator.Current.GetInstance<IUnityContainer>();

            var logMessage = $"[{GetType().Name}] is initialized";
            unityContainer.Resolve<ILoggerFacade>().Log(logMessage, Category.Debug, Priority.None);
        }
        #endregion Constructor

        #region Methods
        /// <summary>Resizes the bitmap in case of new dimension values</summary>
        /// <param name="file">The file.</param>
        /// <param name="width">The new width.</param>
        /// <param name="height">The new height.</param>
        /// <returns>The resized image.</returns>
        public Image ResizeImage(string file, int width, int height)
        {
            Image nImage = Image.FromFile(file);

            return ResizeImage(nImage, width, height);
        }

        /// <summary>Resizes a image from a file in case of a scale value in percent.</summary>
        /// <param name="file">The file.</param>
        /// <param name="scale">The scale.</param>
        /// <returns>The resized image.</returns>
        public Image ResizeImage(string file, int scale)
        {
            Image nImage = Image.FromFile(file);

            var percent = (float) scale / 100;

            var sourceWidth = nImage.Width;
            var sourceHeight = nImage.Height;

            var destWidth = (int) Math.Ceiling(sourceWidth * percent);
            var destHeight = (int) Math.Ceiling(sourceHeight * percent);

            return ResizeImage(nImage, destWidth, destHeight);
        }

        /// <summary>Resizes a image from a file in case of maximal dimension values.</summary>
        /// <param name="file">The file.</param>
        /// <param name="maxWidth">The maximum width.</param>
        /// <param name="maxHeight">The maximum height.</param>
        /// <param name="allowLargerImage">if set to <c>true</c> larger image creation is allowed.</param>
        /// <param name="keepRatio">if set to <c>true</c> keep the ratio.</param>
        /// <returns>The resized image.</returns>
        public Image ResizeImage(string file, int maxWidth, int maxHeight, bool allowLargerImage, bool keepRatio = true)
        {
            Image nImage = Image.FromFile(file);

            var sourceWidth = nImage.Width;
            var sourceHeight = nImage.Height;

            if (!allowLargerImage && maxWidth >= sourceWidth && maxHeight >= sourceHeight) return ResizeImage(nImage, sourceWidth, sourceHeight);
            if (!keepRatio) return ResizeImage(nImage, maxWidth, maxHeight);

            var percentW = (float) maxWidth / sourceWidth;
            var percentH = (float) maxHeight / sourceHeight;

            var percent = percentW < percentH ? percentW : percentH;

            var destWidth = (int) Math.Ceiling(sourceWidth * percent);
            var destHeight = (int) Math.Ceiling(sourceHeight * percent);

            return ResizeImage(nImage, destWidth, destHeight);
        }

        /// <summary>Resizes and crop a image from a file in case of dimension values.</summary>
        /// <param name="file">The file.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="anchor">The anchor position for the crop.</param>
        /// <returns>The resized and cropped image.</returns>
        [SuppressMessage("ReSharper", "ConvertIfStatementToSwitchStatement")]
        public Image ResizeImage(string file, int width, int height, AnchorPosition anchor)
        {
            Image nImage = Image.FromFile(file);

            var sourceWidth = nImage.Width;
            var sourceHeight = nImage.Height;

            int destX = 0, destY = 0;

            float percent;

            var percentW = (float) width / sourceWidth;
            var percentH = (float) height / sourceHeight;

            if (percentH < percentW)
            {
                percent = percentW;
                if (anchor == AnchorPosition.Top) destY = 0;
                else if (anchor == AnchorPosition.Bottom) destY = (int) (height - sourceHeight * percent);
                else destY = (int) ((height - sourceHeight * percent) / 2);
            }
            else
            {
                percent = percentH;
                if (anchor == AnchorPosition.Left) destX = 0;
                else if (anchor == AnchorPosition.Right) destX = (int) (width - sourceWidth * percent);
                else destX = (int) ((width - sourceWidth * percent) / 2);
            }

            int destWidth = (int) (sourceWidth * percent);
            int destHeight = (int) (sourceHeight * percent);

            Bitmap b = new Bitmap(width, height, PixelFormat.Format24bppRgb);
            b.SetResolution(nImage.HorizontalResolution, nImage.VerticalResolution);

            Graphics grPhoto = Graphics.FromImage(b);
            grPhoto.InterpolationMode = InterpolationMode.HighQualityBicubic;
            grPhoto.DrawImage(nImage, new Rectangle(destX, destY, destWidth, destHeight), new Rectangle(0, 0, sourceWidth, sourceHeight),
                GraphicsUnit.Pixel);
            grPhoto.Dispose();

            var logMessage = $"[{GetType().Name}] An existed file was cropped to an image";
            unityContainer.Resolve<ILoggerFacade>().Log(logMessage, Category.Debug, Priority.None);

            return b;
        }

        /// <summary>Resizes the bitmap in case of new dimension values</summary>
        /// <param name="image">The image.</param>
        /// <param name="width">The new width.</param>
        /// <param name="height">The new height.</param>
        /// <returns>The resized image.</returns>
        public Image ResizeImage(Image image, int width, int height)
        {
            Bitmap b = new Bitmap(width, height, PixelFormat.Format24bppRgb);
            b.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            Graphics gfx = Graphics.FromImage(b);
            gfx.InterpolationMode = InterpolationMode.HighQualityBicubic;
            gfx.DrawImage(image, 0, 0, width, height);
            gfx.Dispose();

            var logMessage = $"[{GetType().Name}] An image was resized to a new image object";
            unityContainer.Resolve<ILoggerFacade>().Log(logMessage, Category.Debug, Priority.None);

            return b;
        }

        /// <summary>Creates a single framed image.</summary>
        /// <param name="b">The bitmap.</param>
        /// <param name="outline">The outline color.</param>
        /// <param name="border">The border thickness.</param>
        /// <returns>The framed image.</returns>
        public Image CreateSingleBorder(Image b, SolidBrush outline, int border)
        {
            Bitmap nImage = new Bitmap(b);
            using (Graphics gfx = Graphics.FromImage(nImage))
            {
                gfx.FillRectangle(outline, 0, 0, b.Width, b.Height);
                gfx.DrawImage(b, border, border, b.Width - border * 2, b.Height - border * 2);
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
        public Image CreateDoubleBorder(Image b, SolidBrush outline, SolidBrush inline, int borderOut, int borderIn)
        {
            int border = borderIn + borderOut;

            Bitmap nImage = new Bitmap(b);
            using (Graphics gfx = Graphics.FromImage(nImage))
            {
                gfx.FillRectangle(outline, 0, 0, b.Width, b.Height);
                gfx.FillRectangle(inline, borderOut, borderOut, b.Width - borderOut * 2, b.Height - borderOut * 2);
                gfx.DrawImage(b, border, border, b.Width - border * 2, b.Height - border * 2);
            }

            var logMessage = $"[{GetType().Name}] Image with a double border was created";
            unityContainer.Resolve<ILoggerFacade>().Log(logMessage, Category.Debug, Priority.None);

            return nImage;
        }

        /// <summary>Prepares a image for display in an image element.</summary>
        /// <param name="b">The image that is to be converted.</param>
        /// <param name="imgFormat">The format in which the image is to be cached.</param>
        /// <returns>The image, rendered for viewing in a Imagebox.</returns>
        public BitmapImage GetBitmapImageFromImage(Image b, ImageFormat imgFormat)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                b.Save(ms, imgFormat);
                b.Dispose();
                ms.Position = 0;

                BitmapImage bi = new BitmapImage();

                bi.BeginInit();
                bi.StreamSource = ms;
                bi.CacheOption = BitmapCacheOption.OnLoad;
                bi.EndInit();

                var logMessage = $"[{GetType().Name}] BitmapImage with was created from an Image";
                unityContainer.Resolve<ILoggerFacade>().Log(logMessage, Category.Debug, Priority.None);

                return bi;
            }
        }
        #endregion Methods
    }
}