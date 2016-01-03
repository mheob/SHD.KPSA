using System;
using System.Windows;
using MahApps.Metro;

namespace DataTools
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            ThemeManager.AddAccent("DarkBlue", new Uri("pack://application:,,,/Skins/Accents/DarkBlue.xaml"));
            ThemeManager.AddAppTheme("Dark", new Uri("pack://application:,,,/Skins/Accents/BaseDark.xaml"));
            ThemeManager.AddAppTheme("Light", new Uri("pack://application:,,,/Skins/Accents/BaseLight.xaml"));

            ThemeManager.ChangeAppStyle(Current, ThemeManager.GetAccent("DarkBlue"), ThemeManager.GetAppTheme("Dark"));

            new BootStrapper().Run();
        }
    }
}