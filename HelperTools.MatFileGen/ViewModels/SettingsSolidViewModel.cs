namespace HelperTools.MatFileGen.ViewModels
{
    using System.Windows.Media;
    using Infrastructure.Base;
    using Infrastructure.Events;
    using Infrastructure.Services;
    using Models;
    using Properties;

    /// <summary>The SettingsSolidViewModel.</summary>
    /// <seealso cref="ViewModelBase" />
    public class SettingsSolidViewModel : ViewModelBase
    {
        #region Fields
        private readonly JsonService jsonService = new JsonService();
        private readonly string configFile = Settings.Default.SettingsMfgSolidFile;

        private string solidColorName;
        private Color selectedColor;
        #endregion Fields

        #region Constructor
        /// <summary>Initializes a new instance of the <see cref="SettingsSolidViewModel" /> class.</summary>
        public SettingsSolidViewModel()
        {
            InitializeInternalSettings();
            WriteJson();
        }
        #endregion Constructor

        #region Properties
        /// <summary>Gets or sets the name of the solid color.</summary>
        /// <value>The name of the solid color.</value>
        public string SolidColorName
        {
            get { return solidColorName; }
            set
            {
                if (!SetProperty(ref solidColorName, CharConverterService.ConvertCharsToAscii(value))) return;

                WriteJson();
                EventAggregator.GetEvent<SolidColorNameUpdateEvent>().Publish(SolidColorName);
            }
        }

        /// <summary>Gets or sets the selected color.</summary>
        /// <value>The selected color.</value>
        public Color SelectedColor
        {
            get { return selectedColor; }
            set
            {
                if (!SetProperty(ref selectedColor, value)) return;

                WriteJson();
                EventAggregator.GetEvent<SolidRgbUpdateEvent>().Publish(ColorConverterService.GetRgbFromColor(SelectedColor));
            }
        }

        private void InitializeInternalSettings()
        {
            SolidColorName = string.Empty;
            SelectedColor = Color.FromRgb(35, 23, 215);
        }

        private void WriteJson()
        {
            SettingsSolid settings = new SettingsSolid
            {
                SolidColorName = SolidColorName,
                SelectedColor = SelectedColor
            };

            jsonService.WriteJson(settings, configFile);
        }
        #endregion Properties
    }
}