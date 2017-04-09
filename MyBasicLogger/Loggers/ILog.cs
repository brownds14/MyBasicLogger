using System;

namespace MyBasicLogger.Loggers
{
    /// <summary>
    /// Interface for loggers
    /// </summary>
    public interface ILog
    {
        /// <summary>
        /// Logs a message
        /// </summary>
        /// <param name="type">Severity of the log</param>
        /// <param name="msg">Message for the log</param>
        /// <param name="e">Exception associated with the log</param>
        void Log(LogType type, string msg, Exception e = null);

        /// <summary>
        /// Logs a DEBUG message
        /// </summary>
        /// <param name="msg">Message for the log</param>
        /// <param name="e">Exception associated with the log</param>
        void Debug(string msg, Exception e = null);

        /// <summary>
        /// Logs a INFO message
        /// </summary>
        /// <param name="msg">Message for the log</param>
        /// <param name="e">Exception associated with the log</param>
        void Info(string msg, Exception e = null);

        /// <summary>
        /// Logs a WARN message
        /// </summary>
        /// <param name="msg">Message for the log</param>
        /// <param name="e">Exception associated with the log</param>
        void Warn(string msg, Exception e = null);

        /// <summary>
        /// Logs a ERROR message
        /// </summary>
        /// <param name="msg">Message for the log</param>
        /// <param name="e">Exception associated with the log</param>
        void Error(string msg, Exception e = null);

        /// <summary>
        /// Logs a FATAL message
        /// </summary>
        /// <param name="msg">Message for the log</param>
        /// <param name="e">Exception associated with the log</param>
        void Fatal(string msg, Exception e = null);
    }
}
