namespace TinyFactory
{
    public enum ServiceLifetime : byte
    {
        Singleton = 0x01,
        Transient = 0x02,
        HostedService = 0x03,
        SingletonFirstLoader = 0x04,
        TransientFirstLoader = 0x05,
    }
}
