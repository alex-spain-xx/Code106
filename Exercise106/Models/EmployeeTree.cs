using Exercise106.Exceptions;

namespace Exercise106.Models
{
    /// <summary>
    /// Hierarchical representation of all employees as a Dictionary (ID-keyed).
    /// Inherits Dictionary for unordered CSV loading (no sort guarantee); builds tree links post-load.
    /// Supports CEO detection, tree building, and traversal for violation reports.
    /// </summary>
    public class EmployeeTree : Dictionary<int, Employee>
    {
        /// <summary>
        /// ID of the CEO (root node); 0 until first CEO added.
        /// Ensures exactly one CEO; set automatically on Add().
        /// </summary>
        public int RoodIndex { get; internal set; }

        /// <summary>
        /// Overrides Add() to auto-detect and set CEO (ManagerId == 0).
        /// Enforces single CEO rule; delegates to base for storage.
        /// </summary>
        /// <exception cref="EmployeeException">Multiple CEOs detected.</exception>
        public new void Add(int key, Employee value)
        {
            // Auto-set root if CEO (ManagerId == 0).
            if (value.ManagerId == 0)
            {
                // Check if a CEO (root) has been already set
                if(RoodIndex != 0)
                {
                    throw new EmployeeException(EmployeeCodeError.TwoRoot);
                }
                // If the employee is the CEO, set the RootIndex to its Id
                RoodIndex = value.Id;
            }
            // Store in dictionary.
            base.Add(key, value);
        }


        /// <summary>
        /// Builds downward tree links by scanning all employees' ManagerId.
        /// Calls Employee.AddDirectSubordinate() for each valid parent-child pair.
        /// </summary>
        /// <exception cref="EmployeeException">Missing manager referenced.</exception>
        public void BuildTree()
        {
            foreach(Employee employee in Values)
            {
                if(employee.ManagerId != 0)
                {
                    if(!ContainsKey(employee.ManagerId))
                    {
                        throw new EmployeeException(EmployeeCodeError.MissingManagerId);
                    }
                    this[employee.ManagerId].AddDirectSubordinate(employee);
                }
            }
        }


        /// <summary>
        /// Recursively traverses tree from startIndex (usually RootIndex).
        /// Checks managers for salary violations (20-50% above direct reports' avg)
        /// and chain length (>4 managers from CEO).
        /// Accumulates issues in provided Report.
        /// </summary>
        /// <param name="startIndex">Employee ID to check (CEO: RootIndex).</param>
        /// <param name="managerCount">Managers from CEO to current (CEO: 0).</param>
        /// <param name="report">Mutable report to append violations.</param>
        public void FindEmployeeToReport(int startIndex, int managerCount, Report report)
        {
            Employee employee = this[startIndex];
            var directSubordinates = employee.GetDirectSubordinates();
            // Salary checks only for managers (has direct reports).
            if (directSubordinates != null)
            {
                // Sum salaries of direct reports.
                decimal totalSalary = 0;
                foreach (int childIndex in directSubordinates)
                {
                    totalSalary += this[childIndex].Salary;
                }
                decimal averageSalary = totalSalary / directSubordinates.Count;
                // Flag low salary: < 120% of avg.
                if (employee.Salary < (averageSalary * 1.2m))
                {
                    report.Add(new ReportItem(ReportReason.SalaryLesserThan120PercentAverage, employee));
                }
                // Flag high salary: > 150% of avg.
                else if (employee.Salary > averageSalary * 1.5m)
                {
                    report.Add(new ReportItem(ReportReason.SalaryGreaterThan150PercentAverage, employee));
                }
            }
            // Flag long chain: >4 managers from CEO.
            if (managerCount > 4)
            {
                report.Add(new ReportItem(ReportReason.TooManyManagers, employee, managerCount));
            }
            // Base case: leaf node (no subordinates).
            if (directSubordinates == null)
            {
                return;
            }
            // Recurse on children (increment manager count).
            foreach (int childIndex in directSubordinates)
            {
                FindEmployeeToReport(childIndex, managerCount + 1, report);
            }
        }
    }
}
