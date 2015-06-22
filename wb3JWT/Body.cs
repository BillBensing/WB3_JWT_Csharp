using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace WB3
{
    public class Body
    {
        public static string Create(WebToken.Body body)
        {
            var _json = string.Empty;

            var _srlze = new JavaScriptSerializer();
            _json = _srlze.Serialize(body);

            return Util.Base64Encode(_json);
        }
    }
}
