namespace Clerk.Common
{
    public class NotFoundError
    {
        public string ErrorCode { get; set; }
        public string Message { get; set; } = "Record not found";
    }
}
