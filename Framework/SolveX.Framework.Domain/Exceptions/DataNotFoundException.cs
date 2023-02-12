using System;

namespace SolveX.Framework.Domain.Exceptions;

public class DataNotFoundException : Exception
{
    public DataNotFoundException() : base() { }

    public DataNotFoundException(string message) : base(message) { }

    public DataNotFoundException(string message, Exception innerException) : base(message, innerException) { }


    public DataNotFoundException(int errorCode) : this()
    {
        ErrorCode = errorCode;
    }

    public DataNotFoundException(int errorCode, string message) : this(message)
    {
        ErrorCode = errorCode;
    }

    public DataNotFoundException(int errorCode, string message, Exception innerException) : this(message, innerException)
    {
        ErrorCode = errorCode;
    }

    /// <summary>
    /// Business error code
    /// </summary>
    public int ErrorCode { get; private set; }
}
