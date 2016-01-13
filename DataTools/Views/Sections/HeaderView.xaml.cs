using DataTools.ViewModels.Sections;

namespace DataTools.Views.Sections
{
    /// <summary>
    /// Interaktionslogik für HeaderView.xaml
    /// </summary>
    public partial class HeaderView
    {
        public HeaderView()
        {
            InitializeComponent();
        }

        public HeaderView(HeaderViewModel viewModel) : this()
        {
            DataContext = viewModel;
        }
    }
}