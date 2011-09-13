using System.Security.Cryptography;

namespace EasySec.Encryption
{
    public interface IDPAPIConfiguration
    {
        DataProtectionScope DataProtectionScope { get; set; }
    }
}