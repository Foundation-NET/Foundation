using System.Collections.Generic;

namespace Foundation.ConfigReader
{
    public interface IConfigReader
    {
        bool Connect(string Path, bool Create = false);
        void Write(string Key, string Value);
        string Read(string Key);
        int Exists(string Key);
    }
}