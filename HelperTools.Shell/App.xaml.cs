namespace HelperTools.Shell
{
    using System.IO;
    using System.Windows;
    using Infrastructure.Constants;

    /// <summary>Interaction logic for App.xaml</summary>
    public partial class App
    {
        #region Overrides of Application
        /// <summary>Raises the <see cref="E: System.Windows.Application.Startup" /> - event.</summary>
        /// <param name="e">A <see cref="T: System.Windows.StartupEventArgs" /> that contains the event data.</param>
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            Bootstrapper bootstrapper = new Bootstrapper();
            bootstrapper.Run();
        }

        /// <summary>Raises the <see cref="E: System.Windows.Application.Exit" /> - event.</summary>
        /// <param name="e">A <see cref="T: System.Windows.ExitEventArgs" /> that contains the event data.</param>
        protected override void OnExit(ExitEventArgs e)
        {
            if (Directory.Exists(PathNames.TempFolderPath)) Directory.Delete(PathNames.TempFolderPath, true);

            base.OnExit(e);
        }
        #endregion
    }
}