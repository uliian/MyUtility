using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace MyUtility.Commons.Encrypt
{
    /// <summary>
    /// 这是一个很常用的密码相关类，
    /// byte->HEX，SHA256,随机盐生成
    /// </summary>
    public class EncryptHelper
    {
        public string GenerateSalt(int saltLenth=64)
        {
            using (var rngCsp = new RNGCryptoServiceProvider())
            {
                var bytes = new byte[saltLenth];
                rngCsp.GetBytes(bytes);
                return this.ToHex(bytes);
            }
        }

        public string Sha256Encrypt(string input)
        {
            var mySHA256 = SHA256.Create1();
            var bytes = Encoding.UTF8.GetBytes(input);
            var hash = mySHA256.ComputeHash(bytes);
            return this.ToHex(hash);
        }

        public string ToHex(byte[] input)
        {
            var sb = new StringBuilder();
            int i;
            for (i = 0; i < input.Length; i++)
            {
                sb.AppendFormat("{0:X2}", input[i]);
                if ((i % 4) == 3) Console.Write(" ");
            }
            return sb.ToString();
        }

    }
}
