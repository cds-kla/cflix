using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CFlix.Models.ViewModels
{
    public class CFlixLoginViewModel
    {
        [Required]
        [Display(Name = "Nom d'utilisateur", Prompt = "prenom.nom")]
        public string Login { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Mot de passe", Prompt = "Mot de passe")]
        public string Password { get; set; }
    }
}
