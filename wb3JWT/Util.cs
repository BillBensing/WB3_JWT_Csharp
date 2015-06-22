using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;
using System.Diagnostics;

namespace WB3
{
    class Util
    {
        public static string Base64Encode(string DecodedJSON)
        {
            byte[] _bytes = System.Text.UTF8Encoding.UTF8.GetBytes(DecodedJSON);
            return System.Convert.ToBase64String(_bytes);
        }
        public static string Base64Decode(string EncodedJSON)
        {
            byte[] _bytes = System.Convert.FromBase64String(EncodedJSON);
            return System.Text.UTF8Encoding.UTF8.GetString(_bytes);
        }
        public class DESEncrypt
        {
            public static string EncryptJSON(string json, string key)
            {
                MD5 md5 = new MD5CryptoServiceProvider();
                TripleDES des = new TripleDESCryptoServiceProvider();
                des.Key = md5.ComputeHash(Encoding.Unicode.GetBytes(key));
                des.Mode = CipherMode.ECB;
                des.Padding = PaddingMode.PKCS7;
                des.IV = new byte[des.BlockSize / 8];
                byte[] text = Encoding.Unicode.GetBytes(json);
                ICryptoTransform tran = des.CreateEncryptor();
                byte[] encrypted = tran.TransformFinalBlock(text, 0, text.Length);
                return Convert.ToBase64String(encrypted);
            }

            public static string DecryptJSON(string encryptedSig, string key)
            {
                MD5 md5 = new MD5CryptoServiceProvider();
                TripleDES des = new TripleDESCryptoServiceProvider();
                des.Mode = CipherMode.ECB;
                des.Padding = PaddingMode.PKCS7;
                des.Key = md5.ComputeHash(Encoding.Unicode.GetBytes(key));
                byte[] text = Convert.FromBase64String(encryptedSig);
                ICryptoTransform tran = des.CreateDecryptor();
                try
                {
                    byte[] decrypted = tran.TransformFinalBlock(text, 0, text.Length);
                    return Encoding.Unicode.GetString(decrypted);
                }
                catch (Exception e)
                {
                    return e.ToString();
                }
            }
        }
    }

    public class Utilities
    {
        public static double NowInMilliseconds()
        {
            return DateTime.Today.ToUniversalTime().Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;
        }

        public static double FutureInMiliseconds(double hours)
        {
            return DateTime.Today.ToUniversalTime().Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddHours(hours)).TotalMilliseconds;
        }
    }
}
