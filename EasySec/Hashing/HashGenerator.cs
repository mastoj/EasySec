using System;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace EasySec.Hashing
{
    public class HashGenerator : IHashGenerator
    {
        private readonly IHashGeneratorConfig _config;

        public HashGenerator() : this(new HashGeneratorConfig())
        {
            
        }

        public HashGenerator(IHashGeneratorConfig config)
        {
            _config = config;
        }

        // SHA256 uses 256 bits --> 256/8 bytes
        private const int HashSizeInBytes = 256/8; 
        private readonly SHA256Managed HashProvider = new SHA256Managed();

        public string GenerateHash(string inputText)
        {
            return GenerateHash(inputText, null);
        }

        private string GenerateHash(string inputText, byte[] salt)
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

        private byte[] GenerateSalt()
        {
            int minSaltSize = _config.MinSaltLength;
            int maxSaltSize = _config.MaxSaltLength;
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
    }
}
