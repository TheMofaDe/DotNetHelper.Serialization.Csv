using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using CsvHelper;
using CsvHelper.Configuration;
using DotNetHelper.Serialization.Json.Extension;
using ISerializer = DotNetHelper.Serialization.Abstractions.Interface.ISerializer;


namespace DotNetHelper.Serialization.Csv
{
    public class DataSourceCsv : ISerializer
    {

        /// <summary>
        /// Gets the CSV configuration.
        /// </summary>
        /// <value>The CSV configuration.</value>
        public Configuration Configuration { get; } = new Configuration() { UseNewObjectForNullReferenceMembers = false };


        /// <summary>
        /// Initializes a new instance of the <see cref="DataSourceCsv" /> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public DataSourceCsv(Configuration configuration = null)
        {
            Configuration = configuration ?? Configuration;
        }
 
        public dynamic Deserialize(string csv)
        {
            csv.IsNullThrow(nameof(csv));
            using (var csvReader = new CsvReader(new StringReader(csv), Configuration, false))
            {
                csvReader.Read();
                return csvReader.GetRecord<dynamic>();
            }
        }

        public dynamic Deserialize(Stream stream, int bufferSize = 1024, bool leaveStreamOpen = false)  
        {
            stream.IsNullThrow(nameof(stream));
            using (var sr = new StreamReader(stream, Configuration.Encoding,false,bufferSize,leaveStreamOpen))
            using (var csvReader = new CsvReader(sr, Configuration, false))
            {
                csvReader.Read();
                return csvReader.GetRecord<dynamic>();
            }
        }

        public T Deserialize<T>(string csv) where T : class
        {
            csv.IsNullThrow(nameof(csv));
            using (var sr = new StreamReader(new MemoryStream(Configuration.Encoding.GetBytes(csv)), Configuration.Encoding, false,1024,false))
            // using (var csvReader = new CsvReader(new StringReader(csv), Configuration, false))
            using (var csvReader = new CsvReader(sr, Configuration, false))
            {
                csvReader.Read();
                return csvReader.GetRecord<T>();
            }
        }

        public T Deserialize<T>(Stream stream, int bufferSize = 1024, bool leaveStreamOpen = false) where T : class
        {
            stream.IsNullThrow(nameof(stream));
            using (var sr = new StreamReader(stream, Configuration.Encoding, false, bufferSize, leaveStreamOpen))
            using (var csvReader = new CsvReader(sr, Configuration, false))
            {
                csvReader.Read();
                return csvReader.GetRecord<T>();
            }
        }

        public object Deserialize(string csv, Type type)
        {
            csv.IsNullThrow(nameof(csv));
            using (var csvReader = new CsvReader(new StringReader(csv), Configuration, false))
            {
                csvReader.Read();
                return csvReader.GetRecord(type);
            }
        }

        public object Deserialize(Stream stream, Type type, int bufferSize = 1024, bool leaveStreamOpen = false)
        {
            stream.IsNullThrow(nameof(stream));
            using (var sr = new StreamReader(stream, Configuration.Encoding, false, bufferSize, leaveStreamOpen))
            using (var csvReader = new CsvReader(sr, Configuration, false))
            {
                csvReader.Read();
                return csvReader.GetRecord(type);
            }
        }

        public List<dynamic> DeserializeToList(string csv)
        {
            csv.IsNullThrow(nameof(csv));
            using (var csvReader = new CsvReader(new StringReader(csv), Configuration, false))
            {
                return csvReader.GetRecords<dynamic>().ToList();
            }
        }

        public List<dynamic> DeserializeToList(Stream stream, int bufferSize = 1024, bool leaveStreamOpen = false)
        {
            stream.IsNullThrow(nameof(stream));
            using (var sr = new StreamReader(stream, Configuration.Encoding, false, bufferSize, leaveStreamOpen))
            using (var csvReader = new CsvReader(sr, Configuration, false))
            {
                return csvReader.GetRecords<dynamic>().ToList();
            }
        }

        public List<T> DeserializeToList<T>(string csv) where T : class
        {
            csv.IsNullThrow(nameof(csv));
            using (var csvReader = new CsvReader(new StringReader(csv), Configuration, false))
            {
                return csvReader.GetRecords<T>().ToList();
            }
        }

        public List<T> DeserializeToList<T>(Stream stream, int bufferSize = 1024, bool leaveStreamOpen = false) where T : class
        {
            stream.IsNullThrow(nameof(stream));
            using (var sr = new StreamReader(stream, Configuration.Encoding, false, bufferSize, leaveStreamOpen))
            using (var csvReader = new CsvReader(sr, Configuration, false))
            {
                return csvReader.GetRecords<T>().ToList();
            }
        }

        public List<object> DeserializeToList(string csv, Type type)
        {
            csv.IsNullThrow(nameof(csv));
            using (var csvReader = new CsvReader(new StringReader(csv), Configuration, false))
            {
                return csvReader.GetRecords(type).ToList();
            }
        }

        public List<object> DeserializeToList(Stream stream, Type type, int bufferSize = 1024, bool leaveStreamOpen = false)
        {
            stream.IsNullThrow(nameof(stream));
            using (var sr = new StreamReader(stream, Configuration.Encoding, false, bufferSize, leaveStreamOpen))
            using (var csvReader = new CsvReader(sr, Configuration, false))
            {
                return csvReader.GetRecords(type).ToList();
            }
        }



        public void SerializeToStream<T>(T obj, Stream stream, int bufferSize = 1024, bool leaveStreamOpen = false) where T : class
        {
            obj.IsNullThrow(nameof(obj));
            stream.IsNullThrow(nameof(stream));
            using (var sw = new StreamWriter(stream, Configuration.Encoding, bufferSize, leaveStreamOpen))
            {
                using (var csv = new CsvWriter(sw, Configuration))
                {
                    WriteRecord(csv, obj.GetType(), obj);
                }
            }
        }

        public void SerializeToStream(object obj, Type type, Stream stream, int bufferSize = 1024, bool leaveStreamOpen = false)
        {
            using (var sw = new StreamWriter(stream, Configuration.Encoding, bufferSize, leaveStreamOpen))
            {
                using (var csv = new CsvWriter(sw, Configuration))
                {
                    WriteRecord(csv, obj.GetType(), obj);
                }
            }
        }

        public Stream SerializeToStream<T>(T obj, int bufferSize = 1024, bool leaveStreamOpen = false) where T : class
        {
            var memoryStream = new MemoryStream();
            using (var sw = new StreamWriter(memoryStream, Configuration.Encoding, bufferSize, leaveStreamOpen))
            {
                using (var csv = new CsvWriter(sw, Configuration))
                {
                    WriteRecord(csv, obj.GetType(), obj);
                }
            }
            return memoryStream;
        }

        public Stream SerializeToStream(object obj, Type type, int bufferSize = 1024, bool leaveStreamOpen = false)
        {
            var memoryStream = new MemoryStream();
            using (var sw = new StreamWriter(memoryStream, Configuration.Encoding, bufferSize, leaveStreamOpen))
            {
                using (var csv = new CsvWriter(sw, Configuration))
                {
                    WriteRecord(csv, type, obj);
                }
            }
            return memoryStream;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public string SerializeToString(object obj)
        {
            obj.IsNullThrow(nameof(obj));
            var sb = new StringBuilder();
            using (var csv = new CsvWriter(new StringWriter(sb), Configuration))
            {
                WriteRecord(csv, obj.GetType(), obj);
                return sb.ToString();
            }      
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public string SerializeToString<T>(T obj) where T : class
        {
            obj.IsNullThrow(nameof(obj));
            var sb = new StringBuilder();
            using (var csv = new CsvWriter(new StringWriter(sb), Configuration))
            {
                WriteRecord(csv, obj.GetType(), obj);
                return sb.ToString();
            }
        }


        /// <summary>
        /// Checks if developer wants to include the type headers
        /// </summary>
        /// <param name="csv">The CSV.</param>
        /// <param name="type">The type.</param>
        /// <param name="obj"></param>
        private void WriteRecord(CsvWriter csv, Type type, object obj)
        {
            if (Configuration.HasHeaderRecord)
            {
                csv.WriteHeader(type);
                csv.NextRecord();
            }
            if (obj is IEnumerable)
            {
                csv.WriteRecords(obj as IEnumerable<dynamic>);
            }
            else
            {
                csv.WriteRecord(obj);
            }
        }


    


        public TypeCode GetTypeCode()
        {
            throw new NotImplementedException();
        }
        public bool ToBoolean(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public byte ToByte(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public char ToChar(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public DateTime ToDateTime(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public decimal ToDecimal(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public double ToDouble(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public short ToInt16(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public int ToInt32(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public long ToInt64(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public sbyte ToSByte(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public float ToSingle(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public string ToString(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public object ToType(Type conversionType, IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public ushort ToUInt16(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public uint ToUInt32(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public ulong ToUInt64(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }
    }
}
