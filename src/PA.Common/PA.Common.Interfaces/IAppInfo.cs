namespace PA.Common.Interfaces
{
    public interface IAppInfo
    {
        string AppName { get; }
        IDictionary<string, object?> Info { get; set; }
    }
}
