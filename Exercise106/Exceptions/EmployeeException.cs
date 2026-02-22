namespace Exercise106.Exceptions;

/// <summary>
/// Domain-specific exception for employee data processing errors.
/// Always includes an <see cref="EmployeeCodeError"/> for typed handling/messages.
/// </summary>
public class EmployeeException : Exception
{
    /// <summary>
    /// Immutable error code detailing the failure (e.g., WrongCsvFormat).
    /// Enables switch-based recovery or localized messages.
    /// </summary>
    public readonly EmployeeCodeError ErrorCode;

    /// <summary>
    /// Initializes with error code only (default message).
    /// </summary>
    public EmployeeException(EmployeeCodeError errorCode) : base()
    {
        ErrorCode = errorCode;
    }

    /// <summary>
    /// Initializes with custom message and error code.
    /// </summary>
    public EmployeeException(string message, EmployeeCodeError errorCode) : base(message)
    {
        ErrorCode = errorCode;
    }

    /// <summary>
    /// Initializes with message, error code, and inner exception (e.g., IO errors).
    /// </summary>
    public EmployeeException(string message, EmployeeCodeError errorCode, Exception innerException)
        : base(message, innerException)
    {
        ErrorCode = errorCode;
    }
}
