using System;
using System.Collections.Generic;
using System.Dynamic;
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
    public class StringTestFixture : BaseDeserialize
    {
      
        public StringTestFixture()
        {

        }

        [OneTimeSetUp]
        public void RunBeforeAnyTests()
        {
           
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
        public void Test_Deserialize_String_To_Dynamic_Object()
        {
            var employee = DataSource.Deserialize(MockData.EmployeeAsCsvWithHeader);
            EnsureFirstNameAndLastNameMatchMockData(employee.FirstName, employee.LastName);
        }

        [Author("Joseph McNeal Jr", "josephmcnealjr@gmail.com")]
        [Test]
        public void Test_Deserialize_String_To_Dynamic_Object_List()
        {
            var employees = DataSource.DeserializeToList(MockData.EmployeeAsCsvWithHeader);
            EnsureFirstNameAndLastNameMatchMockData(employees.First().FirstName, employees.First().LastName);
        }


        [Author("Joseph McNeal Jr", "josephmcnealjr@gmail.com")]
        [Test]
        public void Test_Deserialize_String_To_Expando_Object()
        {
            var employee = DataSource.Deserialize<ExpandoObject>(MockData.EmployeeAsCsvWithHeader);
            if (employee is IDictionary<string, object> dictionary)
            {
                EnsureFirstNameAndLastNameMatchMockData(dictionary["FirstName"].ToString(), dictionary["LastName"].ToString());
            }
            else
            {
                Assert.Fail("Object did not deserialize to expected type");
            }
        }

        [Author("Joseph McNeal Jr", "josephmcnealjr@gmail.com")]
        [Test]
        public void Test_Deserialize_String_To_Expando_Object_2()
        {
            var employee2 = DataSource.Deserialize(MockData.EmployeeAsCsvWithHeader, typeof(ExpandoObject));
            if (employee2 is IDictionary<string, object> dictionary2)
            {
                EnsureFirstNameAndLastNameMatchMockData(dictionary2["FirstName"].ToString(), dictionary2["LastName"].ToString());
            }
            else
            {
                Assert.Fail("Object did not deserialize to expected type");
            }
        }



        [Author("Joseph McNeal Jr", "josephmcnealjr@gmail.com")]
        [Test]
        public void Test_Deserialize_String_To_Expando_Object_List()
        {
            var employees = DataSource.Deserialize<List<ExpandoObject>>(MockData.EmployeeAsCsvWithHeader);
            EnsureDynamicObjectMatchMockData(employees.First());
        }

        [Author("Joseph McNeal Jr", "josephmcnealjr@gmail.com")]
        [Test]
        public void Test_Deserialize_String_To_Expando_Object_List_2()
        {
            var employees = DataSource.Deserialize(MockData.EmployeeAsCsvWithHeader, typeof(List<ExpandoObject>));
            if (employees is List<ExpandoObject> list)
            {
                EnsureDynamicObjectMatchMockData(list.First());
            }
            else
            {
                Assert.Fail("Deserialize Failed");
            }
        }




        [Author("Joseph McNeal Jr", "josephmcnealjr@gmail.com")]
        [Test]
        public void Test_Deserialize_String_To_Typed_Object()
        {
            var employee = DataSource.Deserialize<Employee>(MockData.EmployeeAsCsvWithHeader);
            EnsureFirstNameAndLastNameMatchMockData(employee.FirstName, employee.LastName);
        }

        [Author("Joseph McNeal Jr", "josephmcnealjr@gmail.com")]
        [Test]
        public void Test_Deserialize_String_To_Typed_Object_2()
        {
            var employee = DataSource.Deserialize(MockData.EmployeeAsCsvWithHeader, typeof(Employee));
            if (employee is Employee dyn)
            {
                EnsureFirstNameAndLastNameMatchMockData(dyn.FirstName, dyn.LastName);
            }
            else
            {
                Assert.Fail("Object did not deserialize to expected type");
            }
        }


        [Author("Joseph McNeal Jr", "josephmcnealjr@gmail.com")]
        [Test]
        public void Test_Deserialize_String_To_Typed_Object_List()
        {
            var employees = DataSource.Deserialize<List<Employee>>(MockData.EmployeeAsCsvWithHeader);
            EnsureFirstNameAndLastNameMatchMockData(employees.First().FirstName, employees.First().LastName);
        }

        [Author("Joseph McNeal Jr", "josephmcnealjr@gmail.com")]
        [Test]
        public void Test_Deserialize_String_To_Typed_Object_List_2()
        {
            var employee = DataSource.Deserialize(MockData.EmployeeAsCsvWithHeader, typeof(List<Employee>));
            if (employee is List<Employee> employees)
            {
                var dyn = employees.First();
                EnsureFirstNameAndLastNameMatchMockData(dyn.FirstName, dyn.LastName);
            }
            else
            {
                Assert.Fail("Object did not deserialize to expected type");
            }
        }








        //[Author("Joseph McNeal Jr", "josephmcnealjr@gmail.com")]
        //[Test]
        //public void Test_Deserialize_String_To_Typed_Object_List()
        //{
        //    var objects = DataSource.Deserialize(MockData.EmployeeAsCsvWithHeaderList, typeof(List<Employee>));
        //    var employees = (List<Employee>)objects;
        //    var employee = employees.First();
        //    EnsureFirstNameAndLastNameMatchMockData(employee.FirstName, employee.LastName);
        //}


    }
}