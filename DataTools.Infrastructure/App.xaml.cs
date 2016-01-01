using System.Windows;

namespace DataTools.Infrastructure
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            BootStrapper bootStrapper = new BootStrapper();
            bootStrapper.Run();
        }
    }
}