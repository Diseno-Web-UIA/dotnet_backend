namespace backend.Utils
{
    public class Error(string message, int status, string details = "")
    {
        public string Message { get; set; } = message;
        public int StatusCode { get; set; } = status;
        public string Details { get; set; } = details;
    }
}