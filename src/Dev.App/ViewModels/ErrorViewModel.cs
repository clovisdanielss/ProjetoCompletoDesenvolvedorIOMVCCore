namespace Dev.App.ViewModels
{
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }
        public int Code { get; set; } = 500;

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}