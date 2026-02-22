using System.Runtime.CompilerServices;
using Exercise106.Exceptions;
using Exercise106.Models;

// Enables internal testing
[assembly: InternalsVisibleTo("Exercise106.Tests")]
namespace Exercise106
{
    /*
    * Console application that reads employee data from a CSV file and writes analysis results to the console.
    * The CSV filename must be provided as a single command-line argument.
    *
    * Expected CSV structure:
    *   Id,FirstName,LastName,Salary,ManagerId
    *   123,Joe,Doe,60000,
    *   124,Martin,Chekov,45000,123
    *   125,Bob,Ronstad,47000,123
    *   300,Alice,Hasacat,50000,124
    *   305,Brett,Hardleaf,34000,300
    *
    * Each row represents a single employee; the CEO has an empty ManagerId.
    * The file can contain up to 1,000 rows.
    * Assumption: the order of employees in the CSV file is not guaranteed.
    * Note: Console output is used for simplicity and may differ from the final logging strategy.
    */

    /// <summary>
    /// Console app analyzes employee org structure from CSV.
    /// Expects single arg: CSV filename (e.g., employees.csv).
    /// Reports salary violations and long chains; handles errors gracefully.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Orchestrates CSV load → tree build → analysis → console output.
        /// </summary>
        /// <param name="args">CLI args; args[0] = CSV path.</param>
        internal static void Main(string[] args)
        {
            // Validate exactly one arg (filename).
            if (args?.Length != 1)
            {
                Console.Out.WriteLine("Usage: dotnet Exercise106.dll <filename>");
                Console.Out.WriteLine($"You provided {args?.Length} parameter(s).");
                return;
            }
            try
            {
                // Load CSV into raw tree.
                string filename = args[0];
                // Use the parameters
                Console.Out.WriteLine($"Filename: {filename}");
                Console.Out.WriteLine("Reading the file");
                EmployeeCsvReader employeeCsvReader = new(filename);
                // Link parent-child relations.
                Console.Out.WriteLine("Building the tree");
                var employeeTree = employeeCsvReader.GetEmployeeTree();
                employeeTree.BuildTree();
                // Traverse and collect violations (start at CEO).
                Console.Out.WriteLine("Analyzing the tree");
                Report report = new();
                employeeTree.FindEmployeeToReport(employeeTree.RoodIndex, -1, report);
                // Print results or "no issues".
                if (report.Count == 0)
                {
                    Console.Out.WriteLine("No employees to report");
                    return;
                }
                Console.Out.WriteLine("Employee Report");
                Console.Out.WriteLine("===============");
                foreach (var reportItem in report)
                {
                    if (reportItem.Reason == ReportReason.TooManyManagers)
                    {
                        Console.Out.WriteLine($"Employee: {reportItem.Employee} Reason: {reportItem.Reason} Managers: {reportItem.ManagerCount}");
                    }
                    else
                    {
                        Console.Out.WriteLine($"Employee: {reportItem.Employee} Reason: {reportItem.Reason}");
                    }
                }
                Console.Out.WriteLine("===============");
                Console.Out.WriteLine($"Total reported employees: {report.Count}");
                Console.Out.WriteLine("===============");
            }
            catch(EmployeeException ex)
            {
                // Detailed error output for debugging.
                Console.Out.WriteLine("Error during the execution.");
                Console.Out.WriteLine($"ErrorCode: {ex.ErrorCode} Message: {ex.Message} InnerException: {ex.InnerException}");
            }
        }
    }
}