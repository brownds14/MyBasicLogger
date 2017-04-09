using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyBasicLogger;
using MyBasicLogger.Loggers;

namespace LoggingToFileExample
{
    class Program
    {
        private static ILog _logger;
        static void Main(string[] args)
        {
            try
            {
                _logger = BasicLoggerManager.CreateLogger();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
            }
            _logger.Fatal("Hello World");
            Console.WriteLine("All done");
            Console.Read();
        }
    }
}
