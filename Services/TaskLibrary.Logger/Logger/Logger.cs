using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskLibrary.Logger
{
    public class Logger : ILogger
    {
        private readonly Serilog.ILogger logger;

        public Logger(Serilog.ILogger logger)
        {
            this.logger = logger;
        }

        public Logger()
        {
        }

        private string consructMessage(string message)
        {
            return $"[TaskLibrary] {message} ";
        }

        public void Information(string message, params object[] propertyValues)
        {
            logger?.Information(consructMessage(message), propertyValues);
        }

        public void Debug(string message, params object[] propertyValues)
        {
            logger?.Debug(consructMessage(message), propertyValues);
        }

        public void Error(string message, params object[] propertyValues)
        {
            logger?.Error(consructMessage(message), propertyValues);
        }
    }
}