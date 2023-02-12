using System;

namespace SolveX.Framework.Domain.Exceptions;

public class BadDataException : Exception
{
    public BadDataException() : base() { }

    public BadDataException(string message) : base(message) { }

    public BadDataException(string message, Exception innerException) : base(message, innerException) { }


    public BadDataException(int errorCode) : this()
    {
        ErrorCode = errorCode;
    }

    public BadDataException(int errorCode, string message) : this(message)
    {
        ErrorCode = errorCode;
    }

    public BadDataException(int errorCode, string message, Exception innerException) : this(message, innerException)
    {
        ErrorCode = errorCode;
    }

    /// <summary>
    /// Business error code
    /// </summary>
    public int ErrorCode { get; private set; }
}
