using System;
using MyBasicLogger.Settings;
using MyBasicLogger.Loggers;
using System.IO.Abstractions;
using MyBasicLogger.Helpers;

namespace MyBasicLogger
{
    /// <summary>
    /// Singleton factory class to build a new logger based the settings
    /// </summary>
    public sealed class BasicLoggerManager
    {
        private static readonly BasicLoggerManager _instance = new BasicLoggerManager();
        private static ILog _logger;
        private static object _lock = new object();

        private BasicLoggerManager()
        {
            _logger = null;
        }

        /// <summary>
        /// Creates a new logger instance based on the settings xml
        /// </summary>
        /// <param name="path">File path to the settings xml</param>
        public static ILog CreateLogger(string path = null)
        {
            lock (_lock)
            {
                if (_logger == null)
                {
                    SettingsConfig settings = new SettingsConfig(path);
                    settings.LoadSettings();
                    switch (settings.LoggingType)
                    {
                        case LoggerType.FILE:
                            FileSystem fs = new FileSystem();
                            HelpersIO helper = new HelpersIO(fs);
                            _logger = new LogToFile(settings, helper);
                            break;
                    }
                }
            }
            return _logger;
        }

        /// <summary>
        /// Returns the instance of this class
        /// </summary>
        /// <returns>BasicLoggerManager</returns>
        public static BasicLoggerManager GetInstance()
        {
            return _instance;
        }

        /// <summary>
        /// Returns the instance of the logger
        /// </summary>
        /// <returns>ILog</returns>
        public static ILog GetLogger()
        {
            return _logger;
        }
    }
}
