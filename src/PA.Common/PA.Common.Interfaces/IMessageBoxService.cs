namespace PA.Common.Interfaces
{
    public interface IMessageBoxService
    {
        bool GetApproval(string message, string caption);
        void Show(string message, string caption);
        void ShowException(Exception ex, string addlInfo = "");
        object? Owner { get; set; }
    }
}
