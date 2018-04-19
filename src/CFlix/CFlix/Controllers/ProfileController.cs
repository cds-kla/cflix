using CFlix.Attributes;
using CFlix.Models;
using CFlix.Models.ViewModels;
using CFlix.Services.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CFlix.Controllers
{
    [ChallengeStageFilter(2)]
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly UserManager<CFlixUser> _userManager;
        private readonly IAchievementRepository _achievementRepository;

        public ProfileController(UserManager<CFlixUser> userManager,
            IAchievementRepository achievementRepository)
        {
            _userManager = userManager;
            _achievementRepository = achievementRepository;
        }

        public IActionResult Index()
        {
            return RedirectToAction(nameof(Index), new { profileName = "me" });
        }

        [Route("[controller]/{profileName}")]
        public async Task<IActionResult> Index(string profileName)
        {
            if (profileName == "me")
            {
                profileName = User.Identity.Name;
            }

            var user = await _userManager.FindByNameAsync(profileName);

            if (user == null)
            {
                return NotFound();
            }

            var ee = await _achievementRepository.GetEasterEggs(user.Id);
            return View(new ProfileStatViewModel(user, ee, user.UserName == User.Identity.Name));
        }

        [Route("[controller]/me/[action]")]
        public async Task<IActionResult> Edit()
        {
            var user = await _userManager.GetUserAsync(User);
            return View(new ProfileViewModel(user));
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        [Route("[controller]/me/[action]")]
        public async Task<IActionResult> Edit(ProfileViewModel profile)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var user = await _userManager.GetUserAsync(User);
            user.UpdateWithProfileViewModel(profile);
            await _userManager.UpdateAsync(user);
            //// traitement de AccountType
            //var claims = await _userManager.GetClaimsAsync(user);
            //var roleClaim = claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);

            //if (Enum.TryParse(roleClaim.Value, out AccountType accountType) && profile.AccountType != accountType)
            //{
            //    await _userManager.ReplaceClaimAsync(user, roleClaim, new Claim(ClaimTypes.Role, profile.AccountType.ToString()));
            //}

            return RedirectToAction(nameof(Index), new { profileName = "me" });
        }
    }
}
