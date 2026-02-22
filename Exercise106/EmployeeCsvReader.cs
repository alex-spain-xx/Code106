using Exercise106.Exceptions;
using Exercise106.Models;

namespace Exercise106
{
    /// <summary>
    /// Reads company employee CSV (up to 1000 rows; unordered).
    /// Validates structure, loads into EmployeeTree (auto-detects CEO).
    /// Throws EmployeeException on errors.
    /// CSV format: Id,FirstName,LastName,Salary,ManagerId (case-insensitive header).
    /// </summary>
    public class EmployeeCsvReader
    {
        // Expected CSV header string (exact match, case-insensitive).
        const string Headers = "Id,FirstName,LastName,Salary,ManagerId";
        // Immutable filename provided at construction.
        readonly string csvFilename;

        /// <summary>
        /// Initializes reader for given CSV path.
        /// </summary>
        /// <param name="csvFilename">Path to employee CSV file.</param>
        public EmployeeCsvReader(string csvFilename)
        {
            this.csvFilename = csvFilename;
        }

        /// <summary>
        /// Loads entire CSV into populated EmployeeTree (unbuilt).
        /// Skips header; validates file/CEO; delegates row parsing.
        /// Caller must call BuildTree() next.
        /// </summary>
        /// <returns>Tree with all employees added (RootIndex set).</returns>
        /// <exception cref="EmployeeException">File/header/CEO/parse errors.</exception>
        public EmployeeTree GetEmployeeTree()
        {
            try
            {
                if (!File.Exists(csvFilename))
                {
                    throw new EmployeeException(EmployeeCodeError.FileNotFound);
                }
                // Using a streamReader to avoid future changes if the 1000 record limits will be removed
                using StreamReader streamReader = new(csvFilename);
                // Read the first line to check the expected headers.
                string? line = streamReader.ReadLine();
                if (line == null)
                {
                    throw new EmployeeException(EmployeeCodeError.FileEmpty);
                }
                if (!line.Equals(Headers, StringComparison.CurrentCultureIgnoreCase))
                {
                    throw new EmployeeException(EmployeeCodeError.WrongHeader);
                }
                EmployeeTree employees = new();
                while ((line = streamReader.ReadLine()) != null)
                {
                    var employee = Employee.Read(line);
                    employees.Add(employee.Id, employee);
                }
                if(employees.RoodIndex == 0)
                {
                    throw new EmployeeException(EmployeeCodeError.NoRoot);
                }
                return employees;
            }
            catch(IOException ioException)
            {
                throw new EmployeeException($"Error in accessing the file {csvFilename}", EmployeeCodeError.CannotAccess, ioException);
            }
        }
    }
}

