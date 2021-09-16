﻿using System;

namespace Clerk.Common
{
    public class NotFoundCustomException : Exception
    {
        public string ErrorCode { get; set; }
        public NotFoundCustomException(string message) : base(message)
        { }

        public NotFoundCustomException(string errorCode, string message) : base(message)
        {
            ErrorCode = errorCode;
        }
    }
}
