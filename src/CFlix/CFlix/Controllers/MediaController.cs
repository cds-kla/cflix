using CFlix.Attributes;
using CFlix.Models;
using CFlix.Models.ViewModels;
using CFlix.Services.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CFlix.Controllers
{
    [Authorize]
    [NeedToReadRules]
    public class MediaController : Controller
    {
        private readonly IMediaRepository _repo;
        private readonly UserManager<CFlixUser> _userManager;

        public MediaController(
            UserManager<CFlixUser> userManager,
            IMediaRepository repository)
        {
            _userManager = userManager;
            _repo = repository;
        }

        [Route("[controller]/{mediaId}")]
        public async Task<IActionResult> Detail(int mediaId)
        {
            if ((await _repo.GetMediaWithDetailAsync(mediaId)) is Media m && m.IsAvailable)
            {
                return View(m);
            }

            return RedirectToAction("Home", "Index");
        }

        [Route("[controller]/{mediaId}/[action]", Name = "Post_Review")]
        [HttpPost]
        public async Task<IActionResult> Review(int mediaId, ReviewViewModel data)
        {
            return Forbid();

            var user = await _userManager.GetUserAsync(HttpContext.User);
            //HttpContext.User.Claims.FirstOrDefault(c => c.Type == "DisplayName").Value
            try
            {
                await _repo.AddReviewAsync(mediaId, user.UserName, data.Content);
            }
            catch (ArgumentException)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Detail), mediaId);
        }

    }
}
