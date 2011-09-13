using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EasySec.Hashing;
using NUnit.Framework;

namespace EasySec.Tests.Hashing
{
    [TestFixture]
    public class HashGeneratorTests
    {
        [Test]
        public void CanMatchStrings()
        {
            // arrange
            var stringToHash = "StringToHash";
            var hashGenerator = new HashGenerator();

            // act
            var hash = hashGenerator.GenerateHash(stringToHash);
            var isMatch = hashGenerator.CompareHash(hash, stringToHash);

            // assert
            Assert.AreNotEqual(hash, stringToHash);
            Assert.IsTrue(isMatch);
        }

        [Test]
        public void ReturnFalseForMinorChangeOfInput()
        {
            // arrange
            var stringToHash = "StringToHash";
            var stringToMatch = stringToHash + "2";
            var hashGenerator = new HashGenerator();

            // act
            var hash = hashGenerator.GenerateHash(stringToHash);
            var isMatch = hashGenerator.CompareHash(hash, stringToMatch);

            // assert
            Assert.AreNotEqual(hash, stringToHash);
            Assert.IsFalse(isMatch);
        }
    }
}
