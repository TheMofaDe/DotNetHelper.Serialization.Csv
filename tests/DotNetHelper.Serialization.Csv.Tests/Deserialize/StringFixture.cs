using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using CsvHelper.Configuration;
using DotNetHelper.Serialization.Csv.Tests.Models;
using NUnit.Framework;

namespace DotNetHelper.Serialization.Csv.Tests.Deserialize
{
    [TestFixture]
    [NonParallelizable] //since were sharing a single file across multiple test cases we don't want Parallelizable
    public class StringTestFixture
    {
        public DataSourceCsv DataSource { get; set; } = new DataSourceCsv(new Configuration { Encoding = Encoding.UTF8 });
        public StringTestFixture()
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
        public void Test_Deserialize_Csv_To_Typed_Object()
        {
            var employee = DataSource.Deserialize(MockData.EmployeeAsCsvWithHeader, typeof(Employee));
            dynamic dyn = employee;
            EnsureFirstNameAndLastNameMatchMockData(dyn.FirstName.ToString(), dyn.LastName.ToString());
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




        private void EnsureFirstNameAndLastNameMatchMockData(string firstName, string lastName)
        {
            if (firstName.Equals(MockData.Employee.FirstName) && lastName.Equals(MockData.Employee.LastName))
            {

            }
            else
            {
                Assert.Fail("Dynamic Object doesn't matches expected results");
            }
        }

        private void EnsureDynamicObjectMatchMockData(dynamic dyn)
        {
            EnsureFirstNameAndLastNameMatchMockData(dyn.FirstName.ToString(), dyn.LastName.ToString());
        }

        private void EnsureGenericObjectMatchMockData(Employee employee)
        {
            EnsureFirstNameAndLastNameMatchMockData(employee.FirstName, employee.LastName);
        }

        private void EnsureStreamIsNotDisposeAndIsAtEndOfStream(Stream stream)
        {
            try
            {
                if (stream.Position != stream.Length)
                {
                    Assert.Fail("The entire stream has not been read");
                }
            }
            catch (ObjectDisposedException disposedException)
            {
                Assert.Fail($"The stream has been disposed {disposedException.Message}");
            }

        }


        private void EnsureStreamIsDispose(Stream stream)
        {
            try
            {
                var position = stream.Position;
                Assert.Fail("The stream is not disposed.");
            }
            catch (ObjectDisposedException)
            {
                return;
            }
        }




    }
}