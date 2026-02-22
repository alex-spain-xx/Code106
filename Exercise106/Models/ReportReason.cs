namespace Exercise106.Models;

/// <summary>
/// Policy violation types for company org analysis.
/// Maps to ReportItem reasons from tree traversal.
/// </summary>
public enum ReportReason
{
    /// <summary>
    /// Manager salary &lt; 120% of direct reports' average (must earn at least 20% more).
    /// </summary>
    SalaryLesserThan120PercentAverage,

    /// <summary>
    /// Manager salary &gt; 150% of direct reports' average (must not exceed 50% more).
    /// </summary>
    SalaryGreaterThan150PercentAverage,

    /// <summary>
    /// Employee chain &gt; 4 managers from CEO (too long reporting line).
    /// ExtraData holds exact count.
    /// </summary>
    TooManyManagers
}
