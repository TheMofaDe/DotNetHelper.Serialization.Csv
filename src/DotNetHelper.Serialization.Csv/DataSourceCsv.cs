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
using DotNetHelper.Serialization.Csv.Extension;
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
            using (var sr = new StreamReader(stream, Configuration.Encoding, false, bufferSize, leaveStreamOpen))
            using (var csvReader = new CsvReader(sr, Configuration, false))
            {
                csvReader.Read();
                return csvReader.GetRecord<dynamic>();
            }
        }

        public T Deserialize<T>(string csv) where T : class
        {
            csv.IsNullThrow(nameof(csv));
            using (var sr = new StreamReader(new MemoryStream(Configuration.Encoding.GetBytes(csv)), Configuration.Encoding, false, 1024, false))
            // using (var csvReader = new CsvReader(new StringReader(csv), Configuration, false))
            using (var csvReader = new CsvReader(sr, Configuration, false))
            {
                csvReader.Read();
                return GetRecord<T>(csvReader, typeof(T));
            }
        }

        public T Deserialize<T>(Stream stream, int bufferSize = 1024, bool leaveStreamOpen = false) where T : class
        {
            stream.IsNullThrow(nameof(stream));
            using (var sr = new StreamReader(stream, Configuration.Encoding, false, bufferSize, leaveStreamOpen))
            using (var csvReader = new CsvReader(sr, Configuration, false))
            {
                csvReader.Read();
                return GetRecord<T>(csvReader, typeof(T));
            }
        }

        public object Deserialize(string csv, Type type)
        {
            csv.IsNullThrow(nameof(csv));
            using (var csvReader = new CsvReader(new StringReader(csv), Configuration, false))
            {
                if (typeof(IEnumerable).IsAssignableFrom(type))
                {
                    return GetRecords(csvReader, type).ConvertListToTypeList(type);//.AsList();
                }
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
                return csvReader.GetRecords<dynamic>().AsList();
            }
        }

        public List<dynamic> DeserializeToList(Stream stream, int bufferSize = 1024, bool leaveStreamOpen = false)
        {
            stream.IsNullThrow(nameof(stream));
            using (var sr = new StreamReader(stream, Configuration.Encoding, false, bufferSize, leaveStreamOpen))
            using (var csvReader = new CsvReader(sr, Configuration, false))
            {
                return csvReader.GetRecords<dynamic>().AsList();
            }
        }

        public List<T> DeserializeToList<T>(string csv) where T : class
        {
            csv.IsNullThrow(nameof(csv));
            using (var csvReader = new CsvReader(new StringReader(csv), Configuration, false))
            {
                return csvReader.GetRecords<T>().AsList();
            }
        }

        public List<T> DeserializeToList<T>(Stream stream, int bufferSize = 1024, bool leaveStreamOpen = false) where T : class
        {
            stream.IsNullThrow(nameof(stream));
            using (var sr = new StreamReader(stream, Configuration.Encoding, false, bufferSize, leaveStreamOpen))
            using (var csvReader = new CsvReader(sr, Configuration, false))
            {
                return GetRecords<T>(csvReader, typeof(T)).AsList();
            }
        }

        public List<object> DeserializeToList(string csv, Type type)
        {
            csv.IsNullThrow(nameof(csv));
            using (var csvReader = new CsvReader(new StringReader(csv), Configuration, false))
            {
                return GetRecords(csvReader, type).AsList();
            }
        }

        public List<object> DeserializeToList(Stream stream, Type type, int bufferSize = 1024, bool leaveStreamOpen = false)
        {
            stream.IsNullThrow(nameof(stream));
            using (var sr = new StreamReader(stream, Configuration.Encoding, false, bufferSize, leaveStreamOpen))
            using (var csvReader = new CsvReader(sr, Configuration, false))
            {
                return GetRecords(csvReader, type).AsList();
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

        public void SerializeListToStream<T>(IEnumerable<T> objs, Stream stream, int bufferSize = 1024, bool leaveStreamOpen = false) where T : class
        {
            objs.IsNullThrow(nameof(objs));
            stream.IsNullThrow(nameof(stream));
            using (var sw = new StreamWriter(stream, Configuration.Encoding, bufferSize, leaveStreamOpen))
            {
                using (var csv = new CsvWriter(sw, Configuration))
                {
                    WriteRecords(csv, objs);
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



        public Stream SerializeToStream<T>(T obj, int bufferSize = 1024) where T : class
        {
            var memoryStream = new MemoryStream();
            using (var sw = new StreamWriter(memoryStream, Configuration.Encoding, bufferSize, true))
            {
                using (var csv = new CsvWriter(sw, Configuration))
                {
                    WriteRecord(csv, obj.GetType(), obj);
                }
            }
            return memoryStream;
        }

        public Stream SerializeListToStream<T>(IEnumerable<T> objs, int bufferSize = 1024) where T : class
        {
            var memoryStream = new MemoryStream();
            using (var sw = new StreamWriter(memoryStream, Configuration.Encoding, bufferSize, true))
            {
                using (var csv = new CsvWriter(sw, Configuration))
                {
                    WriteRecords(csv, objs);
                }
            }
            return memoryStream;
        }

        public Stream SerializeToStream(object obj, Type type, int bufferSize = 1024)
        {
            var memoryStream = new MemoryStream();
            using (var sw = new StreamWriter(memoryStream, Configuration.Encoding, bufferSize, true))
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
            using (var sw = new StringWriter(sb))
            using (var csv = new CsvWriter(sw, Configuration))
            {
                WriteRecord(csv, obj.GetType(), obj);
            }
            return sb.ToString();
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
            using (var sw = new StringWriter(sb))
            using (var csv = new CsvWriter(sw, Configuration))
            {
                WriteRecord(csv, obj.GetType(), obj);
            }
            return sb.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public string SerializeListToString<T>(IEnumerable<T> obj) where T : class
        {
            obj.IsNullThrow(nameof(obj));
            var sb = new StringBuilder();
            using (var sw = new StringWriter(sb))
            using (var csv = new CsvWriter(sw, Configuration))
            {
                WriteRecords(csv, obj);
            }
            return sb.ToString();
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
                if (obj is IEnumerable)
                {
                    csv.WriteHeader(type.GetEnumerableItemType());
                }
                else
                {
                    csv.WriteHeader(type);
                }
                csv.NextRecord();
            }
            if (obj is IEnumerable)
            {
                csv.WriteRecords(obj as IEnumerable<object>);
            }
            else
            {
                csv.WriteRecord(obj);
            }
        }

        private void WriteRecords<T>(CsvWriter csv, IEnumerable<T> obj)
        {
            if (Configuration.HasHeaderRecord)
            {
                csv.WriteHeader<T>();
                csv.NextRecord();
            }
            csv.WriteRecords(obj);
        }


        private T GetRecord<T>(CsvReader csvReader, Type type)
        {
            var mapping = Configuration.Maps[type];
            if (mapping.MemberMaps.Count <= 0)
            {
                Configuration.AutoMap<T>();
                var record = (T)csvReader.GetRecord(type);
                return record;
            }
            else
            {
                return csvReader.GetRecord<T>();
            }
        }

        private IEnumerable<T> GetRecords<T>(CsvReader csvReader, Type type)
        {
            var mapping = Configuration.Maps[type];
            if (mapping.MemberMaps.Count <= 0)
            {
                if (typeof(IEnumerable).IsAssignableFrom(type))
                {
                    var underlyingType = FindElementType(type);
                    var errorMessage = $"Types that inherit IEnumerable cannot be auto mapped. If your code looks like this {Environment.NewLine} " +
                                       $"var list = new {nameof(DataSourceCsv)}.DeserializeToList(stream, {type.FullName}) then change it to {Environment.NewLine} " +
                                       $"var list = new {nameof(DataSourceCsv)}.DeserializeToList(stream, {underlyingType.FullName}). Otherwise you need register a ClassMap documentation on how to do that can be found here." +
                                       $" https://joshclose.github.io/CsvHelper/getting-started";
                    throw new ConfigurationException(errorMessage);
                }
                Configuration.AutoMap<T>();

                var record = csvReader.GetRecords(type);
                return (IEnumerable<T>)record;
            }
            else
            {
                return csvReader.GetRecords<T>();
            }
        }


        private IEnumerable<object> GetRecords(CsvReader csvReader, Type type)
        {

            if (typeof(IEnumerable).IsAssignableFrom(type))
            {
                var underlyType = type.GetEnumerableItemType();
                var mapping = Configuration.Maps[underlyType];
                if (mapping == null)
                {
                    // Configuration.AutoMap(underlyType);
                }
                var list = csvReader.GetRecords(underlyType);
                return list;
            }
            else
            {
                return csvReader.GetRecords(type);

            }


        }

        /// <summary>Finds the type of the element of a type. Returns null if this type does not enumerate.</summary>
        /// <param name="type">The type to check.</param>
        /// <returns>The element type, if found; otherwise, <see langword="null"/>.</returns>
        private Type FindElementType(Type type)
        {
            if (type.IsArray)
                return type.GetElementType();

            // type is IEnumerable<T>;
            if (ImplIEnumT(type))
                return type.GetGenericArguments().First();

            // type implements/extends IEnumerable<T>;
            var enumType = type.GetInterfaces().Where(ImplIEnumT).Select(t => t.GetGenericArguments().First()).FirstOrDefault();
            if (enumType != null)
                return enumType;

            // type is IEnumerable
            if (IsIEnum(type) || type.GetInterfaces().Any(IsIEnum))
                return typeof(object);

            return null;

            bool IsIEnum(Type t) => t == typeof(System.Collections.IEnumerable);
            bool ImplIEnumT(Type t) => t.IsGenericType && t.GetGenericTypeDefinition() == typeof(IEnumerable<>);
        }


    }
}
