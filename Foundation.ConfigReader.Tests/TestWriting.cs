using Microsoft.VisualStudio.TestTools.UnitTesting;
using Foundation.ConfigReader;

namespace Foundation.ConfigReader.Tests
{
    [TestClass]
    public class ConfigReaderTests
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
        public void TestWriteReadBlankFile2()
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
    }
}