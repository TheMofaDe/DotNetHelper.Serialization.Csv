using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper.Configuration;
using DotNetHelper.Serialization.Csv.Tests.Models;
using NUnit.Framework;

namespace DotNetHelper.Serialization.Csv.Tests
{
    public class BaseSerialize
    {

        public DataSourceCsv DataSource { get; set; } = new DataSourceCsv();


        public void EnsureGenericObjectMatchMockDataJson(string csv)
        {
            var equals = string.Equals(csv, MockData.EmployeeAsCsvWithHeader, StringComparison.OrdinalIgnoreCase);
            Assert.IsTrue(equals, $"Test failed due to json not matching mock data json");
        }
        public void EnsureGenericObjectMatchMockDataJsonSingleList(string csv)
        {
            var equals = string.Equals(csv, MockData.EmployeeAsCsvWithHeaderList, StringComparison.OrdinalIgnoreCase);
            Assert.IsTrue(equals, $"Test failed due to json not matching mock data json");
        }

        public void EnsureStreamIsNotDisposeAndIsAtEndOfStream(Stream stream)
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


        public void EnsureStreamIsDispose(Stream stream)
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



        public bool CompareStreams(Stream a, Stream b)
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
