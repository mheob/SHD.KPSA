namespace HelperTools.Infrastructure.Services
{
    using System.Drawing;
    using System.Globalization;
    using System.Windows.Media;
    using Color = System.Windows.Media.Color;

    /// <summary>Converter class for colors.</summary>
    public class ColorConverterService
    {
        /// <summary>Get the RGB values using a HEX color.</summary>
        /// <param name="hex">The HEX-color with or without leading #.</param>
        /// <returns>The values (R)ed, (G)reen und (B)lue.</returns>
        public static byte[] GetRgbfromHex(string hex)
        {
            byte[] rgb = new byte[3];

            int pos = hex.Length > 6 ? 1 : 0;

            for (byte i = 0; i < 3; i++)
            {
                rgb[i] = byte.Parse(hex.Substring(pos, 2), NumberStyles.HexNumber);

                pos += 2;
            }

            return rgb;
        }

        /// <summary>Gets the RGB values using an image file.</summary>
        /// <param name="file">The image file.</param>
        /// <returns>The values (R)ed, (G)reen und (B)lue.</returns>
        public static byte[] GetRgbFromImage(string file)
        {
            byte[] rgb = new byte[3];

            Bitmap img = (Bitmap) Image.FromFile(file);

//            int pixel = img.Width * img.Height;

            for (int x = 0, maxX = img.Width; x < maxX; x++)
            {
                for (int y = 0, maxY = img.Height; y < maxY; y++)
                {
                    System.Drawing.Color color = img.GetPixel(x, y);

                    rgb[0] += color.R;
                    rgb[1] += color.G;
                    rgb[2] += color.B;
                }
            }

            return rgb;
        }

        /// <summary>Get the RGB values back using a Color.</summary>
        /// <param name="color">The color.</param>
        /// <returns>The values (R)ed, (G)reen und (B)lue.</returns>
        public static byte[] GetRgbFromColor(Color color)
        {
            byte[] rgb = new byte[3];

            rgb[0] = color.R;
            rgb[1] = color.G;
            rgb[2] = color.B;

            return rgb;
        }

        /// <summary>Get the RGB values back using a SolidColorBrush.</summary>
        /// <param name="br">The color as SolidColorBrush.</param>
        /// <returns>The values (R)ed, (G)reen und (B)lue.</returns>
        public static byte[] GetRgbFromSolidColorBrush(SolidColorBrush br)
        {
            byte[] rgb = new byte[3];

            rgb[0] = br.Color.R;
            rgb[1] = br.Color.G;
            rgb[2] = br.Color.B;

            return rgb;
        }

        /// <summary>Converts a SolidColorBrush into a SolidBrush.</summary>
        /// <param name="br">The color as SolidColorBrush.</param>
        /// <returns>The color as SolidBrush.</returns>
        public static SolidBrush SolidColorBrushToSolidBrush(SolidColorBrush br)
        {
            return new SolidBrush(System.Drawing.Color.FromArgb(br.Color.A, br.Color.R, br.Color.G, br.Color.B));
        }
    }
}