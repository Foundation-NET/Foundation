using Microsoft.VisualStudio.TestTools.UnitTesting;
using Foundation.ConfigReader;

namespace Foundation.ConfigReader.Tests
{
    [TestClass]
    public class ConfigTxtReaderTests
    {
        [TestMethod]
        public void TestWriteReadBlankFile()
        {
            IConfigReader configReader = new ConfigTxtReader();
            
            configReader.Connect("./Test", true);

            configReader.Write("TestKey1", "Value");
            configReader.Write("TestKey2", "Value");

            IConfigReader configReader2 = new ConfigTxtReader();

            configReader2.Connect("./Test");

            string k1 = configReader2.Read("TestKey1");

            Assert.AreEqual("Value",k1);
        }

        [TestMethod]
        public void TestWriteReadBlankFileNamespace()
        {
            IConfigReader configReader = new ConfigTxtReader();

            configReader.Connect("./Test", true);

            configReader.Write("Section.TestKey1", "Value2");

            IConfigReader configReader2 = new ConfigTxtReader();

            configReader2.Connect("./Test");

            string k1 = configReader2.Read("Section.TestKey1");

            Assert.AreEqual("Value2",k1);
        }
        [TestMethod]
        public void TestExistingCheck()
        {
            IConfigReader configReader = new ConfigTxtReader();

            configReader.Connect("../../../ConfigTestReaderPopulatedKeyFile.txt");

            int result = configReader.Exists("Key1");

            Assert.AreEqual(0, result);
        }
        [TestMethod]
        public void TestReadCheck()
        {
            IConfigReader configReader = new ConfigTxtReader();

            configReader.Connect("../../../ConfigTestReaderPopulatedKeyFile.txt");

            string result = configReader.Read("Key1");

            Assert.AreEqual("Value", result);
        }
    }
}