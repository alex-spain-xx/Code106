namespace Exercise106.Models
{
    /// <summary>
    /// Single violation entry in a Report.
    /// Captures reason, affected employee, and optional extra data (e.g., chain length).
    /// </summary>
    /// <param name="Reason">Violation type (salary low/high or long chain).</param>
    /// <param name="Employee">Manager/employee with issue.</param>
    /// <param name="ManagerCount">Optional numeric detail for manager count.</param>
    public record ReportItem(ReportReason Reason, Employee Employee, int ManagerCount = 0)
    {
    }
}
