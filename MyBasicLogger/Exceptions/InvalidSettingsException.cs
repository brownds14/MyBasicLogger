using System;

namespace MyBasicLogger.Exceptions
{
    /// <summary>
    /// When a setting is incorrectly parsed from the settings file
    /// </summary>
    public class InvalidSettingsException : Exception
    {
        /// <summary>
        /// Initializes a new Exception with a blank message
        /// </summary>
        public InvalidSettingsException() : base() { }

        /// <summary>
        /// Initializes a new Exception with a internal message
        /// </summary>
        /// <param name="msg">Internal message for the Exception</param>
        public InvalidSettingsException(string msg) : base(msg) { }
    }
}
