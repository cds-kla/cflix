using CFlix.Attributes;
using CFlix.Models.ViewModels;
using CFlix.Services.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CFlix.Controllers
{
    [ChallengeStageFilter(2)]
    [Authorize(Policy = "AlphaUser")]
    public class ViewerController : Controller
    {
        private readonly IViewerRepository _repo;

        public ViewerController(IViewerRepository repo)
        {
            _repo = repo;
        }

        //[Route("[controller]/{mediaId}")]
        public async Task<IActionResult> Index(string mediaId, string imageName)
        {
            var medias = await _repo.ListAsync(mediaId);

            if (medias.Count == 0)
            {
                return View("Empty");
            }

            if (string.IsNullOrWhiteSpace(imageName))
            {
                imageName = medias.FirstOrDefault();
            }

            if (medias.Contains(imageName))
            {
                medias.Remove(imageName);
            }
            
            return View(new ViewerViewModel
            {
                MediaId = mediaId,
                CurrentImage = imageName,
                Images = medias
            });
        }

        //[Route("[controller]/[action]/{mediaId}")]
        public async Task<IActionResult> View(string mediaId, string imageName)
        {
            var stream = await _repo.GetAsync(mediaId, imageName);

            if (stream == null)
            {
                return NotFound();
            }

            return File(stream, "application/octet-stream");
        }
    }
}
