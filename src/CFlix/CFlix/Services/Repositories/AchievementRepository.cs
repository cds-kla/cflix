using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CFlix.Models;
using CFlix.Data;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace CFlix.Services.Repositories
{
    public class AchievementRepository : IAchievementRepository
    {
        private readonly CFlixAuthContext _context;

        public AchievementRepository(CFlixAuthContext context)
        {
            _context = context;
        }

        public async Task<Dictionary<EasterEgg, CFlixUserEasterEgg>> GetEasterEggs(string userId)
        {
            var easterEggs = await _context.EasterEggs.ToListAsync();
            var userEasterEggs = await _context.UserEasterEggs.Where(ueg => ueg.CFlixUserId == userId).ToListAsync();

            return (from ee in easterEggs
                    let ueg = userEasterEggs.FirstOrDefault(u => u.EasterEggId == ee.Id)
                    select new { key = ee, value = ueg }).ToDictionary(x => x.key, x => x.value);
        }

        public async Task<CFlixUserEasterEgg> CheckEasterEgg(string userId, string easterEgg)
        {
            if (easterEgg == null)
            {
                return null;
            }

            string hash = GenerateHash(easterEgg);
            var easter = await _context.EasterEggs.FirstOrDefaultAsync(ee => ee.Hash == hash);

            if (easter == null || !easter.IsAvailable)
            {
                return null;
            }

            var userEasterEgg = await _context.UserEasterEggs.FindAsync(easter.Id, userId);

            if (userEasterEgg == null)
            {
                userEasterEgg = new CFlixUserEasterEgg
                {
                    CFlixUserId = userId,
                    EasterEggId = easter.Id
                };

                await _context.AddAsync(userEasterEgg);
                await _context.SaveChangesAsync();
            }

            return userEasterEgg;
        }

        public async Task RateEasterEggAsync(string userId, int easterEggId, short rate)
        {
            if (rate < 0 || rate > 5)
            {
                return;
            }

            var userEasterEgg = await _context.UserEasterEggs.FindAsync(easterEggId, userId);

            if (userEasterEgg == null)
            {
                return;
            }

            userEasterEgg.Rate = rate;
            await _context.SaveChangesAsync();
        }

        private string GenerateHash(string easterEgg)
        {
            return BitConverter.ToString(SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(easterEgg)))
                .Replace("-", string.Empty)
                .ToLower();
        }
    }
}
