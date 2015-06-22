using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WB3
{
        public class WebToken
        {
            public class Head
            {
                public string alg { get; set; }
            }
            public class Body
            {
                public double exp { get; set; }
                public double nbf { get; set; }
                public double iat { get; set; }
                public string iss { get; set; }
                public string aud { get; set; }
                public string prn { get; set; }
                public string jti { get; set; }
                public string typ { get; set; }
                public string scp { get; set; }
            }
            public class Token
            {
                public Head Header = new Head();
                public Body Body = new Body();
                public Body Signature = new Body();

            }
            public enum EncryptionAlgo
            {
                TripleDES,
            }
        }
}
