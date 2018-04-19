using CFlix.Services.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CFlix.ViewComponents
{
    public class NewsReleaseViewComponent : ViewComponent
    {
        private readonly INewsReleaseRepository _repo;

        public NewsReleaseViewComponent(INewsReleaseRepository repo)
        {
            _repo = repo;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var news = await _repo.GetNewsReleasesAsync();
            return View(news);
        }
    }
}
