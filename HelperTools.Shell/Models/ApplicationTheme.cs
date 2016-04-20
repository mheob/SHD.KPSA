namespace HelperTools.Shell.Models
{
    using System.Windows.Media;
    using Infrastructure.Base;
    using Microsoft.Practices.ServiceLocation;
    using Microsoft.Practices.Unity;
    using Prism.Logging;

    /// <summary>The model for the ApplicationTheme.</summary>
    /// <seealso cref="BindableBase" />
    public class ApplicationTheme : BindableBase
    {
        #region Fields
        private string name;
        private Brush colorBrush;
        private Brush borderColorBrush;
        #endregion Fields

        #region Constructor
        /// <summary>Initializes a new instance of the <see cref="ApplicationTheme" /> class.</summary>
        public ApplicationTheme()
        {
            var container = ServiceLocator.Current.GetInstance<IUnityContainer>();
            container.Resolve<ILoggerFacade>().Log("ApplicationTheme initializes", Category.Info, Priority.None);
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

        /// <summary>Gets or sets the border color brush.</summary>
        /// <value>The border color brush.</value>
        public Brush BorderColorBrush
        {
            get { return borderColorBrush; }
            set { SetProperty(ref borderColorBrush, value); }
        }
        #endregion Properties
    }
}