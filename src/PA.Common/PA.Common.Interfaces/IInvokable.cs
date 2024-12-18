namespace PA.Common.Interfaces
{
    public interface IInvokable
    {
        object InvokableObj { get; }
        Thread CurrentThread { get; }
        void BeginInvoke(Action action);
        void Invoke(Action action);
        object InvokeAsync(Action action);
    }
}
