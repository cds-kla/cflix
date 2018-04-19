using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CFlix.Models.ViewModels
{
    public class ProfileStatViewModel : ProfileViewModel
    {
        public ProfileStatViewModel()
        {
        }

        public ProfileStatViewModel(CFlixUser user, Dictionary<EasterEgg, CFlixUserEasterEgg> userEasterEgg, bool isEditable)
            : base(user)
        {
            AchievementCount = userEasterEgg.Count;
            UnlockedAchivements = userEasterEgg.Count(pair => pair.Value != null);
            UserEasterEggs = userEasterEgg;
            IsEditable = isEditable;
        }

        public bool IsEditable { get; private set; }
        
        public int UnlockedAchivements { get; private set; }

        public int AchievementCount { get; private set; }

        public Dictionary<EasterEgg, CFlixUserEasterEgg> UserEasterEggs { get; set; }
    }
}
