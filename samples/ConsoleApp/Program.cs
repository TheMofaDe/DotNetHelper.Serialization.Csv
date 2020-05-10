using System;
using System.Collections.Generic;
using System.IO;
using DotNetHelper.Serialization.Csv;


namespace ConsoleApp
{
    class Program
    {

        public class Employee
        {
            public string FirstName { get; set; } = "Jon";
            public string LastName { get; set; } = "Last";
            public int Id { get; set; }
        }
        static void Main(string[] args)
        {
            // 
            var csvSerializer = new DataSourceCsv();
            var employee = new Employee { Id = 1 };

            // WRITE A SINGLE OBJECT TO A FILE
            using (var fileStream = new FileStream($@"C:\Temp\employee.csv", FileMode.Create, FileAccess.Write))
                csvSerializer.SerializeToStream(employee, fileStream);

            // WRITE A LIST OF OBJECTS TO A FILE
            using (var fileStream = new FileStream($@"C:\Temp\employeeList.csv", FileMode.Create, FileAccess.Write))
                csvSerializer.SerializeListToStream(new List<Employee>() { employee }, fileStream);


            // READ OBJECT FROM CSV FILE 
            var employeeFromFile = csvSerializer.Deserialize<Employee>(new StreamReader($@"C:\Temp\employee.csv").BaseStream);

            // READ LIST OF OBJECTS FROM CSV FILE 
            var employeesFromFile = csvSerializer.DeserializeToList<Employee>(new StreamReader($@"C:\Temp\employeeList.csv").BaseStream);

            Console.ReadLine();
            Console.WriteLine("Hello World!");
        }
    }
}
