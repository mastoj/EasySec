using System;
using System.Security.Cryptography;
using System.Text;

namespace EasySec.Encryption
{
    public class DPAPIEncryptor : IEncryptor
    {
        private readonly IDPAPIConfiguration _config;

        public DPAPIEncryptor() : this(new DPAPIConfiguration())
        {
            
        }

        public DPAPIEncryptor(IDPAPIConfiguration config)
        {
            _config = config;
        }

        public string Encrypt(string textToEnrypt)
        {
            var encryptedData = ProtectedData.Protect(Encoding.UTF8.GetBytes(textToEnrypt), null,
                                                      _config.DataProtectionScope);
            var encryptedString = Convert.ToBase64String(encryptedData);
            return encryptedString;
        }

        public string Decrypt(string textToDecrypt)
        {
            var encryptedData = Convert.FromBase64String(textToDecrypt);
            var decryptedData = ProtectedData.Unprotect(encryptedData, null,
                                                        _config.DataProtectionScope);
            return Encoding.UTF8.GetString(decryptedData);
        }
    }
}