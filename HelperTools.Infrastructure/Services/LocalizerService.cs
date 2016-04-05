namespace HelperTools.Infrastructure.Services
{
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Reflection;
    using Interfaces;
    using WPFLocalizeExtension.Engine;
    using WPFLocalizeExtension.Extensions;

    /// <summary>The LocalizerService.</summary>
    /// <seealso cref="ILocalizerService" />
    public class LocalizerService : ILocalizerService
    {
        #region Constructor
        /// <summary>Initializes a new instance of the <see cref="LocalizerService" /> class.</summary>
        /// <param name="culture">The culture.</param>
        public LocalizerService(string culture)
        {
            SupportedLanguages =
                CultureInfo.GetCultures(CultureTypes.AllCultures)
                    .Where(c => c.IetfLanguageTag.Equals("de-DE") || c.IetfLanguageTag.Equals("en-US"))
                    .ToList();

            SetLocale(culture);
        }
        #endregion Constructor

        /// <summary>Sets the locale.</summary>
        /// <param name="locale">The locale.</param>
        public void SetLocale(string locale)
        {
            LocalizeDictionary.Instance.Culture = CultureInfo.GetCultureInfo(locale);
        }

        /// <summary>Sets the locale.</summary>
        /// <param name="culture">The culture.</param>
        public void SetLocale(CultureInfo culture)
        {
            LocalizeDictionary.Instance.Culture = culture;
        }

        /// <summary>Gets the localized string.</summary>
        /// <param name="key">The key.</param>
        /// <returns>The localized String.</returns>
        public string GetLocalizedString(string key)
        {
            string uiString;

            LocExtension locExtension = new LocExtension(key);
            locExtension.ResolveLocalizedValue(out uiString);

            return uiString;
        }

        /// <summary>Gets the localized value.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public T GetLocalizedValue<T>(string key)
        {
            return LocExtension.GetLocalizedValue<T>(Assembly.GetCallingAssembly().GetName().Name + ":Resources:" + key);
        }

        /// <summary>Gets the supported languages.</summary>
        /// <value>The supported languages.</value>
        public IList<CultureInfo> SupportedLanguages { get; }

        /// <summary>Gets or sets the selected language.</summary>
        /// <value>The selected language.</value>
        public CultureInfo SelectedLanguage
        {
            get { return LocalizeDictionary.Instance.Culture; }
            set { SetLocale(value); }
        }
    }
}