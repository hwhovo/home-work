using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Net;

namespace YB.Todo.Core.Models
{
    public class ApiResult<T> : JsonResult
    {
        public ApiResult() : base(null)
        {
            base.StatusCode = (int)HttpStatusCode.OK;
        }

        public ApiResult(HttpStatusCode statusCode) : this()
        {
            base.StatusCode = (int)statusCode;
        }

        public ApiResult(T result): this()
        {
            base.Value = new ApiResponse<T>(result);
        }

        public ApiResult(T result, HttpStatusCode statusCode) : this(result)
        {
            base.StatusCode = (int)statusCode;
        }

        public ApiResult(string errorMessage, HttpStatusCode statusCode = HttpStatusCode.InternalServerError) : this(statusCode)
        {
            base.Value = new ApiResponse<object>(errorMessage);
        }

        public ApiResult(ErrorCode errorCode, HttpStatusCode statusCode = HttpStatusCode.BadRequest) : this(errorCode.ToString(), errorCode, statusCode)
        {
        }

        public ApiResult(string errorMessage, ErrorCode errorCode, HttpStatusCode statusCode = HttpStatusCode.BadRequest) : this(statusCode)
        {
            base.Value = new ApiResponse<object>(errorMessage, errorCode);
        }

        public ApiResult(T result, string errorMessage, ErrorCode errorCode, HttpStatusCode statusCode): this(statusCode)
        {
            base.Value = new ApiResponse<T>(result, errorMessage, errorCode);
        }
    }
}
