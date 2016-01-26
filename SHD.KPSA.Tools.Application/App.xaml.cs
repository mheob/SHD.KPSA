using System;
using System.Windows;
using MahApps.Metro;
using SHD.KPSA.Tools.Application.Properties;
using SHD.KPSA.Tools.Application.ViewModels;
using SHD.KPSA.Tools.Application.Views;

namespace SHD.KPSA.Tools.Application
{
    /// <summary>
    /// Interaction logic for "App.xaml"
    /// </summary>
    public partial class App
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            ApplicationView app = new ApplicationView();
            ApplicationViewModel context = new ApplicationViewModel();

            ThemeManager.AddAccent(context.AccentDarkBlue, new Uri(Settings.Default.AccentDarkBluePath));
            ThemeManager.AddAppTheme(context.ThemeDark, new Uri(Settings.Default.ThemeDarkPath));
            ThemeManager.AddAppTheme(context.ThemeLight, new Uri(Settings.Default.ThemeLightPath));

            ThemeManager.ChangeAppStyle(Current, ThemeManager.GetAccent(context.AccentDarkBlue),
                ThemeManager.GetAppTheme(context.ThemeDark));

            app.DataContext = context;
            app.Show();
        }
    }
}