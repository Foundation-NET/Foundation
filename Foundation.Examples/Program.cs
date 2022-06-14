using Foundation.ConfigReader;
using Foundation.Csv.Reader;
using Foundation.Csv.Writer;

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

            Console.WriteLine("CsvWriterExample");
            CsvWriterExample CSVWriter = new CsvWriterExample();
            CSVWriter.Run();
            Console.WriteLine("Done");


        }
    }

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
        ICsvReader _Csv;

        public CsvReaderExample()
        {
            var Scope = CreateScope();
            //ResolveRequired the interface
            _Csv = (ICsvReader)GetRequiredServiceScope<ICsvReader>(Scope);
            if (_Csv == null)
                throw new Exception("FFS");
            
        }

        public void Run()
        {
            _Csv.SetHeader("Test(string)|Test2(Int32)");
            _Csv.SetFile("./test.csv");
            var r = _Csv.GetAllRows();

            foreach(var v in r)
            {
                Console.WriteLine("{0} : {1}", v.GetColByName("Test"), v.GetColByName("Test2"));
            }
        }

    }

    public class CsvWriterExample : ObjectBase 
    {
        ICsvWriter _Csv;

        public CsvWriterExample()
        {
            var Scope = CreateScope();
            //ResolveRequired the interface
            _Csv = (ICsvWriter)GetRequiredServiceScope<ICsvWriter>(Scope);
            if (_Csv == null)
                throw new Exception("FFS");
            
        }

        public void Run()
        {
            _Csv.SetHeader("Test(string)|Test2(Int32)");
            var r = _Csv.NewRow();
            r.SetColByName("Test", "Some Text");
            r.SetColByName("Test2", 23);

            _Csv.CommitRow(r);
            _Csv.SetOutputFile("./out.csv");

            _Csv.CommitFile();
        }

    }
}
