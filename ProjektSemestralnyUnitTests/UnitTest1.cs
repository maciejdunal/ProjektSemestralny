using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjektSemestralny;
using System;

namespace ProjektSemestralnyUnitTests
{
    [TestClass]
    public class UnitTest1
    
    {
        [TestMethod]
        public void GoodLoginTest()
        {
            //Assert.AreEqual(true, user.Login("test", "test"), "test/test are correct credentials");
        }
        [TestMethod]
        public void BadLoginTest()
        {
           // Assert.AreEqual(false, user.Login("test", "badpassword"), "Any other than test/test are incorrect credentials");
        }
    }
}
