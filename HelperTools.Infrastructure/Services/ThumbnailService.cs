namespace HelperTools.Infrastructure.Services
{
    using Properties;
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using Color = System.Drawing.Color;

    /// <summary>The ThumbnailService.</summary>
    [SuppressMessage("ReSharper", "SuggestBaseTypeForParameter")]
    public class ThumbnailService
    {
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

        private readonly ImageService imageService;

        #region Constructor
        /// <summary>Initializes a new instance of the <see cref="ThumbnailService" /> class.</summary>
        public ThumbnailService()
        {
            imageService = new ImageService();
        }
        #endregion Constructor

        /// <summary>Gets the thumbnail dim.</summary>
        /// <value>The thumbnail dim.</value>
        public int[] ThumbDim { get; } = {Settings.Default.ThumbWidth, Settings.Default.ThumbHeight};

        /// <summary>Gets or sets the preview image.</summary>
        /// <value>The preview image.</value>
        public BitmapImage PreviewImage { get; set; }

        /// <summary>Creates a thumbnail based on RGB values without a frame.</summary>
        /// <param name="file">The full path to the new image.</param>
        /// <param name="thumbFolder">The folder where the thumbnail should be saved.</param>
        /// <param name="rgb">The desired color as RGB values.</param>
        /// <param name="isPreview">if set to <c>true</c> only a preview should displayed.</param>
        public void BuildThumbnail(string file, string thumbFolder, byte[] rgb, bool isPreview)
        {
            if (!File.Exists(thumbFolder)) Directory.CreateDirectory(thumbFolder);

            CreateThumbnail(file, thumbFolder, rgb, ThumbDim[0], ThumbDim[1], Border.None, null, 0, null, 0, isPreview);
        }

        /// <summary>Creates a thumbnail based on RGB values with a single frame.</summary>
        /// <param name="file">The full path to the new image.</param>
        /// <param name="thumbFolder">The folder where the thumbnail should be saved.</param>
        /// <param name="rgb">The desired color as RGB values.</param>
        /// <param name="borderBrush">The border brush.</param>
        /// <param name="borderSize">Size of the border.</param>
        /// <param name="isPreview">if set to <c>true</c> only a preview should displayed.</param>
        public void BuildThumbnail(string file, string thumbFolder, byte[] rgb, SolidColorBrush borderBrush, int borderSize, bool isPreview)
        {
            if (!File.Exists(thumbFolder)) Directory.CreateDirectory(thumbFolder);

            CreateThumbnail(file, thumbFolder, rgb, ThumbDim[0], ThumbDim[1], Border.Single,
                ColorConverterService.SolidColorBrushToSolidBrush(borderBrush), borderSize, null, 0, isPreview);
        }

        /// <summary>Creates a thumbnail based on RGB values with a double frame.</summary>
        /// <param name="file">The full path to the new image.</param>
        /// <param name="thumbFolder">The folder where the thumbnail should be saved.</param>
        /// <param name="rgb">The desired color as RGB values.</param>
        /// <param name="outerBorderBrush">The outer border brush.</param>
        /// <param name="outerBorderSize">Size of the outer border.</param>
        /// <param name="innerBorderBrush">The inner border brush.</param>
        /// <param name="innerBorderSize">Size of the inner border.</param>
        /// <param name="isPreview">if set to <c>true</c> only a preview should displayed.</param>
        public void BuildThumbnail(string file, string thumbFolder, byte[] rgb, SolidColorBrush outerBorderBrush, int outerBorderSize,
            SolidColorBrush innerBorderBrush, int innerBorderSize, bool isPreview)
        {
            if (!File.Exists(thumbFolder)) Directory.CreateDirectory(thumbFolder);

            CreateThumbnail(file, thumbFolder, rgb, ThumbDim[0], ThumbDim[1], Border.Double,
                ColorConverterService.SolidColorBrushToSolidBrush(outerBorderBrush), outerBorderSize,
                ColorConverterService.SolidColorBrushToSolidBrush(innerBorderBrush), innerBorderSize, isPreview);
        }

        /// <summary>Creates a thumbnail based on an existing image without a frame.</summary>
        /// <param name="file">The full path to the original image.</param>
        /// <param name="thumbFolder">The folder where the thumbnail should be saved.</param>
        /// <param name="isPreview">if set to <c>true</c> only a preview should displayed.</param>
        public void BuildThumbnail(string file, string thumbFolder, bool isPreview)
        {
            if (!File.Exists(thumbFolder)) Directory.CreateDirectory(thumbFolder);

            CreateThumbnail(file, thumbFolder, ThumbDim[0], ThumbDim[1], Border.None, null, 0, null, 0, isPreview);
        }

        /// <summary>Creates a thumbnail based on an existing image single a frame.</summary>
        /// <param name="file">The full path to the original image.</param>
        /// <param name="thumbFolder">The folder where the thumbnail should be saved.</param>
        /// <param name="borderBrush">The border brush.</param>
        /// <param name="borderSize">Size of the border.</param>
        /// <param name="isPreview">if set to <c>true</c> only a preview should displayed.</param>
        public void BuildThumbnail(string file, string thumbFolder, SolidColorBrush borderBrush, int borderSize, bool isPreview)
        {
            if (!File.Exists(thumbFolder)) Directory.CreateDirectory(thumbFolder);

            CreateThumbnail(file, thumbFolder, ThumbDim[0], ThumbDim[1], Border.Single, ColorConverterService.SolidColorBrushToSolidBrush(borderBrush),
                borderSize, null, 0, isPreview);
        }

        /// <summary>Creates a thumbnail based on an existing image double a frame.</summary>
        /// <param name="file">The full path to the original image.</param>
        /// <param name="thumbFolder">The folder where the thumbnail should be saved.</param>
        /// <param name="outerBorderBrush">The outer border brush.</param>
        /// <param name="outerBorderSize">Size of the outer border.</param>
        /// <param name="innerBorderBrush">The inner border brush.</param>
        /// <param name="innerBorderSize">Size of the inner border.</param>
        /// <param name="isPreview">if set to <c>true</c> only a preview should displayed.</param>
        public void BuildThumbnail(string file, string thumbFolder, SolidColorBrush outerBorderBrush, int outerBorderSize,
            SolidColorBrush innerBorderBrush, int innerBorderSize, bool isPreview)
        {
            if (!File.Exists(thumbFolder)) Directory.CreateDirectory(thumbFolder);

            CreateThumbnail(file, thumbFolder, ThumbDim[0], ThumbDim[1], Border.Double,
                ColorConverterService.SolidColorBrushToSolidBrush(outerBorderBrush), outerBorderSize,
                ColorConverterService.SolidColorBrushToSolidBrush(innerBorderBrush), innerBorderSize, isPreview);
        }

        private void CreateThumbnail(string file, string thumbFolder, byte[] rgb, int width, int height, Border border, SolidBrush outerBorderBrush,
            int outerBorderSize, SolidBrush innerBorderBrush, int innerBorderSize, bool isPreview)
        {
            var b = new Bitmap(width, height);

            using (var g = Graphics.FromImage(b))
            {
                g.FillRectangle(new SolidBrush(Color.FromArgb(rgb[0], rgb[1], rgb[2])), 0, 0, width, height);
                g.DrawImage(b, 0, 0, width, height);
            }

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
                    throw new ArgumentOutOfRangeException(nameof(border), border, null);
            }
            
            if (isPreview) PreviewImage = imageService.BitmapToImageSource(b, ImageFormat.Jpeg);
            else b.Save(Path.Combine(Path.GetDirectoryName(file) + @"\", thumbFolder) + @"\" + Path.GetFileName(file), ImageFormat.Jpeg);
        }

        private void CreateThumbnail(string file, string thumbFolder, int width, int height, Border border, SolidBrush outerBorderBrush,
            int outerBorderSize, SolidBrush innerBorderBrush, int innerBorderSize, bool isPreview)
        {
            var b = new Bitmap(file);
            b = imageService.ResizeBitmap(b, width, height);

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
                    throw new ArgumentOutOfRangeException(nameof(border), border, null);
            }

            if (isPreview) PreviewImage = imageService.BitmapToImageSource(b, ImageFormat.Jpeg);
            else b.Save(Path.Combine(Path.GetDirectoryName(file) + @"\", thumbFolder) + @"\" + Path.GetFileName(file), ImageFormat.Jpeg);
        }
    }
}