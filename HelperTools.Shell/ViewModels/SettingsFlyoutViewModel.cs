namespace HelperTools.Shell.ViewModels
{
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Windows;
    using System.Windows.Media;
    using Infrastructure.Base;
    using Infrastructure.Constants;
    using Infrastructure.Events;
    using Infrastructure.Interfaces;
    using MahApps.Metro;
    using Microsoft.Practices.Unity;
    using Models;
    using Properties;

    /// <summary>The SettingsFlyoutViewModel.</summary>
    /// <seealso cref="ViewModelBase" />
    public class SettingsFlyoutViewModel : ViewModelBase
    {
        #region Fields
        private readonly ILocalizerService localizerService;
        private IList<ApplicationTheme> applicationsThemes;
        private IList<AccentColor> accentColors;
        private ApplicationTheme selectedTheme;
        private AccentColor selectedAccentColor;
        #endregion Fields

        #region Constructor
        /// <summary>Initializes a new instance of the <see cref="SettingsFlyoutViewModel" /> class.</summary>
        public SettingsFlyoutViewModel()
        {
            localizerService = Container.Resolve<ILocalizerService>(ServiceNames.LOCALIZER_SERVICE);

            // create metro theme color menu items for the demo
            ApplicationThemes = ThemeManager.AppThemes.Select(a => new ApplicationTheme()
            {
                Name = a.Name,
                BorderColorBrush = a.Resources["BlackColorBrush"] as Brush,
                ColorBrush = a.Resources["WhiteColorBrush"] as Brush
            }).ToList();

            // create accent colors list
            AccentColors = ThemeManager.Accents.Select(a => new AccentColor()
            {
                Name = a.Name,
                ColorBrush = a.Resources["AccentColorBrush"] as Brush
            }).ToList();

            SelectedTheme = ApplicationThemes.FirstOrDefault(t => t.Name.Equals("BaseDark"));
            SelectedAccentColor = AccentColors.FirstOrDefault(c => c.Name.Equals("Cobalt"));
        }
        #endregion Constructor

        #region Properties
        /// <summary>Gets or sets the application themes.</summary>
        /// <value>The application themes.</value>
        public IList<ApplicationTheme> ApplicationThemes
        {
            get { return applicationsThemes; }
            set { SetProperty(ref applicationsThemes, value); }
        }

        /// <summary>Gets or sets the accent colors.</summary>
        /// <value>The accent colors.</value>
        public IList<AccentColor> AccentColors
        {
            get { return accentColors; }
            set { SetProperty(ref accentColors, value); }
        }

        /// <summary>Gets or sets the selected theme.</summary>
        /// <value>The selected theme.</value>
        public ApplicationTheme SelectedTheme
        {
            get { return selectedTheme; }
            set
            {
                if (!SetProperty(ref selectedTheme, value)) return;

                var theme = ThemeManager.DetectAppStyle(Application.Current);
                var appTheme = ThemeManager.GetAppTheme(value.Name);

                ThemeManager.ChangeAppStyle(Application.Current, theme.Item2, appTheme);

                EventAggregator.GetEvent<StatusBarMessageUpdateEvent>().Publish(Resources.StatusBarThemeChanged + value.Name);
            }
        }

        /// <summary>Gets or sets the color of the selected accent.</summary>
        /// <value>The color of the selected accent.</value>
        public AccentColor SelectedAccentColor
        {
            get { return selectedAccentColor; }
            set
            {
                if (!SetProperty(ref selectedAccentColor, value)) return;

                var theme = ThemeManager.DetectAppStyle(Application.Current);
                var accent = ThemeManager.GetAccent(value.Name);

                ThemeManager.ChangeAppStyle(Application.Current, accent, theme.Item1);

                EventAggregator.GetEvent<StatusBarMessageUpdateEvent>().Publish(Resources.StatusBarAccentColorChanged + value.Name);
            }
        }

        /// <summary>Gets the supported languages.</summary>
        /// <value>The supported languages.</value>
        public IList<CultureInfo> SupportedLanguages => localizerService?.SupportedLanguages;

        /// <summary>Gets or sets the selected language.</summary>
        /// <value>The selected language.</value>
        public CultureInfo SelectedLanguage
        {
            get { return localizerService?.SelectedLanguage; }
            set
            {
                if (value == null || Equals(value, localizerService.SelectedLanguage)) return;

                if (localizerService != null)
                    localizerService.SelectedLanguage = value;

                EventAggregator.GetEvent<StatusBarMessageUpdateEvent>().Publish(Resources.StatusBarLanguageChanged + value.DisplayName);
            }
        }
        #endregion Properties
    }
}