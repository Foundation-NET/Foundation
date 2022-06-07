using Foundation.Annotations;

namespace Foundation.ConfigReader
{
    public interface IConfigReaderFactory : IFactoryBuilder
    {
        public IConfigReader GetService(Type? type);
    }
}