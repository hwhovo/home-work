using Microsoft.Extensions.Logging;

namespace YB.Todo.Core.Exceptions
{
    public class LogicException : Exception
    {
        public LogicException(ErrorCode errorCode) : base(errorCode.ToString())
        {
            ErrorCode = errorCode;
        }

        public LogicException(ErrorCode errorCode, ILogger logger) : this(errorCode)
        {
            logger.LogError(this, errorCode.ToString());
        }

        public ErrorCode ErrorCode { get; set; }
    }
}
