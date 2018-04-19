using CFlix.Attributes;
using CFlix.Data;
using CFlix.Models;
using CFlix.Services.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;

namespace CFlix.Controllers
{
    [Authorize]
    [Route("/[action]")]
    public class HomeController : Controller
    {

        private readonly IMediaRepository _repo;
        private readonly CFlixROContext _context;

        public HomeController(IMediaRepository repository, CFlixROContext context)
        {
            _repo = repository;
            _context = context;
        }
        
        [Route("/")]
        [NeedToReadRules]
        public async Task<IActionResult> Index()
        {
            return View(await _repo.GetAllMediasAsync());
        }

        [Route("", Name = "Get_Search")]
        [NeedToReadRules]
        public async Task<IActionResult> Search(string query)
        {
            try
            {
                var repo = new MediaRepository(_context);
                var results = await repo.SearchMediaAsync(query);
                ViewBag.Query = query;
                return View("Index", results);
            }
            catch (Exception ex) when (ex is InvalidCastException || ex is DbException)
            {
                ViewBag.DBError = ex.Message;
                return View("Index", new List<Media>());
            }
        }

        [Route("{statusCode?}")]
        public IActionResult Error(int statusCode)
        {
            statusCode = statusCode == 0 ? HttpContext.Response.StatusCode : statusCode;

            return View(statusCode);
        }
    }
}
