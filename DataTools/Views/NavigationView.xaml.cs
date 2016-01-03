using DataTools.ViewModels;

namespace DataTools.Views
{
    /// <summary>
    /// Interaktionslogik für NavigationView.xaml
    /// </summary>
    public partial class NavigationView
    {
        public NavigationView()
        {
            InitializeComponent();
        }

        public NavigationView(NavigationViewModel viewModel) : this()
        {
            DataContext = viewModel;
        }
    }
}