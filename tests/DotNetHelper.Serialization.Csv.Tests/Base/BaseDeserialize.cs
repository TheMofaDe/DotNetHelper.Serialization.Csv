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
    public class BaseDeserialize
    {

        public DataSourceCsv DataSource { get; set; } = new DataSourceCsv();

        public void EnsureFirstNameAndLastNameMatchMockData(string firstName, string lastName)
        {
            if (firstName.Equals(MockData.Employee.FirstName) && lastName.Equals(MockData.Employee.LastName))
            {

            }
            else
            {
                Assert.Fail("Dynamic Object doesn't matches expected results");
            }
        }

        public void EnsureDynamicObjectMatchMockData(dynamic dyn)
        {
            EnsureFirstNameAndLastNameMatchMockData(dyn.FirstName.ToString(), dyn.LastName.ToString());
        }

        public void EnsureGenericObjectMatchMockData(Employee employee)
        {
            EnsureFirstNameAndLastNameMatchMockData(employee.FirstName, employee.LastName);
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

    }
}
