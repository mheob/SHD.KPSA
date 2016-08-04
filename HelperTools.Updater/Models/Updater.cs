using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using HelperTools.Infrastructure.Constants;
using HelperTools.Infrastructure.Services;
using HelperTools.Updater.Properties;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using Prism.Events;
using Prism.Logging;

namespace HelperTools.Updater.Models
{
    /// <summary>The GenerateThumb.</summary>
    public class Updater
    {
        #region Fields
        private readonly IUnityContainer unityContainer;
        #endregion Fields

        #region Constructor
        /// <summary>Initializes a new instance of the <see cref="Updater" /> class.</summary>
        public Updater(Uri location)
        {
            unityContainer = ServiceLocator.Current.GetInstance<IUnityContainer>();
            
            UpdateApplication(location);

            var logMessage = $"[{GetType().Name}] is initialized";
            unityContainer.Resolve<ILoggerFacade>().Log(logMessage, Category.Debug, Priority.None);
        }
        #endregion Constructor

        #region Methods
        private void UpdateApplication(Uri location)
        {
            var tmpPath = PathNames.TempFolderPath;
            if (!Directory.Exists(tmpPath)) Directory.CreateDirectory(tmpPath);

            var tmpFile = tmpPath + Settings.Default.UpdatedSetupFile;

            if (!File.Exists(tmpFile) || (File.GetCreationTimeUtc(tmpFile).Date - DateTime.Now).TotalHours <= 1)
            {
                if (!WebService.ExistsOnServer(location)) return;

                new WebClient().DownloadFile(location.ToString(), tmpFile);
            }

            Process.Start(new ProcessStartInfo { FileName = tmpFile });

            Application.Current.Shutdown();

            var logMessage = $"[{GetType().Name}] update progress prepared";
            unityContainer.Resolve<ILoggerFacade>().Log(logMessage, Category.Debug, Priority.None);
        }
        #endregion Methods
    }
}
