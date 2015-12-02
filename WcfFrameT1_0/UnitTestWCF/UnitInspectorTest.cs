using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WCF.Inspector;

namespace UnitTestWCF
{
    [TestClass]
    public class UnitInspectorTest
    {
        [TestMethod]
        public void TestKeyIncode()
        {
            ClientTokens _tokens = new ClientTokens(ClientIdentityKey.GetClientKeys(),
                ClientIdentityKey.GetClientTokenKeys());

            var keyValue = "7DaEvpzZxw48Wtf3rS/B21UBZnUcCcRu";
            var _curtoken = _tokens.KeyEnCode(keyValue);

            var isExprise = _tokens.CheckClientToken(_curtoken);
            Assert.AreEqual(true, isExprise);
        }
    }
}
