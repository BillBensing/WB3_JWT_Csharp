using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WB3;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.Web.Script.Serialization;

namespace WB3.Tests
{
    [TestClass()]
    public class SignatureTests
    {
        [TestMethod()]
        public void CreateAndReadSignatureTest()
        {
            // Set input attributes
            var b = new WebToken.Body();
            b.aud = "Tester";
            b.scp = "create,somethingelse";
            
            // Set encryption key
            string key = "TOPSECRET";

            // Searlized C# Object into JSON Object
            var _srlz = new JavaScriptSerializer();
            string json = _srlz.Serialize(b);

            // Pass in JSON Object and Key to get an Encrypted Signature
            //  which is Base 64 Encoded
            var _sigEncryptedBase64 = Signature.Create(b, key);

            // Decrpyt the Encrypted Signature
            var _sigDecrypted = Signature.Read(_sigEncryptedBase64, key);
            
            // Dompare the input attributes to the decrypted signature
            Assert.AreEqual(b.aud, _sigDecrypted.aud);
            Assert.AreEqual(b.scp, _sigDecrypted.scp);
        }
    }
}
