using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EasySec.Encryption;
using NUnit.Framework;

namespace EasySec.Tests.Encryption
{
    [TestFixture]
    public class DPAPIEncryptorTests
    {
        [Test]
        public void EncryptReturnsNotSameString()
        {
            // arrange
            var encryptor = new DPAPIEncryptor();
            var stringToEncrypt = "stringToEncrypt";

            // act
            var encryptedString = encryptor.Encrypt(stringToEncrypt);

            // assert
            Assert.AreNotEqual(stringToEncrypt, encryptedString);
        }

        [Test]
        public void EncryptDecryptReturnsSameString()
        {
            // arrange
            var encryptor = new DPAPIEncryptor();
            var stringToEncrypt = "stringToEncrypt";

            // act
            var encryptedString = encryptor.Encrypt(stringToEncrypt);
            var decryptedString = encryptor.Decrypt(encryptedString);

            // assert
            Assert.AreEqual(stringToEncrypt, decryptedString);
        }
    }
}
