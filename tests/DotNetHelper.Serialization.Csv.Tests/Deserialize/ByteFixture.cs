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
    public class StreamTestFixture : BaseDeserialize
    {
      
        public StreamTestFixture()
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
        public void Test_Deserialize_Stream_To_Dynamic_Object([Values(false, true)] bool leaveStreamOpen)
        {
            var stream = MockData.GetEmployeeAsStream(DataSource.Configuration.Encoding);
            var employee = DataSource.Deserialize(stream,1024,leaveStreamOpen);
            EnsureFirstNameAndLastNameMatchMockData(employee.FirstName, employee.LastName);
            if (leaveStreamOpen)
            {
                EnsureStreamIsNotDisposeAndIsAtEndOfStream(stream);
            }
            else
            {
                EnsureStreamIsDispose(stream);
            }
        }

        [Author("Joseph McNeal Jr", "josephmcnealjr@gmail.com")]
        [Test]
        public void Test_Deserialize_Stream_To_Dynamic_Object_List([Values(false, true)] bool leaveStreamOpen)
        {
            var stream = MockData.GetEmployeeAsStream(DataSource.Configuration.Encoding);
            var employees = DataSource.DeserializeToList(stream,1024,leaveStreamOpen);
            EnsureFirstNameAndLastNameMatchMockData(employees.First().FirstName, employees.First().LastName);
            if (leaveStreamOpen)
            {
                EnsureStreamIsNotDisposeAndIsAtEndOfStream(stream);
            }
            else
            {
                EnsureStreamIsDispose(stream);
            }
        }


        [Author("Joseph McNeal Jr", "josephmcnealjr@gmail.com")]
        [Test]
        public void Test_Deserialize_Stream_To_Expando_Object([Values(false, true)] bool leaveStreamOpen)
        {
            var stream = MockData.GetEmployeeAsStream(DataSource.Configuration.Encoding);
            var employee = DataSource.Deserialize<ExpandoObject>(stream,1024,leaveStreamOpen);
            if (employee is IDictionary<string, object> dictionary)
            {
                EnsureFirstNameAndLastNameMatchMockData(dictionary["FirstName"].ToString(), dictionary["LastName"].ToString());
            }
            else
            {
                Assert.Fail("Object did not deserialize to expected type");
            }
            if (leaveStreamOpen)
            {
                EnsureStreamIsNotDisposeAndIsAtEndOfStream(stream);
            }
            else
            {
                EnsureStreamIsDispose(stream);
            }
        }

        [Author("Joseph McNeal Jr", "josephmcnealjr@gmail.com")]
        [Test]
        public void Test_Deserialize_Stream_To_Expando_Object_2([Values(false, true)] bool leaveStreamOpen)
        {
            var stream = MockData.GetEmployeeAsStream(DataSource.Configuration.Encoding);
            var employee2 = DataSource.Deserialize(stream, typeof(ExpandoObject),1024,leaveStreamOpen);
            if (employee2 is IDictionary<string, object> dictionary2)
            {
                EnsureFirstNameAndLastNameMatchMockData(dictionary2["FirstName"].ToString(), dictionary2["LastName"].ToString());
            }
            else
            {
                Assert.Fail("Object did not deserialize to expected type");
            }
            if (leaveStreamOpen)
            {
                EnsureStreamIsNotDisposeAndIsAtEndOfStream(stream);
            }
            else
            {
                EnsureStreamIsDispose(stream);
            }
        }



        [Author("Joseph McNeal Jr", "josephmcnealjr@gmail.com")]
        [Test]
        public void Test_Deserialize_Stream_To_Expando_Object_List([Values(false, true)] bool leaveStreamOpen)
        {
            var stream = MockData.GetEmployeeListAsStream(DataSource.Configuration.Encoding);
            var employees = DataSource.Deserialize<List<ExpandoObject>>(stream,1024,leaveStreamOpen);
            EnsureDynamicObjectMatchMockData(employees.First());
            if (leaveStreamOpen)
            {
                EnsureStreamIsNotDisposeAndIsAtEndOfStream(stream);
            }
            else
            {
                EnsureStreamIsDispose(stream);
            }
        }

        [Author("Joseph McNeal Jr", "josephmcnealjr@gmail.com")]
        [Test]
        public void Test_Deserialize_Stream_To_Expando_Object_List_2([Values(false, true)] bool leaveStreamOpen)
        {
            var stream = MockData.GetEmployeeListAsStream(DataSource.Configuration.Encoding);
            var employees = DataSource.Deserialize(stream, typeof(List<ExpandoObject>),1024,leaveStreamOpen);
            if (employees is List<ExpandoObject> list)
            {
                EnsureDynamicObjectMatchMockData(list.First());
            }
            else
            {
                Assert.Fail("Deserialize Failed");
            }
            if (leaveStreamOpen)
            {
                EnsureStreamIsNotDisposeAndIsAtEndOfStream(stream);
            }
            else
            {
                EnsureStreamIsDispose(stream);
            }
        }




        [Author("Joseph McNeal Jr", "josephmcnealjr@gmail.com")]
        [Test]
        public void Test_Deserialize_Stream_To_Typed_Object([Values(false, true)] bool leaveStreamOpen)
        {
            var stream = MockData.GetEmployeeAsStream(DataSource.Configuration.Encoding);
            var employee = DataSource.Deserialize<Employee>(stream,1024,leaveStreamOpen);
            EnsureFirstNameAndLastNameMatchMockData(employee.FirstName, employee.LastName);
            if (leaveStreamOpen)
            {
                EnsureStreamIsNotDisposeAndIsAtEndOfStream(stream);
            }
            else
            {
                EnsureStreamIsDispose(stream);
            }
        }

        [Author("Joseph McNeal Jr", "josephmcnealjr@gmail.com")]
        [Test]
        public void Test_Deserialize_Stream_To_Typed_Object_2([Values(false, true)] bool leaveStreamOpen)
        {
            var stream = MockData.GetEmployeeAsStream(DataSource.Configuration.Encoding);
            var employee = DataSource.Deserialize(stream, typeof(Employee),1024,leaveStreamOpen);
            if (employee is Employee dyn)
            {
                EnsureFirstNameAndLastNameMatchMockData(dyn.FirstName, dyn.LastName);
            }
            else
            {
                Assert.Fail("Object did not deserialize to expected type");
            }
            if (leaveStreamOpen)
            {
                EnsureStreamIsNotDisposeAndIsAtEndOfStream(stream);
            }
            else
            {
                EnsureStreamIsDispose(stream);
            }
        }


        [Author("Joseph McNeal Jr", "josephmcnealjr@gmail.com")]
        [Test]
        public void Test_Deserialize_Stream_To_Typed_Object_List([Values(false, true)] bool leaveStreamOpen)
        {
            var stream = MockData.GetEmployeeListAsStream(DataSource.Configuration.Encoding);
            var employees = DataSource.Deserialize<List<Employee>>(stream,1024,leaveStreamOpen);
            EnsureFirstNameAndLastNameMatchMockData(employees.First().FirstName, employees.First().LastName);
            if (leaveStreamOpen)
            {
                EnsureStreamIsNotDisposeAndIsAtEndOfStream(stream);
            }
            else
            {
                EnsureStreamIsDispose(stream);
            }
        }

        [Author("Joseph McNeal Jr", "josephmcnealjr@gmail.com")]
        [Test]
        public void Test_Deserialize_Stream_To_Typed_Object_List_2([Values(false, true)] bool leaveStreamOpen)
        {
            var stream = MockData.GetEmployeeListAsStream(DataSource.Configuration.Encoding);
            var employee = DataSource.Deserialize(stream, typeof(List<Employee>),1024,leaveStreamOpen);
            if (employee is List<Employee> employees)
            {
                var dyn = employees.First();
                EnsureFirstNameAndLastNameMatchMockData(dyn.FirstName, dyn.LastName);
            }
            else
            {
                Assert.Fail("Object did not deserialize to expected type");
            }
            if (leaveStreamOpen)
            {
                EnsureStreamIsNotDisposeAndIsAtEndOfStream(stream);
            }
            else
            {
                EnsureStreamIsDispose(stream);
            }
        }













    }
}