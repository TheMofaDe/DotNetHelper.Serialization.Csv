using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using CsvHelper.Configuration;
using DotNetHelper.Serialization.Csv.Tests.Models;
using NUnit.Framework;

namespace DotNetHelper.Serialization.Csv.Tests
{
    [TestFixture]
    [NonParallelizable] //since were sharing a single file across multiple test cases we don't want Parallelizable
    public class GenericTestFixture : BaseDeserialize
    {
        

        public DataSourceCsv DataSource { get; set; } =  new DataSourceCsv(new Configuration { Encoding = Encoding.UTF8 });

        public GenericTestFixture()
        {

        }


        [OneTimeSetUp]
        public void RunBeforeAnyTests()
        {
           DataSource = new DataSourceCsv(new Configuration { Encoding = Encoding.UTF8 });
        }

        [OneTimeTearDown]
        public void RunAfterAnyTests()
        {

        }



        [SetUp]
        public void Init()
        {

        }

        [TearDown]
        public void Cleanup()
        {

        }

        [Author("Joseph McNeal Jr", "josephmcnealjr@gmail.com")]
        [Test]
        public void Test_Deserialize_Csv_To_Dynamic()
        {
            var dyn = DataSource.Deserialize(MockData.EmployeeAsCsvWithHeader);
            EnsureDynamicObjectMatchMockData(dyn);
        }
      

        [Author("Joseph McNeal Jr", "josephmcnealjr@gmail.com")]
        [Test]
        public void Test_Deserialize_Csv_To_Generic()
        {
            var employee = DataSource.Deserialize<Employee>(MockData.EmployeeAsCsvWithHeader);
            EnsureGenericObjectMatchMockData(employee);
        }

      

        [Author("Joseph McNeal Jr", "josephmcnealjr@gmail.com")]
        [Test]
        public void Test_Deserialize_Csv_To_Typed_Object()
        {
            var employee = DataSource.Deserialize(MockData.EmployeeAsCsvWithHeader, typeof(Employee));
            dynamic dyn = employee;
            EnsureFirstNameAndLastNameMatchMockData(dyn.FirstName.ToString(), dyn.LastName.ToString());
        }

    

        [Author("Joseph McNeal Jr", "josephmcnealjr@gmail.com")]
        [Test]
        public void Test_Deserialize_Csv_To_Dynamic_List()
        {
            var employees = DataSource.DeserializeToList(MockData.EmployeeAsCsvWithHeaderList);
            EnsureFirstNameAndLastNameMatchMockData(employees.First().FirstName.ToString(), employees.First().LastName.ToString());
        }

       

        [Author("Joseph McNeal Jr", "josephmcnealjr@gmail.com")]
        [Test]
        public void Test_Deserialize_Csv_To_Generic_List()
        {
            var employees = DataSource.DeserializeToList<Employee>(MockData.EmployeeAsCsvWithHeaderList);
            EnsureGenericObjectMatchMockData(employees.First());
        }


        [Author("Joseph McNeal Jr", "josephmcnealjr@gmail.com")]
        [Test]
        public void Test_Deserialize_Json_To_Typed_Object_Of_List()
        {
            var objects = DataSource.Deserialize(MockData.EmployeeAsCsvWithHeaderList, typeof(List<Employee>));
            var employees = (List<Employee>)objects;
            var employee = employees.First();
            EnsureFirstNameAndLastNameMatchMockData(employee.FirstName, employee.LastName);



        }
        
        [Author("Joseph McNeal Jr", "josephmcnealjr@gmail.com")]
        [Test]
        public void Test_Deserialize_Json_To_Typed_Object_Of_List2()
        {
            var employees = DataSource.DeserializeToList(MockData.EmployeeAsCsvWithHeaderList, typeof(List<Employee>));
            var employee = employees.First() as Employee;
            EnsureFirstNameAndLastNameMatchMockData(employee.FirstName, employee.LastName);
        }




 


    }
}