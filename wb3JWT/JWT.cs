using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Web.Script.Serialization;

namespace WB3
{
    public partial class JWT
    {
        /// <summary>
        /// Method constructs a JSON Web Token string.
        /// </summary>
        /// <param name="head">WebToken Header</param>
        /// <param name="body">WebToken Body</param>
        /// <param name="sig">WebToken Signature</param>
        /// <returns>JSON Web Token String</returns>
        public static string ConstructWebToken(WebToken.Token token, string secretKey)
        {
            // Create JSON objects from C# Objects
            var _head = Header.Create(token.Header);
            var _body = Body.Create(token.Body);
            var _sig = Signature.Create(token.Body, secretKey);
      
            // return complied token
            return _head + "." + _body + "." + _sig;
        }

        public static WebToken.Token DeconstructWebToken(string jwt, string key)
        {
            var _t = new WebToken.Token();
            var _lstJSON = new List<string>();

            // Split string into a list of strings
            string[] _segments = jwt.Split('.');
            foreach (string seg in _segments)
            {
                _lstJSON.Add(seg);          
            }

            // Convert list of strings into an array and deserailze the JSON strings
            // 0 -- > header; 1 --> Body; 2--> Signed Body
            var _aryJSON = _lstJSON.ToArray();
            JavaScriptSerializer _srlz = new JavaScriptSerializer();

            _t.Header = (WebToken.Head)_srlz.Deserialize(Util.Base64Decode(_aryJSON[0]), typeof (WebToken.Head));
            _t.Body = (WebToken.Body)_srlz.Deserialize(Util.Base64Decode(_aryJSON[1]), typeof(WebToken.Body));
            
            // Decrypt signature and Deserilize returned jsotn string as
            var _sigDecrypt = Util.DESEncrypt.DecryptJSON(_aryJSON[2], key);
            _t.Signature = (WebToken.Body)_srlz.Deserialize(_sigDecrypt, typeof(WebToken.Body));

            return _t;
        }

    }


}
