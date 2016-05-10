namespace HelperTools.MatFileGen.ViewModels
{
    using System.Windows.Media;
    using Infrastructure.Base;
    using Infrastructure.Events;
    using Infrastructure.Services;

    /// <summary>The SettingsSolidViewModel.</summary>
    /// <seealso cref="ViewModelBase" />
    public class SettingsSolidViewModel : ViewModelBase
    {
        #region Fields
        private string solidColorName;
        private Color selectedColor;
        #endregion Fields

        #region Constructor
        /// <summary>Initializes a new instance of the <see cref="SettingsSolidViewModel" /> class.</summary>
        public SettingsSolidViewModel()
        {
            SelectedColor = Color.FromRgb(51, 68, 85);
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
                if (!SetProperty(ref solidColorName, value))
                    return;

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
                if (SetProperty(ref selectedColor, value))
                    return;

                EventAggregator.GetEvent<SolidRgbUpdateEvent>().Publish(ColorConverterService.GetRgbFromColor(SelectedColor));
            }
        }
        #endregion Properties

        #region Methods
        #endregion Methods
    }
}