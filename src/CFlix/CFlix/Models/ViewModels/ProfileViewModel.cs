using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CFlix.Models.ViewModels
{
    public class ProfileViewModel
    {
        public ProfileViewModel()
        {
        }

        public ProfileViewModel(CFlixUser user)
        {
            this.AccountType = user.AccountType;
            this.AvatarType = user.AvatarType;
            this.DisplayName = user.DisplayName;
        }

        [Display(Name = "Type de compte")]
        public AccountType AccountType { get; set; }

        [Required]
        [Display(Name = "Avatar")]
        public AvatarType AvatarType { get; set; }

        [Required]
        [Display(Name = "Nom d'utilisateur")]
        public string DisplayName { get; set; }
    }

    public static class ProfileViewModelExtension
    {
        public static void UpdateWithProfileViewModel(this CFlixUser user, ProfileViewModel model)
        {
            if (model.AccountType != AccountType.User)
            {
                user.AccountType = model.AccountType;
            }

            user.AvatarType = model.AvatarType;
            user.DisplayName = model.DisplayName;
        }
    }
}
