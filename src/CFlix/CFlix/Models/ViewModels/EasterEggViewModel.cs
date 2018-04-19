using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CFlix.Models.ViewModels
{
    public class EasterEggViewModel
    {
        public EasterEgg EasterEgg { get; set; }

        public CFlixUserEasterEgg UserEasterEgg { get; set; }

        public bool IsFound
        {
            get => UserEasterEgg != null;
        }
    }
}
