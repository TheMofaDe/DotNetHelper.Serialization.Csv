using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace DotNetHelper.Serialization.Csv
{
    public interface ICsvSerializer
    {
        dynamic Deserialize(string csv);
        dynamic Deserialize(Stream stream, int bufferSize = 1024, bool leaveStreamOpen = false);
        T Deserialize<T>(string csv) where T : class;
        T Deserialize<T>(Stream stream, int bufferSize = 1024, bool leaveStreamOpen = false) where T : class;
        object Deserialize(string csv, Type type);
        object Deserialize(Stream stream, Type type, int bufferSize = 1024, bool leaveStreamOpen = false);
        List<object> DeserializeToList(string csv);
        List<object> DeserializeToList(Stream stream, int bufferSize = 1024, bool leaveStreamOpen = false);
        List<T> DeserializeToList<T>(string csv) where T : class;
        List<T> DeserializeToList<T>(Stream stream, int bufferSize = 1024, bool leaveStreamOpen = false) where T : class;
        List<object> DeserializeToList(string csv, Type type);
        List<object> DeserializeToList(Stream stream, Type type, int bufferSize = 1024, bool leaveStreamOpen = false);
        Task SerializeToStreamAsync<T>(T obj, Stream stream, int bufferSize = 1024, bool leaveStreamOpen = false) where T : class;
        void SerializeToStream<T>(T obj, Stream stream, int bufferSize = 1024, bool leaveStreamOpen = false) where T : class;
        Task SerializeListToStreamAsync<T>(IEnumerable<T> objs, Stream stream, int bufferSize = 1024, bool leaveStreamOpen = false) where T : class;
        void SerializeListToStream<T>(IEnumerable<T> objs, Stream stream, int bufferSize = 1024, bool leaveStreamOpen = false) where T : class;
        Task SerializeToStreamAsync(object obj, Type type, Stream stream, int bufferSize = 1024, bool leaveStreamOpen = false);
        void SerializeToStream(object obj, Type type, Stream stream, int bufferSize = 1024, bool leaveStreamOpen = false);
        Task<Stream> SerializeToStreamAsync<T>(T obj, int bufferSize = 1024) where T : class;
        Stream SerializeToStream<T>(T obj, int bufferSize = 1024) where T : class;
        Task<Stream> SerializeListToStreamAsync<T>(IEnumerable<T> objs, int bufferSize = 1024) where T : class;
        Stream SerializeListToStream<T>(IEnumerable<T> objs, int bufferSize = 1024) where T : class;
        Task<Stream> SerializeToStreamAsync(object obj, Type type, int bufferSize = 1024);
        Stream SerializeToStream(object obj, Type type, int bufferSize = 1024);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        string SerializeToString(object obj);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        Task<string> SerializeToStringAsync(object obj);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        Task<string> SerializeListToStringAsync<T>(IEnumerable<T> obj) where T : class;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        string SerializeListToString<T>(IEnumerable<T> obj) where T : class;
    }
}