using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Net;

namespace YB.Todo.Core.Models
{
    public class ApiResponse<T>
    {
        public ApiResponse()
        { }

        public ApiResponse(T result) : this()
        {
            Result = result;
        }

        public ApiResponse(ErrorCode errorCode) : this()
        {
            ErrorCode = errorCode;
            ErrorMessage = errorCode.ToString();
        }

        public ApiResponse(string errorMessage) : this()
        {
            ErrorMessage = errorMessage;
        }

        public ApiResponse(string errorMessage, ErrorCode errorCode) : this(errorCode)
        {
            ErrorMessage = errorMessage;
        }

        public ApiResponse(T result, string errorMessage, ErrorCode errorCode) : this(errorMessage, errorCode)
        {
            Result = result;
        }

        public T? Result { get; set; }
        public string? ErrorMessage { get; set; }
        public ErrorCode? ErrorCode { get; set; }
    }
}
