using DataTools.Utils;
using DataTools.Views;
using MahApps.Metro;
using Microsoft.Practices.ServiceLocation;
using Prism.Regions;
using System.Windows;

namespace DataTools
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public static string Theme { get; private set; } =
            ThemeManager.DetectAppStyle(Application.Current).Item1.Name;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonTheme_OnClick(object sender, RoutedEventArgs e)
        {
            Theme = Theme != "Dark" ? "Dark" : "Light";

            ThemeManager.ChangeAppStyle(Application.Current, ThemeManager.GetAccent("DarkBlue"),
                ThemeManager.GetAppTheme(Theme));
        }

        private void ButtonChangelog_OnClick(object sender, RoutedEventArgs e)
        {
            var regionManager = ServiceLocator.Current.GetInstance<IRegionManager>();

            regionManager.RequestNavigate(Constants.RegionContent, typeof (Changelog).Name);
        }
    }
}