namespace HelperTools.Shell
{
    using System.Windows;

    /// <summary>Interaction logic for App.xaml</summary>
    public partial class App
    {
        #region Overrides of Application
        /// <summary>Löst das <see cref="E:System.Windows.Application.Startup" />-Ereignis aus.</summary>
        /// <param name="e">Ein <see cref="T:System.Windows.StartupEventArgs" />, das die Ereignisdaten enthält.</param>
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            Bootstrapper bootstrapper = new Bootstrapper();
            bootstrapper.Run();
        }
        #endregion
    }
}