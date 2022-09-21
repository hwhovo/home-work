using Microsoft.AspNetCore.Diagnostics;
using System.Net;
using YB.Todo.Core;
using YB.Todo.Core.Exceptions;
using YB.Todo.Core.Models;

namespace YB.Todo.Middlewares
{
    public static class ExceptionMiddleware
    {
        public static void ExceptionHandler(IApplicationBuilder exceptionHandlerApp)
        {
            exceptionHandlerApp.Run(async context =>
            {
                var exceptionHandlerPathFeature =
                    context.Features.Get<IExceptionHandlerPathFeature>();

                var responseBody = new ApiResponse<object>();

                if (exceptionHandlerPathFeature?.Error is LogicException logicException)
                {
                    responseBody.ErrorCode = logicException.ErrorCode;
                    responseBody.ErrorMessage = logicException.Message;
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                }
                else
                {
                    responseBody.ErrorCode = ErrorCode.SOMETHING_WENT_WRONG;
                    responseBody.ErrorMessage = ErrorCode.SOMETHING_WENT_WRONG.ToString();
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                }

                await context.Response.WriteAsJsonAsync(responseBody);
            });
        }
    }
}
