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
    public class CsvSerializerTextFixture 
    {
        

        public DataSourceCsv DataSource { get; set; } = new DataSourceCsv();

        public CsvSerializerTextFixture()
        {

        }


        [OneTimeSetUp]
        public void RunBeforeAnyTests()
        {
            var configuration = new Configuration { Encoding = Encoding.UTF8 };
            DataSource = new DataSourceCsv(configuration);
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
        [Author("Joseph McNeal Jr", "josephmcnealjr@gmail.com")]
        [Test]
        public void Test_Serialize_Generic_To_My_Stream_And_Stream_Wont_Dispose()
        {
            var stream = new MemoryStream();
            DataSource.SerializeToStream(MockData.Employee,stream,1024,true);
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
            DataSource.SerializeToStream(MockData.Employee,typeof(Employee), stream, 1024, true);
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

            var stream = Stream.Synchronized(DataSource.SerializeToStream(MockData.Employee, 1024, true));
            // TODO :: EnsureStreamMatchMockDataJson(stream);
            EnsureStreamIsNotDisposeAndIsAtEndOfStream(stream);
            stream.Seek(0, SeekOrigin.Begin);
        }
        [Author("Joseph McNeal Jr", "josephmcnealjr@gmail.com")]
        [Test]
        public void Test_Serialize_Generic_To_Stream_And_Stream_Is_Dispose()
        {

            var stream = DataSource.SerializeToStream(MockData.Employee, 1024, false);
            // TODO :: EnsureStreamMatchMockDataJson(stream);
            EnsureStreamIsDispose(stream);
        }



        [Author("Joseph McNeal Jr", "josephmcnealjr@gmail.com")]
        [Test]
        public void Test_Serialize_Object_To_Stream_And_Stream_Wont_Dispose()
        {

            var stream = Stream.Synchronized(DataSource.SerializeToStream(MockData.Employee,MockData.Employee.GetType(), 1024, true));
            // TODO :: EnsureStreamMatchMockDataJson(stream);
            EnsureStreamIsNotDisposeAndIsAtEndOfStream(stream);
            stream.Seek(0, SeekOrigin.Begin);
        }
        [Author("Joseph McNeal Jr", "josephmcnealjr@gmail.com")]
        [Test]
        public void Test_Serialize_Object_To_Stream_And_Stream_Is_Dispose()
        {

            var stream = DataSource.SerializeToStream(MockData.Employee, MockData.Employee.GetType(),1024, false);
            // TODO :: EnsureStreamMatchMockDataJson(stream);
            EnsureStreamIsDispose(stream);
        }





        private void EnsureGenericObjectMatchMockDataJson(string csv)
        {
            var equals = string.Equals(csv, MockData.EmployeeAsCsvWithHeader, StringComparison.OrdinalIgnoreCase);
            Assert.IsTrue(equals, $"Test failed due to json not matching mock data json");
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



        private bool CompareStreams(Stream a, Stream b)
        {
            if (a == null &&
                b == null)
                return true;
            if (a == null ||
                b == null)
            {
                throw new ArgumentNullException(
                    a == null ? "a" : "b");
            }

            if (a.Length < b.Length)
                return false;
            if (a.Length > b.Length)
                return false;

            for (int i = 0; i < a.Length; i++)
            {
                int aByte = a.ReadByte();
                int bByte = b.ReadByte();
                if (aByte.CompareTo(bByte) != 0)
                    return false;
            }

            return true;
        }
    }
}