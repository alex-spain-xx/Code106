using System.Reflection.Metadata;
using Exercise106.Exceptions;
using Exercise106.Models;

namespace Exercise106.Tests
{
    [TestClass]
    public class EmployeeCsvReaderTest
    {
        [TestMethod]
        public void Test_GetEmployees_When_FileNotExists()
        {
            var employeeCsvReader = new EmployeeCsvReader("notExistingFile");

            var ex = Assert.Throws<EmployeeException>(() => employeeCsvReader.GetEmployeeTree());
            Assert.AreEqual(EmployeeCodeError.FileNotFound, ex.ErrorCode);
        }

        [TestMethod]
        public void Test_GetEmployees_When_EmptyFile()
        {
            var employeeCsvReader = new EmployeeCsvReader("../../../TestFiles/EmptyFile.csv");

            var ex = Assert.Throws<EmployeeException>(() => employeeCsvReader.GetEmployeeTree());
            Assert.AreEqual(EmployeeCodeError.FileEmpty, ex.ErrorCode);
        }

        [TestMethod]
        public void Test_GetEmployees_When_FileContainsWrongHeader()
        {
            var employeeCsvReader = new EmployeeCsvReader("../../../TestFiles/WrongHeaderFile.csv");

            var ex = Assert.Throws<EmployeeException>(() => employeeCsvReader.GetEmployeeTree());
            Assert.AreEqual(EmployeeCodeError.WrongHeader, ex.ErrorCode);
        }

        [TestMethod]
        public void Test_GetEmployees_When_FileContainsValidSample()
        {
            var employeeCsvReader = new EmployeeCsvReader("../../../TestFiles/ValidSampleFile.csv");

            var employees = employeeCsvReader.GetEmployeeTree();
            Assert.HasCount(5, employees);
            Assert.AreEqual(new Employee(123, "Joe", "Doe", 60000,0), employees[123]);
            Assert.AreEqual(new Employee(124, "Martin", "Chekov", 45000, 123), employees[124]);
            Assert.AreEqual(new Employee(125, "Bob", "Ronstad", 47000, 123), employees[125]);
            Assert.AreEqual(new Employee(300, "Alice", "Hasacat", 50000, 124), employees[300]);
            Assert.AreEqual(new Employee(305, "Brett", "Hardleaf", 34000, 300), employees[305]);
        }

        [TestMethod]
        public void Test_GetEmployees_When_FileContainsNoRoot()
        {
            var employeeCsvReader = new EmployeeCsvReader("../../../TestFiles/NoRootSampleFile.csv");

            var ex = Assert.Throws<EmployeeException>(() => employeeCsvReader.GetEmployeeTree());
            Assert.AreEqual(EmployeeCodeError.NoRoot, ex.ErrorCode);
        }

        [TestMethod]
        public void Test_GetEmployees_When_FileContainsTwoRoot()
        {
            var employeeCsvReader = new EmployeeCsvReader("../../../TestFiles/TwoRootFile.csv");

            var ex = Assert.Throws<EmployeeException>(() => employeeCsvReader.GetEmployeeTree());
            Assert.AreEqual(EmployeeCodeError.TwoRoot, ex.ErrorCode);
        }
    }
}

