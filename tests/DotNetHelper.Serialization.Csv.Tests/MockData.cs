using System.Collections.Generic;
using System.IO;
using System.Text;
using DotNetHelper.Serialization.Csv.Tests.Models;

namespace DotNetHelper.Serialization.Csv.Tests
{
    public static class MockData
    {

        public static Employee Employee { get; } = new Employee();
        public static string EmployeeAsCsvWithHeader { get; } = @"FirstName,LastName
Kate,Blake";

        public static List<Employee> EmployeeList { get; } = new List<Employee>() { Employee
            , new Employee(){FirstName = "Mabelle",LastName = "Black" } };
        public static string EmployeeAsCsvWithHeaderList { get; } = @"FirstName,LastName
Kate,Blake
Mabelle,Black
";

        public static Stream GetEmployeeAsStream(Encoding encoding)
        {
            return new MemoryStream(encoding.GetBytes(EmployeeAsCsvWithHeader));
        }
        public static Stream GetEmployeeListAsStream(Encoding encoding)
        {
            return new MemoryStream(encoding.GetBytes(EmployeeAsCsvWithHeaderList));
        }
    }
}
