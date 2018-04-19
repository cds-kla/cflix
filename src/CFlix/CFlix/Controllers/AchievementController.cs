using CFlix.Attributes;
using CFlix.Models;
using CFlix.Services.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CFlix.Controllers
{
    [Authorize]
    [NeedToReadRules]
    public class AchievementController : Controller
    {
        private readonly IAchievementRepository _repo;
        private readonly UserManager<CFlixUser> _userManager;

        public AchievementController(IAchievementRepository repository,
            UserManager<CFlixUser> userManager)
        {
            _userManager = userManager;
            _repo = repository;
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Check(string easteregg)
        {
            await _repo.CheckEasterEgg((await _userManager.GetUserAsync(User)).Id, easteregg);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Index()
        {
            var easterEggs = await _repo.GetEasterEggs((await _userManager.GetUserAsync(User)).Id);
            return View(easterEggs);
        }
        
        public async Task<IActionResult> Rate(int easterEggId, short value)
        {
            //User.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.NameIdentifier).Value
            await _repo.RateEasterEggAsync((await _userManager.GetUserAsync(User)).Id, easterEggId, value);
            return RedirectToAction(nameof(Index));
        }
    }
}
