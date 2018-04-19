using CFlix.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CFlix.Services.Repositories
{
    public interface IAchievementRepository
    {
        Task<Dictionary<EasterEgg, CFlixUserEasterEgg>> GetEasterEggs(string userId);

        Task<CFlixUserEasterEgg> CheckEasterEgg(string userId, string easterEgg);

        Task RateEasterEggAsync(string userId, int easterEggId, short rate);
    }
}
