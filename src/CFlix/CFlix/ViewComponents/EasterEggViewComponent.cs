using CFlix.Models;
using CFlix.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CFlix.ViewComponents
{
    public class EasterEggViewComponent : ViewComponent
    {
        public EasterEggViewComponent()
        {
        }

        public IViewComponentResult Invoke(EasterEgg easterEgg, CFlixUserEasterEgg userEasterEgg)
        {
            if (userEasterEgg == null)
            {
                return View(easterEgg);
            }

            return View("Found", new EasterEggViewModel { EasterEgg = easterEgg, UserEasterEgg = userEasterEgg });
        }
    }
}
