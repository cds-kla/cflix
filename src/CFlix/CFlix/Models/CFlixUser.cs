using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CFlix.Models
{
    public class CFlixUser : IdentityUser
    {
        public string DisplayName { get; set; }

        public string EmployeeID { get; set; }

        public AvatarType AvatarType { get; set; }

        public AccountType AccountType { get; set; }

        public bool HaveReadRules { get; set; }

        public List<CFlixUserEasterEgg> EasterEggs { get; set; }
    }

    public enum AvatarType : short
    {
        [Display(Name = "Avatar #0")]
        Avatar0 = 0,
        [Display(Name = "Avatar #1")]
        Avatar1 = 1,
        [Display(Name = "Avatar #2")]
        Avatar2 = 2,
        [Display(Name = "Avatar #3")]
        Avatar3 = 3,
        [Display(Name = "Avatar #4")]
        Avatar4 = 4,
        [Display(Name = "Avatar #5")]
        Avatar5 = 5,
        [Display(Name = "Avatar #6")]
        Avatar6 = 6,
        [Display(Name = "Avatar #7")]
        Avatar7 = 7,
        [Display(Name = "Avatar #8")]
        Avatar8 = 8,
    }

    public enum AccountType
    {
        User,
        Beta,
        Alpha
    }
}
