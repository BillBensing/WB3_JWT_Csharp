using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WB3;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WB3.Tests
{
    [TestClass()]
    public class JWTTests
    {
        [TestMethod()]
        public void WebTokenTest()
        {
            var now = DateTime.Today.ToUniversalTime().Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;
            string _aud = "html5";
            string _iss = "TestApplication";
            string _scp = "execute,create";
            string secret = "ThisIsSecret";
            string _alg = WebToken.EncryptionAlgo.TripleDES.ToString();
            double _exp = Utilities.FutureInMiliseconds(50);
            double _iat = Utilities.NowInMilliseconds();
            double _nbf = Utilities.FutureInMiliseconds(20);

            var t = new WebToken.Token();

            t.Header.alg = WebToken.EncryptionAlgo.TripleDES.ToString();

            t.Body.aud = _aud;
            t.Body.iss = _iss;
            t.Body.scp = _scp;
            t.Body.exp = _exp;
            t.Body.iat = _iat;
            t.Body.nbf = _nbf;

            // Convert C# Obect to JSON Object
            var _JSONtoken = JWT.ConstructWebToken(t, secret);

            // Deconstrcut JSON token into C# Object
            var _CSToken = JWT.DeconstructWebToken(_JSONtoken, secret);

            var hOut = _CSToken.Header;
            var bOut = _CSToken.Body;
            var sOut = _CSToken.Signature;

            // Check if output is equal to input
            Assert.AreEqual(hOut.alg, _alg);

            // Check if output is equal to input
            Assert.AreEqual(bOut.aud, _aud);
            Assert.AreEqual(bOut.iss, _iss);
            Assert.AreEqual(bOut.scp, _scp);
            Assert.AreEqual(bOut.exp, _exp);
            Assert.AreEqual(bOut.iat, _iat);
            Assert.AreEqual(bOut.nbf, _nbf);

            // Check if output is equal to input
            Assert.AreEqual(sOut.aud, _aud);
            Assert.AreEqual(sOut.iss, _iss);
            Assert.AreEqual(sOut.scp, _scp);
            Assert.AreEqual(sOut.exp, _exp);
            Assert.AreEqual(sOut.iat, _iat);
            Assert.AreEqual(sOut.nbf, _nbf);

            // Check if body is equal to signature output
            // Check if output is equal to input
            Assert.AreEqual(sOut.aud, bOut.aud);
            Assert.AreEqual(sOut.iss, bOut.iss);
            Assert.AreEqual(sOut.scp, bOut.scp);
            Assert.AreEqual(sOut.exp, bOut.exp);
            Assert.AreEqual(sOut.iat, bOut.iat);
            Assert.AreEqual(sOut.nbf, bOut.nbf);

        }
    }
}
