using System.Security.Cryptography;

namespace EasySec.Encryption
{
    public class DPAPIConfiguration : IDPAPIConfiguration
    {
        private DataProtectionScope? _dataProtectionScope;
        public DataProtectionScope DataProtectionScope
        {
            get
            {
                _dataProtectionScope = _dataProtectionScope ?? DataProtectionScope.CurrentUser;
                return _dataProtectionScope.Value;
            }
            set { _dataProtectionScope = value; }
        }
    }
}