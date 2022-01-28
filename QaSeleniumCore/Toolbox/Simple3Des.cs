using System;
using System.Security.Cryptography;

namespace QaSeleniumCore.Toolbox
{
    public class Simple3Des
    {
        private readonly TripleDESCryptoServiceProvider _tripleDes = new TripleDESCryptoServiceProvider();
        private const string Key = "axeri56g3555555555555555555555545462dc";

        public Simple3Des()
        {
            _tripleDes.Key = TruncateHash(Key, _tripleDes.KeySize / 8);
            _tripleDes.IV = TruncateHash("", _tripleDes.BlockSize / 8);
        }

        private static byte[] TruncateHash(string key, int length)
        {
            SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider();
            byte[] keyBytes = System.Text.Encoding.Unicode.GetBytes(key);
            byte[] hash = sha1.ComputeHash(keyBytes);
            byte[] padHash = new byte[length];
            for (int i = 0; i < length; i++)
            {
                if (i >= hash.Length)
                    padHash[i] = 0;
                else
                    padHash[i] = hash[i];
            }
            return padHash;
        }

        public string EncryptData(string plaintext)
        {
            byte[] plaintextBytes = System.Text.Encoding.Unicode.GetBytes(plaintext);
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            CryptoStream encStream = new CryptoStream(ms, _tripleDes.CreateEncryptor(), System.Security.Cryptography.CryptoStreamMode.Write);
            encStream.Write(plaintextBytes, 0, plaintextBytes.Length);
            encStream.FlushFinalBlock();

            return Convert.ToBase64String(ms.ToArray());
        }

        public string DecryptData(string encryptedtext)
        {
            byte[] encryptedBytes = Convert.FromBase64String(encryptedtext);
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            CryptoStream decStream = new CryptoStream(ms, _tripleDes.CreateDecryptor(), System.Security.Cryptography.CryptoStreamMode.Write);

            decStream.Write(encryptedBytes, 0, encryptedBytes.Length);
            decStream.FlushFinalBlock();

            return System.Text.Encoding.Unicode.GetString(ms.ToArray());
        }
    }
}
