namespace PA.Common.Extensions
{
    public static partial class FunctionExtensions
    {
        public static async Task<bool> RunInBackgroundAsync(this Func<Task<bool>> action, TimeSpan interval, CancellationToken token)
        {
            using PeriodicTimer timer = new(interval);

            while (true)
            {
                if (token.IsCancellationRequested) { return true; }

                _ = await action.Invoke();//ensures the action is invoked as soon as the extension is called.
                await timer.WaitForNextTickAsync(token);
            }
        }
    }
}
