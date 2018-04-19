using CFlix.Models.ViewModels;
using CFlix.Services.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CFlix.ViewComponents
{
    public class ReviewViewComponent : ViewComponent
    {
        private IMediaRepository _repo;
            
        public ReviewViewComponent(IMediaRepository repo)
        {
            _repo = repo;
        }

        public async Task<IViewComponentResult> InvokeAsync(int mediaId)
        {
            ViewBag.Reviews = await _repo.GetMediaReviewsAsync(mediaId);
            return View(new ReviewViewModel());
        }
    }
}
