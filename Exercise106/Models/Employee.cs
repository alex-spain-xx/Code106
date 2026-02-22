using Exercise106.Exceptions;

namespace Exercise106.Models
{
    /// <summary>
    /// Immutable model representing an employee from the input CSV.
    /// Uses a record for value equality, immutability, and concise data modeling.
    /// Assumptions:
    /// - Salary is decimal (currency precision) and > 0.
    /// - Employee ID >= 1.
    /// - CSV uses comma separators; no embedded commas; decimals use dot.
    /// </summary>
    /// <param name="Id">Unique employee identifier.</param>
    /// <param name="FirstName">Employee's first name.</param>
    /// <param name="LastName">Employee's last name.</param>
    /// <param name="Salary">Annual salary in currency units.</param>
    /// <param name="ManagerId">Direct manager's ID (0 for CEO).</param>
    public record Employee(int Id, string FirstName, string LastName, decimal Salary, int ManagerId)
    {
        // Backing field for downward tree navigation (manager -> direct reports).
        // It stores subordinate IDs for efficient lookup.
        private List<int>? directSubordinates = null;


        /// <summary>
        /// Parses a single CSV line into an Employee instance.
        /// Validates format, required fields, and business rules; throws on errors.
        /// </summary>
        /// <param name="csvLine">Raw CSV line (e.g., "123,Joe,Doe,60000,").</param>
        /// <returns>Validated Employee record.</returns>
        /// <exception cref="EmployeeException">On parse/invalid data errors.</exception>
        public static Employee Read(string csvLine)
        {
            // Parse comma-separated values.
            var values = csvLine.Split(',');
            // Trim whitespace from each field.
            for (int i = 0; i < values.Length; i++)
            {
                values[i] = values[i].Trim();
            }
            // Validate exactly 5 fields expected.
            if (values.Length != 5)
            {
                throw new EmployeeException(EmployeeCodeError.WrongCsvFormat);
            }
            // Parse and validate ID (>0).
            if (!int.TryParse(values[0], out int id))
            {
                throw new EmployeeException(EmployeeCodeError.WrongCsvFormat);
            }
            if(id < 1)
            {
                throw new EmployeeException(EmployeeCodeError.InvalidEmployeeId);
            }
            // Validate non-empty names.
            if (string.IsNullOrEmpty(values[1]))
            {
                throw new EmployeeException(EmployeeCodeError.MissingFirstName);
            }
            if (string.IsNullOrEmpty(values[2]))
            {
                throw new EmployeeException(EmployeeCodeError.MissingLastName);
            }
            // Parse and validate salary (>0).
            if (!decimal.TryParse(values[3], out decimal salary))
            {
                throw new EmployeeException(EmployeeCodeError.WrongCsvFormat);
            }
            if (salary <= 0)
            {
                throw new EmployeeException(EmployeeCodeError.InvalidSalary);
            }
            // Parse manager ID(0 if empty for CEO; else >= 1).
            if (!int.TryParse(values[4], out int managerId))
            {
                if (string.IsNullOrEmpty(values[4]))
                {
                    managerId = 0;
                }
                else
                {
                    throw new EmployeeException(EmployeeCodeError.WrongCsvFormat);
                }
            }
            else if(managerId < 1)
            {
                throw new EmployeeException(EmployeeCodeError.InvalidManagerId);
            }
            return new Employee(id, values[1], values[2], salary, managerId);
        }

        /// <summary>
        /// Adds a direct subordinate's ID to this employee's list.
        /// Enables tree traversal from managers to reports.
        /// </summary>
        public void AddDirectSubordinate(Employee employee)
        {
            directSubordinates ??= new();
            directSubordinates.Add(employee.Id);
        }

        /// <summary>
        /// Gets the list of direct subordinate IDs (nullable if none).
        /// Used for salary average calculations and chain length checks.
        /// </summary>
        public ICollection<int>? GetDirectSubordinates()
        {
            return directSubordinates;
        }
    }
}
