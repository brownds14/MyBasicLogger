using System;
using System.IO;
using System.Text;
using System.Threading;
using MyBasicLogger.Settings;
using MyBasicLogger.Helpers;

namespace MyBasicLogger.Loggers
{
    internal sealed class LogToFile : LogBase, IDisposable
    {
        private Stream _fs;
        private string _currentFS;
        private TimeSpan _logRetention;
        private string _directory;
        private Timer _timer;
        private SemaphoreSlim _lock;
        private IHelpersIO _ioHelper;

        internal LogToFile(SettingsConfig settings, IHelpersIO helper)
        {
            _ioHelper = helper;
            _directory = settings.Directory;
            _logRetention = new TimeSpan(settings.LogRetention * -1 , 0, 0, 0);
            _timer = new Timer(CleanUpOldLogs, null, new TimeSpan(0), new TimeSpan(1, 0, 0, 0));
            _lock = new SemaphoreSlim(1);
            _fs = null;
            _currentFS = "";
            currentLogLevel = settings.LogLevel;
        }

        ~LogToFile()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Dispose(bool safe)
        {
            if (safe)
            {
                _timer.Dispose();
                _fs.Dispose();
            }
        }

        private Stream GetStream(DateTime date)
        {
            if (String.CompareOrdinal(_currentFS, date.ToString(FileDateTimeStr)) != 0)
            {
                string dateStr = date.ToString(FileDateTimeStr);
                _ioHelper.CreateDirectory(_directory);
                _currentFS = dateStr;

                //Clear up old Stream
                if (_fs != null)
                {
                    _fs.Flush();
                    _fs.Dispose();
                    _fs = null;
                }

                _fs = _ioHelper.OpenStream(_directory, $"{dateStr}.txt");
            }

            return _fs;
        }

        /// <summary>
        /// Write a log to the FileStream
        /// </summary>
        /// <param name="type">Severity of the log</param>
        /// <param name="msg">Message for the log</param>
        /// <param name="e">Exception associated with the log</param>
        public async override void Log(LogType type, string msg, Exception e)
        {
            await _lock.WaitAsync();
            try
            {
                if (CanLog(type))
                {
                    DateTime date = DateTime.Now;
                    _fs = GetStream(date);
                    string log = FormatLog(date, type, msg, e);
                    byte[] bytes = Encoding.UTF8.GetBytes(log);
                    await _fs.WriteAsync(bytes, 0, bytes.Length);
                    await _fs.FlushAsync();
                }
            }
            finally
            {
                _lock.Release();
            }
        }

        /// <summary>
        /// Cleans out old logs in the log directory
        /// </summary>
        /// <param name="ignore">Parameter not used. Pass in any object</param>
        protected override void CleanUpOldLogs(object ignore)
        {
            _ioHelper.DeleteOldFiles(_directory, DateTime.Now.Add(_logRetention));
        }
    }
}
