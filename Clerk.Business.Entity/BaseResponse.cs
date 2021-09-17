using System;

namespace Clerk.Business.Entity
{
    public class BaseResponse<T>
    {
        public BaseResponse()
        {
            Time = DateTime.Now.ToLongDateString();
        }      

        public BaseResponse(bool defaultValue, string message)
        {
            IsSuccess = defaultValue;
            Message = message;
            Time = DateTime.Now.ToLongDateString();
        }
        public bool IsSuccess { get; set; }

        public string Message { get; set; }

        public string Time { get; set; }
        public T Data { get; set; }

    }
}
