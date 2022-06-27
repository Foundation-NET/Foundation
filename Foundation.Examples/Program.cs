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

            Runner r = new Runner();
        }
    }

    public class Runner : ObjectBase
    {
        public Runner()
        {
            Console.WriteLine("ConfigReaderExample");
            Launch<ConfigReaderExample>().Run();
            Console.WriteLine("Done");

            Console.WriteLine("CsvReaderExample");
            Launch<CsvReaderExample>().Run();
            Console.WriteLine("Done");

            Console.WriteLine("CsvWriterExample");
            Launch<CsvWriterExample>(1, 2, 3).Run();
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
    public class CsvReaderExample : ObjectProcess<Csv.Shared.IRow>
    {
        ICsvReader _Csv;

        public CsvReaderExample()
        {
            var Scope = CreateScope();
            //ResolveRequired the interface
            _Csv = (ICsvReader)Scope.GetRequiredServiceScope<ICsvReader>();
            if (_Csv == null)
                throw new Exception("FFS");
        }

        public void InitializeDataView()
        {
            _Csv.SetHeader("Test(string)|Test2(Int32)");
            _Csv.SetFile("./test.csv");
            From  = _Csv.GetAllRows().ToList<Csv.Shared.IRow>();
            Where = row => (string?)row.GetColByName("Test") == "Some  Other Text";
            Where = row => (int?)row.GetColByName("Test2") < 4;
        }

        public void Run()
        {
            InitializeDataView();
            Execute();
        }

        public override void LeaveRow(Csv.Shared.IRow row)
        {
            Console.WriteLine("{0} : {1}", row.GetColByIndex(0), row.GetColByIndex(1));
        }

    }

    public class CsvWriterExample : ObjectBase 
    {
        ICsvWriter _Csv;

        public CsvWriterExample(int i, int i2, int i3)
        {
            var Scope = CreateScope();
            //ResolveRequired the interface
            _Csv = (ICsvWriter)Scope.GetRequiredServiceScope<ICsvWriter>();
            if (_Csv == null)
                throw new Exception("FFS");
            Console.WriteLine("{0} : {1} : {2}", i, i2, i3);
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
