using System;
using System.Collections.Generic;
using System.Text;

namespace MyUtility.Commons.Encrypt
{
    public interface IEncryptHelper
    {
        string GenerateSalt(int length = 64);
        string Sha256Encrypt(string input);
        string ToHex(byte[] input);
    }
}
