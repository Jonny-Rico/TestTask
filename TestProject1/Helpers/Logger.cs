using System;

namespace TestProject1.Helpers
{
    /// <summary>
    /// Logging utility to use across the test framework.
    /// Used for adding messages to log to console during tests execution
    /// </summary>
    public class Logger
    {
        [ThreadStatic]
        private static Logger log = null;

        // This is the logger instance that is directly used in framework to perform logging
        public static Logger Log
        {
            get
            {
                if (log == null)
                {
                    log = new Logger();
                }

                return log;
            }
        }

        private Logger() { }

        private class LogLevel
        {
            public static string Info = "I";
            public static string Warning = "W";
            public static string Error = "E";
        }

        /// <summary>
        /// Log message with Info level
        /// </summary>
        /// <param name="message">string message to write into log</param>
        public void Info(string message)
        {
            AddMessage(LogLevel.Info, message);
        }

        /// <summary>
        /// Log message with Warning level
        /// </summary>
        /// <param name="message">string message to write into log</param>
        public void Warning(string message)
        {
            AddMessage(LogLevel.Warning, message);
        }

        /// <summary>
        /// Log message with Error level
        /// </summary>
        /// <param name="message">string message to write into log</param>
        public void Error(string message)
        {
            AddMessage(LogLevel.Error, message);
        }

        /// <summary>
        /// Method to append message to the console with specified log level and text
        /// </summary>
        /// <param name="level">log level</param>
        /// <param name="message">message text to add to reporting target</param>
        private void AddMessage(string level, string message)
        {
            var time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            var line = $"{time} [{level}] {message}";

            Console.WriteLine($"{line}:    {message}");
        }
    }
}
