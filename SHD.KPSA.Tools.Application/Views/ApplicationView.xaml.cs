namespace SHD.KPSA.Tools.Application.Views
{
    using ViewModels;

    /// <summary>
    /// Interaction logic for ApplicationView.xaml
    /// </summary>
    public partial class ApplicationView
    {
        /// <summary>
        /// Initialize a new instance of the ApplicationView class.
        /// </summary>
        public ApplicationView()
        {
            InitializeComponent();

            DataContext = new ApplicationViewModel();
        }
    }
}