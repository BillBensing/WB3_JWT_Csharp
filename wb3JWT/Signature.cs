using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace WB3
{
    public class Signature
    {
        public static string Create(WebToken.Body body, string secret)
        {
            // Searlize incoming C# object
            var _srlz = new JavaScriptSerializer();
            var _json = _srlz.Serialize(body);

            // Encrypt JSON Searlizied object and return
            var _encryptedAndBase64Encoded = Util.DESEncrypt.EncryptJSON(_json, secret);
            return _encryptedAndBase64Encoded;
        }
        
        public static WebToken.Body Read(string encrypted, string secret)
        {
            if (String.IsNullOrEmpty(encrypted))
            {
                throw new ArgumentNullException("JWT.CreateRead():  You must provide a non-null encrypted body string.");
            }

            var _decrypted = Util.DESEncrypt.DecryptJSON(encrypted, secret);
            var _srlz = new JavaScriptSerializer();

            return (WebToken.Body)_srlz.Deserialize(_decrypted, typeof(WebToken.Body));


        }

    }
}
