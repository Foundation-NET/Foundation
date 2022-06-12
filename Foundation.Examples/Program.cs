using Foundation.ConfigReader;
using Foundation.Csv.Reader;

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

            Console.WriteLine("CsvReaderExample");
            CsvReaderExample CSE = new CsvReaderExample();
            CSE.Run();
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
    public class CsvReaderExample : ObjectBase 
    {
        ICsvReader _Config;

        public CsvReaderExample()
        {
            var Scope = CreateScope();
            //ResolveRequired the interface
            _Config = (ICsvReader)GetRequiredServiceScope<ICsvReader>(Scope);
            if (_Config == null)
                throw new Exception("FFS");
            
        }

        public void Run()
        {
            _Config.SetHeader("Test(string)|Test2(Int32)");
            _Config.SetFile("./test.csv");
            var r = _Config.GetAllRows();

            foreach(var v in r)
            {
                Console.WriteLine("{0} : {1}", v.GetColByName("Test"), v.GetColByName("Test2"));
            }
        }

    }
}
