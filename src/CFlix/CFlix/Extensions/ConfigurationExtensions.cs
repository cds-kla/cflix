using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.FileProviders.Embedded;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Primitives;

namespace CFlix.Extensions
{
    public static class ConfigurationExtensions
    {
        public static IConfigurationBuilder AddFromZipRessource(this IConfigurationBuilder builder, string ressourceName, string filename)
        {
            builder.AddJsonFile(new EmbeddedZipFileProvider(Assembly.GetEntryAssembly(), ressourceName), filename, true, false);

            return builder;
        }

        private class EmbeddedZipFileProvider : IFileProvider
        {
            private readonly string _ressourceName;
            private readonly Assembly _assembly;
            private EmbeddedFileProvider _embeddedFileProvider;

            public EmbeddedZipFileProvider(Assembly assembly, string ressourceName)
            {
                _ressourceName = ressourceName;
                _assembly = assembly;
                _embeddedFileProvider = new EmbeddedFileProvider(assembly);
            }

            public IDirectoryContents GetDirectoryContents(string subpath)
            {
                return _embeddedFileProvider.GetDirectoryContents(_ressourceName);
            }

            public IFileInfo GetFileInfo(string subpath)
            {
                var fileInfo = _embeddedFileProvider.GetFileInfo(_ressourceName);

                return new EmbeddedZipResourceFileInfo(fileInfo, subpath);
            }

            public IChangeToken Watch(string filter)
            {
                return _embeddedFileProvider.Watch(filter);
            }

            internal class EmbeddedZipResourceFileInfo : IFileInfo
            {
                private readonly IFileInfo _embeddedFile;
                private readonly string _filename;

                public EmbeddedZipResourceFileInfo(IFileInfo embeddedFile, string filename)
                {
                    _embeddedFile = embeddedFile;
                    _filename = filename;
                }

                public bool Exists => _embeddedFile.Exists;

                public long Length => _embeddedFile.Length;

                public string PhysicalPath => _embeddedFile.PhysicalPath;

                public string Name => _embeddedFile.Name;

                public DateTimeOffset LastModified => _embeddedFile.LastModified;

                public bool IsDirectory => _embeddedFile.IsDirectory;

                public Stream CreateReadStream()
                {
                    MemoryStream memStream = new MemoryStream();

                    using (ZipArchive zip = new ZipArchive(_embeddedFile.CreateReadStream(), ZipArchiveMode.Read, false))
                    using (var entry = zip.GetEntry(_filename).Open())
                    {
                        entry.CopyTo(memStream);
                        entry.Flush();
                    }

                    memStream.Seek(0, SeekOrigin.Begin);

                    return memStream;
                }
            }
        }
    }
}
