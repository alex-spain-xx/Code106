namespace Exercise106.Tests
{
    [TestClass]
    public class EmployeeConsoleTest
    {
        private StringWriter? stringWriter = null;
        private TextWriter? originalOut = null;


        [TestInitialize]
        public void TestInitialize()
        {
            stringWriter = new StringWriter();
            originalOut = Console.Out;
            Console.SetOut(stringWriter);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            if (originalOut != null)
            {
                Console.SetOut(originalOut);
            }
            stringWriter?.Dispose();
        }

        [TestMethod]
        public void Test_ConsoleApp_When_NoInput()
        {
            Program.Main(new string[] { });

            string consoleOut = stringWriter?.ToString() ?? string.Empty;
            StringAssert.Contains(consoleOut, "Usage: dotnet Exercise106.dll <filename>");
            StringAssert.Contains(consoleOut, "You provided 0 parameter(s)");
        }

        [TestMethod]
        public void Test_ConsoleApp_When_NonExistingFile()
        {
            Program.Main(new string[] { "Test.txt" });

            string consoleOut = stringWriter?.ToString() ?? string.Empty;
            StringAssert.Contains(consoleOut, "Error during the execution");
            StringAssert.Contains(consoleOut, "ErrorCode: FileNotFound");
        }

        [TestMethod]
        public void Test_ConsoleApp_When_EmptyFile()
        {
            Program.Main(new string[] { "../../../TestFiles/EmptyFile.csv" });

            string consoleOut = stringWriter?.ToString() ?? string.Empty;
            StringAssert.Contains(consoleOut, "Error during the execution");
            StringAssert.Contains(consoleOut, "ErrorCode: FileEmpty");
        }

        [TestMethod]
        public void Test_ConsoleApp_When_FileContainsWrongHeader()
        {
            Program.Main(new string[] { "../../../TestFiles/WrongHeaderFile.csv" });

            string consoleOut = stringWriter?.ToString() ?? string.Empty;
            StringAssert.Contains(consoleOut, "Error during the execution");
            StringAssert.Contains(consoleOut, "ErrorCode: WrongHeader");
        }

        [TestMethod]
        public void Test_ConsoleApp_When_FileContainsNoRoot()
        {
            Program.Main(new string[] { "../../../TestFiles/NoRootSampleFile.csv" });

            string consoleOut = stringWriter?.ToString() ?? string.Empty;
            StringAssert.Contains(consoleOut, "Error during the execution");
            StringAssert.Contains(consoleOut, "ErrorCode: NoRoot");
        }

        [TestMethod]
        public void Test_ConsoleApp_When_FileNotValid()
        {
            Program.Main(new string[] { "../../../TestFiles/NotValidFile.csv" });

            string consoleOut = stringWriter?.ToString() ?? string.Empty;
            StringAssert.Contains(consoleOut, "Error during the execution");
            StringAssert.Contains(consoleOut, "ErrorCode: WrongCsvFormat");
        }

        [TestMethod]
        public void Test_ConsoleApp_When_FileTwoRoot()
        {
            Program.Main(new string[] { "../../../TestFiles/TwoRootFile.csv" });

            string consoleOut = stringWriter?.ToString() ?? string.Empty;
            StringAssert.Contains(consoleOut, "Error during the execution");
            StringAssert.Contains(consoleOut, "ErrorCode: TwoRoot");
        }

        [TestMethod]
        public void Test_ConsoleApp_When_FileNoEmployeeToReport()
        {
            Program.Main(new string[] { "../../../TestFiles/ValidSampleNoErrorFile.csv" });

            string consoleOut = stringWriter?.ToString() ?? string.Empty;
            StringAssert.Contains(consoleOut, "Reading the file");
            StringAssert.Contains(consoleOut, "Building the tree");
            StringAssert.Contains(consoleOut, "Analyzing the tree");
            StringAssert.Contains(consoleOut, "No employees to report");
        }

        [TestMethod]
        public void Test_ConsoleApp_When_FileOneEmployeeToReport()
        {
            Program.Main(new string[] { "../../../TestFiles/ValidSampleFile.csv" });

            string consoleOut = stringWriter?.ToString() ?? string.Empty;
            StringAssert.Contains(consoleOut, "Reading the file");
            StringAssert.Contains(consoleOut, "Building the tree");
            StringAssert.Contains(consoleOut, "Analyzing the tree");
            StringAssert.Contains(consoleOut, "Employee Report");
            StringAssert.Contains(consoleOut, "Employee: Employee { Id = 124, FirstName = Martin, LastName = Chekov, Salary = 45000, ManagerId = 123 } Reason: SalaryLesserThan120PercentAverage");
            StringAssert.Contains(consoleOut, "Total reported employees: 1");
        }

        [TestMethod]
        public void Test_ConsoleApp_When_FileTwoEmployeeToReport()
        {
            Program.Main(new string[] { "../../../TestFiles/SimpleAverageFile.csv" });

            string consoleOut = stringWriter?.ToString() ?? string.Empty;
            StringAssert.Contains(consoleOut, "Reading the file");
            StringAssert.Contains(consoleOut, "Building the tree");
            StringAssert.Contains(consoleOut, "Analyzing the tree");
            StringAssert.Contains(consoleOut, "Employee Report");
            StringAssert.Contains(consoleOut, "Employee: Employee { Id = 200, FirstName = Laura, LastName = LowMgr, Salary = 52000, ManagerId = 1 } Reason: SalaryLesserThan120PercentAverage");
            StringAssert.Contains(consoleOut, "Employee: Employee { Id = 300, FirstName = Grace, LastName = HighMgr, Salary = 90000, ManagerId = 1 } Reason: SalaryGreaterThan150PercentAverage");
            StringAssert.Contains(consoleOut, "Total reported employees: 2");
        }

        [TestMethod]
        public void Test_ConsoleApp_When_FileThreeEmployeeToReport()
        {
            Program.Main(new string[] { "../../../TestFiles/ComplexAverageFile.csv" });

            string consoleOut = stringWriter?.ToString() ?? string.Empty;
            StringAssert.Contains(consoleOut, "Reading the file");
            StringAssert.Contains(consoleOut, "Building the tree");
            StringAssert.Contains(consoleOut, "Analyzing the tree");
            StringAssert.Contains(consoleOut, "Employee Report");
            StringAssert.Contains(consoleOut, "Employee: Employee { Id = 150, FirstName = L1B, LastName = Overpaid2, Salary = 110000, ManagerId = 100 } Reason: SalaryGreaterThan150PercentAverage");
            StringAssert.Contains(consoleOut, "Employee: Employee { Id = 200, FirstName = L2, LastName = Overpaid, Salary = 85000, ManagerId = 100 } Reason: SalaryGreaterThan150PercentAverage");
            StringAssert.Contains(consoleOut, "Employee: Employee { Id = 300, FirstName = L3, LastName = Underpaid, Salary = 51900, ManagerId = 200 } Reason: SalaryLesserThan120PercentAverage");
            StringAssert.Contains(consoleOut, "Total reported employees: 3");
        }

        [TestMethod]
        public void Test_ConsoleApp_When_FileTooManyManagersToReport()
        {
            Program.Main(new string[] { "../../../TestFiles/TooManyManagerFile.csv" });

            string consoleOut = stringWriter?.ToString() ?? string.Empty;
            StringAssert.Contains(consoleOut, "Reading the file");
            StringAssert.Contains(consoleOut, "Building the tree");
            StringAssert.Contains(consoleOut, "Analyzing the tree");
            StringAssert.Contains(consoleOut, "Employee Report");
            StringAssert.Contains(consoleOut, "Employee: Employee { Id = 150, FirstName = L1B, LastName = Overpaid2, Salary = 110000, ManagerId = 100 } Reason: SalaryGreaterThan150PercentAverage");
            StringAssert.Contains(consoleOut, "Employee: Employee { Id = 200, FirstName = L2, LastName = Overpaid, Salary = 85000, ManagerId = 100 } Reason: SalaryGreaterThan150PercentAverage");
            StringAssert.Contains(consoleOut, "Employee: Employee { Id = 300, FirstName = L3, LastName = Underpaid, Salary = 51900, ManagerId = 200 } Reason: SalaryLesserThan120PercentAverage");
            StringAssert.Contains(consoleOut, "Employee: Employee { Id = 501, FirstName = TooMany1-5, LastName = Deep5, Salary = 24000, ManagerId = 500 } Reason: TooManyManagers Managers: 5");
            StringAssert.Contains(consoleOut, "Employee: Employee { Id = 600, FirstName = L6-TooMany2, LastName = Deep5, Salary = 25000, ManagerId = 500 } Reason: TooManyManagers Managers: 5");
            StringAssert.Contains(consoleOut, "Employee: Employee { Id = 601, FirstName = TooMany1-6, LastName = Deep6, Salary = 20000, ManagerId = 600 } Reason: TooManyManagers Managers: 6");
            StringAssert.Contains(consoleOut, "Employee: Employee { Id = 502, FirstName = TooMany2-5, LastName = Deep5, Salary = 24000, ManagerId = 500 } Reason: TooManyManagers Managers: 5");
            StringAssert.Contains(consoleOut, "Total reported employees: 7");
        }
    }
}

