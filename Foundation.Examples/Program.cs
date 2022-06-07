using Foundation.ConfigReader;

namespace Foundation.Examples
{
    public class Program : ApplicationBase
    {
        static void Main(string[] args )
        {
            // Must be first thing run.
            ApplicationStart(args);

            Console.WriteLine("ConfigReaderExample");
            ConfigReaderExample CRE = new ConfigReaderExample();
            CRE.Run();
            Console.WriteLine("Done");

        }
    }

    // Inherit ObjectBase to resolved DI
    public class ConfigReaderExample : ObjectBase 
    {
        IConfigReader _Config;

        public ConfigReaderExample()
        {
            //ResolveRequired the interface
            _Config = (IConfigReader)ResolveRequired<IConfigReaderFactory>(ConfigReader.Type.Ini);
            if (_Config == null)
                throw new Exception("FFS");
            _Config.Connect("./test", true);
        }

        public void Run()
        {
            string test3 = _Config.Read("test3");

            Console.WriteLine(test3);
        }

    }
}
