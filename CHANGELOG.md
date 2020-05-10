# Changelog
All notable changes to this project will be documented in this file.
The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),

## [TBD] - 2020-05-10
### Added

New async apis
~~~csharp
        Task SerializeToStreamAsync<T>(T obj, Stream stream, int bufferSize = 1024, bool leaveStreamOpen = false) where T : class;
        Task SerializeListToStreamAsync<T>(IEnumerable<T> objs, Stream stream, int bufferSize = 1024, bool leaveStreamOpen = false) where T : class;
        Task SerializeToStreamAsync(object obj, Type type, Stream stream, int bufferSize = 1024, bool leaveStreamOpen = false);
        Task<Stream> SerializeToStreamAsync<T>(T obj, int bufferSize = 1024) where T : class;
        Task<Stream> SerializeListToStreamAsync<T>(IEnumerable<T> objs, int bufferSize = 1024) where T : class;
        Task<Stream> SerializeToStreamAsync(object obj, Type type, int bufferSize = 1024);
~~~

   * Added new interface **ICsvSerializer**

### Removed
   * Remove dependency to **DotNetHelper.Serialization.Abstractions**    

<br/>
<br/>

## [1.0.19] - 2019-08-04
### Changed
- update dependencies package **DotNetHelper.Serialization.Abstractions**

[1.0.19]: https://github.com/TheMofaDe/DotNetHelper.Serialization.Csv/releases/tag/v1.0.19

