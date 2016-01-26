using SHD.KPSA.Tools.Application.ViewModels;

namespace SHD.KPSA.Tools.Application.Views
{
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