using Microsoft.VisualStudio.TestTools.UnitTesting;
using ZaraExam.Logical;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZaraExam.Logical.Tests
{
    [TestClass()]
    public class FileXTests
    {
        FileX file;
        public FileXTests()
        {
            file = new FileX();
        }
        [TestMethod()]
        public void GetStocksTest()
        {
            var list = file.GetStocks();
            Assert.IsTrue(list != null);
        }

        [TestMethod()]
        [DataRow("Texto de prueba")]
        public void AddRowTest(string test)
        {
            Assert.IsTrue(file.AddRow(test));
        }
    }
}