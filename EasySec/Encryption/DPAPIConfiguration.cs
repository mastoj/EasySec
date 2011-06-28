using System.Security.Cryptography;

namespace EasySec.Encryption
{
    public class DPAPIConfiguration
    {
        private DPAPIConfiguration()
        {

        }

        private static DPAPIConfiguration _instance;
        public static DPAPIConfiguration Instance
        {
            get
            {
                _instance = _instance ?? new DPAPIConfiguration();
                return _instance;
            }
            set { _instance = value; }
        }

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