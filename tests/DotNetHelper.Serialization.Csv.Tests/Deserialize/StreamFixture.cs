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
    public class StreamTestFixture : BaseDeserialize
    {
        public DataSourceCsv DataSource { get; set; } = new DataSourceCsv(new Configuration { Encoding = Encoding.UTF8 });
        public StreamTestFixture()
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
        public void Test_Deserialize_Stream_To_Dynamic([Values(false,true)] bool leaveStreamOpen)
        {
            var stream = MockData.GetEmployeeAsStream(DataSource.Configuration.Encoding);
            var dyn = DataSource.Deserialize(stream,1024, leaveStreamOpen);
            EnsureDynamicObjectMatchMockData(dyn);
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
        public void Test_Deserialize_Stream_To_Generic([Values(false, true)] bool leaveStreamOpen)
        {
            var stream = MockData.GetEmployeeAsStream(DataSource.Configuration.Encoding);
            var dyn = DataSource.Deserialize<Employee>(stream,1024,leaveStreamOpen);
            EnsureGenericObjectMatchMockData(dyn);
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
            var employee = DataSource.Deserialize(stream, typeof(Employee),1024,leaveStreamOpen);
            if (employee is Employee dyn)
            {
                EnsureFirstNameAndLastNameMatchMockData(dyn.FirstName.ToString(), dyn.LastName.ToString());
                if (leaveStreamOpen)
                {
                    EnsureStreamIsNotDisposeAndIsAtEndOfStream(stream);
                }
                else
                {
                    EnsureStreamIsDispose(stream);
                }
            }
            else
            {
                Assert.Fail("Object did not deserialize to expected type");
            }
        }
    


        [Author("Joseph McNeal Jr", "josephmcnealjr@gmail.com")]
        [Test]
        public void Test_Deserialize_Stream_To_Dynamic_List([Values(false, true)] bool leaveStreamOpen)
        {
            var stream = MockData.GetEmployeeListAsStream(DataSource.Configuration.Encoding);
            var dyn = DataSource.DeserializeToList(stream,1024,leaveStreamOpen);
            EnsureDynamicObjectMatchMockData(dyn.First());
            EnsureStreamIsDispose(stream);
        }

   
        [Author("Joseph McNeal Jr", "josephmcnealjr@gmail.com")]
        [Test]
        public void Test_Deserialize_Stream_To_Generic_List([Values(false, true)] bool leaveStreamOpen)
        {
            var stream = MockData.GetEmployeeListAsStream(DataSource.Configuration.Encoding);
            var dyn = DataSource.DeserializeToList<Employee>(stream,1024,leaveStreamOpen);
            EnsureDynamicObjectMatchMockData(dyn.First());
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
            var list = DataSource.DeserializeToList(stream, typeof(Employee),1024,leaveStreamOpen);
            Assert.That(list.TrueForAll(o => o is Employee));
            EnsureGenericObjectMatchMockData(list.First() as Employee);
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