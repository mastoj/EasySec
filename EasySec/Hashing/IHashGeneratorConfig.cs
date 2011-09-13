namespace EasySec.Hashing
{
    public interface IHashGeneratorConfig
    {
        int MinSaltLength { get; set; }
        int MaxSaltLength { get; set; }
    }
}