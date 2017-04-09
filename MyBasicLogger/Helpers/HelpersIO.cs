using System;
using System.IO;
using System.IO.Abstractions;

namespace MyBasicLogger.Helpers
{
    internal class HelpersIO : IHelpersIO
    {
        private IFileSystem _fileSystem;

        internal HelpersIO(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
        }

        /// <summary>
        /// Creates a directory for the given path
        /// </summary>
        /// <param name="directory">Path for the directory to be created</param>
        public void CreateDirectory(string directory)
        {
            if (!DirectoryExists(directory))
            {
                _fileSystem.Directory.CreateDirectory(directory);
            }
        }

        /// <summary>
        /// Checks if a directory exists
        /// </summary>
        /// <param name="directory">File path to the directory</param>
        /// <returns>Returns true if the directory exists; false otherwise</returns>
        public bool DirectoryExists(string directory)
        {
            return _fileSystem.Directory.Exists(directory);
        }

        /// <summary>
        /// Opens a stream
        /// </summary>
        /// <param name="directory">Directory to open the stream in</param>
        /// <param name="fileName">File name for the stream</param>
        /// <returns>Returns an opened Stream</returns>
        public Stream OpenStream(string directory, string fileName)
        {
            CreateDirectory(directory);
            string path = _fileSystem.Path.Combine(directory, fileName);
            return new FileStream(path, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
        }

        /// <summary>
        /// Deletes old files in a directory
        /// </summary>
        /// <param name="directory">Directory to delete old files</param>
        /// <param name="date">Files older than this date will be deleted</param>
        public void DeleteOldFiles(string directory, DateTime date)
        {
            if (DirectoryExists(directory))
            {
                string[] oldLogs = _fileSystem.Directory.GetFiles(directory);
                foreach (var log in oldLogs)
                    if (_fileSystem.File.GetCreationTime(log) <= date)
                        _fileSystem.File.Delete(log);
            }
        }
    }
}
