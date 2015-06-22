using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace WB3
{
    public partial class JWT
    {
        public class Header
        {
            public static string Create(WebToken.Head head)
            {
                if (head.alg.ToString() == string.Empty)
                {
                    throw new ArgumentNullException("JWT.CreateHeader(): You must provide a non-null valid Encryption Algorithm.");
                }

                var _srlze = new JavaScriptSerializer();
                var _json = _srlze.Serialize(head);
                return Util.Base64Encode(_json);

            }        
        }
    }

}
