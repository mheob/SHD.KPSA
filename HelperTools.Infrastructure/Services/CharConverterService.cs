namespace HelperTools.Infrastructure.Services
{
    /// <summary>Class for converting chars to special characters</summary>
    public static class CharConverterService
    {
        /// <summary>Converts special characters in ASCII code.</summary>
        /// <param name="s">The string to be converted.</param>
        /// <returns>The converted string.</returns>
        public static string ConvertCharsToAscii(string s)
        {
            s = s.ToLower();

            s = s.Replace("*", "");
            s = s.Replace("ä", "ae");
            s = s.Replace("ö", "oe");
            s = s.Replace("ü", "ue");
            s = s.Replace("ß", "ss");
            s = s.Replace("&", "_");
            s = s.Replace("-", "_");
            s = s.Replace(" ", "_");

            return s;
        }
    }
}