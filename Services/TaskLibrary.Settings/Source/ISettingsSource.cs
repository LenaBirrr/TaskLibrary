using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskLibrary.Settings
{
    public interface ISettingsSource
    {
        string GetConnectionString(string? source = null);
        string GetAsString(string source, string? defaultValue = null);
        bool GetAsBool(string source, bool defaultValue = false);
        int GetAsInt(string source, int defaultValue = 0);
    }
}
