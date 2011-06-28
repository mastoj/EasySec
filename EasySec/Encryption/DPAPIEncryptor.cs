using System;
using System.Security.Cryptography;
using System.Text;

namespace EasySec.Encryption
{
    public class DPAPIEncryptor : IEncryptor
    {
        public string Encrypt(string textToEnrypt)
        {
            var encryptedData = ProtectedData.Protect(Encoding.UTF8.GetBytes(textToEnrypt), null,
                                                      DPAPIConfiguration.Instance.DataProtectionScope);
            var encryptedString = Convert.ToBase64String(encryptedData);
            return encryptedString;
        }

        public string Decrypt(string textToDecrypt)
        {
            var encryptedData = Convert.FromBase64String(textToDecrypt);
            var decryptedData = ProtectedData.Unprotect(encryptedData, null,
                                                        DPAPIConfiguration.Instance.DataProtectionScope);
            return Encoding.UTF8.GetString(decryptedData);
        }
    }
}