using System;
using System.IO;
using System.Text;
using CsvHelper.Configuration;
using DotNetHelper.Serialization.Csv.Tests.Models;
using NUnit.Framework;

namespace DotNetHelper.Serialization.Csv.Tests
{
    [TestFixture]
    [NonParallelizable] //since were sharing a single file across multiple test cases we don't want Parallelizable
    public class CsvSerializerTextFixture : BaseSerialize
    {


        public CsvSerializerTextFixture()
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
        public void Test_Serialize_Generic_To_Csv()
        {
            var csv = DataSource.SerializeToString(MockData.Employee);
            EnsureGenericObjectMatchMockDataJson(csv);
        }
        [Author("Joseph McNeal Jr", "josephmcnealjr@gmail.com")]
        [Test]
        public void Test_Serialize_Object_To_Csv()
        {
            var csv = DataSource.SerializeToString((object)MockData.Employee);
            EnsureGenericObjectMatchMockDataJson(csv);
        }
        [Test]
        public void Test_Serialize_ObjectList_To_Csv()
        {
            var csv = DataSource.SerializeToString((object)MockData.EmployeeList);
            EnsureGenericObjectMatchMockDataJsonSingleList(csv);
        }
        [Author("Joseph McNeal Jr", "josephmcnealjr@gmail.com")]
        [Test]
        public void Test_Serialize_Generic_List_To_My_Stream_And_Stream_Wont_Dispose()
        {
            var stream = new MemoryStream();
            DataSource.SerializeToStream(MockData.EmployeeList, stream, 1024, true);
            // TODO :: EnsureStreamMatchMockDataJson(stream);
            EnsureStreamIsNotDisposeAndIsAtEndOfStream(stream);
            stream.Seek(0, SeekOrigin.Begin);
            using (var reader = new StreamReader(stream, DataSource.Configuration.Encoding))
            {
                string value = reader.ReadToEnd();
                EnsureGenericObjectMatchMockDataJsonSingleList(value);
                // Do something with the value
            }
        }

        [Author("Joseph McNeal Jr", "josephmcnealjr@gmail.com")]
        [Test]
        public void Test_Serialize_Generic_To_My_Stream_And_Stream_Wont_Dispose()
        {
            var stream = new MemoryStream();
            DataSource.SerializeToStream(MockData.Employee, stream, 1024, true);
            // TODO :: EnsureStreamMatchMockDataJson(stream);
            EnsureStreamIsNotDisposeAndIsAtEndOfStream(stream);
            stream.Seek(0, SeekOrigin.Begin);
        }

        [Author("Joseph McNeal Jr", "josephmcnealjr@gmail.com")]
        [Test]
        public void Test_Serialize_Generic_To_My_Stream_And_Stream_Is_Dispose()
        {
            var stream = new MemoryStream();
            DataSource.SerializeToStream(MockData.Employee, stream, 1024, false);
            // TODO :: EnsureStreamMatchMockDataJson(stream);
            EnsureStreamIsDispose(stream);
        }

        [Author("Joseph McNeal Jr", "josephmcnealjr@gmail.com")]
        [Test]
        public void Test_Serialize_Object_To_My_Stream_And_Stream_Wont_Dispose()
        {
            var stream = new MemoryStream();
            DataSource.SerializeToStream(MockData.Employee, typeof(Employee), stream, 1024, true);
            // TODO :: EnsureStreamMatchMockDataJson(stream);
            EnsureStreamIsNotDisposeAndIsAtEndOfStream(stream);
            stream.Seek(0, SeekOrigin.Begin);
        }
        [Author("Joseph McNeal Jr", "josephmcnealjr@gmail.com")]
        [Test]
        public void Test_Serialize_Object_To_My_Stream_And_Stream_Is_Dispose()
        {
            var stream = new MemoryStream();
            DataSource.SerializeToStream(MockData.Employee, typeof(Employee), stream, 1024, false);
            // TODO :: EnsureStreamMatchMockDataJson(stream);
            EnsureStreamIsDispose(stream);
        }




        [Author("Joseph McNeal Jr", "josephmcnealjr@gmail.com")]
        [Test]
        public void Test_Serialize_Generic_To_Stream_And_Stream_Wont_Dispose()
        {

            var stream = Stream.Synchronized(DataSource.SerializeToStream(MockData.Employee, 1024));
            // TODO :: EnsureStreamMatchMockDataJson(stream);
            EnsureStreamIsNotDisposeAndIsAtEndOfStream(stream);
            stream.Seek(0, SeekOrigin.Begin);
        }
        [Author("Joseph McNeal Jr", "josephmcnealjr@gmail.com")]
        [Test]
        public void Test_Serialize_Generic_To_Stream_And_Stream_Is_Dispose()
        {

            var stream = DataSource.SerializeToStream(MockData.Employee, 1024);
            // TODO :: EnsureStreamMatchMockDataJson(stream);
            EnsureStreamIsNotDisposeAndIsAtEndOfStream(stream);
        }



        [Author("Joseph McNeal Jr", "josephmcnealjr@gmail.com")]
        [Test]
        public void Test_Serialize_Object_To_Stream_And_Stream_Wont_Dispose()
        {

            var stream = Stream.Synchronized(DataSource.SerializeToStream(MockData.Employee, MockData.Employee.GetType(), 1024));
            // TODO :: EnsureStreamMatchMockDataJson(stream);
            EnsureStreamIsNotDisposeAndIsAtEndOfStream(stream);
            stream.Seek(0, SeekOrigin.Begin);
        }
        [Author("Joseph McNeal Jr", "josephmcnealjr@gmail.com")]
        [Test]
        public void Test_Serialize_Object_To_Stream_And_Stream_Is_Dispose()
        {

            var stream = DataSource.SerializeToStream(MockData.Employee, MockData.Employee.GetType(), 1024);
            // TODO :: EnsureStreamMatchMockDataJson(stream);
            EnsureStreamIsNotDisposeAndIsAtEndOfStream(stream);

        }


    }
}