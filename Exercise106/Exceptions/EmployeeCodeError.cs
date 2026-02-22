namespace Exercise106.Exceptions;

/// <summary>
/// Standardized error codes for employee data loading and tree building.
/// Thrown as EmployeeException; enables user-friendly messages.
/// </summary>
public enum EmployeeCodeError
{
    /// <summary>Success (no error).</summary>
    OK,

    /// <summary>CSV file not found.</summary>
    FileNotFound,

    /// <summary>Cannot read file (permissions, etc.).</summary>
    CannotAccess,

    /// <summary>CSV file is empty.</summary>
    FileEmpty,

    /// <summary>CSV header line invalid (e.g., wrong columns).</summary>
    WrongHeader,

    /// <summary>CSV row malformed (wrong #fields, parse fails).</summary>
    WrongCsvFormat,

    /// <summary>Employee ID < 1 or non-integer.</summary>
    InvalidEmployeeId,

    /// <summary>Salary <= 0 or non-decimal.</summary>
    InvalidSalary,

    /// <summary>Manager ID < 1 or non-integer (non-CEO).</summary>
    InvalidManagerId,

    /// <summary>FirstName empty or missing.</summary>
    MissingFirstName,

    /// <summary>LastName empty or missing.</summary>
    MissingLastName,

    /// <summary>No CEO (RootIndex == 0) found after load.</summary>
    NoRoot,

    /// <summary>Multiple CEOs - Multiple ManagerId == 0 detected.</summary>
    TwoRoot,

    /// <summary>Employee references non-existent ManagerId.</summary>
    MissingManagerId
}
