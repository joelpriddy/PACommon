namespace PA.Common.Interfaces
{
    public interface IValidationService<T> where T : class
    {
        bool IsValid(T value, object? args = null);
        IDictionary<string, string> GetValidation(T? model = null, object? args = null);
    }
}
