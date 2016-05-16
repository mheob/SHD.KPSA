namespace HelperTools.Infrastructure.Services
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Reflection;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using Microsoft.Practices.ServiceLocation;
    using Microsoft.Practices.Unity;
    using Prism.Logging;
    using Properties;
    using Color = System.Drawing.Color;

    /// <summary>The ThumbnailService.</summary>
    public class ThumbnailService
    {
        #region Enum
        /// <summary>Selection as the preview image to be generated.</summary>
        public enum ShowPreview
        {
            /// <summary>No preview image.</summary>
            No,

            /// <summary>The original image as a preview.</summary>
            Original,

            /// <summary>The thumbnail image as a preview.</summary>
            Thumbnail
        }
        #endregion Enum

        /// <summary>The possible frames.</summary>
        public enum Border
        {
            /// <summary>Without border.</summary>
            None,

            /// <summary>Single border.</summary>
            Single,

            /// <summary>Double border.</summary>
            Double
        }

        private readonly IUnityContainer unityContainer;
        private readonly ImageService imageService;

        #region Constructor
        /// <summary>Initializes a new instance of the <see cref="ThumbnailService" /> class.</summary>
        public ThumbnailService()
        {
            unityContainer = ServiceLocator.Current.GetInstance<IUnityContainer>();
            imageService = new ImageService();
        }
        #endregion Constructor

        /// <summary>Gets the thumbnail dim.</summary>
        /// <value>The thumbnail dim.</value>
        public int[] ThumbDim { get; set; } = {Settings.Default.ThumbWidth, Settings.Default.ThumbHeight};

        /// <summary>Creates a thumbnail based on RGB values without a frame.</summary>
        /// <param name="file">The full path to the new image.</param>
        /// <param name="thumbFolder">The folder where the thumbnail should be saved.</param>
        /// <param name="rgb">The desired color as RGB values.</param>
        /// <param name="preview">The variant of the preview.</param>
        /// <param name="saveThumb">if set to <c>true</c> the thumbnail should save.</param>
        /// <returns>The preview BitmapImage.</returns>
        public BitmapImage BuildThumbnail(string file, string thumbFolder, byte[] rgb, ShowPreview preview, bool saveThumb)
        {
            if (preview == ShowPreview.No && !File.Exists(thumbFolder))
                Directory.CreateDirectory(Path.Combine(Path.GetDirectoryName(file) + @"\", thumbFolder));

            return CreateThumbnail(file, thumbFolder, rgb, Border.None, null, 0, null, 0, preview, saveThumb);
        }

        /// <summary>Creates a thumbnail based on RGB values with a single frame.</summary>
        /// <param name="file">The full path to the new image.</param>
        /// <param name="thumbFolder">The folder where the thumbnail should be saved.</param>
        /// <param name="rgb">The desired color as RGB values.</param>
        /// <param name="borderBrush">The border brush.</param>
        /// <param name="borderSize">Size of the border.</param>
        /// <param name="preview">The variant of the preview.</param>
        /// <param name="saveThumb">if set to <c>true</c> the thumbnail should save.</param>
        /// <returns>The preview BitmapImage.</returns>
        public BitmapImage BuildThumbnail(string file, string thumbFolder, byte[] rgb, SolidColorBrush borderBrush, int borderSize,
            ShowPreview preview, bool saveThumb)
        {
            if (preview == ShowPreview.No && !File.Exists(thumbFolder))
                Directory.CreateDirectory(Path.Combine(Path.GetDirectoryName(file) + @"\", thumbFolder));

            return CreateThumbnail(file, thumbFolder, rgb, Border.Single, ColorConverterService.SolidColorBrushToSolidBrush(borderBrush), borderSize,
                null, 0, preview, saveThumb);
        }

        /// <summary>Creates a thumbnail based on RGB values with a double frame.</summary>
        /// <param name="file">The full path to the new image.</param>
        /// <param name="thumbFolder">The folder where the thumbnail should be saved.</param>
        /// <param name="rgb">The desired color as RGB values.</param>
        /// <param name="outerBorderBrush">The outer border brush.</param>
        /// <param name="outerBorderSize">Size of the outer border.</param>
        /// <param name="innerBorderBrush">The inner border brush.</param>
        /// <param name="innerBorderSize">Size of the inner border.</param>
        /// <param name="preview">The variant of the preview.</param>
        /// <param name="saveThumb">if set to <c>true</c> the thumbnail should save.</param>
        /// <returns>The preview BitmapImage.</returns>
        public BitmapImage BuildThumbnail(string file, string thumbFolder, byte[] rgb, SolidColorBrush outerBorderBrush, int outerBorderSize,
            SolidColorBrush innerBorderBrush, int innerBorderSize, ShowPreview preview, bool saveThumb)
        {
            if (preview == ShowPreview.No && !File.Exists(thumbFolder))
                Directory.CreateDirectory(Path.Combine(Path.GetDirectoryName(file) + @"\", thumbFolder));

            return CreateThumbnail(file, thumbFolder, rgb, Border.Double, ColorConverterService.SolidColorBrushToSolidBrush(outerBorderBrush),
                outerBorderSize, ColorConverterService.SolidColorBrushToSolidBrush(innerBorderBrush), innerBorderSize, preview, saveThumb);
        }

        /// <summary>Creates a thumbnail based on an existing image without a frame.</summary>
        /// <param name="file">The full path to the original image.</param>
        /// <param name="thumbFolder">The folder where the thumbnail should be saved.</param>
        /// <param name="preview">The variant of the preview.</param>
        /// <param name="saveThumb">if set to <c>true</c> the thumbnail should save.</param>
        /// <returns>The preview BitmapImage.</returns>
        public BitmapImage BuildThumbnail(string file, string thumbFolder, ShowPreview preview, bool saveThumb)
        {
            if (preview == ShowPreview.No && !File.Exists(thumbFolder))
                Directory.CreateDirectory(Path.Combine(Path.GetDirectoryName(file) + @"\", thumbFolder));

            return CreateThumbnail(file, thumbFolder, Border.None, null, 0, null, 0, preview, saveThumb);
        }

        /// <summary>Creates a thumbnail based on an existing image single a frame.</summary>
        /// <param name="file">The full path to the original image.</param>
        /// <param name="thumbFolder">The folder where the thumbnail should be saved.</param>
        /// <param name="borderBrush">The border brush.</param>
        /// <param name="borderSize">Size of the border.</param>
        /// <param name="preview">The variant of the preview.</param>
        /// <param name="saveThumb">if set to <c>true</c> the thumbnail should save.</param>
        /// <returns>The preview BitmapImage.</returns>
        public BitmapImage BuildThumbnail(string file, string thumbFolder, SolidColorBrush borderBrush, int borderSize, ShowPreview preview,
            bool saveThumb)
        {
            if (preview == ShowPreview.No && !File.Exists(thumbFolder))
                Directory.CreateDirectory(Path.Combine(Path.GetDirectoryName(file) + @"\", thumbFolder));

            return CreateThumbnail(file, thumbFolder, Border.Single, ColorConverterService.SolidColorBrushToSolidBrush(borderBrush), borderSize, null,
                0, preview, saveThumb);
        }

        /// <summary>Creates a thumbnail based on an existing image double a frame.</summary>
        /// <param name="file">The full path to the original image.</param>
        /// <param name="thumbFolder">The folder where the thumbnail should be saved.</param>
        /// <param name="outerBorderBrush">The outer border brush.</param>
        /// <param name="outerBorderSize">Size of the outer border.</param>
        /// <param name="innerBorderBrush">The inner border brush.</param>
        /// <param name="innerBorderSize">Size of the inner border.</param>
        /// <param name="preview">The variant of the preview.</param>
        /// <param name="saveThumb">if set to <c>true</c> the thumbnail should save.</param>
        /// <returns>The preview BitmapImage.</returns>
        public BitmapImage BuildThumbnail(string file, string thumbFolder, SolidColorBrush outerBorderBrush, int outerBorderSize,
            SolidColorBrush innerBorderBrush, int innerBorderSize, ShowPreview preview, bool saveThumb)
        {
            if (preview == ShowPreview.No && !File.Exists(thumbFolder))
                Directory.CreateDirectory(Path.Combine(Path.GetDirectoryName(file) + @"\", thumbFolder));

            return CreateThumbnail(file, thumbFolder, Border.Double, ColorConverterService.SolidColorBrushToSolidBrush(outerBorderBrush),
                outerBorderSize, ColorConverterService.SolidColorBrushToSolidBrush(innerBorderBrush), innerBorderSize, preview, saveThumb);
        }

        private BitmapImage CreateThumbnail(string file, string thumbFolder, IReadOnlyList<byte> rgb, Border border, SolidBrush outerBorderBrush,
            int outerBorderSize, SolidBrush innerBorderBrush, int innerBorderSize, ShowPreview preview, bool saveThumb)
        {
            ChoiceDimension(preview == ShowPreview.Original);

            var b = new Bitmap(ThumbDim[0], ThumbDim[1]);

            using (var g = Graphics.FromImage(b))
            {
                g.FillRectangle(new SolidBrush(Color.FromArgb(rgb[0], rgb[1], rgb[2])), 0, 0, ThumbDim[0], ThumbDim[1]);
                g.DrawImage(b, 0, 0, ThumbDim[0], ThumbDim[1]);
                g.Dispose();
            }

            return SwitchDoing(b, file, thumbFolder, border, outerBorderBrush, outerBorderSize, innerBorderBrush, innerBorderSize, preview, saveThumb);
        }

        private BitmapImage CreateThumbnail(string file, string thumbFolder, Border border, SolidBrush outerBorderBrush, int outerBorderSize,
            SolidBrush innerBorderBrush, int innerBorderSize, ShowPreview preview, bool saveThumb)
        {
            ChoiceDimension(preview == ShowPreview.Original);

            var b = new Bitmap(file);
            b = imageService.ResizeBitmap(b, ThumbDim[0], ThumbDim[1]);

            return SwitchDoing(b, file, thumbFolder, border, outerBorderBrush, outerBorderSize, innerBorderBrush, innerBorderSize, preview, saveThumb);
        }

        private void ChoiceDimension(bool original)
        {
            if (original)
            {
                ThumbDim[0] = Settings.Default.OriginalImagePreview;
                ThumbDim[1] = Settings.Default.OriginalImagePreview;
            }
            else
            {
                ThumbDim[0] = Settings.Default.ThumbWidth;
                ThumbDim[1] = Settings.Default.ThumbHeight;
            }
        }

        private BitmapImage SwitchDoing(Bitmap b, string file, string thumbFolder, Border border, SolidBrush outerBorderBrush, int outerBorderSize,
            SolidBrush innerBorderBrush, int innerBorderSize, ShowPreview preview, bool saveThumb)
        {
            string logMessage;

            switch (border)
            {
                case Border.Single:
                    b = imageService.CreateSingleBorder(b, outerBorderBrush, outerBorderSize);
                    break;
                case Border.Double:
                    b = imageService.CreateDoubleBorder(b, outerBorderBrush, innerBorderBrush, outerBorderSize, innerBorderSize);
                    break;
                case Border.None:
                    break;
                default:
                    var ex = new ArgumentOutOfRangeException(nameof(border), border, null);

                    logMessage = $"[{GetType().Name}] Exception at {MethodBase.GetCurrentMethod()}: {ex}";
                    unityContainer.Resolve<ILoggerFacade>().Log(logMessage, Category.Exception, Priority.High);

                    DialogService.Exception(ex, DialogService.ExceptionType.Universal);
                    throw ex;
            }

            switch (preview)
            {
                case ShowPreview.No:
                    if (!saveThumb) return null;

                    var newFile = Path.Combine(Path.GetDirectoryName(file) + @"\", thumbFolder) + @"\" + Path.GetFileName(file);
                    
                    imageService.Save(b, newFile, 60, ImageFormat.Jpeg);

                    logMessage = $"[{GetType().Name}] File ({newFile}) was created";
                    unityContainer.Resolve<ILoggerFacade>().Log(logMessage, Category.Debug, Priority.None);

                    return null;
                case ShowPreview.Original:
                    logMessage = $"[{GetType().Name}] Preview in original size was created";
                    unityContainer.Resolve<ILoggerFacade>().Log(logMessage, Category.Debug, Priority.None);

                    return imageService.BitmapToImageSource(b, ImageFormat.Jpeg);
                case ShowPreview.Thumbnail:
                    logMessage = $"[{GetType().Name}] Preview in thumbnail size was created";
                    unityContainer.Resolve<ILoggerFacade>().Log(logMessage, Category.Debug, Priority.None);

                    return imageService.BitmapToImageSource(b, ImageFormat.Jpeg);
                default:
                    var ex = new ArgumentOutOfRangeException(nameof(preview), preview, null);

                    logMessage = $"[{GetType().Name}] Exception at {MethodBase.GetCurrentMethod()}: {ex}";
                    unityContainer.Resolve<ILoggerFacade>().Log(logMessage, Category.Exception, Priority.High);

                    DialogService.Exception(ex, DialogService.ExceptionType.Universal);
                    throw ex;
            }
        }
    }
}