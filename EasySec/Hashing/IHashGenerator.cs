namespace EasySec.Hashing
{
    public interface IHashGenerator
    {
        /// <summary>
        /// Generate hash from input text.
        /// </summary>
        /// <param name="inputText"></param>
        /// <returns></returns>
        string GenerateHash(string inputText);
        /// <summary>
        /// Compares the hashed text with compareText and returns true if the hash of compareText 
        /// is the same as hashedText.
        /// </summary>
        /// <param name="hashedText"></param>
        /// <param name="compareText"></param>
        /// <returns></returns>
        bool CompareHash(string hashedText, string compareText);
    }
}
