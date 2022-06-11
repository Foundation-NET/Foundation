using Microsoft.VisualStudio.TestTools.UnitTesting;
using Foundation.ConfigReader;
using System.IO;

namespace Foundation.ConfigReader.Tests
{
    [TestClass]
    public class ConfigIniReaderTests
    {
        [ClassInitialize]
        public static void CleanData(TestContext t)
        {
            if (File.Exists("./Test2"))
                File.Delete("./Test2");
            
            if (File.Exists("./Test3"))
                File.Delete("./Test3");
        }

        [TestMethod]
        public void TestWriteReadBlankFile()
        {
            IConfigReader configReader = new ConfigIniReader();
            
            configReader.Connect("./Test3", true);

            configReader.Write("TestKey1", "Value");
            configReader.Write("TestKey2", "Value");

            IConfigReader configReader2 = new ConfigIniReader();

            configReader2.Connect("./Test3");

            string k1 = configReader2.Read("TestKey1");

            Assert.AreEqual("Value",k1);
        }

        [TestMethod]
        public void TestWriteReadBlankFileNamespace()
        {
            IConfigReader configReader = new ConfigIniReader();

            configReader.Connect("./Test2", true);

            configReader.Write("Section.TestKey1", "Value2");

            IConfigReader configReader2 = new ConfigIniReader();

            configReader2.Connect("./Test2");

            string k1 = configReader2.Read("Section.TestKey1");

            Assert.AreEqual("Value2",k1);
        }
        [TestMethod]
        public void TestExistingCheck()
        {
            IConfigReader configReader = new ConfigIniReader();

            configReader.Connect("../../../ConfigTestReaderPopulatedKeyFile.ini");

            int result = configReader.Exists("Section.Key1");

            Assert.AreEqual(1, result);
        }
        [TestMethod]
        public void TestReadCheck()
        {
            IConfigReader configReader = new ConfigIniReader();

            configReader.Connect("../../../ConfigTestReaderPopulatedKeyFile.ini");

            string result = configReader.Read("Section.Key1");

            Assert.AreEqual("Value", result);
        }
    }
}