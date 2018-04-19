using CFlix.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CFlix.Services.Repositories
{
    public interface INewsReleaseRepository
    {
        Task<IEnumerable<NewsRelease>> GetNewsReleasesAsync();
    }
}
