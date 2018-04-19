using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CFlix.Services.Repositories
{
    public interface IViewerRepository
    {
        Task<List<string>> ListAsync(string mediaId);

        Task<Stream> GetAsync(string mediaId, string imageName);
    }
}
