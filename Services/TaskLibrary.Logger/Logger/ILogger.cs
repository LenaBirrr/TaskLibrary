using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskLibrary.Logger
{
    public interface ILogger
    {
        void Debug(string message, params object[] propertyValues);
        void Error(string message, params object[] propertyValues);
        void Information(string message, params object[] propertyValues);
    }
}
