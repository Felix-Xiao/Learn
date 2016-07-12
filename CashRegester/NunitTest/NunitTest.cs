using System;
using NUnit.Framework;

namespace NunitTest
{
    [TestFixture()]
    public class NunitTest
    {
        [Test()]
        public void TestMethod1()
        {
            int a = 1;
            int b = 1;
            Assert.IsTrue(a.Equals(b));
        }
    }
}
