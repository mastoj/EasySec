using System;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace EasySec.Hashing
{
    public class HashGenerator : IHashGenerator
    {
        // SHA256 uses 256 bits --> 256/8 bytes
        private const int HashSizeInBytes = 256/8; 
        private static readonly SHA256Managed HashProvider = new SHA256Managed();

        public string GenerateHash(string inputText)
        {
            return GenerateHash(inputText, null);
        }

        private static string GenerateHash(string inputText, byte[] salt)
        {
            if (inputText == null) throw new ArgumentNullException("inputText");
            salt = salt ?? GenerateSalt();
            var plainTextBytes = Encoding.UTF8.GetBytes(inputText);

            var plainTextAndSaltBytes = JoinByteArrays(plainTextBytes, salt);
            var hashBytes = HashProvider.ComputeHash(plainTextAndSaltBytes);
            var hashWithSalt = JoinByteArrays(hashBytes, salt);

            var hashValue = Convert.ToBase64String(hashWithSalt);
            return hashValue;
        }

        private static byte[] JoinByteArrays(byte[] firstArray, byte[] secondArray)
        {
            var joinedBytes = new byte[firstArray.Length + secondArray.Length];
            for (int i = 0; i < firstArray.Length; i++)
            {
                joinedBytes[i] = firstArray[i];
            }
            for (int i = 0; i < secondArray.Length; i++)
            {
                joinedBytes[firstArray.Length + i] = secondArray[i];
            }
            return joinedBytes;
        }

        private static byte[] GenerateSalt()
        {
            int minSaltSize = HashGeneratorConfig.Instance.MinSaltLength;
            int maxSaltSize = HashGeneratorConfig.Instance.MaxSaltLength;
            var random = new Random();
            int saltSize = random.Next(minSaltSize, maxSaltSize);
            var saltBytes = new byte[saltSize];
            using (var rngCryptoServiceProvider = new RNGCryptoServiceProvider())
            {
                rngCryptoServiceProvider.GetNonZeroBytes(saltBytes);
            }
            return saltBytes;
        }

        public bool CompareHash(string hashedText, string compareText)
        {
            var hashWithSalt = Convert.FromBase64String(hashedText);
            if(hashWithSalt.Length < HashSizeInBytes)
            {
                return false;
            }
            var salt = ExtractSaltFromHash(hashWithSalt);
            var compareHashedText = GenerateHash(compareText, salt);
            var areEqual = hashedText == compareHashedText;
            return areEqual;
        }

        private static byte[] ExtractSaltFromHash(byte[] hashWithSalt)
        {
            var saltBytes = hashWithSalt.Skip(HashSizeInBytes).ToArray();
            return saltBytes;
        }
    }

    public class HashGeneratorConfig
    {
        private static HashGeneratorConfig _instance;
        public static HashGeneratorConfig Instance
        {
            get
            {
                _instance = _instance ?? new HashGeneratorConfig();
                return _instance;
            }
            set { _instance = value; }
        }

        private HashGeneratorConfig()
        {
            
        }

        private int? _minSaltLength;
        public int MinSaltLength
        {
            get
            {
                _minSaltLength = _minSaltLength ?? 4;
                return _minSaltLength.Value;
            }
            set { _minSaltLength = value; }
        }

        private int? _maxSaltLength;
        public int MaxSaltLength
        {
            get
            {
                _maxSaltLength = _maxSaltLength ?? 4;
                return _maxSaltLength.Value;
            }
            set { _maxSaltLength = value; }
        }
    }
}
