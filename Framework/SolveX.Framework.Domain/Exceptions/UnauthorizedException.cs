using System;

namespace SolveX.Framework.Domain.Exceptions;

public class UnauthorizedException : Exception
{
    public UnauthorizedException() : base() { }

    public UnauthorizedException(string message) : base(message) { }

    public UnauthorizedException(string message, Exception innerException) : base(message, innerException) { }


    public UnauthorizedException(int errorCode) : this()
    {
        ErrorCode = errorCode;
    }

    public UnauthorizedException(int errorCode, string message) : this(message)
    {
        ErrorCode = errorCode;
    }

    public UnauthorizedException(int errorCode, string message, Exception innerException) : this(message, innerException)
    {
        ErrorCode = errorCode;
    }

    /// <summary>
    /// Business error code
    /// </summary>
    public int ErrorCode { get; private set; }
}
