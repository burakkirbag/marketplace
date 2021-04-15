namespace Marketplace
{
    public interface IStartupTask
    {
        void Execute();

        int Order { get; }
    }
}