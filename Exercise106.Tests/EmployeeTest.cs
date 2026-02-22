using Exercise106.Exceptions;
using Exercise106.Models;

namespace Exercise106.Tests;

[TestClass]
public class EmployeeTest
{
    [TestMethod]
    public void Test_Read_When_EmptyString()
    {
        string csvLine = string.Empty;

        var ex = Assert.Throws<EmployeeException>(() => Employee.Read(csvLine));
        Assert.AreEqual(EmployeeCodeError.WrongCsvFormat, ex.ErrorCode);
    }

    [TestMethod]
    public void Test_Read_When_NoId()
    {
        string csvLine = ",Martin,Chekov,45000,123";

        var ex = Assert.Throws<EmployeeException>(() => Employee.Read(csvLine));
        Assert.AreEqual(EmployeeCodeError.WrongCsvFormat, ex.ErrorCode);
    }

    [TestMethod]
    public void Test_Read_When_IdLessThanOne()
    {
        string csvLine = "0,Martin,Chekov,45000,123";

        var ex = Assert.Throws<EmployeeException>(() => Employee.Read(csvLine));
        Assert.AreEqual(EmployeeCodeError.InvalidEmployeeId, ex.ErrorCode);
    }

    [TestMethod]
    public void Test_Read_When_NoFirstName()
    {
        string csvLine = "124,,Chekov,45000,123";

        var ex = Assert.Throws<EmployeeException>(() => Employee.Read(csvLine));
        Assert.AreEqual(EmployeeCodeError.MissingFirstName, ex.ErrorCode);
    }

    [TestMethod]
    public void Test_Read_When_NoLastName()
    {
        string csvLine = "124,Martin,,45000,123";

        var ex = Assert.Throws<EmployeeException>(() => Employee.Read(csvLine));
        Assert.AreEqual(EmployeeCodeError.MissingLastName, ex.ErrorCode);
    }

    [TestMethod]
    public void Test_Read_When_NoSalary()
    {
        string csvLine = "124,Martin,Chekov,,123";

        var ex = Assert.Throws<EmployeeException>(() => Employee.Read(csvLine));
        Assert.AreEqual(EmployeeCodeError.WrongCsvFormat, ex.ErrorCode);
    }

    [TestMethod]
    public void Test_Read_When_SalaryLessThanOne()
    {
        string csvLine = "124,Martin,Chekov,0,123";

        var ex = Assert.Throws<EmployeeException>(() => Employee.Read(csvLine));
        Assert.AreEqual(EmployeeCodeError.InvalidSalary, ex.ErrorCode);
    }

    [TestMethod]
    public void Test_Read_When_NoManagerId()
    {
        string csvLine = "124,Martin,Chekov,45000,";

        var employee = Employee.Read(csvLine);
        Assert.AreEqual(new Employee(124, "Martin", "Chekov", 45000, 0), employee);
    }

    [TestMethod]
    public void Test_Read_When_ManagerIdLessThanOne()
    {
        string csvLine = "124,Martin,Chekov,45000,0";

        var ex = Assert.Throws<EmployeeException>(() => Employee.Read(csvLine));
        Assert.AreEqual(EmployeeCodeError.InvalidManagerId, ex.ErrorCode);
    }

    [TestMethod]
    public void Test_Read_When_AllValues()
    {
        string csvLine = "124,Martin,Chekov,45000,123";

        var employee = Employee.Read(csvLine);
        Assert.AreEqual(new Employee(124, "Martin", "Chekov", 45000, 123), employee);
    }
}
