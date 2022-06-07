using Microsoft.Extensions.DependencyInjection;
using System;
using Foundation.Annotations;

namespace Foundation.ConfigReader
{
    public enum Type
    {
        Txt,
        Ini
    }

    [Factory]
    public class ConfigReaderFactory : IConfigReaderFactory
    {
        private IServiceProvider _SP;
        public ConfigReaderFactory(IServiceProvider serviceProvider)
        {
            _SP = serviceProvider;
        }

        public Object GetService(Object? o) => GetService((Type?)o);
        public IConfigReader GetService(Type? type)
        {
            switch ((Type?)type)
            {
                case Type.Txt:
                    return ActivatorUtilities.CreateInstance<ConfigTxtReader>(_SP);
                case Type.Ini:
                    return ActivatorUtilities.CreateInstance<ConfigIniReader>(_SP);
                default:
                    throw new Exception("Unknown Type");
            }
        }
    }
}