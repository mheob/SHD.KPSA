namespace SHD.KPSA.Tools.Application.Views
{
    using ViewModels;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class ApplicationView
    {
        public ApplicationView()
        {
            InitializeComponent();

            DataContext = new ApplicationViewModel();
        }
    }
}