using Microsoft.VisualStudio.TestTools.UnitTesting;
using NewBridge.Zoro.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewBridge.Zoro.Library.Tests
{
    [TestClass()]
    public class DoubanHelperTests
    {
        [TestMethod()]
        public void GetSummaryFailTest()
        {
            string summary = DoubanHelper.GetSummary("1234");
            Assert.IsTrue(summary == string.Empty);
        }

        [TestMethod]
        public void GetSummarySuccessTest()
        {
            string summary = DoubanHelper.GetSummary("26870044");
            Assert.IsTrue(summary.Length > 0);
        }
    }
}