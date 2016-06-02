namespace HelperTools.Infrastructure.Services
{
    using Constants;
    using Events;
    using Microsoft.Practices.ServiceLocation;
    using Microsoft.Practices.Unity;
    using Newtonsoft.Json;
    using Prism.Events;
    using Prism.Logging;
    using Properties;
    using System;
    using System.IO;
    using System.Reflection;

    /// <summary>The JsonService.</summary>
    public class JsonService
    {
        #region Enum
        /// <summary>Defines the area where the JSON file is cached.</summary>
        public enum StoringArea
        {
            /// <summary>Just keep in memory.</summary>
            None,

            /// <summary>The application configuration area.</summary>
            ApplicationConfig,

            /// <summary>The appdata temp folder.</summary>
            Tempfolder
        }
        #endregion Enum

        #region Fields
        private readonly IUnityContainer unityContainer;
        private readonly IEventAggregator eventAggregator;
        #endregion Fields

        #region Constructor
        /// <summary>Initializes a new instance of the <see cref="JsonService" /> class.</summary>
        public JsonService()
        {
            unityContainer = ServiceLocator.Current.GetInstance<IUnityContainer>();
            eventAggregator = ServiceLocator.Current.GetInstance<IEventAggregator>();
        }
        #endregion Constructor

        #region Methods
        /// <summary>Reads the JSON-File.</summary>
        /// <typeparam name="T">The type of the object to deserialize to.</typeparam>
        /// <param name="filename">The filename of the JSON file.</param>
        /// <param name="storingArea">The storing area.</param>
        /// <returns>The deserialized object from the JSON string.</returns>
        public T ReadJson<T>(string filename, StoringArea storingArea = StoringArea.ApplicationConfig)
        {
            try
            {
                //var file = forUpdates ? filename : PathNames.ConfigPath + filename;
                string file;

                switch (storingArea)
                {
                    case StoringArea.None:
                        file = filename;
                        break;
                    case StoringArea.ApplicationConfig:
                        file = PathNames.ConfigPath + filename;
                        break;
                    case StoringArea.Tempfolder:
                        file = PathNames.TempFolderPath + filename;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(storingArea), storingArea, null);
                }

                if (!File.Exists(file)) return new JsonSerializer().Deserialize<T>(null);

                var logMessage = $"[{GetType().Name}] JSON ({file}) was read";
                unityContainer.Resolve<ILoggerFacade>().Log(logMessage, Category.Debug, Priority.None);

                eventAggregator.GetEvent<StatusBarMessageUpdateEvent>().Publish(Resources.StatusBarReadSettings);

                return JsonConvert.DeserializeObject<T>(File.ReadAllText(file));
            }
            catch (Exception ex)
            {
                var logMessage = $"[{GetType().Name}] Exception at {MethodBase.GetCurrentMethod()}: {ex}";
                unityContainer.Resolve<ILoggerFacade>().Log(logMessage, Category.Exception, Priority.High);

                DialogService.Exception(ex, DialogService.ExceptionType.Universal);

                return new JsonSerializer().Deserialize<T>(null);
            }
        }

        /// <summary>Writes the JSON-File.</summary>
        /// <param name="settings">The complete object of the settings.</param>
        /// <param name="filename">The filename of the JSON file.</param>
        /// <param name="storingArea">The storing area.</param>
        public void WriteJson(object settings, string filename, StoringArea storingArea = StoringArea.ApplicationConfig)
        {
            try
            {
                string file;

                switch (storingArea)
                {
                    case StoringArea.None:
                        file = filename;
                        break;
                    case StoringArea.ApplicationConfig:
                        if (!Directory.Exists(PathNames.ConfigPath)) Directory.CreateDirectory(PathNames.ConfigPath);
                        file = PathNames.ConfigPath + filename;
                        break;
                    case StoringArea.Tempfolder:
                        if (!Directory.Exists(PathNames.TempFolderPath)) Directory.CreateDirectory(PathNames.TempFolderPath);
                        file = PathNames.TempFolderPath + filename;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(storingArea), storingArea, null);
                }

                File.WriteAllText(file, JsonConvert.SerializeObject(settings, Formatting.Indented));

                var logMessage = $"[{GetType().Name}] JSON ({file}) was writen";
                unityContainer.Resolve<ILoggerFacade>().Log(logMessage, Category.Debug, Priority.None);
            }
            catch (Exception ex)
            {
                var logMessage = $"[{GetType().Name}] Exception at {MethodBase.GetCurrentMethod()}: {ex}";
                unityContainer.Resolve<ILoggerFacade>().Log(logMessage, Category.Exception, Priority.High);

                DialogService.Exception(ex, DialogService.ExceptionType.Universal);
            }
        }
        #endregion Methods
    }
}