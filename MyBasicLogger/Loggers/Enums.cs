namespace MyBasicLogger.Loggers
{
    /// <summary>
    /// Type of logging
    /// </summary>
    public enum LoggerType
    {
        /// <summary>Log to a file</summary>
        FILE
    }

    /// <summary>
    /// Severity of the log
    /// </summary>
    public enum LogType
    {
        /// <summary>Log to help debug issues</summary>
        DEBUG = 1,
        /// <summary>Useful information pertaining to the functioning of the program</summary>
        INFO,
        /// <summary>Unexpected results that need to be logged</summary>
        WARN,
        /// <summary>Unexpected errors that could be recovered from</summary>
        ERROR,
        /// <summary>Unexpected errors that could not be recovered from</summary>
        FATAL
    }

    /// <summary>
    /// Determines what severity of logs are logged
    /// </summary>
    public enum LogLevel
    {
        /// <summary>Nothing is logged</summary>
        NONE,
        /// <summary>Log severity Debug and above are logged</summary>
        DEBUG,
        /// <summary>Log severity Info and above are logged</summary>
        INFO,
        /// <summary>Log severity Warn and above are logged</summary>
        WARN,
        /// <summary>Log severity Error and above are logged</summary>
        ERROR,
        /// <summary>Log severity Fatal and above are logged</summary>
        FATAL,
        /// <summary>Everything is logged</summary>
        ALL
    }
}
