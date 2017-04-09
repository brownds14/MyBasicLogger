using System;
using System.Text;

namespace MyBasicLogger.Loggers
{
    /// <summary>
    /// Base class for loggers
    /// </summary>
    public abstract class LogBase : ILog
    {
        /// <summary>
        /// Determines what severity of logs should be logged
        /// </summary>
        protected LogLevel currentLogLevel = LogLevel.INFO;

        /// <summary>
        /// Format for log file name
        /// </summary>
        protected static readonly string FileDateTimeStr = "yyyy-MM-dd";

        /// <summary>
        /// Format for the time a log was created
        /// </summary>
        protected static readonly string LogDateTimeStr = "HH:mm";

        /// <summary>
        /// Logs a message
        /// </summary>
        /// <param name="type">Severity of the log</param>
        /// <param name="msg">Message for the log</param>
        /// <param name="e">Exception associated with the log</param>
        public abstract void Log(LogType type, string msg, Exception e);

        /// <summary>
        /// Cleans out old logs in the log directory
        /// </summary>
        /// <param name="ignore">Parameter not used. Pass in any object</param>
        ///
        protected abstract void CleanUpOldLogs(object ignore);

        /// <summary>
        /// Logs a DEBUG message
        /// </summary>
        /// <param name="msg">Message for the log</param>
        /// <param name="e">Exception associated with the log</param>
        public void Debug(string msg, Exception e)
        {
            Log(LogType.DEBUG, msg, e);
        }

        /// <summary>
        /// Logs a INFO message
        /// </summary>
        /// <param name="msg">Message for the log</param>
        /// <param name="e">Exception associated with the log</param>
        public void Info(string msg, Exception e)
        {
            Log(LogType.INFO, msg, e);
        }

        /// <summary>
        /// Logs a WARN message
        /// </summary>
        /// <param name="msg">Message for the log</param>
        /// <param name="e">Exception associated with the log</param>
        public void Warn(string msg, Exception e)
        {
            Log(LogType.WARN, msg, e);
        }

        /// <summary>
        /// Logs a ERROR message
        /// </summary>
        /// <param name="msg">Message for the log</param>
        /// <param name="e">Exception associated with the log</param>
        public void Error(string msg, Exception e)
        {
            Log(LogType.ERROR, msg, e);
        }

        /// <summary>
        /// Logs a FATAL message
        /// </summary>
        /// <param name="msg">Message for the log</param>
        /// <param name="e">Exception associated with the log</param>
        public void Fatal(string msg, Exception e)
        {
            Log(LogType.FATAL, msg, e);
        }

        /// <summary>
        /// Returns a of the LogType
        /// </summary>
        /// <param name="type">LogType to be converted to a string</param>
        /// <returns>string</returns>
        protected string LogTypeString(LogType type)
        {
            return Enum.GetName(typeof(LogType), type);
        }

        /// <summary>
        /// Determines if the severity of the log is high enough to be logged
        /// </summary>
        /// <param name="type">Severity of the log</param>
        /// <returns>bool</returns>
        protected bool CanLog(LogType type)
        {
            return (int)type >= (int)currentLogLevel;
        }

        /// <summary>
        /// Formats the log
        /// </summary>
        /// <param name="date">Date the log was created</param>
        /// <param name="type">Severity of the log</param>
        /// <param name="msg">Log message</param>
        /// <param name="e">Exception associated with the log</param>
        /// <returns></returns>
        protected string FormatLog(DateTime date, LogType type, string msg, Exception e)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("[").Append(DateTime.Now.ToString(LogDateTimeStr)).Append(" ").Append(LogTypeString(type)).Append("] ");
            sb.AppendLine(msg);

            if (e != null)
            {
                sb.Append("\t");
                sb.AppendLine(e.Message);
                sb.AppendLine(e.StackTrace);
            }

            return sb.ToString();
        }
    }
}
