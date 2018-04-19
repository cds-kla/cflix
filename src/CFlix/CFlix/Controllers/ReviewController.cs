using CFlix.Models;
using CFlix.Services.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CFlix.Controllers
{
    [Authorize]
    public class ReviewController : Controller
    {
        private readonly IMediaRepository _repo;
        private readonly UserManager<CFlixUser> _userManager;

        public ReviewController(
            UserManager<CFlixUser> userManager,
            IMediaRepository repository)
        {
            _userManager = userManager;
            _repo = repository;
        }

        [Route("[controller]/[action]/{reviewId}")]
        public async Task<IActionResult> ToggleHide(int reviewId, bool isHidden)
        {
            var review = await _repo.GetReviewAsync(reviewId);
            if (review == null || review.UserName != User.Identity.Name)
            {
                return NotFound();
            }

            review.IsHidden = !review.IsHidden;
            await _repo.EditReviewAsync(review);
            return RedirectToAction(nameof(MediaController.Detail), "Media", new { mediaId = review.MediaId });
        }
    }
}
