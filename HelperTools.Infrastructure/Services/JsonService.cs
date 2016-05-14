namespace HelperTools.Infrastructure.Services
{
    using System;
    using System.IO;
    using System.Reflection;
    using Constants;
    using Events;
    using Microsoft.Practices.ServiceLocation;
    using Microsoft.Practices.Unity;
    using Newtonsoft.Json;
    using Prism.Events;
    using Prism.Logging;
    using Properties;

    /// <summary>The JsonService.</summary>
    public class JsonService
    {
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
        /// <returns>The deserialized object from the JSON string.</returns>
        public T ReadJson<T>(string filename)
        {
            try
            {
                var serializer = new JsonSerializer();

                var file = PathNames.ConfigPath + filename;
                if (!File.Exists(file)) return serializer.Deserialize<T>(null);


                using (StreamReader sr = new StreamReader(file))
                using (JsonTextReader reader = new JsonTextReader(sr))
                {
                    var logMessage = $"[{GetType().Name}] JSON ({filename}) was read";
                    unityContainer.Resolve<ILoggerFacade>().Log(logMessage, Category.Debug, Priority.None);

                    eventAggregator.GetEvent<StatusBarMessageUpdateEvent>().Publish(Resources.StatusBarReadSettings);

                    return serializer.Deserialize<T>(reader);
                }
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
        public void WriteJson(object settings, string filename)
        {
            try
            {
                var serializer = new JsonSerializer {Formatting = Formatting.Indented};

                if (!Directory.Exists(PathNames.ConfigPath)) Directory.CreateDirectory(PathNames.ConfigPath);

                using (StreamWriter sw = new StreamWriter(PathNames.ConfigPath + filename, false))
                using (JsonWriter writer = new JsonTextWriter(sw))
                {
                    var logMessage = $"[{GetType().Name}] JSON ({filename}) was write";
                    unityContainer.Resolve<ILoggerFacade>().Log(logMessage, Category.Debug, Priority.None);

                    serializer.Serialize(writer, settings);
                }
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