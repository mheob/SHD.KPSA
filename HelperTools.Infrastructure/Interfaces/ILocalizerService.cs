namespace HelperTools.Infrastructure.Interfaces
{
    using System.Collections.Generic;
    using System.Globalization;

    /// <summary>The ILocalizerService.</summary>
    public interface ILocalizerService
    {
        /// <summary>Sets the locale.</summary>
        /// <param name="locale">The locale.</param>
        void SetLocale(string locale);

        /// <summary>Sets the locale.</summary>
        /// <param name="culture">The culture.</param>
        void SetLocale(CultureInfo culture);

        /// <summary>Gets the localized string.</summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        string GetLocalizedString(string key);

        /// <summary>Gets the localized value.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        T GetLocalizedValue<T>(string key);

        /// <summary>Gets the supported languages.</summary>
        /// <value>The supported languages.</value>
        IList<CultureInfo> SupportedLanguages { get; }


        /// <summary>Gets or sets the selected language.</summary>
        /// <value>The selected language.</value>
        CultureInfo SelectedLanguage { get; set; }
    }
}