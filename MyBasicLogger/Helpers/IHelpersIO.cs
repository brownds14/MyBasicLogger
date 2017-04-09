using System;
using System.IO;

namespace MyBasicLogger.Helpers
{
    /// <summary>
    /// Provides functions to help interface with the file system
    /// </summary>
    public interface IHelpersIO
    {
        /// <summary>
        /// Creates a directory for the given path
        /// </summary>
        /// <param name="directory">Path for the directory to be created</param>
        void CreateDirectory(string directory);

        /// <summary>
        /// Checks if a directory exists
        /// </summary>
        /// <param name="directory">File path to the directory</param>
        /// <returns>Returns true if the directory exists; false otherwise</returns>
        bool DirectoryExists(string directory);

        /// <summary>
        /// Opens a stream
        /// </summary>
        /// <param name="directory">Directory to open the stream in</param>
        /// <param name="fileName">File name for the stream</param>
        /// <returns>Returns an opened Stream</returns>
        Stream OpenStream(string directory, string fileName);

        /// <summary>
        /// Deletes old files in a directory
        /// </summary>
        /// <param name="directory">Directory to delete old files</param>
        /// <param name="date">Files older than this date will be deleted</param>
        void DeleteOldFiles(string directory, DateTime date);
    }
}
