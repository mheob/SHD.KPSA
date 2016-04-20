namespace HelperTools.Shell.Models
{
    using Infrastructure.Base;
    using Microsoft.Practices.ServiceLocation;
    using Microsoft.Practices.Unity;
    using Prism.Logging;
    using System.Windows.Media;

    /// <summary>The model for the AccentColor.</summary>
    /// <seealso cref="BindableBase" />
    public class AccentColor : BindableBase
    {
        #region Fields
        private string name;
        private Brush colorBrush;
        #endregion Fields

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="AccentColor"/> class.
        /// </summary>
        public AccentColor()
        {
            var container = ServiceLocator.Current.GetInstance<IUnityContainer>();
            container.Resolve<ILoggerFacade>().Log("AccentColor initializes", Category.Info, Priority.None);
        }
        #endregion Constructor

        #region Properties
        /// <summary>Gets or sets the name.</summary>
        /// <value>The name.</value>
        public string Name
        {
            get { return name; }
            set { SetProperty(ref name, value); }
        }


        /// <summary>Gets or sets the color brush.</summary>
        /// <value>The color brush.</value>
        public Brush ColorBrush
        {
            get { return colorBrush; }
            set { SetProperty(ref colorBrush, value); }
        }
        #endregion Properties
    }
}