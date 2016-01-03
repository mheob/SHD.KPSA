using DataTools.ViewModels;

namespace DataTools.Views
{
    /// <summary>
    /// Interaktionslogik für Tools3DsView.xaml
    /// </summary>
    public partial class Tools3DsView
    {
        public Tools3DsView()
        {
            InitializeComponent();
        }

        public Tools3DsView(Tools3DsViewModel viewModel) : this()
        {
            DataContext = viewModel;
        }
    }
}