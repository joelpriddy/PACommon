namespace PA.Common.Interfaces
{
    public interface ILoadable
    {
        bool IsLoaded { get; }
        bool Load(object args);
    }
}
