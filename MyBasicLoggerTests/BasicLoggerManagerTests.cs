using System;
using Xunit;
using MyBasicLogger.Loggers;
using System.Threading.Tasks;

namespace MyBasicLogger.Tests
{
    /// <summary>
    /// Tests for BasicLoggerManager
    /// </summary>
    public class BasicLoggerManagerTests
    {
        /// <summary>
        /// BasicLoggerManager should only hold one instance of the logger
        /// </summary>
        [Fact]
        public void SingleLoggerUse()
        {
            ILog log1 = BasicLoggerManager.CreateLogger();
            ILog log2 = BasicLoggerManager.CreateLogger();
            Assert.Same(log1, log2);
        }

        /// <summary>
        /// BasicLoggerManager is a singleton class so only one instance of it should exist
        /// </summary>
        [Fact]
        public void SingleManagerUse()
        {
            var instance1 = BasicLoggerManager.GetInstance();
            var instance2 = BasicLoggerManager.GetInstance();
            Assert.Same(instance1, instance2);
        }

        [Fact]
        public void SingleLoggerAcrossMultipleThreads()
        {
            Task<ILog> t1 = new Task<ILog>(() => GetLogger());
            Task<ILog> t2 = new Task<ILog>(() => GetLogger());
            Task<ILog> t3 = new Task<ILog>(() => GetLogger());
            Task<ILog> t4 = new Task<ILog>(() => GetLogger());

            t1.Start();
            t2.Start();
            t3.Start();
            t4.Start();

            Task.WhenAll(t1, t2, t3, t4);

            bool success = false;
            if (Object.ReferenceEquals(t1.Result, t2.Result) &&
                Object.ReferenceEquals(t2.Result, t3.Result) &&
                Object.ReferenceEquals(t3.Result, t4.Result))
                success = true;

            Assert.True(success);
        }

        private ILog GetLogger()
        {
            return BasicLoggerManager.CreateLogger();
        }
    }
}
