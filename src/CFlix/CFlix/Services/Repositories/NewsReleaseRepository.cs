using CFlix.Data;
using CFlix.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CFlix.Services.Repositories
{
    public class NewsReleaseRepository : INewsReleaseRepository
    {
        private readonly CFlixAuthContext _context;

        public NewsReleaseRepository(CFlixAuthContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<NewsRelease>> GetNewsReleasesAsync()
        {
            return await _context.NewsReleases.ToListAsync();
        }
    }
}
