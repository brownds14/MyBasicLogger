using System;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Xml;
using System.Text;
using System.IO;
using System.Linq;
using MyBasicLogger.Loggers;
using MyBasicLogger.Exceptions;

namespace MyBasicLogger.Settings
{
    /// <summary>
    /// Class to hold all the settings for the logger
    /// </summary>
    internal sealed class SettingsConfig
    {
        private delegate void Setter(string property, XElement element);

        //Hold the classes property names and their accompaning setter
        private static Dictionary<string, Setter> _setters;

        //Path to the settings file
        internal string SettingsPath { get; private set; } = $@"{AppDomain.CurrentDomain.BaseDirectory}LoggerSettings.xml";

        internal LogLevel LogLevel { get; private set; } = LogLevel.INFO;

        internal LoggerType LoggingType { get; private set; } = LoggerType.FILE;

        internal UInt16 LogRetention { get; private set; } = 3;

        internal string Directory { get; private set; } = $@"{AppDomain.CurrentDomain.BaseDirectory}Logs\";

        /// <summary>
        /// Create a new instance of SettingsConfig with default path to settings
        /// </summary>
        internal SettingsConfig() : this(null) { }

        /// <summary>
        /// Create a new instance of SettingsConfig with a custom path to settings
        /// </summary>
        /// <param name="path"></param>
        internal SettingsConfig(string path)
        {
            if (path != null)
                SettingsPath = path;

            _setters = new Dictionary<string, Setter>()
            {
                { "LogLevel", SetLogLevel },
                { "LoggingType", SetLoggingType },
                { "Directory", SetDirectory },
                { "LogRetention", SetLogRetention }
            };
        }

        internal void SetProperty(string property, XElement element)
        {
            if (element != null && _setters.ContainsKey(property))
            {
                Setter setter = _setters[property];
                setter(property, element);
            }
        }

        private Exception BuildInvalidSettingsException(string property, XElement element)
        {
            StringBuilder sb = new StringBuilder();
            IXmlLineInfo line = element;

            sb.Append("There is an invalid setting at Line: ").Append(line.LineNumber)
                .Append(" Pos: ").Append(line.LinePosition).Append(" in your settings config file. ")
                .Append("'").Append(property).Append("' could not be set to '").
                Append(element.Value).AppendLine("'.");

            return new InvalidSettingsException(sb.ToString());
        }

        //Methods to validate then set new values to properties of this class 
        #region Setters
        private void SetLogLevel(string property, XElement element)
        {
            LogLevel tmp = LogLevel;
            if (!Enum.TryParse(element.Value, true, out tmp))
                throw BuildInvalidSettingsException(property, element);
            else
                LogLevel = tmp;
        }

        private void SetLoggingType(string property, XElement element)
        {
            LoggerType tmp = LoggingType;
            if (!Enum.TryParse(element.Value, true, out tmp))
                throw BuildInvalidSettingsException(property, element);
            else
                LoggingType = tmp;
        }

        private void SetLogRetention(string property, XElement element)
        {
            UInt16 tmp = LogRetention;
            bool success = UInt16.TryParse(element.Value, out tmp);
            if (tmp == 0 || !success)
                throw BuildInvalidSettingsException(property, element);
            else
                LogRetention = tmp;
        }

        private void SetDirectory(string property, XElement element)
        {
            Directory = element.Value;
        }
        #endregion

        //Methods to Save and Load the settings xml file
        #region SettingsXmlMethods
        internal void SaveSettings()
        {
            //XmlSerializer xs = new XmlSerializer(typeof(SettingsConfig));
            //using (FileStream _fs = new FileStream(SettingsPath, FileMode.Create, FileAccess.Write))
            //{
            //    xs.Serialize(_fs, this);
            //}
            XDocument doc = new XDocument(
                new XDeclaration("1.0", "UTF8", null),
                new XElement("SettingsConfig",
                    new XElement("LogLevel", LogLevel),
                    new XElement("LoggingType", LoggingType),
                    new XElement("LogRetention", LogRetention),
                    new XElement("Directory", Directory)));
            doc.Save(SettingsPath);
        }

        internal void LoadSettings()
        {
            if (!File.Exists(SettingsPath))
                SaveSettings();

            XDocument doc = XDocument.Load(SettingsPath, LoadOptions.SetLineInfo);
            ParseSettingsElements(doc.Root);
        }

        private void ParseSettingsElements(XElement element)
        {
            var elements = element.Descendants();
            if (elements.Count() == 0)
                SetProperty(element.Name.LocalName, element);
            else
                foreach (var e in elements)
                    ParseSettingsElements(e);
        }
        #endregion
    }
}
