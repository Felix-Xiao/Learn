using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test
{
    [TestClass]
    public class MSTesT
    {
        [TestMethod]
        public void TestMethod1()
        {
            int a = 1;
            int b = 1;
            Assert.IsTrue(a.Equals(b));
        }
    }
}
