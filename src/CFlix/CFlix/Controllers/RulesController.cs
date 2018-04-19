using CFlix.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CFlix.Controllers
{
    //[Authorize]
    public class RulesController : Controller
    {
        private readonly UserManager<CFlixUser> _userManager;

        public RulesController(UserManager<CFlixUser> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AcceptRules(bool accept)
        {
            if (accept)
            {
                var user = await _userManager.GetUserAsync(User);
                user.HaveReadRules = true;
                await _userManager.UpdateAsync(user);
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }

            ModelState.AddModelError(nameof(accept), "Vous devez cocher la case avant de pouvoir valider.");

            return RedirectToAction(nameof(Index));
        }
    }
}
