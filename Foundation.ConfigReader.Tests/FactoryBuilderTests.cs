using Microsoft.VisualStudio.TestTools.UnitTesting;
using Foundation.ConfigReader;
using Foundation;
using System.IO;
using System;

namespace Foundation.ConfigReader.Tests
{
    [TestClass]
    public class FactoryBuilderTests
    {
        // Setup DI Container
        [ClassInitialize]
        public static void BuildDI(TestContext t)
        {
            Main main = new Main();
            main.Run();
        }
        [TestMethod]
        public void BuildINIReader()
        {
            if (ApplicationBase._Host == null)
            {
                throw new Exception("Host not created, run ApplicationStart first");
            }
            
            IConfigReader configReader = new ConfigReaderFactory(ApplicationBase._Host.Services).GetService(Type.Ini);
            
            Assert.IsInstanceOfType(configReader, typeof(ConfigIniReader));
        }

        [TestMethod]
        public void BuildTXTReader()
        {
            if (ApplicationBase._Host == null)
            {
                throw new Exception("Host not created, run ApplicationStart first");
            }
            IConfigReader configReader = new ConfigReaderFactory(ApplicationBase._Host.Services).GetService(Type.Txt);
            
            Assert.IsInstanceOfType(configReader, typeof(ConfigTxtReader));
        }
    }

    public class Main : ApplicationBase
    {
        public void Run()
        {
            ApplicationStart(new string[0]);       
        }
    }
}