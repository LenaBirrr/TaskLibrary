using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskLibrary.Settings
{
    public interface IDbSettings
    {
        string ConnectionString { get; }
    }
}
