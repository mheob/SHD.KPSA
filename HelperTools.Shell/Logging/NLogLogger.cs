namespace HelperTools.Shell.Logging
{
    using NLog;
    using Prism.Logging;

    /// <summary>The NLogLogger class.</summary>
    /// <seealso cref="ILoggerFacade" />
    public class NLogLogger : ILoggerFacade
    {
        #region Fields
        private readonly Logger logger = LogManager.GetCurrentClassLogger();
        #endregion Members and Constants 

        #region ILoggerFacade Members
        /// <summary>Write a new log entry with the specified category and priority.</summary>
        /// <param name="message">Message body to log.</param>
        /// <param name="category">Category of the entry.</param>
        /// <param name="priority">The priority of the entry.</param>
        public void Log(string message, Category category = Category.Info, Priority priority = Priority.None)
        {
            switch (category)
            {
                case Category.Debug:
                    logger.Debug(message);
                    break;
                case Category.Info:
                    logger.Info(message);
                    break;
                case Category.Warn:
                    logger.Warn(message);
                    break;
                case Category.Exception:
                    logger.Error(message);
                    break;
                default:
                    logger.Info(message);
                    break;
            }
        }
        #endregion ILoggerFacade Members
    }
}