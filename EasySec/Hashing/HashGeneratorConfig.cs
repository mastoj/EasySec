namespace EasySec.Hashing
{
    public class HashGeneratorConfig : IHashGeneratorConfig
    {
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