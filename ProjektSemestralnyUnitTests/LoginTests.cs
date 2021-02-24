using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjektSemestralny;

namespace ProjektSemestralnyUnitTests
{
    [TestClass]
    public class LoginTests
    
    {
        [DataTestMethod]
        [DataRow("user", "1234")]
 
        public void AssertCredentialsToConnectionString(string user, string pass)
        {
            var credentials = DatabaseService.AddCredentials(user, pass);
            var expected = $"{DatabaseService.connectionStringSSA}User ID={user};Password={pass}";
            Assert.AreEqual(credentials, expected);
        }

        [DataTestMethod]
        [DataRow("user", "1234")]
        public void AssertSuccessfulSSALogin(string user, string pass)
        {
            Assert.AreEqual(true, DatabaseService.OpenConnectionSSA(user, pass), "user/1234 are correct credentials");
        }

        [DataTestMethod]
        [DataRow("user", "123423212312")]
        public void AssertUnsuccessfulSSALogin(string user, string pass)
        {
            Assert.AreEqual(false, DatabaseService.OpenConnectionSSA(user, pass), "user/1234 are incorrect credentials");
        }
    }
}
