using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CFlix.ImageViewer.Controllers
{
    public class ViewerController : Controller
    {
        //private readonly IFileProvider _fileProvider;

        //public ViewerController(IFileProvider fileProvider)
        //{
        //    _fileProvider = fileProvider;
        //}

        [Route("[controller]")]
        public IActionResult Index(string mediaId, string image)
        {
            //var fileInfo = _fileProvider.GetFileInfo(Path.Combine("images", image));
            //if (!fileInfo.Exists)
            //{
            //    return NotFound();
            //}

            //return PhysicalFile(fileInfo.PhysicalPath, "application/octet-stream");
            if (string.IsNullOrWhiteSpace(mediaId) || string.IsNullOrWhiteSpace(image))
            {
                return NotFound();
            }

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", mediaId, image);
            if (!System.IO.File.Exists(path))
            {
                return NotFound();
            }

            var bytes = System.IO.File.ReadAllBytes(path);

            return File(bytes, "application/octet-stream");
        }

        [Route("[controller]/[action]")]
        public IActionResult List(string mediaId)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", mediaId ?? string.Empty);

            if (Directory.Exists(path))
            {
                return Json(Directory.EnumerateFiles(path).Select(p => Path.GetFileName(p)));
            }

            return Json(new string[0]);
        }
    }
}
