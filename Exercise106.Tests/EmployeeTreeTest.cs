using Exercise106.Exceptions;
using Exercise106.Models;

namespace Exercise106.Tests
{
    [TestClass]
    public class EmployeeTreeTest
    {

        [TestMethod]
        public void Test_BuildTree_When_FileContainsValidSample()
        {
            var employeeCsvReader = new EmployeeCsvReader("../../../TestFiles/ValidSampleFile.csv");

            var employees = employeeCsvReader.GetEmployeeTree();
            Assert.HasCount(5, employees);
            employees.BuildTree();

            CollectionAssert.AreEqual(new List<int>() { 124, 125 }, (System.Collections.ICollection?)employees[123].GetDirectSubordinates());
            CollectionAssert.AreEqual(new List<int>() { 300 }, (System.Collections.ICollection?)employees[124].GetDirectSubordinates());
            Assert.IsNull(employees[125].GetDirectSubordinates());
            CollectionAssert.AreEqual(new List<int>() { 305 }, (System.Collections.ICollection?)employees[300].GetDirectSubordinates());
            Assert.IsNull(employees[305].GetDirectSubordinates());
        }

        [TestMethod]
        public void Test_Report_When_OneEmployeeToReport()
        {
            var employeeCsvReader = new EmployeeCsvReader("../../../TestFiles/ValidSampleFile.csv");

            var employees = employeeCsvReader.GetEmployeeTree();
            Assert.HasCount(5, employees);
            employees.BuildTree();
            Report report = new();
            employees.FindEmployeeToReport(employees.RoodIndex, -1, report);
            Assert.HasCount(1, report);
            Assert.AreEqual(ReportReason.SalaryLesserThan120PercentAverage, report[0].Reason);
            Assert.AreEqual(124, report[0].Employee.Id);
        }

        [TestMethod]
        public void Test_Report_When_TwoEmployeeToReport()
        {
            var employeeCsvReader = new EmployeeCsvReader("../../../TestFiles/SimpleAverageFile.csv");

            var employees = employeeCsvReader.GetEmployeeTree();
            Assert.HasCount(13, employees);
            employees.BuildTree();
            Report report = new();
            employees.FindEmployeeToReport(employees.RoodIndex, -1, report);
            Assert.HasCount(2, report);
            Assert.AreEqual(ReportReason.SalaryLesserThan120PercentAverage, report[0].Reason);
            Assert.AreEqual(200, report[0].Employee.Id);
            Assert.AreEqual(ReportReason.SalaryGreaterThan150PercentAverage, report[1].Reason);
            Assert.AreEqual(300, report[1].Employee.Id);
        }

        [TestMethod]
        public void Test_Report_When_ThreeEmployeeToReport()
        {
            var employeeCsvReader = new EmployeeCsvReader("../../../TestFiles/ComplexAverageFile.csv");

            var employees = employeeCsvReader.GetEmployeeTree();
            Assert.HasCount(15, employees);
            employees.BuildTree();
            Report report = new();
            employees.FindEmployeeToReport(employees.RoodIndex, -1, report);
            Assert.HasCount(3, report);
            Assert.AreEqual(ReportReason.SalaryGreaterThan150PercentAverage, report[0].Reason);
            Assert.AreEqual(150, report[0].Employee.Id);
            Assert.AreEqual(ReportReason.SalaryGreaterThan150PercentAverage, report[1].Reason);
            Assert.AreEqual(200, report[1].Employee.Id);
            Assert.AreEqual(ReportReason.SalaryLesserThan120PercentAverage, report[2].Reason);
            Assert.AreEqual(300, report[2].Employee.Id);
        }

        [TestMethod]
        public void Test_Report_When_TooManyManagersToReport()
        {
            var employeeCsvReader = new EmployeeCsvReader("../../../TestFiles/TooManyManagerFile.csv");

            var employees = employeeCsvReader.GetEmployeeTree();
            Assert.HasCount(23, employees);
            employees.BuildTree();
            Report report = new();
            employees.FindEmployeeToReport(employees.RoodIndex, -1, report);
            Assert.HasCount(7, report);
            Assert.AreEqual(ReportReason.SalaryGreaterThan150PercentAverage, report[0].Reason);
            Assert.AreEqual(150, report[0].Employee.Id);
            Assert.AreEqual(ReportReason.SalaryGreaterThan150PercentAverage, report[1].Reason);
            Assert.AreEqual(200, report[1].Employee.Id);
            Assert.AreEqual(ReportReason.SalaryLesserThan120PercentAverage, report[2].Reason);
            Assert.AreEqual(300, report[2].Employee.Id);

            Assert.AreEqual(ReportReason.TooManyManagers, report[3].Reason);
            Assert.AreEqual(501, report[3].Employee.Id);
            Assert.AreEqual(5, report[3].ManagerCount);
            Assert.AreEqual(ReportReason.TooManyManagers, report[4].Reason);
            Assert.AreEqual(600, report[4].Employee.Id);
            Assert.AreEqual(5, report[4].ManagerCount);
            Assert.AreEqual(ReportReason.TooManyManagers, report[5].Reason);
            Assert.AreEqual(601, report[5].Employee.Id);
            Assert.AreEqual(6, report[5].ManagerCount);
            Assert.AreEqual(ReportReason.TooManyManagers, report[6].Reason);
            Assert.AreEqual(502, report[6].Employee.Id);
            Assert.AreEqual(5, report[6].ManagerCount);
        }
    }
}

