namespace PA.Common.Interfaces
{
    public interface IAsyncRunnable
    {
        Func<Task<bool>> Action { get; }
        Task<bool> RunActionAsync();
    }
}
